using HalconDotNet;
using MachineVision.Core;
using MachineVision.Core.TemplateMatch;
using MachineVision.Core.TemplateMatch.Share;
using MachineVision.Shared.Controls;
using MachineVision.Shared.EventAggregator;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MachineVision.TemplateMatch.ViewModels
{
    public class NccViewModel:NavigationViewModel
    {
        public ITemplateMatchService MatchService { get; }
        private readonly IEventAggregator _eventAggregator;

        public NccViewModel(IEventAggregator eventAggregator)
        {
            //// 通过容器获取模板匹配服务
            MatchService = ContainerLocator.Container.Resolve<ITemplateMatchService>(nameof(TemplateMatchType.NccMatch));

            LoadImageCommand = new DelegateCommand(LoadImage);
            AddTemplateCommand = new DelegateCommand(AddTemplate);
            RunCommand = new DelegateCommand(Run);
            ClearCommand = new DelegateCommand(() =>
            {
                MatchService.clearTemplate();
                DrawInfo = null;
                Image = null;
                Info = null;

            });

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<DrawObjectEvent>().Subscribe(OnDrawObject);
        }

        



        /// <summary>
        /// 获取绘制对象的信息，通过事件聚合器订阅
        /// </summary>
        /// <param name="info"></param>
        private void OnDrawObject(DrawObjectInfo info)
        {
            DrawInfo = info as DrawObjectInfo;
        }



        #region 属性

        private DrawObjectInfo drawInfo;

        public DrawObjectInfo DrawInfo
        {
            get { return drawInfo; }
            set { drawInfo = value; }
        }


        private TemplateResult info;

        public TemplateResult Info
        {
            get { return info; }
            set { info = value; RaisePropertyChanged(); }
        }
        


        private HObject image;

        public HObject Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// template创建命令
        /// </summary>
        public DelegateCommand AddTemplateCommand { get; set; }

        private void AddTemplate()
        {
            if (drawInfo == null || drawInfo.HObject == null)
            {
                MessageBox.Show("请先绘制一个形状对象！");
                return;
            }

            if (Image == null)
            {
                MessageBox.Show("请先加载图像！");
                return;
            }

            MatchService.CraeteTemplate(Image, drawInfo.HObject);
            MessageBox.Show("模板创建成功！");
        }



        /// <summary>
        /// 匹配命令
        /// </summary>
        public DelegateCommand RunCommand { get; set; }


        private void Run()
        {
            if(Image == null)
            {
                MessageBox.Show("请先加载图像！");
                return;
            }

          Info= MatchService.Run(Image);
        }


        /// <summary>
        /// 清除命令
        /// </summary>
        public DelegateCommand ClearCommand { get; set; }
        /// <summary>
        /// 加载图像命令
        /// </summary>

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
