using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LazySetup.Charts.ViewComponents
{
    [ViewComponent(Name = "LazySetup.Charts.Pie")]
    public class PieChartViewComponent : ViewComponent
    {
        private readonly IChartService _service;

        public PieChartViewComponent(IChartService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(string identifier, DateTime? from = null, DateTime? to = null, string title = null, bool is3D = false, double pieHole = 0)
        {
            var data = await _service.GetPieData(identifier, from, to);
            var model = new PieViewModel(title, is3D, pieHole, data);
            return View(model);
        }
    }

    public class PieSetupModel
    {
        public string Identifier { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Title { get; set; }
        public PieOptions Options { get; set; }

        public class PieOptions
        {
            public bool Is3D { get; set; }
            public double PieHole { get; set; }
            public IEnumerable<string> Colors { get; set; }
        }
    }

    public class PieViewModel
    {
        public PieViewModel(string title, bool is3D, double pieHole, IEnumerable<PieData> data)
        {
            Id = Guid.NewGuid();
            Title = title;
            Is3D = is3D;
            PieHole = pieHole;
            Data = data;
        }
        public IEnumerable<PieData> Data { get; }
        public Guid Id { get; }
        public string Title { get; }
        public bool Is3D { get; }
        public double PieHole { get; }
    }
}
