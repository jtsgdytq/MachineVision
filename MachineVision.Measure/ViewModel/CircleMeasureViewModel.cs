using HalconDotNet;
using MachineVision.Core;
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
using System.Windows.Documents;

namespace MachineVision.Measure.ViewModel
{
   public class CircleMeasureViewModel:NavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
       private readonly Mertology_Circle _mertology_Circle;

        public CircleMeasureViewModel(IEventAggregator eventAggregator,Mertology_Circle mertology_Circle)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DrawObjectEvent>().Subscribe(OnDrawObject);
            LoadImageCommand = new DelegateCommand(LoadImage);
            RunCommand = new DelegateCommand(run);
            _mertology_Circle = mertology_Circle;
           

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
                Console.WriteLine("ViewModel 接收到 HWindow：" + (value != null));
                RaisePropertyChanged(); }
        }



        public DelegateCommand RunCommand {  get; set; }

        private void run()
        {
            result= _mertology_Circle.Run_Metrology_Circle(Image,DrawObjectInfo);
            if(result != null)
            {
                HOperatorSet.DispObj(result.contuns, HalconWindow);
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


    }
}
