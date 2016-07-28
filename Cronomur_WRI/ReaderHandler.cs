using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Szaat.RFID.CSharpAPI;
using System.Runtime.InteropServices;
using System.Threading;

namespace Cronomur_WRI
{
	public class ReaderHandler
	{
		/// <summary>
		/// Singleton instance
		/// </summary>
		private static ReaderHandler _instance = null;

		/// <summary>
		/// Thread to read tags
		/// </summary>
		private Thread _readThread = null;

		/// <summary>
		/// connect state
		/// </summary>
		private bool bConnState = false;
		/// <summary>
		/// Read start
		/// </summary>
		private bool bReadCodeState = false;
		/// <summary>
		/// Reader received msg buffer
		/// </summary>
		private RevMsgBuffer ReceivedMsgBuffer = new RevMsgBuffer();
		/// <summary>
		/// Reader default IP
		/// </summary>
		private string _readerIP = "192.168.0.238";
		/// <summary>
		/// Reader default port
		/// </summary>
		private int _readerPort = 7086;
		/// <summary>
		/// Delay between read requests.
		/// </summary>
		private int _readerReadTimeout = 100;
		/// <summary>
		/// Reader info
		/// </summary>
		private string _readerName;
		private string _readerType;
		private string _readerSerial;
		private string _readerProcessVersion;
		private string _readerHardwareVersion;
		/// <summary>
		/// Antenna Attenuation Power
		/// </summary>
		private byte _readerAntennaAttenuation;
		/// <summary>
		/// System info
		/// </summary>
		private string[] sSysInfoBuf = new string[7];
		private byte[] sSysInfoLen = new byte[] { 8, 6, 8, 4, 4, 4, 4 };

		/// <summary>
		/// Safe event system delegate
		/// </summary>
		/// <param name="text"></param>
		delegate void AddTextEventCallback(string text);

		/// <summary>
		/// Events
		/// </summary>
		public event Action<ReaderHandler> OnConnect;
		public event Action<ReaderHandler> OnDisconnect;
		public event Action OnStartReading;
		public event Action OnStopReading;

		private ReaderHandler()
		{
			
		}

		#region Getters
		public string getIpAddress() { return _readerIP; }
		public int getPort() { return _readerPort; }
		public string getName() { return _readerName; }
		public string getType() { return _readerType; }
		public string getSerial() { return _readerSerial; }
		public string getProcessVersion() { return _readerProcessVersion; }
		public string getHardwareVersion() { return _readerHardwareVersion; }
		public byte getAntennaPower() { return _readerAntennaAttenuation; }
		public Thread getReadThread() { return _readThread; }
		public RevMsgBuffer getReceivedMsgBuffer() { return ReceivedMsgBuffer; }
		public int getReaderTimeout() { return _readerReadTimeout; }
		#endregion

		#region Setters
		public void setIpAddress(string ip)
		{
			_readerIP = ip;
		}
		public void setPort(int port)
		{
			_readerPort = port;
		}
		public void setName(string name)
		{
			if (string.IsNullOrEmpty(name.Trim()))
			{
				AddSafeTextEvent("Hubo un error al intentar cambiar el nombre del dispositivo. El nombre no puede ser nulo.");
				return;
			}

			_readerName = name;
			CSysSet(0x0, name);
		}
		public void setAntennaPower(byte power)
		{
			_readerAntennaAttenuation = power;
			CAntennaPowerSet(power);
		}
		public void setLangId(byte id)
		{
			if (RfidApi.SAAT_SetLangId(id))
			{
				AddSafeTextEvent("Idioma de interacción con el dispositivo definido correctamente. (" + id + ")");
			}
			else
			{
				AddSafeTextEvent("Error: No se pudo cambiar el idioma de interacción con el dispositivo. Es posible que algunos errores no sean legibles.");
			}
		}
		public void setReadTimeout(int timeout)
		{
			_readerReadTimeout = timeout;
		}
		#endregion

		#region Booleans
		public bool isConnected() { return bConnState; }
		public bool isReading() { return bReadCodeState; }
		#endregion

		/// <summary>
		/// Try to connect to the device
		/// </summary>
		/// <returns></returns>
		public bool connect()
		{
			bool bInit = RfidApi.SAAT_TCPInit(_readerIP, _readerPort);
			if (!bInit)
			{
				AddSafeTextEvent("Error intentando conectar al dispositivo. Comprueba que la dirección IP y el puerto son correctos.");
				return false;
			}

			bConnState = RfidApi.SAAT_Open();
			if (!bConnState)
			{
				AddSafeTextEvent("Error intentando abrir una conexión con el dispositivo. Sin embargo la interfaz TCP ha sido inicializada.");
				return false;
			}

			// Setting lang to EN
			setLangId(2);

			// Loading system information
			loadSystemInfo();

			// Loading antenna power
			loadAntennaAttenuation();

			// Firing the OnConnect event
			OnConnect(this);

			AddSafeTextEvent("Se ha conectado al dispositivo correctamente.");

			return true;
		}

		public bool disconnect()
		{
			if (RfidApi.pHandle == IntPtr.Zero)
			{
				return true;
			}

			if (_readThread != null)
			{
				if (_readThread.IsAlive)
				{
					_readThread.Abort();
					_readThread.Join();
				}
			}

			bool bRet = RfidApi.SAAT_Close();
			if (bRet)
			{
				RfidApi.pHandle = IntPtr.Zero;
				AddSafeTextEvent("Se ha desconectado del dispositivo correctamente.");
				bConnState = false;
				OnDisconnect(this);
				Console.WriteLine("ReaderHandler: Disconnected.");
			}

			return bRet;
		}

		public void startReading()
		{
			if (_readThread != null)
			{
				if (_readThread.IsAlive)
				{
					_readThread.Abort();
					_readThread.Join();
				}
			}

			if (!RfidApi.SAAT_YMakeTagUpLoadIDCode(0x01, 0x01))
			{
				AddSafeTextEvent("Error: No se pudo empezar a leer.");
				return;
			}

			bReadCodeState = true;
			ReceivedMsgBuffer.ClearMsg();

			AddSafeTextEvent("La lectura ha comenzado.");

			_readThread = new Thread(new ThreadStart(ReceiveCodeMsgThread));
			_readThread.Start();

			// Firing the event
			OnStartReading();
		}

		public void stopReading()
		{
			if (_readThread.IsAlive)
			{
				_readThread.Abort();
				_readThread.Join();
			}

			Console.WriteLine("Trying to stop reading...");

			if (RfidApi.SAAT_YPowerOff())
			{
				Console.WriteLine("PowerOff TRUE");
				AddSafeTextEvent("La lectura ha terminado.");
			}
			else
			{
				Console.WriteLine("PowerOff FALSE");
				AddSafeTextEvent("Error: " + CGetErrorMessage());
			}

			bReadCodeState = false;

			// Firing the event
			OnStopReading();
		}

		/// <summary>
		/// Block the thread until it starts to read again.
		/// </summary>
		private void Reconnect()
		{
			while (true)
			{
				if (isConnected())
				{
					if (RfidApi.SAAT_YPowerOff())
					{
						if (RfidApi.SAAT_YMakeTagUpLoadIDCode(0x01, 0x01))
						{
							// If the read request was successful we break the loop and continue reading.
							break;
						}
					}
				}
				else
				{
					AddSafeTextEvent("Error: No hay conexión con el dispositivo y se está intentando reconectar la lectura.");
					break;
				}
			}
		}

		private void ReceiveCodeMsgThread()
		{
			int nRevMsgResult = 0;

			RevMsgStruct revMsg = new RevMsgStruct();
			int dwStart = System.Environment.TickCount;
			bool bConnectIsOK = false;
			while (bReadCodeState)
			{
				nRevMsgResult = ReceiveCodeMsg(ref revMsg);
				if (nRevMsgResult == 1)
				{
					bConnectIsOK = true;
					ReceivedMsgBuffer.RevMsgAdd(revMsg);

					if (Inicio.sounds)
						Win32.Beep(Win32.BeepType.Success);
				}
				else if (nRevMsgResult == 2)
				{
					bConnectIsOK = true;
				}

				// Every number of seconds it checks if the connection is OK and try to reconnect if not
				if (System.Environment.TickCount - dwStart > _readerReadTimeout)
				{
					// If after a number of seconds there is no chip signal, we reconnect just in case of some reader error.
					if (!bConnectIsOK)
					{
						//AddSafeTextEvent("Reiniciando el proceso de lectura...");
						Reconnect();
						//AddSafeTextEvent("Proceso reiniciado.");
					}

					bConnectIsOK = false;
					dwStart = System.Environment.TickCount;
				}
			}
		}

		public int ReceiveCodeMsg(ref RevMsgStruct revMsg)
		{
			byte nBit = 0x00;
			uint btData = 0;
			int bResult = RfidApi.SAAT_YRevIDMsgDec(out btData, out nBit);

			if (bResult == 1)
			{
				revMsg.sCodeData = btData.ToString();
				revMsg.nRepeatTime = 1;
				revMsg.tBeginTime = System.DateTime.Now;
				revMsg.tLastTime = System.DateTime.Now;
			}

			return bResult;
		}

		#region AntennaPower
		private void loadAntennaAttenuation()
		{
			byte sCallback = 0;

			if (CAntennaAttenuationQuery(ref sCallback))
			{
				_readerAntennaAttenuation = sCallback;
			} else
			{
				AddSafeTextEvent("No se pudo cargar la información sobre la antena.");
			}
		}

		private bool CAntennaAttenuationQuery(ref byte pPara)
		{
			return RfidApi.SAAT_YAntennaPowerQuery(ref pPara);
		}

		public void CAntennaPowerSet(byte sValue)
		{
			if (sValue >= 0 && sValue <= 15)
			{
				if (RfidApi.SAAT_YAntennaPowerSet(sValue))
				{
					AddSafeTextEvent("Atenuación de la antena del dispositivo cambiada correctamente.");
				}
				else
				{
					AddSafeTextEvent("Error intentando cambiar la atenuación de la antena del dispositivo: " + CGetErrorMessage());
				}
			} else
			{
				AddSafeTextEvent("Error: " + sValue + " no está en el rango de 0 a 15 para la potencia de la antena.");
			}
		}
		#endregion

		#region SystemInformation
		private void loadSystemInfo()
		{
			string sCallback = "";

			for (byte i = 0; i <= 6; i++)
			{
				if (!GetSysInfo(i, ref sCallback, sSysInfoLen[i]))
				{
					AddSafeTextEvent("Error intentando cargar la información del dispositivo.");
					return;
				}
				sSysInfoBuf[i] = sCallback;
			}

            _readerName = sSysInfoBuf[0];
			_readerType = sSysInfoBuf[1];
            _readerSerial = sSysInfoBuf[2];
            _readerProcessVersion = sSysInfoBuf[3];
            _readerHardwareVersion = sSysInfoBuf[5];
		}

		private bool GetSysInfo(byte nType, ref string sCallback, byte iLenght)
		{
			if (CSysQuery(nType, ref sCallback))
			{
				if (sCallback.Length > iLenght)
					sCallback = sCallback.Substring(0, iLenght);
				else
					sCallback = sCallback.ToString();
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool CSysQuery(byte nType, ref string pPara)
		{
			pPara = "";
			IntPtr ptr = Marshal.AllocHGlobal(255);
			byte sLength = 255;
			bool bResult = RfidApi.SAAT_SysInfQuery(nType, ptr, ref sLength);
			if (bResult)
				pPara = Marshal.PtrToStringAnsi(ptr, sLength);
			Marshal.FreeHGlobal(ptr);
			return bResult;
		}

		public void CSysSet(byte nType, string sValue)
		{
			byte[] btValue = Encoding.GetEncoding("utf-8").GetBytes(sValue.Trim());

			if (RfidApi.SAAT_SysInfSet(nType, btValue, (byte)(btValue.Length)))
			{
				AddSafeTextEvent("Propiedad del dispositivo cambiada correctamente.");
			}
			else
			{
				AddSafeTextEvent("Error intentando cambiar una propiedad del dispositivo: " + CGetErrorMessage());
			}
		}
		#endregion

		public string CGetErrorMessage()
		{
			byte[] btErrorMsg = new byte[255];
			string sErrorMsg = "";
			if (RfidApi.pHandle == IntPtr.Zero)
			{
				sErrorMsg = "Error!";
			}
			else
			{
				RfidApi.SAAT_GetErrorMessage(btErrorMsg, 255);
				sErrorMsg = Encoding.Default.GetString(btErrorMsg);
			}
			return sErrorMsg;
		}

		public int GetErrorCode()
		{
			int errCode = 0;
			if (RfidApi.pHandle == IntPtr.Zero)
			{
				errCode = -1;
			}
			else
			{
				RfidApi.SAAT_GetErrorCode(ref errCode);
			}

			return errCode;
		}

		/// <summary>
		/// Safe event system
		/// </summary>
		/// <param name="text"></param>
		private static void AddSafeTextEvent(string text)
		{
			Inicio.events.getListBox().Dispatcher.Invoke(() => Inicio.events.add(text));
		}

		/// <summary>
		/// Returns the singleton instance of this class
		/// </summary>
		/// <returns></returns>
		public static ReaderHandler getInstance()
		{
			if (ReaderHandler._instance == null)
				ReaderHandler._instance = new ReaderHandler();

			return ReaderHandler._instance;
		}
	}
}
