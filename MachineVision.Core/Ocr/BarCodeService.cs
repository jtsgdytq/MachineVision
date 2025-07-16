using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Core.Ocr
{
    public class BarCodeService
    {



        public BarCodeService()
        {
            //初始化条形码模型句柄
            HOperatorSet.CreateBarCodeModel(new HTuple(), new HTuple(), out hv_BarCodeHandle);
        }


        HObject ho_SymbolRegions = null;
        HTuple hv_BarCodeHandle = new HTuple(), hv_WindowHandle = new HTuple();
        HTuple hv_I = new HTuple(), hv_DecodedDataStrings = new HTuple();
        HTuple hv_Reference = new HTuple(), hv_String = new HTuple();
        HTuple hv_J = new HTuple(), hv_Char = new HTuple();

        public string Run(HObject Image)
        {
            HOperatorSet.FindBarCode(Image, out ho_SymbolRegions, hv_BarCodeHandle, "auto", out hv_DecodedDataStrings);


            if (hv_DecodedDataStrings.Length > 0)
            {
                // 获取第一个条码的结果（索引从 0 开始）
                HOperatorSet.GetBarCodeResult(hv_BarCodeHandle, 0, "decoded_reference", out hv_Reference);

                hv_String = "";
                HTuple end_val15 = (hv_DecodedDataStrings.TupleStrlen()) - 1;
                HTuple step_val15 = 1;

                for (hv_J = 0; hv_J.Continue(end_val15, step_val15); hv_J = hv_J.TupleAdd(step_val15))
                {
                    if ((int)((((hv_DecodedDataStrings.TupleStrBitSelect(hv_J))).TupleOrd()).TupleLess(32)) != 0)
                    {
                        hv_Char = "\\x" + (((hv_DecodedDataStrings.TupleStrBitSelect(hv_J))).TupleOrd()).TupleString("02x");
                    }
                    else
                    {
                        hv_Char = hv_DecodedDataStrings.TupleStrBitSelect(hv_J);
                    }

                    hv_String += hv_Char;
                }
            }
            else
            {
                hv_String = "未识别到条形码";
            }

            string result = "识别结果: " + hv_String;
            return result;
        }
        
    }
}
