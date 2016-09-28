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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Reader Hanlder instance
		/// </summary>
		private ReaderHandler readerHandler = ReaderHandler.getInstance();
		/// <summary>
		/// List of views (frames)
		/// </summary>
		private List<Frame> views = new List<Frame>();
		/// <summary>
		/// Reading mode (tests)
		/// </summary>
		public static int _readMode = 0;

        public MainWindow()
        {
            InitializeComponent();

			// Adding views to the list
			views.Add(_frameInicio);
			views.Add(_frameConfiguracion);
			views.Add(_frameCarrera);
			views.Add(_frameReadConfig);

			// Registering to the reader handler events
			readerHandler.OnConnect += ReaderHandler_OnConnect;
			readerHandler.OnDisconnect += ReaderHandler_OnDisconnect;
        }

		/**
		 * UI Functions
		 */
		private void showView(int index)
		{
			for (int i = 0; i < views.Count; i++)
			{
				if (i == index)
				{
					views[i].Visibility = Visibility.Visible;
				} else
				{
					views[i].Visibility = Visibility.Hidden;
				}
			}
		}

		public void setConnectionStatus(bool connected)
		{
			if (connected)
			{
				btnConnectionStatus.Content = "Conectado";
				btnConnectionStatus.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebb23"));
				btnConnectionStatus.FontWeight = FontWeights.Normal;
			} else
			{
				btnConnectionStatus.Content = "Desconectado";
				btnConnectionStatus.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
				btnConnectionStatus.FontWeight = FontWeights.Light;
			}
		}

		/**
		 * UI Events
		 */
		private void ToolBar_Loaded(object sender, RoutedEventArgs e)
		{
			ToolBar toolBar = sender as ToolBar;
			var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
			if (overflowGrid != null)
			{
				overflowGrid.Visibility = Visibility.Collapsed;
			}
			var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
			if (mainPanelBorder != null)
			{
				mainPanelBorder.Margin = new Thickness();
			}
		}
		private void btnInicio_Click(object sender, RoutedEventArgs e)
		{
			showView(0);
		}

		private void btnConfiguración_Click(object sender, RoutedEventArgs e)
		{
			showView(1);
		}

		private void btnCarrera_Click(object sender, RoutedEventArgs e)
		{
			showView(2);
		}

		private void btnReadConfig_Click(object sender, RoutedEventArgs e) {
			showView(3);
		}

		/**
		 * Events
		 */
		private void ReaderHandler_OnConnect(ReaderHandler obj)
		{
			setConnectionStatus(true);
		}
		private void ReaderHandler_OnDisconnect(ReaderHandler obj)
		{
			setConnectionStatus(false);
		}

		private void main_wnd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			RunScoreSocket.disconnect();
			readerHandler.disconnect();
		}
	}
}
