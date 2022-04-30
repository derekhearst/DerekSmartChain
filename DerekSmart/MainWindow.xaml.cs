using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
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
           MainNavView.SelectedItem= MainNavView.MenuItems[0];
        }

        private void MainNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions()
            {
                IsNavigationStackEnabled = false,

                
            };

            switch (args.InvokedItemContainer.Tag.ToString())
            {
                case "AddPrinter":                   
                    ContentFrame.NavigateToType(typeof(Panes.AddPrinter),null, navOptions);                   
                    break;
                case "Home":
                    ContentFrame.NavigateToType(typeof(Panes.HomePage),null,navOptions);
                    break;
                default:
                    break;  

            }

    
        }
    }
}
