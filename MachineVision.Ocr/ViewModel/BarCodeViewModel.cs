using HalconDotNet;
using MachineVision.Core;
using MachineVision.Core.Ocr;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineVision.Ocr.ViewModel
{
   public class BarCodeViewModel:NavigationViewModel
    {
        public BarCodeViewModel(BarCodeService barCodeService)
        {
            LoadImageCommand = new DelegateCommand(LoadImage);
            this.barCodeService = barCodeService;

            RunCommand = new DelegateCommand(Run);
        }

        

        private readonly BarCodeService barCodeService;
        private HObject image;
        

        public HObject Image
        {
            get { return image; }

           set { image = value;  RaisePropertyChanged(); }

        }

        private string result;

        public string Result
        {
            get { return result; }
            set { result = value; RaisePropertyChanged(); }
        }
        



        public DelegateCommand LoadImageCommand { get; set; }
        private void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var result = (bool)openFileDialog.ShowDialog();
            if (result)
            {
                var images = new HObject();

               HOperatorSet.ReadImage(out images, openFileDialog.FileName);
                if (images != null && images.IsInitialized())
                {
                    Image = images;

                }
                else
                {
                    MessageBox.Show("图像加载失败！");
                }

            }
        }

        public DelegateCommand RunCommand { get; set; }
        private void Run()
        {
            if( Image == null || !Image.IsInitialized())
            {
                MessageBox.Show("请先加载图像！");
                return;
            }

            try
            {
                Result = barCodeService.Run(Image);             
            }
            catch (Exception ex)
            {
                MessageBox.Show($"条形码识别失败：{ex.Message}");
            }
        }

    }
}
