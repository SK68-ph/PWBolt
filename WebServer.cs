
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PWBolt.Network
{
    static class WebServer
    {

        private static CookieContainer cookieContainer = new CookieContainer();
        private static string webserver = "https://elucidative-designa.000webhostapp.com";
        private static bool _isLoggedIn = false;
        public static bool IsLoggedIn { get { return _isLoggedIn; } set { _isLoggedIn = value; } }


        private static HttpWebRequest SendRequest(byte[] postData, string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.CookieContainer = cookieContainer;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.KeepAlive = false;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            try
            {
                using (Stream webpageStream = request.GetRequestStream())
                {
                    webpageStream.Write(postData, 0, postData.Length);
                }
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine("Failed connecting to webserver");
                Console.ReadKey();
                Environment.Exit(-1);
            }
            return request;
        }

        public static string GetWebResponse(HttpWebRequest request)
        {
            string responseFromServer = "";
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var dataStream = response.GetResponseStream();
                    var reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd().Trim();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    return "\n"+ responseFromServer;
                }
                else
                {
                    Console.WriteLine("An error occured while connecting to webserver.");
                    Console.ReadKey();
                    Environment.Exit(-1);
                }
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine("Failed connecting to webserver");
                Console.ReadKey();
                Environment.Exit(-1);
            }
            return "";
        }

        public static void LoginPWBolt(string username, string pwbolt)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes("username=" + username + "&" + "pwbolt=" + pwbolt);
            var request = SendRequest(byteArray, webserver + "/login.php");
            var response = GetWebResponse(request);
            
            if (response.Contains("ERROR"))
            {
                cookieContainer = new CookieContainer();
                IsLoggedIn = false;
            }
            else
                IsLoggedIn = true;
            Console.ForegroundColor = IsLoggedIn ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(response);
            Console.ResetColor();
        }

        public static void RegisterPWBolt(string username, string pwbolt)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes("username=" + username + "&" + "pwbolt=" + pwbolt);
            var request = SendRequest(byteArray, webserver + "/register.php");
            var response = GetWebResponse(request);
            Console.ForegroundColor = response.Contains("ERROR") ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(response);
            Console.ResetColor();
        }

        public static void bolt_DisplayAccount()
        {
            var request = (HttpWebRequest)WebRequest.Create(webserver + "/bolt_DisplayAccount.php");
            request.CookieContainer = cookieContainer;
            request.Credentials = CredentialCache.DefaultCredentials;
            var response = GetWebResponse(request);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ForegroundColor = response.Contains("WARNING") | response.Contains("ERROR") ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(response);
            Console.ResetColor();
        }

        public static void bolt_AddAccount(string website, string username, string password)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes("website=" + website + "&" + "username=" + username + "&" + "password=" + password);
            var request = SendRequest(byteArray, webserver + "/bolt_AddAccount.php");
            var response = GetWebResponse(request);
            Console.ForegroundColor = response.Contains("ERROR") ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(response);
            Console.ResetColor();
        }

        public static void bolt_EditAccount(string id,string website,string username,string password)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes("id=" + id + "&" + "website=" + website + "&" + "username=" + username + "&" + "password=" + password);
            var request = SendRequest(byteArray, webserver + "/bolt_EditAccount.php");
            var response = GetWebResponse(request);
            Console.ForegroundColor = response.Contains("ERROR") ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(response);
            Console.ResetColor();
        }

        public static void bolt_DeleteAccount(string id)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes("id=" + id);
            var request = SendRequest(byteArray, webserver + "/bolt_DeleteAccount.php");
            var response = GetWebResponse(request);
            Console.ForegroundColor = response.Contains("ERROR") ? ConsoleColor.Red : ConsoleColor.Green;
            Console.WriteLine(response);
            Console.ResetColor();
        }

        public static void Logout(string username, string pwbolt)
        {
        }

    }
}
