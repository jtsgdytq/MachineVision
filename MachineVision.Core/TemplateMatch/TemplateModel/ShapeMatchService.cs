using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Core.TemplateMatch.TemplateModel
{
    public class ShapeMatchService : ITemplateMatchService
    {
        // Fixed method to ensure all code paths return a Task
        public Task CraeteTemplate(HObject Image, HObject hObject)
        {
           
            return Task.CompletedTask;
        }

        public void Run(HObject Image)
        {
            
        }
    }
}
