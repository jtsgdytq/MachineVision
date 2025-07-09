using MachineVision.Core;
using MachineVision.Core.TemplateMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.TemplateMatch.ViewModels
{
    public class ShapeViewModel:NavigationViewModel
    {
        public ITemplateMatchService MatchService { get; }

        public ShapeViewModel()
        {
            MatchService = ContainerLocator.Container.Resolve<ITemplateMatchService>(nameof(TemplateMatchType.ShapeMatch));
        }
    }
}
