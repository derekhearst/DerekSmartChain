using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DerekSmart
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            MainNavView.SelectedItem = MainNavView.MenuItems.First();
            ContentFrame.Navigate(typeof(Panes.HomePage), null, new DrillInNavigationTransitionInfo());

        }

        private void MainNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(Panes.Settings), null, new DrillInNavigationTransitionInfo());
            }
            else
            {
                switch (args.InvokedItemContainer.Tag.ToString())
                {
                    case "AddPrinter":
                        ContentFrame.Navigate(typeof(Panes.AddPrinter), null, new DrillInNavigationTransitionInfo());
                        break;
                    case "Home":
                        ContentFrame.Navigate(typeof(Panes.HomePage), null, new DrillInNavigationTransitionInfo());
                        break;
                    case "User":
                        ContentFrame.Navigate(typeof(Panes.User), null, new DrillInNavigationTransitionInfo());
                        break;
                    default:
                        break;
                }
            }
    
        }
    }
}
