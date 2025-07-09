using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Core.TemplateMatch
{
    public interface ITemplateMatchService
    {
        Task CraeteTemplate(HObject Image,HObject hObject);

        public void Run (HObject Image);
    }
}
