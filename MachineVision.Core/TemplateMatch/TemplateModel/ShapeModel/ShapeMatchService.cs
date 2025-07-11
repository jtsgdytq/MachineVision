using HalconDotNet;
using MachineVision.Core.TemplateMatch.TemplateModel.ShapeModel.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MachineVision.Core.TemplateMatch.TemplateModel.ShapeModel
{
    public class ShapeMatchService : ITemplateMatchService
    {
        HTuple modelID;

        HObject templateimage;

        HObject resultRegion;
        HTuple row, column, angle, score;

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
                out modelID);

           

            return Task.CompletedTask;
        }

        // MatchTemplateResult Run(HObject Image)
        //{
        //    if (modelID == null )
        //        throw new InvalidOperationException("模型未创建或无效");

        //    HOperatorSet.FindShapeModel(Image, 
        //        modelID, FindShape.AngleStart,
        //        FindShape.AngleExtent,
        //        FindShape.MinScore,
        //        FindShape.NumMatches,
        //        FindShape.MaxOverlap,
        //        FindShape.SubPixel,
        //        FindShape.NumLevels,
        //        FindShape.Greediness, 
        //        out row, 
        //        out column,
        //        out angle, 
        //        out score);



        //    for(int i=0;i<=score.Length-1; i++)
        //    {

        //    }


        //    return matchresult;


        //}
        public TemplateResult Run(HObject Image)
        {
            var result = new TemplateResult();

            if (modelID == null)
            {
                result.IsSuccess = false;
                result.Message = "模型未创建或无效";
                return result;
            }

            try
            {
                HOperatorSet.FindShapeModel(
                    Image,
                    modelID,
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

                for (int i = 0; i < score.Length; i++)
                {
                    result.Results.Add(new MatchTemplateResult
                    {
                        Index = i,
                        Row = row[i].D,
                        Column = column[i].D,
                        Angle = angle[i].D,
                        Score = score[i].D
                    });
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


        public ShapeMatchService()
        {
            CreateShape = new CreateShapeTemplateParam();
            FindShape = new FindShapeTemplateParam();
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
    }
}
