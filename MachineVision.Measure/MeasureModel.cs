using MachineVision.Measure.View;
using MachineVision.Measure.ViewModel;
using MachineVision.TemplateMatch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Measure
{
    public class MeasureModel : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry service)
        {
            service.RegisterForNavigation<CircleMeasureView,CircleMeasureViewModel>();
            service.RegisterSingleton<Mertology_Circle>();
        }
    }
}
