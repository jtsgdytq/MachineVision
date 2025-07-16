using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Core.Ocr
{
    public class QrCodeService
    {
        public QrCodeService()
        {
            HOperatorSet.CreateBarCodeModel(new HTuple(), new HTuple(), out hv_BarCodeHandle);
        }


        HObject ho_SymbolRegions = null;


        HTuple hv_BarCodeHandle = new HTuple();
        HTuple  hv_DecodedDataStrings = new HTuple();



        public string Run(HObject image)
        {
            try
            {
                // 转灰度（防止透明图）
                HOperatorSet.Rgb1ToGray(image, out HObject grayImage);

                // 查找二维码
                HOperatorSet.FindBarCode(grayImage, out ho_SymbolRegions, hv_BarCodeHandle,
                    "auto", out hv_DecodedDataStrings);

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


    }
}
