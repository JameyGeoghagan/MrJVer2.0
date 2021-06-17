using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mr_J_Vers_2._0.ApiCalls
{
    public class ChuckNorise
    {
            public static string Chuckcall()
        {
            var client = new HttpClient();
            var chuckURL = "https://api.chucknorris.io/jokes/random";
            var chuckResponse = client.GetStringAsync(chuckURL).Result;
            var chuckQuote = JObject.Parse(chuckResponse).GetValue("value").ToString();

            return chuckQuote;
           
        }

    }
}
