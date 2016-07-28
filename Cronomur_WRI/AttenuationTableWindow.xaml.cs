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
using System.Windows.Shapes;

namespace Cronomur_WRI
{
	/// <summary>
	/// Interaction logic for AttenuationTableWindow.xaml
	/// </summary>
	public partial class AttenuationTableWindow : Window
	{
		List<AttenuationRange> ranges = new List<AttenuationRange>();

		public AttenuationTableWindow()
		{
			InitializeComponent();

			ranges.Add(new AttenuationRange() { Level = "0 dB", Range = "0 ~ 250m" });
			ranges.Add(new AttenuationRange() { Level = "-2 dB", Range = "0 ~ 200m" });
			ranges.Add(new AttenuationRange() { Level = "-4 dB", Range = "0 ~ 160m" });
			ranges.Add(new AttenuationRange() { Level = "-6 dB", Range = "0 ~ 120m" });
			ranges.Add(new AttenuationRange() { Level = "-8 dB", Range = "0 ~ 100m" });
			ranges.Add(new AttenuationRange() { Level = "-10 dB", Range = "0 ~ 80m" });
			ranges.Add(new AttenuationRange() { Level = "-12 dB", Range = "0 ~ 60m" });
			ranges.Add(new AttenuationRange() { Level = "-14 dB", Range = "0 ~ 50m" });
			ranges.Add(new AttenuationRange() { Level = "-16 dB", Range = "0 ~ 40m" });
			ranges.Add(new AttenuationRange() { Level = "-18 dB", Range = "0 ~ 30m" });
			ranges.Add(new AttenuationRange() { Level = "-20 dB", Range = "0 ~ 25m" });
			ranges.Add(new AttenuationRange() { Level = "-22 dB", Range = "0 ~ 20m" });
			ranges.Add(new AttenuationRange() { Level = "-24 dB", Range = "0 ~ 15m" });
			ranges.Add(new AttenuationRange() { Level = "-26 dB", Range = "0 ~ 12m" });
			ranges.Add(new AttenuationRange() { Level = "-28 dB", Range = "0 ~ 9m" });
			ranges.Add(new AttenuationRange() { Level = "-30 dB", Range = "0 ~ 7m" });

			listView.ItemsSource = ranges;
		}

		public class AttenuationRange
		{
			public string Level { get; set; }
			public string Range { get; set; }
		}
	}
}
