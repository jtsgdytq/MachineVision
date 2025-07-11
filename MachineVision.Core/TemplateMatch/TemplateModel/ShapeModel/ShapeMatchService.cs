using HalconDotNet;
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
        HTuple modelID; // Change type from HObject to HTuple

        public Task CraeteTemplate(HObject hObject)
        {
            // 检查 hObject 是否为空或无效
            if (hObject == null || !hObject.IsInitialized())
                throw new ArgumentException("模板对象未初始化或无效");

            // 检查 hObject 是否有像素
            HTuple width, height;
            HOperatorSet.GetImageSize(hObject, out width, out height);
            if (width.I == 0 || height.I == 0)
                throw new ArgumentException("模板对象没有有效的像素数据");

            HOperatorSet.CreateShapeModel(hObject, "auto", (new HTuple(0)).TupleRad(), (new HTuple(90)).TupleRad(), "auto", "auto", "use_polarity", "auto", "auto", out modelID);

            return Task.CompletedTask;
        }

        public void Run(HObject Image)
        {

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
