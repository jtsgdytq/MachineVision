using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MachineVision.Shared.Controls
{
    public class ImageEditView : Control
    {
        private HSmartWindowControlWPF hSmart;
        private HWindow hWindow;



        public override void OnApplyTemplate()
        {
            if(GetTemplateChild("PATH_SMART") is HSmartWindowControlWPF HSmart)
            {
                this.hSmart = HSmart;
                hWindow=hSmart.HalconWindow;
            }
                  




          if(  GetTemplateChild("PATH_RECT") is Button BtnRect)
            {
                BtnRect.Click += (s, e) =>
                {
                    // 在这里处理按钮点击事件
                    System.Diagnostics.Debug.WriteLine("Rectangle button clicked");
                };
            }


            base.OnApplyTemplate();
            // 在这里可以添加模板应用后的逻辑
        }
    }
}
