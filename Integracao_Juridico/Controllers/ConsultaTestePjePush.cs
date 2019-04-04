﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsultaPushPjeService;
using Integracao_Juridico.Models;
using Newtonsoft.Json;

namespace Integracao_Juridico.Controllers
{
    public class ConsultaTestePjePush
    {
        private const string UrlServico = @"https://wwwh.cnj.jus.br/pjemni-2x/intercomunicacao?wsdl";
        private const string UserName = "alexandre.trapp@hotmail.com";
        private const string Password = "15303@le371030";

        public string Consultar()
        {
            //response.Start();

            //while (!response.IsCompleted)
            //    response.Wait();

            //if (!response.IsCompletedSuccessfully)
            //    throw new Exception("Erro na consulta do processo: " + response.Status + response.Exception);

            var response = RetornarResponse();
            return JsonConvert.SerializeObject(response);
        }

        public string Consultar(string idStr)
        {
            long id = Convert.ToInt64(idStr);
            var response = RetornarResponse();

            if (id > response.Count)
                return string.Empty;
            
            var processo1 = from p in response
                            where p.IdProcesso == id -1
                            select p.ResponseProcesso;

            return string.Format("Consulta processo - mensagem: " + processo1.First().mensagem + "{0}" +
                                 "Sucesso? - " + (processo1.First().sucesso ? "Sim" : "Não") + "{0}" +
                                 "Dados básicos - numero: " + processo1.First().processo.dadosBasicos.numero + "{0}" +
                                 "Data ajuizamento: " + processo1.First().processo.dadosBasicos.dataAjuizamento + "{0}" +
                                 "Valor causa: " + processo1.First().processo.dadosBasicos.valorCausa, 
                                 Environment.NewLine); 
        }

        private List<DadosProcessosLayout> RetornarResponse()
        {
            //var bindingConexao = new BasicHttpBinding
            //{
            //    MaxReceivedMessageSize = 2147483647,
            //    SendTimeout = TimeSpan.MaxValue
            //};
            //bindingConexao.Security.Mode = BasicHttpSecurityMode.None;
            //bindingConexao.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            //var endpoint = new EndpointAddress(new Uri(UrlServico));
            //var client = new servicointercomunicacao222Client(bindingConexao, endpoint);

            //client.ClientCredentials.UserName.UserName = UserName;
            //client.ClientCredentials.UserName.Password = Password;

            //using (new OperationContextScope(client.InnerChannel))
            //{
            //    var request = GetRequest();
            //    var response = client.consultarProcessoAsync(request);
            //    if (response == null)
            //        throw new Exception("response nulo");

            //    return response;
            //}

            var response = new List<DadosProcessosLayout>
            {
                new DadosProcessosLayout
                {
                    IdProcesso = 1,
                    ResponseProcesso =  new consultarProcessoResponse()
                    {
                        mensagem = "Consulta processo 1 efetuada com sucesso",
                        sucesso = true,
                        processo = new tipoProcessoJudicial
                        {
                            dadosBasicos = new tipoCabecalhoProcesso
                            {
                                numero = "0599794-97.1991.8.02.0008",
                                dataAjuizamento = "20/03/2019",
                                valorCausa = 555d
                            }
                        }
                    }
                },

                new DadosProcessosLayout
                {
                    IdProcesso = 2,
                    ResponseProcesso = new consultarProcessoResponse()
                    {
                        mensagem = "Consulta processo 2 efetuada com sucesso",
                        sucesso = true,
                        processo = new tipoProcessoJudicial
                        {
                            dadosBasicos = new tipoCabecalhoProcesso
                            {
                                numero = "0000001-11.2222.3.44.5555",
                                dataAjuizamento = "25/03/2019",
                                valorCausa = 1000d
                            }
                        }
                    }
                }
            };

            return response;
        }
    }
}
