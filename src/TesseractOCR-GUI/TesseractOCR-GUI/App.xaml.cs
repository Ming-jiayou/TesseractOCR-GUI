using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TesseractOCR_GUI.Views;
using Prism.Ioc;
using Prism.Unity;

namespace TesseractOCR_GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CommandLineWrapping>();
            containerRegistry.RegisterForNavigation<APIWrapping>();
        }
    }
}
