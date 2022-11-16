using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PO_Importer;
using RestSharp.Serializers;

namespace PO_Importer
{
    public class SmartsheetRest
    {

        readonly RestClient _client;
        private const string _smartsheetURL = "https://api.smartsheet.com/2.0";

        public SmartsheetRest()
        {
            string authToken = "";
            _client = new RestClient(_smartsheetURL)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(authToken, "Bearer")
            };
        }



        public async Task<SmartSheetHome> GetHome()
        {
            var request = new RestRequest("/home?include=source");
            var response = await _client.GetAsync(request);

            return JsonConvert.DeserializeObject<SmartSheetHome>(response.Content);

        }

        public async Task<SheetRoot> GetSheet(string sheetId)
        {
            
            var request = new RestRequest("/sheets/" + sheetId);
            var response = await _client.GetAsync(request);

            return JsonConvert.DeserializeObject<SheetRoot>(response.Content);

        }

        public async Task UpdateRow(RequestRow newRow, string sheetId)
        {
            var request = new RestRequest("/sheets/" + sheetId + "/rows");
            var json = JsonConvert.SerializeObject(newRow, Formatting.Indented);
            request.AddStringBody(json, ContentType.Json);
            Console.WriteLine(json);
            Console.WriteLine("/sheets/" + sheetId + "/rows");



            try
            {
                var response = await _client.PutAsync(request);
                Console.WriteLine(response.Content.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            

            
            



        }









    }
}
