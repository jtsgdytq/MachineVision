using HalconDotNet;
using MachineVision.Core;
using MachineVision.Core.TemplateMatch;
using MachineVision.Core.TemplateMatch.Share;
using MachineVision.Shared.Controls;
using MachineVision.Shared.EventAggregator;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineVision.TemplateMatch.ViewModels
{
    public class ShapeViewModel : NavigationViewModel
    {
        public ITemplateMatchService MatchService { get; }

        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// 通过事件聚合器获取绘制对象的信息
        /// 此处为订阅事件
        public ShapeViewModel(IEventAggregator eventAggregator)
        {
            MatchService = ContainerLocator.Container.Resolve<ITemplateMatchService>(nameof(TemplateMatchType.ShapeMatch));
            LoadImageCommand = new DelegateCommand(LoadImage);
            AddTemplateCommand = new DelegateCommand(AddTemplate);
            RunCommand = new DelegateCommand(Run);
            ClearCommand = new DelegateCommand(() =>
            {
                MatchService.clearTemplate();
                ResultInfo = null;
                DrawObjectInfo = null;
                Image = null;
              
            })
            {

            };
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DrawObjectEvent>().Subscribe(OnDrawObject);


        }



        private void OnDrawObject(DrawObjectInfo info)
        {
            DrawObjectInfo = info as DrawObjectInfo;
        }

        private HObject image;

        public HObject Image
        {
            get { return image; }
            set
            {
                image = value;
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



        public DelegateCommand AddTemplateCommand { get; set; }

        private void AddTemplate()
        {
            if (drawObjectInfo.HObject == null || Image == null)
            {
                MessageBox.Show("请加载图像！");
                return;
            }
            MatchService.CraeteTemplate(Image, DrawObjectInfo.HObject);
            MessageBox.Show("模板创建成功！");
        }

        public DelegateCommand RunCommand { get; set; }

        private void Run()
        {
            ResultInfo = MatchService.Run(Image);
        }


        private TemplateResult resultInfo;

        public TemplateResult ResultInfo
        {
            get { return resultInfo; }
            set
            {
                resultInfo = value;
                RaisePropertyChanged(nameof(ResultInfo));
            }
        }


        public DelegateCommand ClearCommand { get; set; }


    }
       
}
