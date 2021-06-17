using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mr_J_Vers_2._0.ApiCalls
{
    class JokeApiCall
    {
        public static string Jokecall()
        {
            var client = new HttpClient();
            var jokeURL = "https://v2.jokeapi.dev/joke/Any";
            var jokeResponse = client.GetStringAsync(jokeURL).Result;
            var jokeQuote = JObject.Parse(jokeResponse).GetValue("joke").ToString();

            return jokeQuote;

        }
    }
}
