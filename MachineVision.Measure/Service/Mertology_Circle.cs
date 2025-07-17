using HalconDotNet;
using MachineVision.Shared.Controls;
using MachineVision.TemplateMatch.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MachineVision.TemplateMatch.Service
{
   public class Mertology_Circle
    {

        HObject ho_Circle, ho_Contours, ho_Contour;
        HTuple hv_WindowHandle = new HTuple(), hv_Row = new HTuple();
        HTuple hv_Column = new HTuple(), hv_Radius = new HTuple();
        HTuple hv_MetrologyHandle = new HTuple(), hv_Index = new HTuple();
        HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
        HTuple hv_Parameter = new HTuple();
        public Mertology_Circle()
        {
            HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
        }

        public MeasureCircleResult Run_Metrology_Circle (HObject Image, DrawObjectInfo drawObjectInfo)
        {

            HOperatorSet.AddMetrologyObjectCircleMeasure(hv_MetrologyHandle, drawObjectInfo.HTuples[0], drawObjectInfo.HTuples[1],drawObjectInfo.HTuples[2], 20, 5, 1, 30, new HTuple(), new HTuple(), out hv_Index);
            HOperatorSet.ApplyMetrologyModel(Image, hv_MetrologyHandle);
            HOperatorSet.GetMetrologyObjectMeasures(out ho_Contours, hv_MetrologyHandle,"all", "all", out hv_Row1, out hv_Column1);
          
            HOperatorSet.GetMetrologyObjectResultContour(out ho_Contour, hv_MetrologyHandle, 0, "all", 1.5);

           
            HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type","all_param", out hv_Parameter);
            MeasureCircleResult result = new MeasureCircleResult();

            if (hv_Parameter==null)
            {
                result.message = "识别失败";
                result.contuns= null;
                return result;
            }

            result.row = hv_Parameter[0];
            result.column = hv_Parameter[1];
            result.radius = hv_Parameter[2];
            result.contuns = ho_Contour;
            result.message = $"圆心：X坐标{hv_Parameter[0]}，y坐标：{hv_Parameter[1]},半径：{hv_Parameter[2]}";


            return result;

        }
    }
}
