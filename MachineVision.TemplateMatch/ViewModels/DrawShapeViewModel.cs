using HalconDotNet;
using MachineVision.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.TemplateMatch.ViewModels
{
    public class DrawShapeViewModel:NavigationViewModel
    {

        public DrawShapeViewModel()
        {
            LoadImageCommand = new DelegateCommand(loadimage);
        }

       

        private HObject image;

		public HObject Image
		{
			get { return image; }
			set { image = value;
				RaisePropertyChanged(nameof(Image));
			}
		}


		public DelegateCommand LoadImageCommand { get; set; }

        private void loadimage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var result = (bool)openFileDialog.ShowDialog();
            if (result)
            {
                var images = new HObject();
                
                HOperatorSet.ReadImage(out images, openFileDialog.FileName);
                Image = images; 
            }
        }
    }
}
