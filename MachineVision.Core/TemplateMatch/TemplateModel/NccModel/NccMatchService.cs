using HalconDotNet;
using MachineVision.Core.TemplateMatch.Share;
using MachineVision.Core.TemplateMatch.TemplateModel.NccModel.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MachineVision.Core.TemplateMatch.Share.TemplateResult;

namespace MachineVision.Core.TemplateMatch.TemplateModel.NccModel
{
    public class NccMatchService : BindableBase,ITemplateMatchService
    {
       HTuple ModelID;
       HTuple row, column, angle, score;
       TemplateResult result;
        HObject HObject;


        public Task CraeteTemplate(HObject image, HObject hObject)
        {
            if (hObject == null || !hObject.IsInitialized())
                throw new ArgumentException("模板对象未初始化或无效");

            // 若不是单通道，则转换为灰度图
            HOperatorSet.CountChannels(image, out HTuple channels);
            if (channels.I != 1)
            {
                HOperatorSet.Rgb1ToGray(image, out image);
            }

            // 提取模板区域图像
            HOperatorSet.ReduceDomain(image, hObject, out HObject templateImage);

            // 创建模板
            HOperatorSet.CreateNccModel(templateImage,
                CreateNcc.NumLevels,
                CreateNcc.AngleStart,
                CreateNcc.AngleExtent,
                CreateNcc.AngleStep,
                CreateNcc.Metric,
                out ModelID);
            HObject=hObject;

            return Task.CompletedTask;
        }


        public TemplateResult Run(HObject Image)
        {

            result= new TemplateResult();
            // 若不是单通道，则转换为灰度图
            HOperatorSet.CountChannels(Image, out HTuple channels);
            if (channels.I != 1)
            {
                HOperatorSet.Rgb1ToGray(Image, out Image);
            }

            if (HWindow == null)
            {
                result.IsSuccess = false;
                result.Message = "显示窗口未设置或未打开。";
                return result;
            }


            if (ModelID == null)
            {
                result.IsSuccess = false;
                result.Message = "模型未创建或无效";
                return result;
            }

            try
            {
                HOperatorSet.FindNccModel(
                    Image,
                    ModelID,
                    FindNcc.AngleStart,
                    FindNcc.AngleExtent,
                    FindNcc.MinScore,
                    int.Parse(FindNcc.NumMatches),   
                    FindNcc.MaxOverlap,
                    FindNcc.SubPixel,
                    int.Parse(FindNcc.NumLevels),    
                    out row,
                    out column,
                    out angle,
                    out score
                );



                if (score.Length == 0)
                {
                    result.IsSuccess = false;
                    result.Message = "未找到匹配结果";
                    return result;
                }


                //HOperatorSet.GetShapeModelContours(out HObject resultRegion, ModelID, 1);



                for (int i = 0; i < score.Length; i++)
                {

                    //HOperatorSet.VectorAngleToRigid(0, 0, 0,
                    //                                row[i], column[i], angle[i], out HTuple homMat2D);

                    //// hObject 是你最开始用来创建模板的区域
                    //HOperatorSet.AffineTransRegion(HObject, out HObject transRegion, homMat2D, "nearest_neighbor");

                    result.Results.Add(new MatchTemplateResult
                    {
                        Index = i,
                        Row = row[i].D,
                        Column = column[i].D,
                        Angle = angle[i].D,
                        Score = score[i].D,
                        Contours = null

                    });


                    // 在窗口中显示匹配结果
                    if (result.Results != null)
                    {
                        foreach (var item in result.Results)
                        {
                            if (Setting.IsShowCenter)
                                HOperatorSet.DispCross(HWindow, item.Row, item.Column, 30, item.Angle);
                            if (Setting.IsShowText)
                                HOperatorSet.DispText(HWindow, $"Score: {item.Score:F2}", "window", item.Row, item.Column, "black", "box", "true");
                            //if (Setting.IsDdetectionRange)
                            //    HOperatorSet.DispObj(item.Contours, HWindow);

                        }


                    }


                }






                result.IsSuccess = true;
                result.Message = $"匹配成功，共找到 {score.Length} 个结果";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"匹配过程中发生错误: {ex.Message}";
                return result;
            }


           
        }


        public void clearTemplate()
        {
            hWindow?.ClearWindow();
            ModelID = null;
            row = null;
            column = null;
            angle = null;
            score = null;
            result = new TemplateResult();
            HObject = null;
            CreateNcc = new CreateNccTemplateParam();
            FindNcc = new FindNccTemplateParam();
            Setting = new MatchSetting
            {
                IsShowCenter = true,
                IsShowText = true,
                IsDdetectionRange = false
            };
        }


        public NccMatchService()
        {
            CreateNcc = new CreateNccTemplateParam();
            FindNcc = new FindNccTemplateParam();

            
            HWindow = new HWindow();
            Setting = new MatchSetting
            {
                IsShowCenter = true,
                IsShowText = true,
                IsDdetectionRange = false
            };
        }

        private MatchSetting setting;

        public MatchSetting Setting
        {
            get { return setting; }
            set { setting = value;  RaisePropertyChanged(); }
        }
       






        private HWindow hWindow;
        public HWindow HWindow
        {
            get { return hWindow; }
            set { hWindow = value;  }
        }


        private CreateNccTemplateParam createNcc;

        public CreateNccTemplateParam CreateNcc
        {
            get { return createNcc; }
            set { createNcc = value; }
        }

        private FindNccTemplateParam findNcc;
        private HObject resultRegion;

        public FindNccTemplateParam FindNcc
        {
            get { return findNcc; }
            set { findNcc = value; }
        }




    }
}
