using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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

        public string DecriptDataForSsnAndPassport(string value)
        {
            string strIV = "O9fGelU066lJf7tiIjTw7w==";
            string strKey = "4c47c5ac232d39baa8d18b7ba9804694";

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


        public async Task<PassportWebServiceResponse> GetCitizenByPassport(string passportNumber)
        {

            PassportWebServiceResponse passportWebServiceResponse = new PassportWebServiceResponse();

            var queryString = new Dictionary<string, string>()
                {
                    { "token", "c80f94cc-083c-3383-8282-bf77e1c3de35" },
                    { "type", "doc_nam" },
                    { "opaque", "3" },
                    { "documentNumber", passportNumber}
                };

            var requestUri = QueryHelpers.AddQueryString("https://ssn-api.ekeng.am/get-user", queryString);
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            var content = JsonConvert.DeserializeObject<WebServiceResponse>(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                //var decriptedData = this.DecriptData("HjB2CEbIti1CX+F1B/Ar90mkBd+cCZoKSxsqg4saZhU=");
                //var decriptedData = this.DecriptData("HjB2CEbIti1CX+F1B/Ar99YSm7/NMHKVGAKFbc4YW9nkw9rskVyfLjWd1XOdD4Tjpk56aO6poPwU2movPrMwQ5GU6Y+6qL1JfnzJa4uzsy1z7tB5uN1z+iObrNefFn9eUyvGmdBkNdT75uymj9S1SQEPezmPWTIfLyBGNG/VbOCKSBQHqxhAmd7Os5u5/9Bt3SWs/FO+hMJ795FEKFYueeYpceLCrICA4zD9fMtck9iJNH8+yTQKTDanOaLQkKM/UP72DQze+Ux/a1IaIno8VvXI+lSir0zBcgoBGGWxRSXuOhT9Gwe+PeVb0VHHKVScUR/uTksm7mmHpXR4c87PabmYoZQt3dgBjNiu3/AqMoLeJHIv3eQvn6D3RGWYYhJhnJ2oKNIsUrO/rnlR+jgXkPCA9JuGdSoI7qhFkwtLGCazM9LuK6rnkad7KGK0THuAtEm3yjcHMh8Zr+pUThk3GqZKgvJ0+gXp9GnpUPq7RrPNVvgA9bYFdZGsDcCEZIOB");
                var decriptedData = this.DecriptDataForSsnAndPassport(content.Data);
                var replacedDecriptedData = decriptedData.Replace('[', '{').Replace(']', '}');
                PassportDataResponse decriptedDataDeserialize = JsonConvert.DeserializeObject<PassportDataResponse>(replacedDecriptedData);

                if (content.Status == "OK")
                {
                    if (decriptedDataDeserialize.Data.Full_name != null)
                    {
                        passportWebServiceResponse.AVVRegistrationAddress = decriptedDataDeserialize.Data.AVVRegistrationAddress;
                        passportWebServiceResponse.Full_name = decriptedDataDeserialize.Data.Full_name;
                        passportWebServiceResponse.PNum = decriptedDataDeserialize.Data.PNum;
                        passportWebServiceResponse.Status = content.Status;
                    }
                    else
                    {
                        passportWebServiceResponse.Status = "Empty";
                        //  passportWebServiceResponse.ErrorMessage = CommonMessages.msgNoResultFromService;
                    }
                    return passportWebServiceResponse;
                }
                else
                {
                    //passportWebServiceResponse.Status = CommonMessages.msgUnknownStatus;
                    //  passportWebServiceResponse.ErrorMessage = CommonMessages.msgUnknown;

                    return passportWebServiceResponse;
                }
            }
            else
            {
                passportWebServiceResponse.ErrorMessage = response.StatusCode + ": " + content.Message;
                return passportWebServiceResponse;
            }
        }

        public async Task<SSNWebServiceResponse> GetCitizenBySSN(string ssn)
        {
            SSNWebServiceResponse ssnWebServiceResponse = new SSNWebServiceResponse();

            var queryString = new Dictionary<string, string>()
                {
                    { "token", "c80f94cc-083c-3383-8282-bf77e1c3de35" },
                    { "opaque", "3" },
                    { "ssn", ssn}
                };

            var requestUri = QueryHelpers.AddQueryString("https://ssn-api.ekeng.am/authorize", queryString);
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            var content = JsonConvert.DeserializeObject<WebServiceResponse>(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                if (content.Status == "OK")
                {
                    //var decriptedData = this.DecriptData("HjB2CEbIti1CX+F1B/Ar90mkBd+cCZoKSxsqg4saZhU=");
                    ssnWebServiceResponse.Status = content.Status;
                    var decriptedData = this.DecriptDataForSsnAndPassport(content.Data);
                    if (Regex.Matches(decriptedData, "DocumentIdentifier").Count > 1)
                    {
                        SSNDataResponseDocArray decriptedDataDeserialize = JsonConvert.DeserializeObject<SSNDataResponseDocArray>(decriptedData);

                        if (decriptedDataDeserialize.Passport_data.IsDead != null)
                        {
                            ssnWebServiceResponse.IsDead = decriptedDataDeserialize.Passport_data.IsDead;
                        }

                        var docs = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument;
                        foreach (var doc in docs)
                        {
                            if (doc.FirstName != null)
                            {
                                ssnWebServiceResponse.FirstName = doc.FirstName;
                            }
                            if (doc.LastName != null)
                            {
                                ssnWebServiceResponse.LastName = doc.LastName;
                            }
                            if (doc.MiddleName != null)
                            {
                                ssnWebServiceResponse.MiddleName = doc.MiddleName;
                            }
                            if (doc.EnglishFirstName != null)
                            {
                                ssnWebServiceResponse.FirstNameEn = doc.EnglishFirstName;
                            }
                            if (doc.EnglishLastName != null)
                            {
                                ssnWebServiceResponse.LastNameEn = doc.EnglishLastName;
                            }
                            if (doc.EnglishMiddleName != null)
                            {
                                ssnWebServiceResponse.MiddleNameEn = doc.EnglishMiddleName;
                            }
                            if (doc.Gender != null)
                            {
                                ssnWebServiceResponse.Gender = doc.Gender;
                            }
                            if (doc.BirthDate != null)
                            {
                                if (doc.BirthDate.Contains("00/"))
                                {
                                    var replacedBirthDate = doc.BirthDate.Replace("00/", "01/");
                                    ssnWebServiceResponse.BirthDate = replacedBirthDate;
                                }
                                else
                                {
                                    ssnWebServiceResponse.BirthDate = doc.BirthDate;
                                }
                            }
                        }
                        ssnWebServiceResponse.Photo = decriptedDataDeserialize.Passport_data.Photo;
                        ssnWebServiceResponse.LocationCode = decriptedDataDeserialize.Passport_data.AVVRegistrationAddress.LocationCode;
                        ssnWebServiceResponse.Community = decriptedDataDeserialize.Passport_data.AVVRegistrationAddress.Community;

            
                    }
                    else
                    {
                        SSNDataResponse decriptedDataDeserialize = JsonConvert.DeserializeObject<SSNDataResponse>(decriptedData);
                        if (decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.BirthDate.Contains("00/"))
                        {
                            var replacedBirthDate = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.BirthDate.Replace("00/", "01/");
                            ssnWebServiceResponse.BirthDate = replacedBirthDate;

                        }
                        else
                        {
                            ssnWebServiceResponse.BirthDate = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.BirthDate;
                        }
                        ssnWebServiceResponse.FirstName = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.FirstName;
                        ssnWebServiceResponse.LastName = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.LastName;
                        ssnWebServiceResponse.MiddleName = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.MiddleName;
                        ssnWebServiceResponse.Gender = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.Gender;
                        ssnWebServiceResponse.IsDead = decriptedDataDeserialize.Passport_data.IsDead;
                        ssnWebServiceResponse.Photo = decriptedDataDeserialize.Passport_data.Photo;
                    }
                    return ssnWebServiceResponse;
                }
                else
                {
                    // ssnWebServiceResponse.Status = CommonMessages.msgUnknownStatus;
                    // ssnWebServiceResponse.ErrorMessage = CommonMessages.msgUnknown;

                    return ssnWebServiceResponse;
                }
            }
            else
            {
                ssnWebServiceResponse.ErrorMessage = response.StatusCode + ": " + content.Message;
                return ssnWebServiceResponse;
            }
        }

        //Test Services
        public async Task<PassportWebServiceResponse> GetCitizenByPassportTest(string passportNumber)
        {

            PassportWebServiceResponse passportWebServiceResponse = new PassportWebServiceResponse();

            string data = string.Empty;

            if (passportNumber == "000413682")
            {
                data = PersonResponseData.lilia_passport;
            }
            if (passportNumber == "AR0220630")
            {
                data = PersonResponseData.garegin_passport;
            }

            if (!string.IsNullOrEmpty(data))
            {
                //var decriptedData = this.DecriptData("HjB2CEbIti1CX+F1B/Ar90mkBd+cCZoKSxsqg4saZhU=");
                //var decriptedData = this.DecriptData("HjB2CEbIti1CX+F1B/Ar99YSm7/NMHKVGAKFbc4YW9nkw9rskVyfLjWd1XOdD4Tjpk56aO6poPwU2movPrMwQ5GU6Y+6qL1JfnzJa4uzsy1z7tB5uN1z+iObrNefFn9eUyvGmdBkNdT75uymj9S1SQEPezmPWTIfLyBGNG/VbOCKSBQHqxhAmd7Os5u5/9Bt3SWs/FO+hMJ795FEKFYueeYpceLCrICA4zD9fMtck9iJNH8+yTQKTDanOaLQkKM/UP72DQze+Ux/a1IaIno8VvXI+lSir0zBcgoBGGWxRSXuOhT9Gwe+PeVb0VHHKVScUR/uTksm7mmHpXR4c87PabmYoZQt3dgBjNiu3/AqMoLeJHIv3eQvn6D3RGWYYhJhnJ2oKNIsUrO/rnlR+jgXkPCA9JuGdSoI7qhFkwtLGCazM9LuK6rnkad7KGK0THuAtEm3yjcHMh8Zr+pUThk3GqZKgvJ0+gXp9GnpUPq7RrPNVvgA9bYFdZGsDcCEZIOB");
                var decriptedData = this.DecriptDataForSsnAndPassport(data);
                var replacedDecriptedData = decriptedData.Replace('[', '{').Replace(']', '}');
                PassportDataResponse decriptedDataDeserialize = JsonConvert.DeserializeObject<PassportDataResponse>(replacedDecriptedData);


                if (decriptedDataDeserialize.Data.Full_name != null)
                {
                    passportWebServiceResponse.AVVRegistrationAddress = decriptedDataDeserialize.Data.AVVRegistrationAddress;
                    passportWebServiceResponse.Full_name = decriptedDataDeserialize.Data.Full_name;
                    passportWebServiceResponse.PNum = decriptedDataDeserialize.Data.PNum;
                }
                else
                {
                    passportWebServiceResponse.Status = "Empty";
                }

                return passportWebServiceResponse;

            }
            else
            {
                return passportWebServiceResponse;
            }
        }
        public async Task<SSNWebServiceResponse> GetCitizenBySSNTest(string ssn)
        {
            SSNWebServiceResponse ssnWebServiceResponse = new SSNWebServiceResponse();
            string data = string.Empty;

            if (ssn == "8009870498")
            {
                data = PersonResponseData.lilia;
            }
            if (ssn == "1501900412")
            {
                data = PersonResponseData.garegin;
            }

            if (!string.IsNullOrEmpty(data))
            {

                var decriptedData = this.DecriptDataForSsnAndPassport(data);


                if (Regex.Matches(decriptedData, "DocumentIdentifier").Count > 1)
                {
                    SSNDataResponseDocArray decriptedDataDeserialize = JsonConvert.DeserializeObject<SSNDataResponseDocArray>(decriptedData);

                    if (decriptedDataDeserialize.Passport_data.IsDead != null)
                    {
                        ssnWebServiceResponse.IsDead = decriptedDataDeserialize.Passport_data.IsDead;
                    }

                    var docs = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument;
                    foreach (var doc in docs)
                    {
                        if (doc.FirstName != null)
                        {
                            ssnWebServiceResponse.FirstName = doc.FirstName;
                        }
                        if (doc.LastName != null)
                        {
                            ssnWebServiceResponse.LastName = doc.LastName;
                        }
                        if (doc.MiddleName != null)
                        {
                            ssnWebServiceResponse.MiddleName = doc.MiddleName;
                        }
                        if (doc.EnglishFirstName != null)
                        {
                            ssnWebServiceResponse.FirstNameEn = doc.EnglishFirstName;
                        }
                        if (doc.EnglishLastName != null)
                        {
                            ssnWebServiceResponse.LastNameEn = doc.EnglishLastName;
                        }
                        if (doc.EnglishMiddleName != null)
                        {
                            ssnWebServiceResponse.MiddleNameEn = doc.EnglishMiddleName;
                        }
                        if (doc.Gender != null)
                        {
                            ssnWebServiceResponse.Gender = doc.Gender;
                        }
                        if (doc.BirthDate != null)
                        {
                            if (doc.BirthDate.Contains("00/"))
                            {
                                var replacedBirthDate = doc.BirthDate.Replace("00/", "01/");
                                ssnWebServiceResponse.BirthDate = replacedBirthDate;
                            }
                            else
                            {
                                ssnWebServiceResponse.BirthDate = doc.BirthDate;
                            }
                        }
                    }
                    ssnWebServiceResponse.Photo = decriptedDataDeserialize.Passport_data.Photo;
                    ssnWebServiceResponse.LocationCode = decriptedDataDeserialize.Passport_data.AVVRegistrationAddress.LocationCode;
                    ssnWebServiceResponse.Community = decriptedDataDeserialize.Passport_data.AVVRegistrationAddress.Community;
                }
                else
                {
                    SSNDataResponse decriptedDataDeserialize = JsonConvert.DeserializeObject<SSNDataResponse>(decriptedData);
                    if (decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.BirthDate.Contains("00/"))
                    {
                        var replacedBirthDate = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.BirthDate.Replace("00/", "01/");
                        ssnWebServiceResponse.BirthDate = replacedBirthDate;

                    }
                    else
                    {
                        ssnWebServiceResponse.BirthDate = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.BirthDate;
                    }
                    ssnWebServiceResponse.FirstName = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.FirstName;
                    ssnWebServiceResponse.LastName = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.LastName;
                    ssnWebServiceResponse.MiddleName = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.MiddleName;
                    ssnWebServiceResponse.Gender = decriptedDataDeserialize.Passport_data.AVVDocuments.AVVDocument.Gender;
                    ssnWebServiceResponse.IsDead = decriptedDataDeserialize.Passport_data.IsDead;
                    ssnWebServiceResponse.Photo = decriptedDataDeserialize.Passport_data.Photo;
                }
                return ssnWebServiceResponse;

            }
            return ssnWebServiceResponse;
        }


        public static class PersonResponseData
        {
            public const string
                lilia = "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",
                garegin = "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",
                lilia_passport = "HjB2CEbIti1CX+F1B/Ar9960XJJppkwdXoWOq/F39X+LCLH1qWnXQU3gjBNQwJd11EEGXWKxQB0fME96D5hx2zZ8SDi3Azoln6lXlsLwNyo9PN/uSGXnoG6w4DgNCp2sLC6bTD7yW3gSOah1HNlITW2FHkIBnsnpuPqgjYzPTFFO5zWsrVKaqKIpnTFrylyR4F7erULIUk8QNs2+aa2Sv6FBAD6U8xKmaExC/slCLVp9uoVxZv3XOQ1jWk5TuhhV48+3fnS3mUMfG+MYyUX9o8Hjc30qMsBRsOuuaRo+mv+mN6UfhXefriITKPKJICCBzBgX5HiN8Ov+nTaf8tbAiVYAxEr4iqZGBruqFPBx2kAsIsdRwbtxBdb5HdCKyUn19RjP7jMTSud2tuLEzt82gNUX2HRTD+5+U2AJVKpqaq021k9NOQMLmFrMmNwbXfgvdV29ERqD5Tpt9bG8T7xoH4WXs38v5a32T/cP7/HRIKCOr4wjI6MTC2e6ZuTtU3M+ZmzFOyei3bSxhmFj7tq6QFmspYTz2Oe/3kFkBM5jg0M=",
                garegin_passport = "HjB2CEbIti1CX+F1B/Ar97HKcFpRBTay5nZKeaJCYxjdoTSKrLeB4yKc3ZxsDW5LtgJg7j9M9kzR5m8XRTsK2hJCh2fXWmXQL8YmqHFvr8McIXdQWoHKNgDPbr6eGgVRt1e20k0h4h6j2SQzdh7f2ltwawuu/z8o4k5gUMULMN/LYXms0WT2MHVmBoJu0vXnLIKoARh25c58A0jrGkbY4/q/4ItBTTBBL3I7lWyqzAN/iQpLdlN0rBu3iH6IliofDECMEt4EKhMdAoKURd/2as2ex6gEd0zboB4iB5m/XLT0xw/LCBlU88EDv/snX/3jtRZQ5ibaEQWVGNxTRYrQFsED3JZDpiH3N5efR7ZsxpM2E3rIfzQ14VDNmz01t6X6dg6+GJke0tRUAuQpsBWKkoxSTuYdQ8mUhmg89QcKcBfK0CjUskWETx85ztaES3CGMkXDXO4gRTbY3wEuBx2F6ykSoGtqFBKagyRHgod698NwcufsmjN/tc5Ckl9ZonTG";
        }

    }
}
