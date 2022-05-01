using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpIpp.Model;
using System.Text.Json;
using Windows.Storage;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;


namespace DerekSmart.DataTypes
{
	class PrinterObject
	{
		public struct SupplyInfo
		{
			public SupplyInfo(string supplyname, int percent)
			{
				this.supplyname = supplyname;
				this.percent = percent;
			}
			public string supplyname { get; set; }
			public int percent { get; set; }
		}

		//Network Info
		public string IPAddress { get; set; } = "N\\A";
		public bool CloudEnabled { get; set; } = false;
		public string ImageName { get; set; } = "N\\A";


		//General Info
		public string IPPName { get; set; } = "N\\A";
		public string IPPPNameInfo { get; set; } = "N\\A";
		public string IPPFirmwareInstalled { get; set; } = "N\\A";
		public string IPPPPM { get; set; } = "N\\A";
		public string IPPColorSupported { get; set; } = "N\\A";
		public string IPPPrinterState { get; set; } = "N\\A";
		public string IPPPrinterStateMessage { get; set; } = "N\\A";
		public List<SupplyInfo> IPPSupplyValues { get; set; } = new();
		public string IPPUUID { get; set; } = "N\\A";
		public string IPPLocation { get; set; } = "N\\A";


		//SupportedJobAttributes
		public List<string> IPPSupportedMedia { get; set; } = new();
		public List<string> IPPSupportedMediaSource { get; set; } = new();
		public List<string> IPPSupportedMediaType { get; set; } = new();
		public List<string> IPPSuppportedOutputBin { get; set; } = new();
		public List<string> IPPSupportedCollate { get; set; } = new();
		public List<Finishings> IPPSupportedFinishings { get; set; } = new();
		public List<string> IPPSupportedSides { get; set; } = new();

		public async Task RefreshValues()
		{
						
			GetPrinterAttributesRequest req = new()
			{
				PrinterUri = new($"ipp://{IPAddress}:631"),
			};
			SharpIpp.SharpIppClient ippCli = new();
			GetPrinterAttributesResponse response = null;
			try
			{
				await ippCli.GetPrinterAttributesAsync(req);
			}
            catch
            {
				Console.WriteLine("Unable to communicate with " + IPAddress);
				return;
            }

			if (response is null) { return; }
			IPPSupportedMedia.Clear();
			IPPSupportedMediaSource.Clear();
			IPPSupportedMediaType.Clear();
			IPPSuppportedOutputBin.Clear();
			IPPSupportedCollate.Clear();
			IPPSupportedFinishings.Clear();
			IPPSupportedSides.Clear();
			IPPSupplyValues.Clear();

			foreach (IppAttribute at in response.Sections[1].Attributes)
			{
				switch (at.Name)
				{
					case "media-supported":
						IPPSupportedMedia.Add(at.Value.ToString());
						break;
					case "media-source-supported":
						IPPSupportedMediaSource.Add(at.Value.ToString());
						break;
					case "media-type-supported":
						IPPSupportedMediaType.Add(at.Value.ToString());
						break;
					case "output-bin-supported":
						IPPSuppportedOutputBin.Add(at.Value.ToString());
						break;
					case "sides-supported":
						IPPSupportedSides.Add(at.Value.ToString());
						break;
					case "finishings-supported":
						IPPSupportedFinishings.Add((Finishings)at.Value);
						break;
					case "multiple-document-handling-supported":
						IPPSupportedCollate.Add(at.Value.ToString());
						break;



					case "printer-name":
						IPPName = at.Value.ToString();
						break;
					case "printer-make-and-model":
						IPPPNameInfo = at.Value.ToString();
						break;
					case "printer-firmware-version":
						IPPFirmwareInstalled = at.Value.ToString();
						break;
					case "pages-per-minute":
						IPPPPM = at.Value.ToString();
						break;
					case "color-supported":
						IPPColorSupported = at.Value.ToString();
						break;
					case "printer-uuid":
						IPPUUID = at.Value.ToString();
						break;
					case "printer-location":
						IPPLocation = at.Value.ToString();
						break;

					case "printer-state":
						IPPPrinterState = ((PrinterState)at.Value).ToString();
						break;
					case "printer-state-reasons":
						IPPPrinterStateMessage = at.Value.ToString();
						break;
					case "printer-supply":
						SupplyInfo info = new();
						foreach (string temp in at.Value.ToString().Split(";").ToList())
						{
							try
							{
								if (temp.Contains("level")) { info.percent = int.Parse(temp.Split("=")[1]); }
								else if (temp.Contains("colorantname")) { info.supplyname = temp.Split("=")[1]; } //the[1] gets the value after the =
							}
							catch
							{

							}
						}
						IPPSupplyValues.Add(info);
						break;

					default:
						break;
				}

			}
			return;
		}
		//Seralizing and deserializing
		public static string GetJson(PrinterObject printerToSave)
		{
			return JsonSerializer.Serialize(printerToSave);
		}

		public async static Task<PrinterObject> ReadFromFile(StorageFile file)
		{
			var json = await FileIO.ReadTextAsync(file);
			return JsonSerializer.Deserialize<PrinterObject>(json);
		}
	}
}
