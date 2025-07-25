﻿using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Core.TemplateMatch.Share
{
    public class MatchTemplateResult
    {
        public int Index { get; set; }
        public double Row { get; set; }
        public double Column { get; set; }
        public double Angle { get; set; }
        public double Score { get; set; }

        public HObject Contours { get; set; }
    }
}
