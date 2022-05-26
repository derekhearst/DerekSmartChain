using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DerekSmart
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        List<DataTypes.PrinterObject> printerObjects = new();
        public MainWindow()
        {
            this.InitializeComponent();

            InitTask();
            Title = "Derek Smart";

        }

        public async void InitTask()
        {
            StorageFolder localStorage = ApplicationData.Current.LocalFolder;
            StorageFolder printersFolder = null;
            try { printersFolder = await localStorage.CreateFolderAsync("Printers"); }
            catch { printersFolder = await localStorage.GetFolderAsync("Printers"); }
            List<StorageFile> avaliablePrinters = (await printersFolder.GetFilesAsync()).ToList();
            foreach (StorageFile avaliablePrinter in avaliablePrinters)
            {
                try
                {
                    printerObjects.Add(await DataTypes.PrinterObject.ReadFromFile(avaliablePrinter));
                }
                catch
                {
                    Console.WriteLine($"Error Loading {avaliablePrinter.Name}");
                }
            }
            if (printerObjects.Count == 0)
            {
                ContentFrame.Navigate(typeof(Panes.NoPrinter), null, new DrillInNavigationTransitionInfo());
            }
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
                        if (printerObjects.Count == 0)
                        {
                            ContentFrame.Navigate(typeof(Panes.NoPrinter), null, new DrillInNavigationTransitionInfo());
                        }
                        else
                        {
                            ContentFrame.Navigate(typeof(Panes.Home), null, new DrillInNavigationTransitionInfo());
                        }
                        break;
                    case "User":
                        ContentFrame.Navigate(typeof(Panes.UserAccount), null, new DrillInNavigationTransitionInfo());
                        break;
                    default:
                        break;
                }
            }

        }
    }
}

