using HalconDotNet;
using MachineVision.Core;
using MachineVision.Measure.Service;
using MachineVision.Shared.Controls;
using MachineVision.Shared.EventAggregator;
using MachineVision.TemplateMatch.Result;
using MachineVision.TemplateMatch.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MachineVision.Measure.ViewModel
{
   public class CircleMeasureViewModel:NavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
       private readonly Mertology_Circle _mertology_Circle;
        public Mechology_Circle_Param Param { get; }

      

        public CircleMeasureViewModel(IEventAggregator eventAggregator,Mertology_Circle mertology_Circle,Mechology_Circle_Param param)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DrawObjectEvent>().Subscribe(OnDrawObject);
            LoadImageCommand = new DelegateCommand(LoadImage);
            RunCommand = new DelegateCommand(run);
            _mertology_Circle = mertology_Circle;
            Param = param;
            ClearCommand = new DelegateCommand(() => 
            {
                _mertology_Circle.clear();
                DrawObjectInfo = null;
                result = null;
                Image = null;
                HOperatorSet.ClearWindow(halconWindow);

            });

            ShowText = true;
            ShowContour = true;


            
            
        }

       



        DrawObjectInfo DrawObjectInfo;

        MeasureCircleResult result { get; set; } =new MeasureCircleResult();
        private void OnDrawObject(DrawObjectInfo info)
        {
            DrawObjectInfo = info;
        }

        private HObject image;

        public HObject Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(); }
        }

        private HWindow halconWindow;

        public HWindow HalconWindow
        {
            get {

                return halconWindow; }

            set { halconWindow = value;
              
                RaisePropertyChanged(); }
        }



        public DelegateCommand RunCommand {  get; set; }

        private void run()
        {
            if (Image != null || DrawObjectInfo != null)
            {

                result = _mertology_Circle.Run_Metrology_Circle(Image, DrawObjectInfo);

                show(result, Image);
            }

            else
            {
                MessageBox.Show("请加载图像或绘制区域");
                return;
            }
        }

        



        public DelegateCommand LoadImageCommand { get; set; }
        private void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff";
            var result = openFileDialog.ShowDialog();

            if (result != null)
            {
                HalconDotNet.HOperatorSet.ReadImage(out HObject image, openFileDialog.FileName);
                Image = image;
            }
            else
            {
                throw new Exception("无法加载图像");
            }
        }

        private void show( MeasureCircleResult result,HObject image)
        {
            if (result != null)
            {
                HOperatorSet.DispImage(image, HalconWindow);
                HOperatorSet.SetColor(halconWindow, "red");
               
                HOperatorSet.DispObj(result.Contour, HalconWindow);
                HOperatorSet.SetColor(halconWindow, "green");
                if (ShowContour = true)
                    HOperatorSet.DispObj(result.Contours, HalconWindow);
                if(ShowText=true)
                     halconWindow.WriteString(result.message);
            }
        }

        public DelegateCommand ClearCommand {  get; set; }

        private bool showText;

        public bool ShowText
        {
            get { return showText; }
            set { showText = value; RaisePropertyChanged(); }
        }

        private bool showContour;

        public bool ShowContour
        {
            get { return showContour; }
            set { showContour = value;RaisePropertyChanged(); }
        }



    }
}
