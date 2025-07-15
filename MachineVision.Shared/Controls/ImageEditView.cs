using HalconDotNet;
using MachineVision.Shared.EventAggregator;
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

        private IEventAggregator DrawEvent;
        /// <summary>
        /// 发布事件，用于通知其他组件绘制对象的信息
        /// </summary>
        public ImageEditView()
        {
            this.Loaded += (s, e) =>
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    if (DrawEvent == null)
                    {
                        DrawEvent = Prism.Ioc.ContainerLocator.Container.Resolve<IEventAggregator>();
                    }
                }
            };
        }


        private HSmartWindowControlWPF hSmart;
        private HWindow hWindow;


        private HObject rect;
        private HObject circle;
        private HObject ellipse;

        // 放在类中合适的位置（比如靠近属性区域）
        public HWindow HalconWindow => hWindow;


        public DrawObjectInfo DrawObjectInfo
        {
            get => (DrawObjectInfo)GetValue(DrawObjectInfoProperty);
            set => SetValue(DrawObjectInfoProperty, value);
        }

        public static readonly DependencyProperty DrawObjectInfoProperty =
            DependencyProperty.Register(nameof(DrawObjectInfo), typeof(DrawObjectInfo), typeof(ImageEditView), new PropertyMetadata(null));



        /// <summary>
        /// 绘制图像区域
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("PATH_SMART") is HSmartWindowControlWPF HSmart)
            {
                this.hSmart = HSmart;
                this.hSmart.Loaded += HSmart_Loaded;

                if (hSmart.ContextMenu != null)
                {
                    foreach (var item in hSmart.ContextMenu.Items)
                    {
                        if (item is MenuItem menuItem)
                        {
                            switch (menuItem.Name)
                            {
                                case "MENU_RECT":
                                    menuItem.Click += DrawRectangle;
                                    break;
                                case "MENU_CIRCLE":
                                    menuItem.Click += DrawCircle;
                                    break;
                                case "MENU_ELLIPSE":
                                    menuItem.Click += DrawEllipseAsync;
                                    break;
                            case "MENU_CLEAR":
                                    menuItem.Click += (s, e) =>
                                    {
                                        if (hWindow != null)
                                        {
                                            hWindow.ClearWindow();
                                             this.DrawObjectInfo = null; // 清除绘图信息
                                            if (Image != null)
                                            {
                                                // 如果有原图像，重新显示
                                                hWindow.DispObj(Image);
                                            }
                                            

                                        }
                                    };
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private async void DrawCircle(object sender, RoutedEventArgs e)
        {
            if (hWindow == null) return;
            await Task.Run(() =>
            {
                try
                {
                    HOperatorSet.SetDraw(hWindow, "margin");
                    HOperatorSet.SetLineWidth(hWindow, 2);
                    HOperatorSet.SetColor(hWindow, "blue");
                    HOperatorSet.DrawCircle(hWindow,
                        out HTuple row,
                        out HTuple column,
                        out HTuple radius);
                    HOperatorSet.GenCircle(out circle, row, column, radius);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        hWindow.DispObj(circle);
                        this.DrawObjectInfo = new DrawObjectInfo
                        {
                            Type = DrawObjectInfo.ShapeType.Circle,
                            HObject = circle,
                            HTuples = new HTuple[] { row, column, radius }
                        };
                        DrawEvent.GetEvent<DrawObjectEvent>().Publish(this.DrawObjectInfo);
                    });

                }
                catch (Exception ex)
                {
                    Console.WriteLine("绘图出错：" + ex.Message);
                }
            });

        }

        private async void DrawEllipseAsync(object sender, RoutedEventArgs e)
        {
            if (hWindow == null) return;
            await Task.Run(() =>
            {
                try
                {
                    HOperatorSet.SetDraw(hWindow, "margin");
                    HOperatorSet.SetLineWidth(hWindow, 2);
                    HOperatorSet.SetColor(hWindow, "green");
                    HOperatorSet.DrawEllipse(hWindow,
                        out HTuple row,
                        out HTuple column,
                        out HTuple phi,
                        out HTuple radius1,
                        out HTuple radius2);
                    HOperatorSet.GenEllipse(out ellipse, row, column, phi, radius1, radius2);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        hWindow.DispObj(ellipse);
                        this.DrawObjectInfo = new DrawObjectInfo
                        {
                            Type = DrawObjectInfo.ShapeType.Ellipse,
                            HObject = ellipse,
                            HTuples = new HTuple[] { row, column, phi, radius1, radius2 }
                        };
                        DrawEvent.GetEvent<DrawObjectEvent>().Publish(this.DrawObjectInfo);
                    });

                    


                }
                catch (Exception ex)
                {
                    Console.WriteLine("绘图出错：" + ex.Message);
                }
            });
        }

        private async void DrawRectangle(object sender, RoutedEventArgs e)
        {
            if (hWindow == null) return;
            await Task.Run(() =>
            {
                try
                {
                    HOperatorSet.SetDraw(hWindow, "margin");
                    HOperatorSet.SetLineWidth(hWindow, 2);
                    HOperatorSet.SetColor(hWindow, "red");
                    HOperatorSet.DrawRectangle1(hWindow, out HTuple r1, out HTuple c1, out HTuple r2, out HTuple c2);
                    HOperatorSet.GenRectangle1(out rect, r1, c1, r2, c2);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        hWindow.DispObj(rect);
                        this.DrawObjectInfo = new DrawObjectInfo
                        {
                            Type = DrawObjectInfo.ShapeType.Rectangle,
                            HObject = rect,
                            HTuples = new[] { r1, c1, r2, c2 }
                        };
                        DrawEvent.GetEvent<DrawObjectEvent>().Publish(this.DrawObjectInfo);

                    });

                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("绘图出错：" + ex.Message);
                }
            });
        }



        public event Action<HWindow> HalconWindowReady;
        /// <summary>
        /// 初始化 Halcon 窗口的句柄，将句柄传递给外部使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HSmart_Loaded(object sender, RoutedEventArgs e)
        {
            hWindow = this.hSmart.HalconWindow;

            HalconWindowReady?.Invoke(hWindow);// 触发事件，通知外部 Halcon 窗口已准备好，并传递句柄

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


        public void DisposeResources()
        {
            try
            {
                // 释放图像资源
                Image?.Dispose();

                // 释放绘图对象
                DrawObjectInfo?.HObject?.Dispose();

                // 如果你还保存了其他 Halcon 对象，可以在此处一并释放

                DrawObjectInfo = null;
                hWindow?.ClearWindow();
            }
            catch (Exception ex)
            {
                Console.WriteLine("资源释放出错: " + ex.Message);
            }
        }

        public void ShowTemplate(HObject templateImage)
        {
            this.DisposeResources();  // 先清除当前图像和绘图
            this.Image = templateImage;  // 会自动触发回调显示图像
        }



    }
}
