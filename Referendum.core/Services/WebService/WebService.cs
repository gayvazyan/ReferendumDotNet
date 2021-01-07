using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Referendum.core.Models;

namespace Referendum.core

{
    public class WebService : IWebService
    {
        protected readonly IHttpClientFactory _clientFactory;

        public WebService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public CitizenWebServiceResponse GetCitizen(string data)
        {
            CitizenWebServiceResponse citizenWebServiceResponse = new CitizenWebServiceResponse();


            if (data != null)
            {
                var decriptedData = this.DecriptData(data);
                var replacedDecriptedData = decriptedData.Replace('[', '{').Replace(']', '}');
                CitizenWebServiceResponse decriptedDataDeserialize = JsonConvert.DeserializeObject<CitizenWebServiceResponse>(replacedDecriptedData);


                if (decriptedDataDeserialize.Opaque != null)
                {
                    citizenWebServiceResponse.Opaque = decriptedDataDeserialize.Opaque;
                    citizenWebServiceResponse.First_name = decriptedDataDeserialize.First_name;
                    citizenWebServiceResponse.Last_name = decriptedDataDeserialize.Last_name;
                    citizenWebServiceResponse.Ssn = decriptedDataDeserialize.Ssn;
                    citizenWebServiceResponse.Time = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"); ;
                }
                else
                {
                    //citizenWebServiceResponse.Status
                    //citizenWebServiceResponse.ErrorMessage
                }
            }
            return citizenWebServiceResponse;
        }
        public async Task<CitizenWebServiceResponse> GetCitizenById()
        {
            CitizenWebServiceResponse citizenWebServiceResponse = new CitizenWebServiceResponse();

            var queryString = new Dictionary<string, string>()
                {
                    { "token", "66a53b58-8bd9-3cc3-9afe-89105c4f6b82" },

                    { "opaque", "1cr8j1po4ejoer6nqm423jdpn3" },

                };
            var requestUri = QueryHelpers.AddQueryString("https://eid.ekeng.am/authorize", queryString);

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var client = _clientFactory.CreateClient();
           // var data1 = "Dtkn0C9TGORKyCgDJRZUYlvcfaQKWt5kqSebYJ/pDTlrv//qPO0AZWOqDqNxaNPpg8Ugzr7SdZ9yFbVn/8QCwP/zWbM/4leue4OjPPmuPNRKpiA+mNv/yd/ZPP6mwg2DtAZDoQsHJiFxGjaGUI4docTUq/aorZqYD4l1lEV/JqjKqva+3HRqcOXLGYnPmCbyBLD0hvzG445gCGUuOeHHfDnpoG4zpyb6iw+eZwrYTT3dZpK2uNfn3UGESZVi7qPcHa8YI6pGvQPAbnJ1JGuC5QKVd6k7uJP4osaCNko3si9LbYvaCUjTig6v6JVZ2ecOQkpxKnEEfkUp/FjR59oGGfQ1gO/6ZkjgzCw66jjifCkF3kzwo/4Xga+Zl/2pX3ZR";

            var response = await client.SendAsync(request);

            var content = JsonConvert.DeserializeObject<WebServiceResponse>(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                var decriptedData = this.DecriptData(content.Data);
                var replacedDecriptedData = decriptedData.Replace('[', '{').Replace(']', '}');
                CitizenWebServiceResponse decriptedDataDeserialize = JsonConvert.DeserializeObject<CitizenWebServiceResponse>(replacedDecriptedData);

                if (content.Status == "OK")
                {
                    if (decriptedDataDeserialize.Opaque != null)
                    {
                        citizenWebServiceResponse.Opaque = decriptedDataDeserialize.Opaque;
                        citizenWebServiceResponse.First_name = decriptedDataDeserialize.First_name;  
                        citizenWebServiceResponse.Last_name = decriptedDataDeserialize.Last_name;  
                        citizenWebServiceResponse.Ssn = decriptedDataDeserialize.Ssn;  
                      //  citizenWebServiceResponse.Time = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"); 
                    }
                    else
                    {
                        //citizenWebServiceResponse.Status
                        //citizenWebServiceResponse.ErrorMessage
                    }
                    return citizenWebServiceResponse;

                   
                }
                else
                {
                    //passportWebServiceResponse.ErrorMessage = response.StatusCode + ": " + content.Message;
                   
                }
                return citizenWebServiceResponse;
            }
            else
            {
                //passportWebServiceResponse.ErrorMessage = response.StatusCode + ": " + content.Message;
            }

            return citizenWebServiceResponse;
        }

        public string DecriptData(string value)
        {
            string strIV = "O9fGelU066lJf7tiIjTw7w==";
            string strKey = "115d594615063c8d895728969aef7ef5";

            string decriptedData = Decod(value, strKey, strIV);

            return decriptedData;
        }


        public static string Decod(string TextToDecrypt, string strKey, string strIV)
        {
            byte[] EncryptedBytes = Convert.FromBase64String(TextToDecrypt);


            byte[] dataIV = System.Convert.FromBase64String(strIV);


            //Setup the AES provider for decrypting.            
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();

            aesProvider.BlockSize = 128;
            aesProvider.KeySize = 256;
            //My key and iv that i have used in openssl
            aesProvider.Key = System.Text.Encoding.ASCII.GetBytes(strKey);
            //aesProvider.IV = System.Text.Encoding.UTF8.GetBytes(strIV);
            aesProvider.Padding = PaddingMode.PKCS7;
            aesProvider.Mode = CipherMode.CBC;


            ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(aesProvider.Key, dataIV);
            byte[] DecryptedBytes = cryptoTransform.TransformFinalBlock(EncryptedBytes, 0, EncryptedBytes.Length);
            return System.Text.Encoding.ASCII.GetString(DecryptedBytes);
        }

        public string GetPath()
        {
            var queryString = new Dictionary<string, string>()
                {
                    { "token", "66a53b58-8bd9-3cc3-9afe-89105c4f6b82" },

                    { "opaque", "1cr8j1po4ejoer6nqm423jdpn3" },

                };
            var requestUri = QueryHelpers.AddQueryString("https://eid.ekeng.am/authorize", queryString);
          //  var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);

            return requestUri;
        }
    }
}
