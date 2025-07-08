using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Shared.Controls
{
    public class DrawObjectInfo
    {
        public enum ShapeType
        {
            Rectangle,
            Circle,
            Ellipse,
            
        }


        public ShapeType Type { get; set; }
        public HObject HObject { get; set; }

        public HTuple[] HTuples { get; set; }

    }
}
