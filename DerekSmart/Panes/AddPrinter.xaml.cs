using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Zeroconf;
using SharpIpp;
using SharpIpp.Model;
using System.Threading.Tasks;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DerekSmart.Panes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPrinter : Page
    {
        public AddPrinter()
        {
            this.InitializeComponent();
        }

        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            await ProbeForNetworkPrinters();
            LocalPrinterList.Items.Add(new CustomControls.AddPrinterPrinterDisplay());
        }

        private async Task<List<DataTypes.PrinterObject>> ProbeForNetworkPrinters()
        {
            ProgressIndication.IsActive = true;
            List<DataTypes.PrinterObject> printerObjects = new();
            SharpIppClient client = new SharpIppClient();
            IReadOnlyList<IZeroconfHost> results = await ZeroconfResolver.ResolveAsync("_ipp._tcp");

            List<Task> getValuesTasks = new();
            foreach (IZeroconfHost host in results)
            {
                DataTypes.PrinterObject printer = new()
                {
                    IPAddress = host.IPAddress,
                };                
                printerObjects.Add(printer);
                getValuesTasks.Add(printer.RefreshValues());
            }
            await Task.WhenAll(getValuesTasks);
            ProgressIndication.IsActive = false;
            return printerObjects;
        }
    }
}
