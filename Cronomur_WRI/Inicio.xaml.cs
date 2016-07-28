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
using System.Timers;
using System.IO;
using System.Reflection;

namespace Cronomur_WRI
{
	/// <summary>
	/// Interaction logic for Inicio.xaml
	/// </summary>
	public partial class Inicio : Page
	{
		/// <summary>
		/// This holds the events class controllers
		/// </summary>
		public static Events events = null;
		private Timer _readUpdateTimer = null;
		public string results_file = "";
		public static bool sounds = false;

		/// <summary>
		/// Instance of the reader handler
		/// </summary>
		private ReaderHandler readerHandler = ReaderHandler.getInstance();

		public Inicio()
		{
			InitializeComponent();

			events = new Events(events_box);

			_readUpdateTimer = new Timer(500);
			_readUpdateTimer.Elapsed += _readUpdateTimer_Elapsed;

			readerHandler.OnConnect += ReaderHandler_OnConnect;
			readerHandler.OnDisconnect += ReaderHandler_OnDisconnect;
		}

		private void ReaderHandler_OnDisconnect(ReaderHandler obj)
		{
			read_btn.IsEnabled = false;
			read_btn.Content = "Empezar lectura";
		}

		private void ReaderHandler_OnConnect(ReaderHandler obj)
		{
			read_btn.IsEnabled = true;
		}

		/**
		 * UI Events
		 */
		private void connect_btn_Click(object sender, RoutedEventArgs e)
		{
			if (readerHandler.isConnected())
			{
				if (readerHandler.disconnect())
					connect_btn.Content = "Conectar";
			}
			else
			{
				if (!Util.IsCorrenctIP(ConfigCarrera._ip))
				{
					MessageBox.Show("La dirección IP especificada en la configuración del servidor de RunScore no es una dirección IP válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				} else if (!Util.IsCorrectPortNumber(ConfigCarrera._port))
				{
					MessageBox.Show("El puerto especificado en la configuración del servidor de RunScore no es un número de puerto válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				} else if (String.IsNullOrWhiteSpace(ConfigCarrera._event_name))
				{
					MessageBox.Show("El nombre del evento de RunScore no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				} else {
					if (readerHandler.connect())
						connect_btn.Content = "Desconectar";
				}
			}
		}

		private void conn_ip_TextChanged(object sender, TextChangedEventArgs e)
		{
			string _ip = conn_ip.Text;
			if (!Util.IsCorrenctIP(_ip)) {
				events.add("La dirección IP '" + _ip + "' no es una dirección IP correcta.");
				conn_ip.Text = readerHandler.getIpAddress();
			}

			readerHandler.setIpAddress(conn_ip.Text);
		}

		private void conn_ip_Loaded(object sender, RoutedEventArgs e)
		{
			conn_ip.Text = readerHandler.getIpAddress();
		}

		private void conn_port_Loaded(object sender, RoutedEventArgs e)
		{
			conn_port.Text = readerHandler.getPort().ToString();
		}

		private void conn_port_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Util.IsCorrectPortNumber(conn_port.Text))
			{
				readerHandler.setPort(int.Parse(conn_port.Text));
			} else
			{
				events.add("El puerto '" + conn_port.Text + "' no es un puerto correcto.");
				conn_port.Text = readerHandler.getPort().ToString();
			}
		}

		private void read_btn_Click(object sender, RoutedEventArgs e)
		{
			if (readerHandler.isReading())
			{
				// Stop reading
				readerHandler.stopReading();

				if (!readerHandler.isReading())
				{
					_readUpdateTimer.Enabled = false;

					read_btn.Content = "Empezar lectura";
				}
			} else
			{
				// Create the txt file
				var dir = Directory.GetCurrentDirectory() + @"\results";  // folder location
				results_file = System.IO.Path.Combine(dir, "results_" + ConfigCarrera._event_name + "_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt");

				if (!Directory.Exists(dir))  // if it doesn't exist, create
					Directory.CreateDirectory(dir);

				// Create the file
				File.Create(results_file);

				// Start reading
				readerHandler.startReading();

				if (readerHandler.isReading())
				{
					_readUpdateTimer.Enabled = true;

					read_btn.Content = "Terminar lectura";
				}
			}
		}

		private void _readUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (RunScoreSocket.IsConnected())
			{
				string lines = "";

				for (int i = 0; i < readerHandler.getReceivedMsgBuffer().TagCount; i++)
				{
					RevMsgStruct Rev = readerHandler.getReceivedMsgBuffer().Get(i);
					
					// Write to the file first
					using (System.IO.StreamWriter file = new System.IO.StreamWriter(results_file, true))
					{
						file.WriteLine("RSCI," + Rev.sCodeData.ToString() + "," + Rev.tLastTime.ToString("HH:mm:ss.fff") + "," + ConfigCarrera._event_name + "\r\n");
					}

					if (i == readerHandler.getReceivedMsgBuffer().TagCount)
					{
						lines += "RSCI," + Rev.sCodeData.ToString() + "," + Rev.tLastTime.ToString("HH:mm:ss.fff") + "," + ConfigCarrera._event_name + "\r\n<EOF>";
					} else
					{
						lines += "RSCI," + Rev.sCodeData.ToString() + "," + Rev.tLastTime.ToString("HH:mm:ss.fff") + "," + ConfigCarrera._event_name + "\r\n";
					}
				}

				if (!String.IsNullOrWhiteSpace(lines))
				{
					RunScoreSocket.Send(lines);
				}
			}
		}

		private void sound_Checked(object sender, RoutedEventArgs e)
		{
			Inicio.sounds = true;
		}

		private void sound_Unchecked(object sender, RoutedEventArgs e)
		{
			Inicio.sounds = false;
		}
	}
}
