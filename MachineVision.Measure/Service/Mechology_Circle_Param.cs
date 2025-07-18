using MachineVision.Core.TemplateMatch.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Measure.Service
{
    public class Mechology_Circle_Param:BaseParam
    {
        
        private int measureLenght1;

        public int Measurelenght1
        {
            get { return measureLenght1; }
            set { measureLenght1 = value;  RaisePropertyChanged(); }
        }

        private int measureLenght2;

        public int MeasureLenght2
        {
            get { return measureLenght2; }
            set { measureLenght2 = value; RaisePropertyChanged(); }
        }

        private double measureSigma;

        public double MeasureSigma
        {
            get { return measureSigma; }
            set { measureSigma = value;  RaisePropertyChanged(); }
        }


        private int measureThroshold;

        public int MeasureThroshold
        {
            get { return measureThroshold; }
            set { measureThroshold = value; RaisePropertyChanged(); }
        }

        public override void ApplyDefaultParameter()
        {
            measureLenght1 = 20;
            measureLenght2 = 5;
            measureSigma = 1.0;
            measureThroshold = 30;
        }

        public Mechology_Circle_Param()
        {
            ApplyDefaultParameter();
        }

    }
}
