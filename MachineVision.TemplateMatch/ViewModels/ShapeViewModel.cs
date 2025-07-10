using HalconDotNet;
using MachineVision.Core;
using MachineVision.Core.TemplateMatch;
using MachineVision.Shared.Controls;
using MachineVision.Shared.EventAggregator;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.TemplateMatch.ViewModels
{
    public class ShapeViewModel:NavigationViewModel
    {
        public ITemplateMatchService MatchService { get; }

        private readonly IEventAggregator _eventAggregator;
        public ShapeViewModel(IEventAggregator eventAggregator)
        {
            MatchService = ContainerLocator.Container.Resolve<ITemplateMatchService>(nameof(TemplateMatchType.ShapeMatch));
            LoadImageCommand = new DelegateCommand(LoadImage);
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DrawObjectEvent>().Subscribe(OnDrawObject);
        }

        private void OnDrawObject(DrawObjectInfo info)
        {
            var drawObject = info as DrawObjectInfo;
        }

        private HObject image;

        public HObject Image
        {
            get { return image; }
            set { image = value; 
                RaisePropertyChanged(nameof(Image));
            }
        }

        private DrawObjectInfo drawObjectInfo;
        public DrawObjectInfo DrawObjectInfo
        {
            get => drawObjectInfo;
            set
            {
                drawObjectInfo = value;
                RaisePropertyChanged(nameof(DrawObjectInfo));
            }
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
                Image = images;
            }
        }
    }
}
