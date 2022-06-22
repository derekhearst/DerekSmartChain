using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zeroconf;
using WPFUI;
namespace DerekSmartWPFUI.Pages
{
	/// <summary>
	/// Interaction logic for AddPrinter.xaml
	/// </summary>
	public partial class AddPrinter : Page
	{
		public AddPrinter()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new Printers());			
		}

		private async void StackPanel_Loaded(object sender, RoutedEventArgs e)
		{
			await SearchForPrinters();

		}

		private async Task SearchForPrinters()
		{
			var mytimeSpan = new TimeSpan(0, 0, 10);
			var results = await ZeroconfResolver.ResolveAsync("_ipp._tcp.local.", callback: CallBackMethod, scanTime: mytimeSpan);
		}
		
		private async void CallBackMethod(IZeroconfHost host)
		{
			var printerObj = new DataTypes.PrinterObject(host.IPAddress);
			try
			{
				await printerObj.RefreshValues();
			}
			catch
			{
				return;
			}
		
			foundPrinterList.Dispatcher.Invoke(new Action(() =>
			{
				var cardAction = new WPFUI.Controls.CardAction().Content= new CustomControls.PrinterInfo();
				
				foundPrinterList.Items.Add(cardAction);
			}));		
		}

	}
}
