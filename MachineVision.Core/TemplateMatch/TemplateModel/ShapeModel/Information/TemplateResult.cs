using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Core.TemplateMatch.TemplateModel.ShapeModel.Information
{
    public class TemplateResult
    {
        public TemplateResult()
        {
            Results = new  ObservableCollection<MatchTemplateResult>();
        }
        public bool IsSuccess { get; set; }


        public string Message { get; set; }

        public ObservableCollection<MatchTemplateResult> Results { get; set; }

    }
}
