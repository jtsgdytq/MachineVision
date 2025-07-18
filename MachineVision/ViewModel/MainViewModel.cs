using MachineVision.Core;
using MachineVision.Model;
using MachineVision.Service;


namespace MachineVision.ViewModel
{
    public class MainViewModel : NavigationViewModel
    {
        private readonly IRegionManager regionManager;
        public INavigationMenuService NavigationMenuService { get; }

        private bool isOpenDialog;
        public bool IsOpenDialog
        {
            get { return isOpenDialog; }
            set
            {
                isOpenDialog = value;
                RaisePropertyChanged(nameof(IsOpenDialog));
            }
        }

        // 所有的命令声明
        public DelegateCommand<NavigationItems> NavigationCommand { get; set; }
        public DelegateCommand NavigationHomeCommand { get; set; }
        public DelegateCommand CloseMenuCommand { get; set; }
      
        public DelegateCommand<NavigationItems> MenuItemClickCommand { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="navigationMenuService"></param>
        /// <param name="_regionManager"></param>
        public MainViewModel(INavigationMenuService navigationMenuService, IRegionManager _regionManager)
        {
            NavigationMenuService = navigationMenuService; //依赖注入菜单服务
            regionManager = _regionManager; //依赖注入区域管理器
            NavigationMenuService.Inition();

            // 初始化所有命令
            InitializeCommands();
        }

        /// <summary>
        /// 初始化所有命令
        /// </summary>
        private void InitializeCommands()
        {
            // 原有的导航命令
            NavigationCommand = new DelegateCommand<NavigationItems>(Navigation);

            // 首页导航命令
            NavigationHomeCommand = new DelegateCommand(() =>
            {
                IsOpenDialog = false;
                regionManager.RequestNavigate("MainViewRegion", "DashboardView");
            });

            // 关闭菜单命令
            CloseMenuCommand = new DelegateCommand(() =>
            {
                IsOpenDialog = false;
            });

            // 菜单项点击命令
            MenuItemClickCommand = new DelegateCommand<NavigationItems>(Click);
        }

        private void Click(NavigationItems navigationItems)
        {
            if (navigationItems == null) return;

            // 处理菜单项点击逻辑 - 导航到对应页面
            if (!string.IsNullOrEmpty(navigationItems.PageName))
            {
                regionManager.RequestNavigate("MainViewRegion", navigationItems.PageName);
            }

            // 关闭菜单
            IsOpenDialog = false;
        }

        /// <summary>
        /// 原有的导航方法
        /// </summary>
        /// <param name="navigationItems"></param>
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

            //导航到对应的视图
            regionManager.RequestNavigate("MainViewRegion", navigationItems.PageName);
        }

        

        /// <summary>
        /// 导航到此视图时的处理
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            NavigationMenuService.Inition();
            regionManager.RequestNavigate("MainViewRegion", "DashboardView");
        }
    }
}