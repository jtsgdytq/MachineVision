using MachineVision.Core.TemplateMatch.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MachineVision.Core.TemplateMatch.TemplateModel.NccModel.Information
{
    public class CreateNccTemplateParam:BaseParam
    {
        private string numLevels;
        private double angleStart;
        private double angleExtent;
        private string angleStep;
      
        private string metric;
   

        /// <summary>
        /// 金字塔层数
        /// </summary>
        public string NumLevels
        {
            get { return numLevels; }
            set { numLevels = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模板旋转起始角度
        /// </summary>
        public double AngleStart
        {
            get { return angleStart; }
            set { angleStart = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模板旋转角度范围
        /// </summary>
        public double AngleExtent
        {
            get { return angleExtent; }
            set { angleExtent = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 旋转角度步长
        /// </summary>
        public string AngleStep
        {
            get { return angleStep; }
            set { angleStep = value; RaisePropertyChanged(); }
        }

       

        /// <summary>
        /// 匹配方法
        /// </summary>
        public string Metric
        {
            get { return metric; }
            set { metric = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 对比度
        /// </summary>
     
        /// <summary>
        /// 最小对比度
        /// </summary>
     
        public override void ApplyDefaultParameter()
        {
            NumLevels = "auto";
            AngleStart = 0;
            AngleExtent = 360;
            AngleStep = "auto";
           
            Metric = "use_polarity";
            
        }

        public CreateNccTemplateParam()
        {
            ApplyDefaultParameter();
        }
    }
}
