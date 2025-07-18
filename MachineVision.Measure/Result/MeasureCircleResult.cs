using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.TemplateMatch.Result
{
   public class MeasureCircleResult
    {
        public HTuple row { get; set; } // Row coordinate of the center of the circle
        public HTuple column { get; set; } // Column coordinate of the center of the circle
        public HTuple radius { get; set; } // Radius of the circle

        public HObject Contour { get; set; } // Contour of the circle

        public HObject Contours { get; set; }


        public string message { get; set; } // Message or status related to the measurement result



    }
}
