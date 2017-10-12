using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nest;

namespace LazySetup.Elasticsearch
{
    public class ElasticLinkHandler : IElasticLinkHandler
    {
        public int MaxDepth { get; internal set; }
        private readonly IElasticClient _elasticClient;

        public ElasticLinkHandler(IElasticClient elasticClient, int maxDepth)
        {
            MaxDepth = maxDepth;
            _elasticClient = elasticClient;
        }

        public async Task<ElasticLinkedResponse<T>> LinkedSearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null, CancellationToken cancellationToken = new CancellationToken(), int? maxDepth = null) where T : class
        {
            if (maxDepth != null) MaxDepth = maxDepth.Value;

            var timer = new Stopwatch();
            timer.Start();

            var initialCall = await _elasticClient.SearchAsync(selector, cancellationToken);

            var items = initialCall.Documents.ToList();

            await Link(items);
            timer.Stop();
            var response = new ElasticLinkedResponse<T>
            {
                Documents = items,
                QueryTime = timer.Elapsed,
                TotalDocuments = initialCall.Total
            };

            return response;
        }

        private async Task<IEnumerable<T2>> Link<T2>(IEnumerable<T2> docs, int depth = 1, PropertyInfo info = null) where T2 : class
        {
            if (depth > MaxDepth)
                return docs;

            foreach (var doc in docs)
            {
                var docType = doc.GetType();

                foreach (var propertyInfo in docType.GetProperties())
                {
                    ElasticLink elasticLink;
                    if ((elasticLink = propertyInfo.GetCustomAttribute<ElasticLink>()) != null)
                    {
                        var request = ElasticSearchRequest.Create(elasticLink, doc);

                        var method = GetType().GetMethod("LinkSearch", BindingFlags.Instance | BindingFlags.NonPublic);
                        
                        var genericMethod = method.MakeGenericMethod(GetTypeOfProperty(propertyInfo));
                        var res = (Task)genericMethod.Invoke(this, new object[] { propertyInfo, doc, request, depth });
                        await res;
                    }
                }
            }

            return docs;
        }

        private async Task LinkSearch<T2>(PropertyInfo propInfo, object doc, SearchRequest request, int depth) where T2 : class
        {
            var elasticCall = await _elasticClient.SearchAsync<T2>(request);

            var docs = await Link<T2>(elasticCall.Documents, depth++, propInfo);

            if (request.Size > 1)
            {
                propInfo.SetValue(doc, docs);
            }
            else
            {
                propInfo.SetValue(doc, docs.FirstOrDefault());
            }
        }

        private Type GetTypeOfProperty(PropertyInfo info)
        {
            return info.PropertyType.GetGenericArguments().Any() ? info.PropertyType.GetGenericArguments().FirstOrDefault() : info.PropertyType;
        }
    }
}
