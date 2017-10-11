using System;
using System.Collections.Generic;
using System.Text;

namespace LazySetup.Elasticsearch
{
    public class ElasticLinkedResponse<T> where T : class
    {
        public IEnumerable<T> Documents { get; set; }
        public long TotalDocuments { get; set; }
        public TimeSpan QueryTime { get; set; }
    }
}
