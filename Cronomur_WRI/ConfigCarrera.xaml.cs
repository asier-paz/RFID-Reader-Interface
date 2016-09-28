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
using Microsoft.Win32;

namespace Cronomur_WRI
{
	/// <summary>
	/// Interaction logic for ConfigCarrera.xaml
	/// </summary>
	public partial class ConfigCarrera : Page
	{
		private ReaderHandler reader = ReaderHandler.getInstance();

		public static string _ip = "";
		public static string _port = "";
		public static string _event_name = "";
		public static int _timeout = 5000;
		public static bool _use_runscore = false;

		public ConfigCarrera()
		{
			InitializeComponent();

			reader.OnStartReading += Reader_OnStartReading;
			reader.OnStopReading += Reader_OnStopReading;
		}

		private void Reader_OnStopReading()
		{
			if (_use_runscore)
				RunScoreSocket.disconnect();
		}

		private void Reader_OnStartReading()
		{
			if (_use_runscore)
				RunScoreSocket.StartClient(_ip, int.Parse(_port));
		}

		private void rs_ip_TextChanged(object sender, TextChangedEventArgs e)
		{
			_ip = rs_ip.Text;
		}

		private void rs_port_TextChanged(object sender, TextChangedEventArgs e)
		{
			_port = rs_port.Text;
		}

		private void rs_conn_timeout_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				_timeout = int.Parse(rs_conn_timeout.Text);
			} catch
			{
				Inicio.events.add("El tiempo de espera no es un número válido.");
				rs_conn_timeout.Text = "5000";
			}
		}

		private void rs_event_name_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(rs_event_name.Text))
			{
				_event_name = rs_event_name.Text;
			} else
			{
				Inicio.events.add("El nombre del evento no puede estar vacío.");
			}
		}

		private void use_runscore_Checked(object sender, RoutedEventArgs e)
		{
			_use_runscore = true;
			rs_ip.IsEnabled = true;
			rs_port.IsEnabled = true;
			rs_event_name.IsEnabled = true;
		}

		private void use_runscore_Unchecked(object sender, RoutedEventArgs e)
		{
			_use_runscore = false;
			rs_ip.IsEnabled = false;
			rs_port.IsEnabled = false;
			rs_event_name.IsEnabled = false;
		}
	}
}
