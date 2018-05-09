using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LazySetup.Charts
{
    public interface IChartService
    {
        Task<IEnumerable<PieData>> GetPieData(string identifier, DateTime? from, DateTime? to);
    }
}