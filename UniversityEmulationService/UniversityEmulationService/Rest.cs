using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using UniversityEmulationService.models;
using System.Threading.Tasks;
using System.Globalization;

namespace UniversityEmulationService
{
    public class Rest
    {
        public static string GetHandler(string url)
        {
            string data = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version10;
                request.Timeout = 10000;
                request.KeepAlive = false;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                data = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task GetRequest(Action<ResultJson> action, string req)
        {
            string result = GetHandler(req);
            ResultJson mm = Deserialize<ResultJson>(result);
            action(mm);
        }
        public async Task GetAllUniversities(Action<List<University>> action, string req)
        {
            //string a = JsonSerializer<University>(new University(-1,"dsf","sdf",3,"rge",new DateTime(2,3,4),new DateTime(3,4,1)));

            string result = GetHandler(req);
            if (result == null)
            {
                action(null);
            }
            else
            {
                List<University> mm = Deserialize<List<University>>(result);
                action(mm);
            }
        }

        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        public static T Deserialize<T>(string json)
        {
            try
            {
                var settings = new DataContractJsonSerializerSettings
                {
                    DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                };
                var _Bytes = Encoding.Unicode.GetBytes(json);
                using (MemoryStream _Stream = new MemoryStream(_Bytes))
                {

                    var _Serializer = new DataContractJsonSerializer(typeof(T), settings);

                    return (T)_Serializer.ReadObject(_Stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
        }
    }
}
