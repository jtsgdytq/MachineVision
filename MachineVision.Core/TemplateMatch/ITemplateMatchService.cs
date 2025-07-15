using HalconDotNet;
using MachineVision.Core.TemplateMatch.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Core.TemplateMatch
{
    public interface ITemplateMatchService
    {
        Task CraeteTemplate(HObject image,HObject hObject);

       public TemplateResult Run (HObject Image);

        public void clearTemplate();

    }
}
