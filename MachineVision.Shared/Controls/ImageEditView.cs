using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MachineVision.Shared.Controls
{
    public class ImageEditView : Control
    {
        private HSmartWindowControlWPF hSmart;
        private HWindow hWindow;
       

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate(); 

            if (GetTemplateChild("PATH_SMART") is HSmartWindowControlWPF HSmart)
            {
                this.hSmart = HSmart;
                this.hSmart.Loaded += HSmart_Loaded;

              
            }
            //绘制矩形
            if (GetTemplateChild("PATH_RECT") is Button BtnRect)
            {
                BtnRect.Click += async (s, e) =>
                {
                    if (hWindow == null) return;

                    await Task.Run(() =>
                    {
                        try
                        {
                            HOperatorSet.SetDraw(hWindow, "margin");
                            HOperatorSet.SetLineWidth(hWindow, 2);
                            HOperatorSet.SetColor(hWindow, "red");
                            HOperatorSet.DrawRectangle1(hWindow,
                                out HTuple row1,
                                out HTuple column1,
                                out HTuple row2,
                                out HTuple column2);

                            HOperatorSet.GenRectangle1(out HObject rectangle, row1, column1, row2, column2);

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                hWindow.DispObj(rectangle);
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("绘图出错：" + ex.Message);
                        }
                    });
                };

            }
        }

        private void HSmart_Loaded(object sender, RoutedEventArgs e)
        {
            hWindow = this.hSmart.HalconWindow;
            
        }
        public HObject Image
        {
            get { return (HObject)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }


        public static readonly DependencyProperty ImageProperty =
         DependencyProperty.Register(
                nameof(Image),
                 typeof(HObject),
                 typeof(ImageEditView),
                new PropertyMetadata(null, ImageChangedCallBack));

        /// <summary>
        /// 图片改变回调
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void ImageChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageEditView view && e.NewValue is HObject newImage)
            {
                if (view.hWindow != null)
                {
                    view.Display(newImage);
                }
            }
        }


        private void Display(HObject hObject)
        {
            hWindow.DispObj(hObject);
            
        }




        
    }
}
