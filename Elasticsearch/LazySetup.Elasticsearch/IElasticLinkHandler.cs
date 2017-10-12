using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nest;

namespace LazySetup.Elasticsearch
{
    public interface IElasticLinkHandler
    {
        Task<ElasticLinkedResponse<T>> LinkedSearchAsync<T>(Func<SearchDescriptor<T>, ISearchRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken), int? maxDepth = null) where T : class;
    }
}
