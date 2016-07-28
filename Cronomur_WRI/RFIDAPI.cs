using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using Cronomur_WRI;

namespace Szaat.RFID.CSharpAPI
{
    public class RfidApi
    {
        private class RFID_Wrapper
        {
            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_Copyright")]
            public static extern bool SAAT_Copyright(out IntPtr pHandle, StringBuilder copyright);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_TCPInit")]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool SAAT_TCPInit(out IntPtr pHandle, string pHostName, int nsocketPort);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_COMInit")]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool SAAT_COMInit(out IntPtr pHandle, byte nBusAddr, string pComNum, int nBaud);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_Open")]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool SAAT_Open(IntPtr pHandle);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_Close")]
            public static extern bool SAAT_Close(IntPtr pHandle);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_SysInfSet")]
            public static extern bool SAAT_SysInfSet(IntPtr pHandle, byte nType, byte[] pParm, byte nLen);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_SysInfQuery")]
            public static extern bool SAAT_SysInfQuery(IntPtr pHandle, byte nType, IntPtr pPara, ref byte pLen);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_YPowerOff")]
            public static extern bool SAAT_YPowerOff(IntPtr pHandle);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_GetErrorMessage")]
            public static extern bool SAAT_GetErrorMessage(IntPtr pHandle, byte[] szMsg, int nLen);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_IOOperate")]
            public static extern bool SAAT_IOOperate(IntPtr pHandle, byte nPort, byte nState);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_IOStateQuery")]
            public static extern bool SAAT_IOStateQuery(IntPtr pHandle, ref byte pState);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_YMakeTagUpLoadIDCode")]
            public static extern bool SAAT_YMakeTagUpLoadIDCode(IntPtr pHandle, byte nOpType, byte nIDType);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_YRevIDMsgHex")]
            public static extern int SAAT_YRevIDMsgHex(IntPtr pHandle, byte[] pIDData, out byte nIDLen, out byte nBit);

            [DllImport("RFIDAPI.dll", EntryPoint = "SAAT_YRevIDMsgDec")]
            public static extern int SAAT_YRevIDMsgDec(IntPtr pHandle, out uint pId, out byte nBit);

			[DllImport("RFIDAPI.dll", EntryPoint = "SAAT_YAntennaPowerQuery")]
			public static extern bool SAAT_YAntennaPowerQuery(IntPtr pHandle, ref byte rfPower);

			[DllImport("RFIDAPI.dll", EntryPoint = "SAAT_YAntennaPowerSet")]
			public static extern bool SAAT_YAntennaPowerSet(IntPtr pHandle, byte rfPower);

			[DllImport("RFIDAPI.dll", EntryPoint = "SAAT_SetLangId")]
			public static extern bool SAAT_SetLangId(IntPtr pHandle, byte nLangId);

			[DllImport("RFIDAPI.dll", EntryPoint = "SAAT_GetErrorCode")]
			public static extern bool SAAT_GetErrorCode(IntPtr pHandle, ref int pCode);

			[DllImport("RFIDAPI.dll", EntryPoint = "SAAT_HeartSend")]
			public static extern bool SAAT_HeartSend(IntPtr pHandle);
		}

        /// <summary>
        /// Reader Handle
        /// </summary>
        public static IntPtr pHandle = IntPtr.Zero;

		#region functions
		public static bool SAAT_SetLangId(byte nLangId)
		{
			try
			{
				return RFID_Wrapper.SAAT_SetLangId(pHandle, nLangId);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			return false;
		}

		public static bool SAAT_GetErrorMessage(byte[] szMsg, int nLen)
		{
			try
			{
				return RFID_Wrapper.SAAT_GetErrorMessage(pHandle, szMsg, nLen);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			return false;
		}

		public static bool SAAT_GetErrorCode(ref int pCode)
		{
			try
			{
				return RFID_Wrapper.SAAT_GetErrorCode(pHandle, ref pCode);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			return false;
		}

		public static bool SAAT_Copyright(StringBuilder copyright)
        {
            try
            {
                return RFID_Wrapper.SAAT_Copyright(out pHandle, copyright);
            } catch (DllNotFoundException e)
            {
                Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
            }

            return false;
        }

        public static bool SAAT_TCPInit(string pHostName, int nsocketPort)
        {
            try
            {
                return RFID_Wrapper.SAAT_TCPInit(out pHandle, pHostName, nsocketPort);
            }
            catch (DllNotFoundException e)
            {
                Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
            }

            return false;
        }

        public static bool SAAT_Open()
        {
            try
            {
                return RFID_Wrapper.SAAT_Open(pHandle);
            }
            catch (DllNotFoundException e)
            {
                Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
            }

            return false;
        }

		public static bool SAAT_Close()
		{
			try
			{
				return RFID_Wrapper.SAAT_Close(pHandle);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			return false;
		}

		public static bool SAAT_SysInfQuery(byte nType, IntPtr pPara, ref byte pLen)
        {
            try
            {
                return RFID_Wrapper.SAAT_SysInfQuery(pHandle, nType, pPara, ref pLen);
            }
            catch (DllNotFoundException e)
            {
                Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
            }

            return false;
        }

		public static bool SAAT_SysInfSet(byte nType, byte[] pParm, byte nLen)
		{
			try
			{
				return RFID_Wrapper.SAAT_SysInfSet(pHandle, nType, pParm, nLen);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			return false;
		}

		public static bool SAAT_YAntennaPowerQuery(ref byte rfPower)
		{
			try
			{
				return RFID_Wrapper.SAAT_YAntennaPowerQuery(pHandle, ref rfPower);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			return false;
		}

		public static bool SAAT_YAntennaPowerSet(byte rfPower)
		{
			try
			{
				return RFID_Wrapper.SAAT_YAntennaPowerSet(pHandle, rfPower);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			return false;
		}

		public static bool SAAT_YMakeTagUpLoadIDCode(byte nOpType, byte nIDType)
		{
			try
			{
				return RFID_Wrapper.SAAT_YMakeTagUpLoadIDCode(pHandle, nOpType, nIDType);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			return false;
		}

		public static int SAAT_YRevIDMsgDec(out uint pId, out byte nBit)
		{
			try
			{
				return RFID_Wrapper.SAAT_YRevIDMsgDec(pHandle, out pId, out nBit);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			pId = 0;
			nBit = 0;

			return -1;
		}

		public static bool SAAT_YPowerOff()
		{
			try
			{
				return RFID_Wrapper.SAAT_YPowerOff(pHandle);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			return false;
		}

		public static bool SAAT_HeartSend()
		{
			try
			{
				return RFID_Wrapper.SAAT_HeartSend(pHandle);
			}
			catch (DllNotFoundException e)
			{
				Inicio.events.add("Excepción: No se encuentra la librería RFIDAPI.dll. No se podrá interactuar con el dispositivo sin ella.");
			}

			return false;
		}
		#endregion
	}
}