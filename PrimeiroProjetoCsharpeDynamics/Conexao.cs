using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiroProjetoCsharpeDynamics
{
    public class Conexao // classe de conexão ao dynamics
    {
        private static CrmServiceClient crmServiceClientTreinamento; // armazena o conteúdo da conexão
        
        public CrmServiceClient ObterConexao() // método que devolve uma CrmServiceCliente
            {
                 //variável contendo a ConnectionString
                   var connectionStringCRM = @"AuthType=OAuth;
                   Username = emaildafacul@ulife.com.br;
                   Password = senha;
                   AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
                   RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
                   Url = https://org73fcfb64.crm2.dynamics.com/main.aspx;";

            if (crmServiceClientTreinamento == null)
            {
                try
                {
                   // System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    crmServiceClientTreinamento = new CrmServiceClient(connectionStringCRM);
                    if (crmServiceClientTreinamento.IsReady)
                    {
                        Console.WriteLine("Conexão bem-sucedida ao Dynamics 365.");
                        Console.ReadKey();

                    }
                    else
                    {
                        Console.WriteLine("Conexão mal sucedida.");
                        Console.ReadKey();

                    }
                }
                catch (AggregateException ae)
                {
                    foreach (var innerException in ae.InnerExceptions)
                    {
                        Console.WriteLine("Erro ao criar a conexão CRM: " + innerException.Message);
                        Console.ReadKey();
                    }
                }
            }
            return crmServiceClientTreinamento;
        }
    }  
}
