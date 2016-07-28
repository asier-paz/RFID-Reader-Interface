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
	/// Interaction logic for Configuracion.xaml
	/// </summary>
	public partial class Configuracion : Page
	{
		private ReaderHandler readerHandler = ReaderHandler.getInstance();

		public Configuracion()
		{
			InitializeComponent();

			readerHandler.OnConnect += ReaderHandler_OnConnect;
			readerHandler.OnDisconnect += ReaderHandler_OnDisconnect;
		}

		private void ReaderHandler_OnDisconnect(ReaderHandler obj)
		{
			reader_name.Text = "";
			reader_serial.Text = "";
			reader_type.Text = "";
			reader_software_version.Text = "";
			reader_hardware_version.Text = "";
		}

		private void ReaderHandler_OnConnect(ReaderHandler obj)
		{
			reader_name.Text = readerHandler.getName();
			reader_serial.Text = readerHandler.getSerial();
			reader_type.Text = readerHandler.getType();
			reader_software_version.Text = readerHandler.getProcessVersion();
			reader_hardware_version.Text = readerHandler.getHardwareVersion();
			reader_attenuation.SelectedIndex = readerHandler.getAntennaPower();
		}

		private void reader_save_atten_btn_Click(object sender, RoutedEventArgs e)
		{
			var tag = ((ComboBoxItem)reader_attenuation.SelectedItem).Tag;
			byte power;
			try
			{
				power = byte.Parse(tag.ToString());
				readerHandler.setAntennaPower(power);
			} catch
			{
				Inicio.events.add("No se pudo hacer la conversión de tipos entre la potencia de la antena y el tipo de dato byte.");
			}
		}

		private void reader_save_name_btn_Click(object sender, RoutedEventArgs e)
		{
			readerHandler.setName(reader_name.Text);
		}

		private void atten_table_btn_Click(object sender, RoutedEventArgs e)
		{
			var rangesWnd = new AttenuationTableWindow();
			rangesWnd.Show();
		}

		private void reader_read_timeout_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				readerHandler.setReadTimeout(int.Parse(reader_read_timeout.Text));
			} catch
			{
				reader_read_timeout.Text = readerHandler.getReaderTimeout().ToString();
				Inicio.events.add("El tiempo de espera de reconexión debe ser un valor numérico válido.");
			}
		}
	}
}
