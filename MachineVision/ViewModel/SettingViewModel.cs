
using MachineVision.Core;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes;
using MaterialDesignThemes.Wpf;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MachineVision.ViewModel
{
    class SettingViewModel : NavigationViewModel
    {
        public SettingViewModel()
        {
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? BaseTheme.Dark : BaseTheme.Light));
                }
            }
        }

        // 颜色调色板列表（Material Design 提供的颜色）
        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        // 切换颜色命令
        public DelegateCommand<object> ChangeHueCommand { get; private set; }

        // 主题调色器（控制主题颜色）
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        // 修改主题方法（用于切换明/暗色）
        private static void ModifyTheme(Action<Theme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            Theme theme = paletteHelper.GetTheme();
            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }

        // 更换主题主色调的方法（例如蓝色、红色）
        private void ChangeHue(object obj)
        {
            var hue = (Color)obj;
            Theme theme = paletteHelper.GetTheme();
            theme.PrimaryLight = new ColorPair(hue.Lighten());
            theme.PrimaryMid = new ColorPair(hue);
            theme.PrimaryDark = new ColorPair(hue.Darken());
            paletteHelper.SetTheme(theme);
        }
    }





}
