using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using Kroulis.Verify;

namespace Kroulis.Error
{
    class ErrorCatching 
    {
        public void OnEnable()
        {
            Application.logMessageReceived+=ProcessExceptionReport;
        }
        public void OnDisable()
        {
            Application.logMessageReceived -= ProcessExceptionReport;
        }
        private void ProcessExceptionReport(string logString, string stackTrace, LogType type)
        {
            if(type==LogType.Error || type==LogType.Exception)
            {
                WriteOwnErrorXML(logString, stackTrace);
                if (Application.platform != RuntimePlatform.WindowsEditor)
                {
                    Application.OpenURL(FileVerify.GetPath() + "/Castle-Bashers Bug Report.exe");
                    //Application.Quit();
                }
            }
        }
        private void WriteOwnErrorXML(string logString,string stackTrace)
        {
            XmlDocument ErrorEX = new XmlDocument();
            //Set Declare
            XmlDeclaration xmldec = ErrorEX.CreateXmlDeclaration("1.0", "UTF-8", null);
            ErrorEX.AppendChild(xmldec);
            //Create Root
            XmlElement root = ErrorEX.CreateElement("errorcatch");
            ErrorEX.AppendChild(root);
            //Create Error ID
            XmlElement EID = ErrorEX.CreateElement("eid");
            EID.InnerText = "E10004";
            root.AppendChild(EID);
            //Create Log infomation
            XmlElement Log = ErrorEX.CreateElement("log");
            Log.InnerText = logString;
            root.AppendChild(Log);
            //Create Error information
            XmlElement DES = ErrorEX.CreateElement("detail");
            DES.InnerText = stackTrace;
            root.AppendChild(DES);
            //Create User information
            XmlElement INFO = ErrorEX.CreateElement("info");
            INFO.InnerText = GetSystemInfo();
            root.AppendChild(INFO);
            //Save the file
            ErrorEX.Save(FileVerify.GetPath() + "/error.dat");
        }

        private string GetSystemInfo()
        {
            string data="";
            data += "Platform: " + Application.platform.ToString() + "\n";
            data += "System: " + SystemInfo.operatingSystem.ToString() + "\n";
            data += "CPU RATE: " + SystemInfo.processorCount.ToString() + "%\n";
            data += "Mem: " + SystemInfo.systemMemorySize.ToString() + "MB\n";
            data += "GPUName: " + SystemInfo.graphicsDeviceName + "\n";
            return data;
        }

        public static void WriteBugXML()
        {
            XmlDocument ErrorEX = new XmlDocument();
            //Set Declare
            XmlDeclaration xmldec = ErrorEX.CreateXmlDeclaration("1.0", "UTF-8", null);
            ErrorEX.AppendChild(xmldec);
            //Create Root
            XmlElement root = ErrorEX.CreateElement("errorcatch");
            ErrorEX.AppendChild(root);
            //Create Error ID
            XmlElement EID = ErrorEX.CreateElement("eid");
            EID.InnerText = "B99999";
            root.AppendChild(EID);
            //Create Log infomation
            XmlElement Log = ErrorEX.CreateElement("log");
            root.AppendChild(Log);
            //Create Error information
            XmlElement DES = ErrorEX.CreateElement("detail");
            root.AppendChild(DES);
            //Create User information
            XmlElement INFO = ErrorEX.CreateElement("info");
            root.AppendChild(INFO);
            //Save the file
            ErrorEX.Save(FileVerify.GetPath() + "/error.dat");
            Application.OpenURL(FileVerify.GetPath() + "/bugreport.exe");
        }

        public static void WriteConfigErrorXML()
        {
            XmlDocument ErrorEX = new XmlDocument();
            //Set Declare
            XmlDeclaration xmldec = ErrorEX.CreateXmlDeclaration("1.0", "UTF-8", null);
            ErrorEX.AppendChild(xmldec);
            //Create Root
            XmlElement root = ErrorEX.CreateElement("errorcatch");
            ErrorEX.AppendChild(root);
            //Create Error ID
            XmlElement EID = ErrorEX.CreateElement("eid");
            EID.InnerText = "E10001";
            root.AppendChild(EID);
            //Create Log infomation
            XmlElement Log = ErrorEX.CreateElement("log");
            root.AppendChild(Log);
            //Create Error information
            XmlElement DES = ErrorEX.CreateElement("detail");
            root.AppendChild(DES);
            //Create User information
            XmlElement INFO = ErrorEX.CreateElement("info");
            root.AppendChild(INFO);
            //Save the file
            ErrorEX.Save(FileVerify.GetPath() + "/error.dat");
            Application.OpenURL(FileVerify.GetPath() + "/bugreport.exe");
        }

        public static void WriteCharacterDataXML()
        {
            XmlDocument ErrorEX = new XmlDocument();
            //Set Declare
            XmlDeclaration xmldec = ErrorEX.CreateXmlDeclaration("1.0", "UTF-8", null);
            ErrorEX.AppendChild(xmldec);
            //Create Root
            XmlElement root = ErrorEX.CreateElement("errorcatch");
            ErrorEX.AppendChild(root);
            //Create Error ID
            XmlElement EID = ErrorEX.CreateElement("eid");
            EID.InnerText = "E10002";
            root.AppendChild(EID);
            //Create Log infomation
            XmlElement Log = ErrorEX.CreateElement("log");
            root.AppendChild(Log);
            //Create Error information
            XmlElement DES = ErrorEX.CreateElement("detail");
            root.AppendChild(DES);
            //Create User information
            XmlElement INFO = ErrorEX.CreateElement("info");
            root.AppendChild(INFO);
            //Save the file
            ErrorEX.Save(FileVerify.GetPath() + "/error.dat");
            Application.OpenURL(FileVerify.GetPath() + "/bugreport.exe");
        }

        public static void WriteVerifyXML()
        {
            XmlDocument ErrorEX = new XmlDocument();
            //Set Declare
            XmlDeclaration xmldec = ErrorEX.CreateXmlDeclaration("1.0", "UTF-8", null);
            ErrorEX.AppendChild(xmldec);
            //Create Root
            XmlElement root = ErrorEX.CreateElement("errorcatch");
            ErrorEX.AppendChild(root);
            //Create Error ID
            XmlElement EID = ErrorEX.CreateElement("eid");
            EID.InnerText = "E10003";
            root.AppendChild(EID);
            //Create Log infomation
            XmlElement Log = ErrorEX.CreateElement("log");
            root.AppendChild(Log);
            //Create Error information
            XmlElement DES = ErrorEX.CreateElement("detail");
            root.AppendChild(DES);
            //Create User information
            XmlElement INFO = ErrorEX.CreateElement("info");
            root.AppendChild(INFO);
            //Save the file
            ErrorEX.Save(FileVerify.GetPath() + "/error.dat");
            Application.OpenURL(FileVerify.GetPath() + "/bugreport.exe");
        }
    
    }

}
