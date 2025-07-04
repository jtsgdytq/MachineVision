﻿using MachineVision.Core;
using MachineVision.Model;
using MachineVision.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.ViewModel
{
    public class MainViewModel:NavigationViewModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="navigationMenuService"></param>
        public MainViewModel(INavigationMenuService navigationMenuService)
        {
                NavigationMenuService= navigationMenuService;//依赖注入菜单服务
                 NavigationMenuService.Inition();

                 NavigationCommand = new DelegateCommand<NavigationItems>(Navigation);
        }


        public INavigationMenuService NavigationMenuService { get; }

        private bool isOpenDialog;

        public bool IsOpenDialog
        {
            get { return isOpenDialog; }
            set { isOpenDialog = value;  
                RaisePropertyChanged (nameof(IsOpenDialog));
            }
        }

      
        public DelegateCommand<NavigationItems> NavigationCommand { get; set; }

        private void Navigation(NavigationItems navigationItems)
        {
            if (navigationItems == null) return;
            if (navigationItems.Name.Equals("全部"))
            {
                IsOpenDialog = true;
            }
            else
            {
                IsOpenDialog = false;
            }
            
        }



        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            NavigationMenuService.Inition();

            System.Diagnostics.Debug.WriteLine(NavigationMenuService);
        }

    }
}
