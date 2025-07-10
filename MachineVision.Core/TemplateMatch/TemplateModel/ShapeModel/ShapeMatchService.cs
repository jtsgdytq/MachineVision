using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Core.TemplateMatch.TemplateModel.ShapeModel
{
    public class ShapeMatchService : ITemplateMatchService
    {

        public Task CraeteTemplate(HObject Image, HObject hObject)
        {

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
