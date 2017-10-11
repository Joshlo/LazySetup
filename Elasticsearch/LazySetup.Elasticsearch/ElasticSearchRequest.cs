using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Nest;

namespace LazySetup.Elasticsearch
{
    public static class ElasticSearchRequest
    {
        public static SearchRequest Create<T>(ElasticLink elasticLink, T doc)
        {
            Type itemType = doc.GetType();
            PropertyInfo prop = itemType.GetProperty(elasticLink.Value);

            return new SearchRequest(elasticLink.LinkedIndex)
            {
                Query = new MatchQuery
                {
                    Field = elasticLink.LinkedField,
                    Query = prop.GetValue(doc).ToString()
                },
                Size = elasticLink.Size,
                Sort = elasticLink.Sort
            };
        }
    }
}
