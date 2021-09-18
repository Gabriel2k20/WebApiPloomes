using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//using WebAPI.Models;

namespace WebAPI.Functions
{
    public static class Functions
    {
        //Função para formatar JSON
        public static string getJson(this object cep)
        {
            try
            {
                string url = string.Format($"https://viacep.com.br/ws/{cep}/json/");

                RestClient restClient = new RestClient(url);
                RestRequest restRequest = new RestRequest(Method.GET);
                IRestResponse restResponse = restClient.Execute(restRequest);
                string res = restResponse.Content;
                string resp = res.Substring(0, res.IndexOf("  \"complemento") - 2);
                string resp1 = resp + res.Substring(res.IndexOf("  \"bairro") - 2);
                resp = resp1.Substring(0, resp1.IndexOf("  \"ibge") - 2) ;
                resp.Replace("-", "");
                resp += ", \n  \"alunoes\": [], \n  \"instituicoes\": [] \n}";

                return resp;
            }
            catch { return null; }
        }

    }
}
