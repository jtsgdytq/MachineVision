using MachineVision.Core;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MachineVision.ViewModel
{
    public class SettingViewModel : NavigationViewModel
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
                    ModifyTheme(theme => theme.SetBaseTheme(value ? BaseTheme.Dark : BaseTheme.Light), value);
                }
            }
        }

        // 可用颜色列表（MaterialDesign 提供）
        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        // 切换主色调命令
        public DelegateCommand<object> ChangeHueCommand { get; }

        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        /// <summary>
        /// 更换主色调（Primary Color）
        /// </summary>
        private void ChangeHue(object obj)
        {
            if (obj is Color hue)
            {
                var theme = paletteHelper.GetTheme();

                theme.PrimaryLight = new ColorPair(hue.Lighten());
                theme.PrimaryMid = new ColorPair(hue);
                theme.PrimaryDark = new ColorPair(hue.Darken());

                paletteHelper.SetTheme(theme);
            }
        }

        /// <summary>
        /// 切换明/暗主题，包括替换 ResourceDictionary 以更新控件样式
        /// </summary>
        private static void ModifyTheme(Action<Theme> modificationAction, bool isDark)
        {
            var paletteHelper = new PaletteHelper();
            Theme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);

            // 替换完整样式资源字典
            string themeSource = isDark
                ? "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml"
                : "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml";

            var appResources = Application.Current.Resources.MergedDictionaries;

            // 移除旧的明/暗主题样式
            var existingTheme = appResources.FirstOrDefault(rd =>
                rd.Source != null &&
                rd.Source.OriginalString.Contains("MaterialDesignTheme.") &&
                (rd.Source.OriginalString.EndsWith("Dark.xaml") || rd.Source.OriginalString.EndsWith("Light.xaml")));

            if (existingTheme != null)
                appResources.Remove(existingTheme);

            // 添加新的主题样式
            appResources.Insert(0, new ResourceDictionary() { Source = new Uri(themeSource) });
        }
    }
}
