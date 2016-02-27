using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

namespace yxOpcServer
{
    class class_opcDll
    {
        public static Notification notifiaction;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Notification(IntPtr itemHandle, string vt, string value, short Quality);

        public static string OpcServerGUID = "{C6C83062-6C69-11d4-80FF-00C04F790F3C}";
        public static string OpcName = "MyOpcServer";
        public static string OpcDescr = "lee";

        public const short OPC_QUALITY_MASK = 0xC0;
        public const short OPC_STATUS_MASK = 0xFC;
        public const short OPC_LIMIT_MASK = 0x03;
        public const short OPC_QUALITY_BAD = 0x00;
        public const short OPC_QUALITY_UNCERTAIN = 0x40;
        public const short OPC_QUALITY_GOOD = 0xC0;
        public const short OPC_QUALITY_CONFIG_ERROR = 0x04;
        public const short OPC_QUALITY_NOT_CONNECTED = 0x08;
        public const short OPC_QUALITY_DEVICE_FAILURE = 0x0c;
        public const short OPC_QUALITY_SENSOR_FAILURE = 0x10;
        public const short OPC_QUALITY_LAST_KNOWN = 0x14;
        public const short OPC_QUALITY_COMM_FAILURE = 0x18;
        public const short OPC_QUALITY_OUT_OF_SERVICE = 0x1C;
        public const short OPC_QUALITY_LAST_USABLE = 0x44;
        public const short OPC_QUALITY_SENSOR_CAL = 0x50;
        public const short OPC_QUALITY_EGU_EXCEEDED = 0x54;
        public const short OPC_QUALITY_SUB_NORMAL = 0x58;
        public const short OPC_QUALITY_LOCAL_OVERRIDE = 0xD8;
        public const short OPC_LIMIT_OK = 0x00;
        public const short OPC_LIMIT_LOW = 0x01;
        public const short OPC_LIMIT_HIGH = 0x02;
        public const short OPC_LIMIT_CONST = 0x03;

        public static string shortTostring(short status)
        {
            switch (status)
            {
                //case OPC_QUALITY_MASK:
                //    return "QUALITY_MASK";
                case OPC_STATUS_MASK:
                    return "STATUS_MASK";
                case OPC_LIMIT_MASK:
                    return "LIMIT_MASK";
                case OPC_QUALITY_BAD:
                    return "QUALITY_BAD";
                case OPC_QUALITY_UNCERTAIN:
                    return "QUALITY_UNCERTAIN";
                case OPC_QUALITY_GOOD:
                    return "QUALITY_GOOD";
                case OPC_QUALITY_CONFIG_ERROR:
                    return "QUALITY_CONFIG_ERROR";
                case OPC_QUALITY_NOT_CONNECTED:
                    return "QUALITY_NOT_CONNECTED";
                case OPC_QUALITY_DEVICE_FAILURE:
                    return "QUALITY_DEVICE_FAILURE";
                case OPC_QUALITY_SENSOR_FAILURE:
                    return "QUALITY_SENSOR_FAILURE";
                case OPC_QUALITY_LAST_KNOWN:
                    return "QUALITY_LAST_KNOWN";
                case OPC_QUALITY_COMM_FAILURE:
                    return "QUALITY_COMM_FAILURE";
                case OPC_QUALITY_OUT_OF_SERVICE:
                    return "QUALITY_OUT_OF_SERVICE";
                case OPC_QUALITY_LAST_USABLE:
                    return "QUALITY_LAST_USABLE";
                case OPC_QUALITY_SENSOR_CAL:
                    return "QUALITY_SENSOR_CAL";
                case OPC_QUALITY_EGU_EXCEEDED:
                    return "QUALITY_EGU_EXCEEDED";
                case OPC_QUALITY_SUB_NORMAL:
                    return "QUALITY_SUB_NORMAL";
                case OPC_QUALITY_LOCAL_OVERRIDE:
                    return "QUALITY_LOCAL_OVERRIDE";
                //case OPC_LIMIT_OK:
                //    return "LIMIT_OK";
                case OPC_LIMIT_LOW:
                    return "LIMIT_LOW";
                case OPC_LIMIT_HIGH:
                    return "LIMIT_HIGH";
                //case OPC_LIMIT_CONST:
                //    return "LIMIT_CONST";
                default:
                    return "ERROR";
            }
        }
        public static short stringToshort(string status)
        {
            switch (status)
            {
                case "QUALITY_MASK":
                    return OPC_QUALITY_MASK;
                case "STATUS_MASK":
                    return OPC_STATUS_MASK;
                case "LIMIT_MASK":
                    return OPC_LIMIT_MASK;
                case "QUALITY_BAD":
                    return OPC_QUALITY_BAD;
                case "QUALITY_UNCERTAIN":
                    return OPC_QUALITY_UNCERTAIN;
                case "QUALITY_GOOD":
                    return OPC_QUALITY_GOOD;
                case "QUALITY_CONFIG_ERROR":
                    return OPC_QUALITY_CONFIG_ERROR;
                case "QUALITY_NOT_CONNECTED":
                    return OPC_QUALITY_NOT_CONNECTED;
                case "QUALITY_DEVICE_FAILURE":
                    return OPC_QUALITY_DEVICE_FAILURE;
                case "QUALITY_SENSOR_FAILURE":
                    return OPC_QUALITY_SENSOR_FAILURE;
                case "QUALITY_LAST_KNOWN":
                    return OPC_QUALITY_LAST_KNOWN;
                case "QUALITY_COMM_FAILURE":
                    return OPC_QUALITY_COMM_FAILURE;
                case "QUALITY_OUT_OF_SERVICE":
                    return OPC_QUALITY_OUT_OF_SERVICE;
                case "QUALITY_LAST_USABLE":
                    return OPC_QUALITY_LAST_USABLE;
                case "QUALITY_SENSOR_CAL":
                    return OPC_QUALITY_SENSOR_CAL;
                case "QUALITY_EGU_EXCEEDED":
                    return OPC_QUALITY_EGU_EXCEEDED;
                case "QUALITY_SUB_NORMAL":
                    return OPC_QUALITY_SUB_NORMAL;
                case "QUALITY_LOCAL_OVERRIDE":
                    return OPC_QUALITY_LOCAL_OVERRIDE;
                case "LIMIT_OK":
                    return OPC_LIMIT_OK;
                case "LIMIT_LOW":
                    return OPC_LIMIT_LOW;
                case "LIMIT_HIGH":
                    return OPC_LIMIT_HIGH;
                case "LIMIT_CONST":
                    return OPC_LIMIT_CONST;
                default:
                    return 0;
            }
        }
        public static string valueTypeChange(string type)
        {
            if (type == "F")
            {
                return "Float";
            }
            return "";
        }

        [DllImport("OpcServer.dll", EntryPoint = "ClientCount", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ClientCount();

        [DllImport("OpcServer.dll", EntryPoint = "InitOpcSvr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool InitOpcSvr(string sGUID, int ServerRate);

        [DllImport("OpcServer.dll", EntryPoint = "UninitOpcSvr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UninitOpcSvr();

        [DllImport("OpcServer.dll", EntryPoint = "Registry", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Registry(string sGUID, string Name, string Descr, string ExePath);

        [DllImport("OpcServer.dll", EntryPoint = "UnRegister", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnRegister(string sGUID, string Name);

        [DllImport("OpcServer.dll", EntryPoint = "CreateOpcItem", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateOpcItem(string sendName, string vt, string value, short Quality);

        [DllImport("OpcServer.dll", EntryPoint = "UpdateOpcItem", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UpdateOpcItem(IntPtr itemHandle, string vt, string value, short Quality);

        [DllImport("OpcServer.dll", EntryPoint = "RemoveOpcItem", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool RemoveOpcItem(IntPtr itemHandle);

        [DllImport("OpcServer.dll", EntryPoint = "WriteNotification", CharSet = CharSet.Ansi)]
        public static extern void WriteNotification(Notification Notification);

        [DllImport("OpcServer.dll", EntryPoint = "MyRequestDisconnect", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MyRequestDisconnect();
    }
}
