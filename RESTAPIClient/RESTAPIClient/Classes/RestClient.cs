using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using static RESTAPIClient.Enums.Crud;
using static RESTAPIClient.Enums.AuthenticationType;
using static RESTAPIClient.Enums.AuthenticationTechnique;

namespace RESTAPIClient.Classes
{
    /// <summary>
    /// Basic REST Client class. Gets response from given API url.
    /// </summary>
    internal class RestClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }
        public authenticationType authType { get; set; }
        public authenticationTechnique authTech { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public RestClient()
        { 
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }
        public string MakeRequest()
        {
            string result = string.Empty;
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = httpMethod.ToString();
            //Add username and password to header for basic authentication
            string authHeader = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{username}:{password}"));
            request.Headers.Add("Authorization", $"{authType} {authHeader}");

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException($"Request failed {response.StatusCode}");
                }
                //Process the response stream
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = $"Reques failed {ex.Message}";
            }
            finally 
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return result;
        }
    }
}
