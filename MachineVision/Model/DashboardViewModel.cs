using MachineVision.Core;
using MachineVision.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Model
{
    public class DashboardViewModel : NavigationViewModel
    {


        public DashboardViewModel(INavigationMenuService _navigationMenuService)
        {
            navigationMenuService = _navigationMenuService;


        }

        public INavigationMenuService navigationMenuService { get; }
        

    }
}
