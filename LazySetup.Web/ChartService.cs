using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LazySetup.Charts;

namespace LazySetup.Web
{
    public class ChartService : IChartService
    {
        public async Task<IEnumerable<PieData>> GetPieData(string identifier, DateTime? from, DateTime? to)
        {
            var list = new List<PieData>();
            list.Add(new PieData{Identifier = "Emil", Value = 45});
            list.Add(new PieData { Identifier = "Kenneth", Value = 55 });

            return list;
        }
    }
}
