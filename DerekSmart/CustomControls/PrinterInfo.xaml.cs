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

namespace DerekSmart.CustomControls
{
	/// <summary>
	/// Interaction logic for PrinterInfo.xaml
	/// </summary>
	public partial class PrinterInfo : UserControl
	{

		public DataTypes.PrinterObject PrinterObject { get; set; }
		public PrinterInfo()
		{
			InitializeComponent();
		}

		public void AttachPrinterObject(DataTypes.PrinterObject obj)
		{
			PrinterObject = obj;

			PrinterName.Text = obj.IPPName;
			PrinterIP.Text = obj.IPAddress;
			PrinterType.Text = obj.IPPUUID;
			Console.WriteLine("yes");
		}

	}
}
