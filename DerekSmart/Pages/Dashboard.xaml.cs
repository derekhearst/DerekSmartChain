using System.Windows;
using System.Windows.Controls;
using WPFUI.Controls;
namespace DerekSmart.Pages
{
	/// <summary>
	/// Interaction logic for Dashboard.xaml
	/// </summary>
	public partial class Dashboard : Page
	{
		public Dashboard()
		{
			InitializeComponent();
		}

		

		private void PrintButton(object sender, RoutedEventArgs e)
		{
			
		}

		private void ScanButton(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new Scanning());
		}

		private void HelpButton(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
