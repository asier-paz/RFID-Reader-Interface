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
using System.Threading;

namespace Cronomur_WRI
{
	/// <summary>
	/// Interaction logic for Inicio.xaml
	/// </summary>
	public partial class Inicio : Page
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// This holds the events class controllers
		/// </summary>
		public static Events events = null;
		private System.Threading.Timer _readUpdateTimer = null;
		private static int TIMER_MILLIS = 500;
		public StreamWriter file_stream;
		public string results_file_path = "";
		public static bool sounds = false;
		
		/// <summary>
		/// Instance of the reader handler
		/// </summary>
		private ReaderHandler readerHandler = ReaderHandler.getInstance();

		public Inicio()
		{
			InitializeComponent();

			events = new Events(events_box);
			
			_readUpdateTimer = new System.Threading.Timer(ThreadSave, null, TIMER_MILLIS, Timeout.Infinite);

			readerHandler.OnConnect += ReaderHandler_OnConnect;
			readerHandler.OnDisconnect += ReaderHandler_OnDisconnect;
		}

		/// <summary>
		/// Callback to do readings saving tasks
		/// </summary>
		/// <param name="state"></param>
		private void ThreadSave(Object state) {
			if (readerHandler.isReading()) {
				log.Info("Ejecutando proceso de guardado...");
				SaveTask();

				// Reloading the timer of the task
				_readUpdateTimer.Change(TIMER_MILLIS, Timeout.Infinite);
			} else {
				log.Info("El lector no está leyendo. Se guardarán los datos de la lista por última vez hasta que la lectura vuelva a empezar.");
				SaveTask();
				// Disposing the stream
				if (file_stream != null) {
					file_stream.Dispose();
				}
			}
		}

		private void SaveTask() {
			if (readerHandler.getReceivedMsgBuffer().TagCount > 0)
			{
				List<string> lines = new List<string>();
				log.Info("Longitud de la lista de chips registrados: " + readerHandler.getReceivedMsgBuffer().TagCount);

				for (int i = 0; i < readerHandler.getReceivedMsgBuffer().TagCount; i++)
				{
					RevMsgStruct Rev = readerHandler.getReceivedMsgBuffer().Get(i);
					lines.Add("RSCI," + Rev.sCodeData.ToString() + "," + Rev.tBeginTime.ToString("HH:mm:ss.fff") + "," + ConfigCarrera._event_name);
				}

				// ---- File writing

				// Closing the Stream before creating a new one...
				if (file_stream != null)
				{
					log.Info("Cerrando Stream de archivo de resultados...");
					file_stream.Dispose();
				}

				// Open the file stream
				// (se abre de nuevo el stream para limpiar la escritura anterior y no generar un archivo infinito)
				log.Info("Abriendo nuevo Stream de archivo de resultados...");
				file_stream = new StreamWriter(results_file_path, false);
				file_stream.AutoFlush = true;

				if (file_stream != null)
				{
					log.Info("Escribiendo el archivo de chips...");
					foreach (string line in lines)
						file_stream.WriteLine(line);
				}
				else
				{
					log.Fatal("No se puede escribir en el archivo. (El Stream del archivo es nulo)");
				}

				// Sending data to RunScore if connected
				if (RunScoreSocket.IsConnected())
				{
					var l = String.Join("\r\n", lines);
					log.Info("Enviando datos al servidor de RunScore.");
					RunScoreSocket.Send(l);
					log.Debug("Datos enviados a RunScore: \r\n" + l);
				}
			}
			else
			{
				log.Warn("No se ha guardado ningún dato. (Lista de chips registrados vacía)");
			}
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
				if (ConfigCarrera._use_runscore) {
					if (!Util.IsCorrenctIP(ConfigCarrera._ip))
					{
						MessageBox.Show("La dirección IP especificada en la configuración del servidor de RunScore no es una dirección IP válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					else if (!Util.IsCorrectPortNumber(ConfigCarrera._port))
					{
						MessageBox.Show("El puerto especificado en la configuración del servidor de RunScore no es un número de puerto válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					else if (String.IsNullOrWhiteSpace(ConfigCarrera._event_name))
					{
						MessageBox.Show("El nombre del evento de RunScore no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					else
					{
						if (readerHandler.connect())
							connect_btn.Content = "Desconectar";
					}
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
					read_btn.Content = "Empezar lectura";
				}
			} else
			{
				// Create the txt file
				var dir = Directory.GetCurrentDirectory() + @"\results";  // folder location
				results_file_path = System.IO.Path.Combine(dir, "results_" + ConfigCarrera._event_name + "_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt");

				if (!Directory.Exists(dir)) {
					Directory.CreateDirectory(dir);
				}

				// Closing the Stream before creating a new one...
				if (file_stream != null)
				{
					log.Info("Cerrando Stream de archivo de resultados...");
					file_stream.Dispose();
				}

				// Start reading
				readerHandler.startReading();

				if (readerHandler.isReading())
				{
					read_btn.Content = "Terminar lectura";
				}

				// Reloading the timer of the saving task
				_readUpdateTimer.Change(TIMER_MILLIS, Timeout.Infinite);
			}
		}

		private void config_dump_btn_Click(object sender, RoutedEventArgs e) {
			Inicio.events.add("Esta es la configuración actual:\n"+
				"\t[General]\n" +
				"\t\tSonidos activados: " + Inicio.sounds + "\n" +
				"\t[Lector]\n" +
				"\t\tIP del lector: " + readerHandler.getIpAddress() + "\n" +
				"\t\tPuerto: " + readerHandler.getPort() + "\n" +
				"\t\tTiempo de espera entre lecturas: " + readerHandler.getReaderTimeout() + " millis.\n" +
				"\t[RunScore]\n" +
				"\t\tUsar RunScore: " + ConfigCarrera._use_runscore + "\n" +
				"\t\tDirección IP: " + ConfigCarrera._ip + "\n" +
				"\t\tPuerto: " + ConfigCarrera._port + "\n" +
				"\t\tNombre carrera: " + ConfigCarrera._event_name + "\n" +
				"\t\tTiempo máximo para establecer conexión: " + ConfigCarrera._timeout + " millis.\n" +
				"\t[Conf. Lectura]\n" + 
				"\t\tUsar sistema de vueltas: " + ReadConfig.use_laps + "\n" +
				"\t\tTiempo entre vueltas (en segundos): " + ReadConfig.laps_time_between);
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
