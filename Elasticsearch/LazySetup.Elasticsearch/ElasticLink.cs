using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace LazySetup.Elasticsearch
{
    public class ElasticLink : Attribute
    {
        public string LinkedIndex { get; }
        public string LinkedType { get; }
        public string LinkedField { get; }
        public string ValueField { get; }
        public int Size { get; }
        public IList<ISort> Sort { get; }

        protected ElasticLink(string linkedIndex, string linkedType, string linkedField, string valueField, int size, IList<ISort> sort)
        {
            LinkedIndex = linkedIndex;
            LinkedType = linkedType;
            LinkedField = linkedField;
            ValueField = valueField;
            Size = size;
            Sort = sort;
        }
    }
}
