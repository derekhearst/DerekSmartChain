using System.Windows.Controls;

namespace DerekSmart.Pages
{
	/// <summary>
	/// Interaction logic for AddPrinter.xaml
	/// </summary>
	public partial class Printers : Page
	{
		public Printers()
		{
			InitializeComponent();
		}

		private void CardAction_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NavigationService.Navigate(new AddPrinter());
			
		}
	}
}
