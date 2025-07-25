﻿using HalconDotNet;
using MachineVision.Core.Ocr;
using MachineVision.Core.TemplateMatch;
using MachineVision.Core.TemplateMatch.TemplateModel.NccModel;
using MachineVision.Core.TemplateMatch.TemplateModel.ShapeModel;
using MachineVision.Measure.Service;
using MachineVision.Model;
using MachineVision.Service;
using MachineVision.TemplateMatch.Service;
using MachineVision.View;
using MachineVision.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MachineVision
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()=>null;

        protected override void OnInitialized()
        {
            //从容器当中获取MainView的实例对象
            var container = ContainerLocator.Container;
            var shell = container.Resolve<object>("MainView");
            if (shell is Window view)
            {
                //更新Prism注册区域信息
                var regionManager = container.Resolve<IRegionManager>();
                RegionManager.SetRegionManager(view, regionManager);
                RegionManager.UpdateRegions();

                //调用首页的INavigationAware 接口做一个初始化操作
                if (view.DataContext is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(null);
                    //呈现首页
                    App.Current.MainWindow = view;
                }
            }

            base.OnInitialized();
           
        }


        protected override void RegisterTypes(IContainerRegistry services)
        {
            services.RegisterForNavigation<MainView, MainViewModel>();

            services.RegisterSingleton<INavigationMenuService, NavigationMenuService>();
            services.RegisterForNavigation<DashboardView,DashboardViewModel>();
            services.RegisterForNavigation<SettingView, SettingViewModel>();

            //注册模板匹配服务
            services.Register<ITemplateMatchService,ShapeMatchService>(nameof(TemplateMatchType.ShapeMatch));

            services.Register<ITemplateMatchService, NccMatchService>(nameof(TemplateMatchType.NccMatch));


            services.Register<BarCodeService>();
            services.Register<QrCodeService>();
            services.Register<Mertology_Circle>();
            services.RegisterSingleton<Mechology_Circle_Param>();

        }


        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<TemplateMatch.TemplateMatchModel>();
            moduleCatalog.AddModule<Ocr.OcrMatchModel>();
            moduleCatalog.AddModule<Measure.MeasureModel>();



            base.ConfigureModuleCatalog(moduleCatalog);


        }
    }

}
