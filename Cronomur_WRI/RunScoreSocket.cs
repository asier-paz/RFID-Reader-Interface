using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Cronomur_WRI
{
	class RunScoreSocket
	{
		// State object for receiving data from remote device.
		public class StateObject
		{
			// Client socket.
			public Socket workSocket = null;
			// Size of receive buffer.
			public const int BufferSize = 1024;
			// Receive buffer.
			public byte[] buffer = new byte[BufferSize];
			// Received data string.
			public StringBuilder sb = new StringBuilder();
		}

		// Socket Thread
		private static Thread _thread = null;
		// Socket
		private static Socket client = null;
		// The remote device address.
		private static string ip;
		// The port number for the remote device.
		private static int port;

		// ManualResetEvent instances signal completion.
		private static ManualResetEvent connectDone =
			new ManualResetEvent(false);
		public static ManualResetEvent sendDone =
			new ManualResetEvent(false);
		private static ManualResetEvent receiveDone =
			new ManualResetEvent(false);

		// The response from the remote device.
		private static String response = String.Empty;

		// Add messages to the event window
		delegate void AddTextEventCallback(string text);



		public static void StartClient(string ip, int port)
		{
			RunScoreSocket.ip = ip;
			RunScoreSocket.port = port;

			if (_thread != null)
			{
				if (_thread.IsAlive)
				{
					_thread.Abort();
					_thread.Join();
				}
			}

			_thread = new Thread(new ThreadStart(Connect));
			_thread.Start();
		}

		public static void disconnect()
		{
			if (_thread != null)
			{
				if (_thread.IsAlive)
				{
					_thread.Abort();
					_thread.Join();
				}
			}

			if (client != null)
			{
				// Release the socket.
				try
				{
					client.Shutdown(SocketShutdown.Both);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.ToString());
				}

				client.Close();
				client = null;
			}

			Console.WriteLine("RunScoreSocket: Disconnected.");
		}

		public static bool IsConnected() { return client != null; }

		private static void Connect()
		{
			// Connect to a remote device.
			try
			{
				IPAddress ipAddress = IPAddress.Parse(ip);
				IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

				// Create a TCP/IP socket.
				client = new Socket(AddressFamily.InterNetwork,
					SocketType.Stream, ProtocolType.Tcp);

				// Connect to the remote endpoint.
				client.BeginConnect(remoteEP,
					new AsyncCallback(ConnectCallback), client);

				// We wait til we are connected or the timeout
				if (connectDone.WaitOne(ConfigCarrera._timeout))
				{
					AddSafeTextEvent("Conectado al servidor de RunScore.");
				} else
				{
					MessageBox.Show("No se ha podido conectar al servidor de RunScore. Asegúrate de que la información proporcionada es correcta. Asegúrate también de que RunScore está escuchando peticiones en el puerto especificado, que tiene la casilla 'TCP' marcada, y que tiene como dispositivo 'RunScore Open'.\n\nPara intentar reconectar con RunScore debes volver a empezar la lectura.", "Excepción de tiempo de espera", MessageBoxButton.OK, MessageBoxImage.Error);
				}

				/*
				// Send test data to the remote device.
				Send("RSCI,CF1236F,0:19:23.5,META\r\n<EOF>");
				sendDone.WaitOne();

				// Receive the response from the remote device.
				//Receive(client);
				//receiveDone.WaitOne();
				*/
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		private static void ConnectCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.
				Socket client = (Socket)ar.AsyncState;

				// Complete the connection.
				client.EndConnect(ar);

				Console.WriteLine("Socket connected to {0}",
					client.RemoteEndPoint.ToString());

				// Signal that the connection has been made.
				connectDone.Set();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		private static void Receive(Socket client)
		{
			try
			{
				// Create the state object.
				StateObject state = new StateObject();
				state.workSocket = client;

				// Begin receiving the data from the remote device.
				client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
					new AsyncCallback(ReceiveCallback), state);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		private static void ReceiveCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the state object and the client socket 
				// from the asynchronous state object.
				StateObject state = (StateObject)ar.AsyncState;
				Socket client = state.workSocket;

				// Read data from the remote device.
				int bytesRead = client.EndReceive(ar);

				if (bytesRead > 0)
				{
					// There might be more data, so store the data received so far.
					state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));

					// Get the rest of the data.
					client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
						new AsyncCallback(ReceiveCallback), state);
				}
				else
				{
					// All the data has arrived; put it in response.
					if (state.sb.Length > 1)
					{
						response = state.sb.ToString();
					}
					// Signal that all bytes have been received.
					receiveDone.Set();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public static void Send(String data)
		{
			// Convert the string data to byte data using ASCII encoding.
			byte[] byteData = Encoding.UTF8.GetBytes(data + "<EOF>");

			// Begin sending the data to the remote device.
			client.BeginSend(byteData, 0, byteData.Length, 0,
				new AsyncCallback(SendCallback), client);
		}

		private static void SendCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.
				Socket client = (Socket)ar.AsyncState;

				// Complete sending the data to the remote device.
				int bytesSent = client.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to server.", bytesSent);

				// Signal that all bytes have been sent.
				sendDone.Set();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		private static void AddSafeTextEvent(string text)
		{
			Inicio.events.getListBox().Dispatcher.Invoke(() => Inicio.events.add(text));
		}
	}
}
