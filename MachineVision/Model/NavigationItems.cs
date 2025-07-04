using Prism.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Model
{
	
    public class NavigationItems:BindableBase
    {
        public NavigationItems(string key,string icon,string name,string pagename,ObservableCollection<NavigationItems> items=null)
        {
            key = key;
			Icon = icon;
			Name = name;
			PageName = pagename;
			Items = items ?? new ObservableCollection<NavigationItems>();
        }




        /// <summary>
        /// 图标
        /// </summary>
        private string icon;

		public string Icon
		{
			get { return icon; }
			set { 
				icon = value;
				RaisePropertyChanged(nameof(Icon));
            }
		}

		private string key;

		public string Key
		{
			get { return key; }
			set { key = value;
				RaisePropertyChanged(nameof(Key));
            }
		}



		/// <summary>
		/// 菜单名称
		/// </summary>
		private string name;

		public string Name
		{
			get { return name; }
			set { name = value;
				RaisePropertyChanged(nameof(Name));
            }
		}

        /// <summary>
        /// 页面名称
        /// </summary>
        private string pageName;

		public string PageName
		{
			get { return pageName; }
			set { pageName = value; 
				RaisePropertyChanged(nameof(PageName));
            }
		}

		/// <summary>
		/// 导航对象
		/// </summary>
		private ObservableCollection<NavigationItems> items;

		public ObservableCollection<NavigationItems> Items
        {
			get { return  items; }
			set { items = value;
				RaisePropertyChanged(nameof(Items));
            }
			
		}




	}
}
