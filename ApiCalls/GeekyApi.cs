using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mr_J_Vers_2._0.ApiCalls
{
    public class GeekyApi
    {
        public static string GeekApi()
        {
            var client = new HttpClient();
            var geekURL = "https://geek-jokes.sameerkumar.website/api?format=json";
            var geekResponse = client.GetStringAsync(geekURL).Result;
            var geekQuote = JObject.Parse(geekResponse).GetValue("joke").ToString();

            return geekQuote;
        }
    }
}
