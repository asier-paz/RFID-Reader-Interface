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

namespace Cronomur_WRI
{
	/// <summary>
	/// Interaction logic for ReadConfig.xaml
	/// </summary>
	public partial class ReadConfig : Page
	{
		public static Boolean use_laps = false;
		public static int laps_time_between = 300;

		private ReaderHandler readerHandler;

		public ReadConfig()
		{
			InitializeComponent();

			readerHandler = ReaderHandler.getInstance();
			readerHandler.OnStartReading += ReaderHandler_OnStartReading;
			readerHandler.OnStopReading += ReaderHandler_OnStopReading;
		}

		private void ReaderHandler_OnStopReading()
		{
			check_use_laps.IsEnabled = true;
		}

		private void ReaderHandler_OnStartReading()
		{
			check_use_laps.IsEnabled = false;
		}

		private void laps_time_between_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				laps_time_between = int.Parse(text_laps_time_between.Text);
			}
			catch
			{
				laps_time_between = 300;
				text_laps_time_between.Text = "300";
			}
		}

		private void use_laps_Checked(object sender, RoutedEventArgs e)
		{
			use_laps = true;
		}

		private void use_laps_Unchecked(object sender, RoutedEventArgs e)
		{
			use_laps = false;
		}
	}
}
