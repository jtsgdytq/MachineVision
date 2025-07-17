using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MachineVision.Core.Ocr
{
    public class QrCodeService
    {
        public QrCodeService()
        {
            HOperatorSet.CreateDataCode2dModel("QR Code", new HTuple(), new HTuple(), out hv_DataCodeHandle);
        }


        HObject ho_Image, ho_SymbolXLDs = null;

        // Local control variables 

       
        HTuple hv_DataCodeHandle = new HTuple();
        HTuple hv_ResultHandles = new HTuple(), hv_DecodedDataStrings = new HTuple();



        public string Run(HObject image)
        {
            try
            {
                // 转灰度（防止透明图）
                HOperatorSet.Rgb1ToGray(image, out HObject grayImage);

                // 查找二维码
                HOperatorSet.FindDataCode2d(grayImage, out ho_SymbolXLDs, hv_DataCodeHandle,
            new HTuple(), new HTuple(), out hv_ResultHandles, out hv_DecodedDataStrings);

                if (hv_DecodedDataStrings.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("识别结果：");
                    for (int i = 0; i < hv_DecodedDataStrings.Length; i++)
                    {
                        sb.Append(hv_DecodedDataStrings[i].S + " ");
                    }
                    return sb.ToString().Trim();
                }
                else
                {
                    return "识别失败：未检测到二维码";
                }
            }
            catch (HOperatorException ex)
            {
                return $"识别异常：{ex.Message}";
            }
        }


        public void Dispose()
        {
            if (ho_Image != null && ho_Image.IsInitialized())
            {
                ho_Image.Dispose();
            }
            if (ho_SymbolXLDs != null && ho_SymbolXLDs.IsInitialized())
            {
                ho_SymbolXLDs.Dispose();
            }
            if (hv_DataCodeHandle != null && hv_DataCodeHandle.Length > 0)
            {
                HOperatorSet.ClearDataCode2dModel(hv_DataCodeHandle);
            }
           
        }
    }
}
