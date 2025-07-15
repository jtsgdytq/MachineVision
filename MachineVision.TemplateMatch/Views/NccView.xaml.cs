using HalconDotNet;
using MachineVision.Core.TemplateMatch.TemplateModel.NccModel;
using MachineVision.Core.TemplateMatch.TemplateModel.ShapeModel;
using MachineVision.TemplateMatch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MachineVision.TemplateMatch.Views
{
    /// <summary>
    /// NccView.xaml 的交互逻辑
    /// </summary>
    public partial class NccView : UserControl
    {
        public NccView()
        {
            InitializeComponent();
            this.Loaded += NccView_Loaded;
        }


        private void NccView_Loaded(object sender, RoutedEventArgs e)
        {
            // 订阅事件（这里确保 MyImageEditView 已初始化）
            MyImageEditView.HalconWindowReady += OnHalconWindowReady;
        }

        private void OnHalconWindowReady(HWindow hWindow)
        {
            if (this.DataContext is NccViewModel vm &&
                vm.MatchService is NccMatchService service)
            {
                service.HWindow = hWindow;

            }
        }
    }
}
