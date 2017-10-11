using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace LazySetup.Elasticsearch
{
    public class ElasticLink : Attribute
    {
        public string LinkedIndex { get; }
        public string LinkedField { get; }
        public string Value { get; }
        public int Size { get; }
        public IList<ISort> Sort { get; }

        protected ElasticLink(string linkedIndex, string linkedField, string value, int size, IList<ISort> sort)
        {
            LinkedIndex = linkedIndex;
            LinkedField = linkedField;
            Value = value;
            Size = size;
            Sort = sort;
        }
    }
}
