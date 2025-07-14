using HalconDotNet;
using MachineVision.Core.TemplateMatch.TemplateModel.ShapeModel.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using static MachineVision.Core.TemplateMatch.TemplateModel.ShapeModel.Information.TemplateResult;

namespace MachineVision.Core.TemplateMatch.TemplateModel.ShapeModel
{
    public class ShapeMatchService :BindableBase ,ITemplateMatchService
    {
        HTuple ModelID;

        HObject templateimage;

        HObject resultRegion;
        HTuple row, column, angle, score;
        TemplateResult result = new TemplateResult();

        


        public Task CraeteTemplate(HObject image, HObject hObject)
        {
            // 检查 hObject 是否为空或无效
            if (hObject == null || !hObject.IsInitialized())
                throw new ArgumentException("模板对象未初始化或无效");

            HOperatorSet.ReduceDomain(image, hObject, out templateimage);

            HOperatorSet.CreateShapeModel(templateimage, 
                CreateShape.NumLevels,
                CreateShape.AngleStart,
                CreateShape.AngleExtent, 
                createShape.AngleStart,
                CreateShape.Optimization, 
                CreateShape.Metric,
                CreateShape.Contrast, 
                CreateShape.MinContrast, 
                out ModelID);

           

            return Task.CompletedTask;
        }

        
        public TemplateResult Run(HObject Image)
        {

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
                HOperatorSet.FindShapeModel(
                    Image,
                    ModelID,
                    FindShape.AngleStart,
                    FindShape.AngleExtent,
                    FindShape.MinScore,
                    FindShape.NumMatches,
                    FindShape.MaxOverlap,
                    FindShape.SubPixel,
                    FindShape.NumLevels,
                    FindShape.Greediness,
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


                HOperatorSet.GetShapeModelContours(out resultRegion, ModelID, 1);

               

                for (int i = 0; i < score.Length; i++)
                {

                    HOperatorSet.VectorAngleToRigid(0, 0, 0,
                           row[i], column[i], angle[i], out HTuple homMat2D);

                    HOperatorSet.AffineTransContourXld(resultRegion, out HObject transRegion, homMat2D);
                    result.Results.Add(new MatchTemplateResult
                    {
                        Index = i,
                        Row = row[i].D,
                        Column = column[i].D,
                        Angle = angle[i].D,
                        Score = score[i].D,
                        Contours= transRegion

                    });

                   

                    if (result.Results!= null )
                    {
                        foreach (var item in result.Results)
                        {
                            if (Setting.IsShowCenter)
                                HOperatorSet.DispCross(HWindow,item.Row, item.Column,30,item.Angle);
                            if( Setting.IsShowText)
                                HOperatorSet.DispText(HWindow, $"Score: {item.Score:F2}", "window", item.Row, item.Column, "black", "box", "true");
                            if (Setting.IsDdetectionRange)
                                HOperatorSet.DispObj(item.Contours, HWindow);

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
            if (ModelID != null)
            {
                HOperatorSet.ClearShapeModel(ModelID);
                ModelID = null;
            }

            if (templateimage != null)
            {
                templateimage.Dispose();
                templateimage = null;
            }
            if (resultRegion != null)
            {
                resultRegion.Dispose();
                resultRegion = null;
            }
            result.Results.Clear();
            result.IsSuccess = false;
            result.Message = string.Empty;
            row = null;
            column = null;
            angle = null;
            score = null;


        }












        #region 参数属性
        public ShapeMatchService()
        {
            CreateShape = new CreateShapeTemplateParam();
            FindShape = new FindShapeTemplateParam();
            Setting = new ShapematchSetting();
            HWindow = new HWindow();
        }


        private CreateShapeTemplateParam createShape;

        public CreateShapeTemplateParam CreateShape
        {
            get { return createShape; }
            set { createShape = value; }
        }

        private FindShapeTemplateParam findShape;

        public FindShapeTemplateParam FindShape
        {
            get { return findShape; }
            set { findShape = value; }

        }


        private ShapematchSetting setting;

        public ShapematchSetting Setting  
        {
            get { return setting; }
            set { setting = value;  
                RaisePropertyChanged(nameof(Setting));
            }
        }

        private HWindow hWindow;

        public HWindow HWindow
        {
            get { return hWindow; }
            set { hWindow = value;
                RaisePropertyChanged();
            }
        }




        #endregion


    }
}
