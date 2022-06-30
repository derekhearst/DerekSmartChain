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
namespace DerekSmart.Pages
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

		private void BackButtonClick(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new Printers());			
		}

		private async void StackPanel_Loaded(object sender, RoutedEventArgs e)
		{
			await SearchForPrinters();

		}

		private async Task SearchForPrinters()
		{
			progressBarElement.Visibility = Visibility.Visible;
			refreshButton.Visibility = Visibility.Collapsed;
			foundPrinterList.Items.Clear();
			
			var mytimeSpan = new TimeSpan(0, 0, 10);
			await ZeroconfResolver.ResolveAsync("_ipp._tcp.local.", callback: CallBackMethod, scanTime: mytimeSpan);

			progressBarElement.Visibility = Visibility.Collapsed;
			refreshButton.Visibility = Visibility.Visible;

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
				var printerInfo = new CustomControls.PrinterInfo();
				var cardAction = new WPFUI.Controls.CardAction();
				cardAction.Content = printerInfo;
				
				foundPrinterList.Items.Add(cardAction);
			}));		
		}

		private void refreshButton_Click(object sender, RoutedEventArgs e)
		{
			SearchForPrinters();
		}
	}
}
