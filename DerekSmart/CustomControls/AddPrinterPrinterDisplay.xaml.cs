using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DerekSmart.CustomControls
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AddPrinterPrinterDisplay : Page
	{

		public AddPrinterPrinterDisplay(string printerIP, string pinterLocation, string printerID)
		{
			this.InitializeComponent();
			PrinterIP.Text = printerIP;
			PrinterLocation.Text = pinterLocation;
			PrinterName.Text = printerID;
		}

		public void changeImage(ImageSource source)
		{
			PrinterImage.Source = source;
		}

		private void Grid_PointerEntered(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
		{
			PrinterName.Text = "entered!";
		}

		private void Grid_PointerExited(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
		{
			PrinterName.Text = "left!";
		}

		private void Grid_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
		{
			PrinterName.Text = "exited!";
		}
	}
}
