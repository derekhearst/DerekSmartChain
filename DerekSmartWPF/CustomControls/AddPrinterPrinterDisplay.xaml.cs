using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DerekSmartWPF.CustomControls
{
	/// <summary>
	/// Interaction logic for AddPrinterPrinterDisplay.xaml
	/// </summary>
	public partial class AddPrinterPrinterDisplay : UserControl
	{
		public AddPrinterPrinterDisplay()
		{
			InitializeComponent();
		}

		private void Grid_MouseEnter(object sender, MouseEventArgs e)
		{
			PrinterName.Text = "enter";
			RectBackground.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#2084e8");
		}

		private void Grid_MouseLeave(object sender, MouseEventArgs e)
		{
			PrinterName.Text = "exit";
			RectBackground.Fill = new SolidColorBrush(Colors.Transparent);

		}

		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			PrinterName.Text = "click";
		}
	}
}
