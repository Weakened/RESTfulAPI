using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using static RESTAPIClient.Enums.Crud;

namespace RESTAPIClient.Classes
{
    /// <summary>
    /// Basic REST Client class. Gets response from given API url.
    /// </summary>
    internal class RestClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }

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
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
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
            return result;
        }
    }
}
