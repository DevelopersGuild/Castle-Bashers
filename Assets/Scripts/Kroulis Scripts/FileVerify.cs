using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using UnityEngine;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Kroulis.Verify
{
    class FileVerify
    {
        public static string getFileHash(string filePath)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                int len = (int)fs.Length;
                byte[] data = new byte[len];
                fs.Read(data, 0, len);
                fs.Close();
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] result = md5.ComputeHash(data);
                string fileMD5 = "";
                foreach (byte b in result)
                {
                    fileMD5 += Convert.ToString(b, 16);
                }
                return fileMD5;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
        public static string GetPath()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return Application.persistentDataPath;
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                return Application.dataPath;
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Globe.Character_id = "GM000001";
                Globe.Character_Data_File = "GM000001.xml";
                return Application.dataPath;
            }
            else
                return "";
        }
    }
    public class GameData
    {
        //Key to the save data
        public string key;

        //The thing we need//
        public string PlayerName;
        public float MusicVolume;
        public GameData()
        {
            
        }
    }
    public class XmlSaver
    {
        //内容加密
        public string Encrypt(string toE)
        {
            //x32 Key//
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();

            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        //内容解密
        public string Decrypt(string toD)
        {
            //x32 Key//
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789012");

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();

            byte[] toEncryptArray = Convert.FromBase64String(toD);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public string SerializeObject(object pObject, System.Type ty)
        {
            string XmlizedString = null;
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(ty);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, pObject);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            return XmlizedString;
        }

        public object DeserializeObject(string pXmlizedString, System.Type ty)
        {
            XmlSerializer xs = new XmlSerializer(ty);
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return xs.Deserialize(memoryStream);
        }

        //Xml Create
        public void CreateXML(string fileName, string thisData)
        {
            string xxx = Encrypt(thisData);
            StreamWriter writer;
            writer = File.CreateText(fileName);
            writer.Write(xxx);
            writer.Close();
        }

        //Xml Load
        public string LoadXML(string fileName)
        {
            StreamReader sReader = File.OpenText(fileName);
            string dataString = sReader.ReadToEnd();
            sReader.Close();
            string xxx = Decrypt(dataString);
            return xxx;
        }

        //File Exist
        public bool hasFile(String fileName)
        {
            return File.Exists(fileName);
        }
        public string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        public byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }
    }
    //SaveData//
    public class GameDataManager : MonoBehaviour
    {
        private string dataFileName = "ExportData.dat";//File name//
        private XmlSaver xs = new XmlSaver();
        public GameData gameData;

        public void Awake()
        {
            gameData = new GameData();

            //Key will be set by the device id//
            gameData.key = SystemInfo.deviceUniqueIdentifier;
            Load();
        }

        //Save Data//
        public void Save()
        {
            string gameDataFile = GetDataPath() + "/" + dataFileName;
            string dataString = xs.SerializeObject(gameData, typeof(GameData));
            xs.CreateXML(gameDataFile, dataString);
        }

        //Load Data//
        public void Load()
        {
            string gameDataFile = GetDataPath() + "/" + dataFileName;
            if (xs.hasFile(gameDataFile))
            {
                string dataString = xs.LoadXML(gameDataFile);
                GameData gameDataFromXML = xs.DeserializeObject(dataString, typeof(GameData)) as GameData;

                //Verify Success//
                if (gameDataFromXML.key == gameData.key)
                {
                    gameData = gameDataFromXML;
                }
                //Verify Failed//
                else
                {
                    //Todo
                }
            }
            else
            {
                if (gameData != null)
                    Save();
            }
        }

        //获取路径//
        private static string GetDataPath()
        {
            // Your game has read+write access to /var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/Documents
            // Application.dataPath returns ar/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/myappname.app/Data             
            // Strip "/Data" from path
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
                // Strip application name
                path = path.Substring(0, path.LastIndexOf('/'));
                return path + "/Documents";
            }
            else
                //    return Application.dataPath + "/Resources";
                return Application.dataPath;
        }
    }


}
