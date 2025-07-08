using MachineVision.Core;
using MachineVision.Model;
using MachineVision.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.ViewModel
{
    public class DashboardViewModel : NavigationViewModel
    {
        private readonly IRegionManager manager;
        public INavigationMenuService navigationMenuService { get; }

        

        public DashboardViewModel(IRegionManager manager, INavigationMenuService navigation)
        {
            this.manager = manager;
            this.navigationMenuService = navigation;

         
            OpenPageCommand = new DelegateCommand<NavigationItems>(OpenPage);

        }


        public DelegateCommand<NavigationItems> OpenPageCommand { get; private set; }

        private void OpenPage(NavigationItems item)
        {
            manager.Regions["MainViewRegion"].RequestNavigate(item.PageName);
        }
    }



}
