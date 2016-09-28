using NDesk.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Cronomur_WRI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

	}

	public partial class AppEntryPoint {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Application");

		/// <summary>
		/// Application Entry Point.
		/// </summary>
		[System.STAThreadAttribute()]
		[System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
		public static void Main(string[] args)
		{
			// Parsing args
			bool show_help = false;
			int test = 0;

			var p = new OptionSet() {
				{ "t1|test1", "Reading test 1",
					v => test = 1 },
				{ "t2|test2", "Reading test 1",
					v => test = 2 },
				{ "h|help",  "show this message and exit",
					v => show_help = v != null },
			};

			List<string> extra;
			try
			{
				extra = p.Parse(args);
			}
			catch (OptionException e)
			{
				log.Info("greet: ");
				log.Info(e.Message);
				log.Info("Try `greet --help' for more information.");
				return;
			}

			if (show_help) {
				log.Info("This should be the help.");
			} else {
				log.Info("Abriendo aplicación con 'Test Mode " + test + "'");

				// Starting GUI
				Cronomur_WRI.App app = new Cronomur_WRI.App();
				app.InitializeComponent();
				app.Run();

				MainWindow._readMode = test;
			}
		}
	}
}
