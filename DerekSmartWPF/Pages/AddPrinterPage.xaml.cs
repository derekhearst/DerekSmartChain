using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using Zeroconf;
namespace DerekSmartWPF.Pages
{
	/// <summary>
	/// Interaction logic for AddPrinterPage.xaml
	/// </summary>
	public partial class AddPrinterPage : UserControl
	{
		public AddPrinterPage()
		{
			InitializeComponent();
		}

		List<DataTypes.PrinterObject> printerList = new();

		private async void RefreshButtonClick(object sender, RoutedEventArgs e)
		{


			await FindLocalPrinters();



		}

		private async Task FindLocalPrinters()
		{
			var mytimeSpan = new TimeSpan(0, 0, 10);
			var results = await ZeroconfResolver.ResolveAsync("_ipp._tcp.local.", callback: CallBackMethod, scanTime: mytimeSpan);


		}

		private async void CallBackMethod(IZeroconfHost host)
		{
			var printerObj = new DataTypes.PrinterObject(host.IPAddress);
			await printerObj.RefreshValues();
			printerList.Add(printerObj);
		}


	}
}
