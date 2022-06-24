using System.Windows;

namespace DerekSmart
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			RootNavigation.Navigate("settings");

		}
	}
}
