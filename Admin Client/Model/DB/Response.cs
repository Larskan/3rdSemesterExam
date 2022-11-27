using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Admin_Client.Model.DB
{
    public class Response
    {
        public HttpStatusCode StatusCode;
        public HttpContent Body;
        public HttpResponseHeaders Headers;

        //Hold response from API call
        public Response(HttpStatusCode statusCode, HttpContent responseBody, HttpResponseHeaders responeHeaders)
        {
            StatusCode = statusCode;
            Body = responseBody;
            Headers = responeHeaders;
        }

        //Convert string format response body to a Dictionary
        public virtual Dictionary<string, dynamic> DeserializeResponseBody(HttpContent content)
        {
            //JsonSerializer jss = new JsonSerializer();
            var dsContent = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(content.ReadAsStringAsync().Result);
            return dsContent;
        }

        //Convert string format response headers to a Dictionary
        public virtual Dictionary<string, string> DeserializeResponseHeaders(HttpRequestHeaders content)
        {
            var dsContent = new Dictionary<string, string>();
            foreach (var pair in content)
            {
                dsContent.Add(pair.Key, pair.Value.First());
            }
            return dsContent;
        }
    }  

        public enum Methods
        {
            DELETE, GET, PATCH, POST, PUT
        }

    
    
}
