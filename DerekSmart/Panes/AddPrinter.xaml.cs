using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using SharpIpp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zeroconf;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DerekSmart.Panes
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddPrinter : Page
	{
		SharpIppClient client;

		public AddPrinter()
		{
			this.InitializeComponent();
			client = new SharpIppClient();
		}



		private async Task ProbeForNetworkPrinters()
		{
			List<DataTypes.PrinterObject> printerObjects = new();
			var myTimeSpan = new TimeSpan(0, 0, 5);
			var results = await ZeroconfResolver.ResolveAsync("_ipp._tcp.local.", scanTime: myTimeSpan);
			List<Task> getInfoTasks = new();
			foreach (IZeroconfHost host in results)
			{
				getInfoTasks.Add(GetInfo(host));
			}
			await Task.WhenAll(getInfoTasks);
		}

		private async Task GetInfo(IZeroconfHost host)
		{
			var temp = new DataTypes.PrinterObject(host.IPAddress);
			await temp.RefreshValues();
			await temp.DownloadImage();
			var tempPrinter = new CustomControls.AddPrinterPrinterDisplay(host.IPAddress, temp.IPPLocation, host.DisplayName);
			BitmapImage bi3 = new BitmapImage();
			bi3.UriSource = new Uri(temp.DownloadedImageName);
			tempPrinter.changeImage(bi3);
			if (LocalPrinterList.Items.Contains(tempPrinter)) { return; }
			LocalPrinterList.Items.Add(tempPrinter);
		}

		private async Task SearchPrinters()
		{
			DisplayTextTop.Text = "Searching for printers! When you see the printer you want, click on it to continue setup.";
			ProgressBarIndication.IsIndeterminate = true;
			await ProbeForNetworkPrinters();

			ProgressBarIndication.IsIndeterminate = false;
			DisplayTextTop.Text = "When you see the printer you want, click on it to continue setup.";
		}


		private async void ScanButton_Click(object sender, RoutedEventArgs e)
		{
			await SearchPrinters();

		}
		private async void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
		{
			await SearchPrinters();
		}
	}
}
