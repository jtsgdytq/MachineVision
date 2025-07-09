using MachineVision.TemplateMatch.ViewModels;
using MachineVision.TemplateMatch.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.TemplateMatch
{
    public class TemplateMatchModel : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        { 

        }
            

        public void RegisterTypes(IContainerRegistry service)
        {
           service.RegisterForNavigation<DrawShapeView, DrawShapeViewModel>();
            service.RegisterForNavigation<ShapeView, ShapeViewModel>();
        }
    }
}
