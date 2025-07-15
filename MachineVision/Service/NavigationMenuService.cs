using MachineVision.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineVision.Service
{
    internal class NavigationMenuService : BindableBase, INavigationMenuService
    {

        public NavigationMenuService()
        {
            ObservableCollection<NavigationItems> Items = new ObservableCollection<NavigationItems>();
        }

        private ObservableCollection<NavigationItems> items = new ObservableCollection<NavigationItems>();

        public ObservableCollection<NavigationItems> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public void Inition()
        {
            Items.Clear();
            Items.Add(new NavigationItems("", "All", "全部", "", new ObservableCollection<NavigationItems>()
             {
                new NavigationItems("", "TemplateMatch", "模板匹配", "", new ObservableCollection<NavigationItems>()
                 {
                    new NavigationItems("ShapeOutline", "ShapeMatch", "形状匹配", "ShapeView"),
                    new NavigationItems("Cloud", "NccMatch", "灰度匹配", "NccView"),
                    new NavigationItems("ShapeOvalPlus", "DeformationMatch", "形变匹配", "LocalDeformableView"),
                 }),
                    new NavigationItems("", "Measure", "比较测量", "", new ObservableCollection<NavigationItems>()
                {
                    new NavigationItems("Circle", "Caliper", "卡尺找圆", "CircleMeasureView"),
                 }),
                    new NavigationItems("", "Character", "字符识别", "", new ObservableCollection<NavigationItems>()
                {
                    new NavigationItems("Barcode", "BarCode", "一维码识别", "BarCodeView"),
                    new NavigationItems("Qrcode", "QrCode", "二维码识别", "QrCodeView"),
                 })
             }));
            Items.Add(new NavigationItems("Cog", "Setting", "系统设置", "SettingView"));
        }

    }
}
