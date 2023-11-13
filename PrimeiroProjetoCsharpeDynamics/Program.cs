using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiroProjetoCsharpeDynamics
{
    public class Program
    {
        static void Main(string[] args)
        {
            var crmService = new Conexao().ObterConexao();
            DataModel dataModel = new DataModel();
            // dataModel.Create(crmService);
            dataModel.UpdateEntity(crmService, new Guid("b4cea450-cb0c-ea11-a813-000d3a1b1223"));
            dataModel.DeleteEntity(crmService, new Guid("48c32252-e679-ee11-8178-002248d30596"));
        }
        


        /*
        static void FetchXML(CrmServiceClient crmService)
        {
            try
            {
                if (crmService != null)
                {
                    string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='account'>
                                <attribute name='name' />
                                <attribute name='telephone1' />
                                <attribute name='accountid' />
                                <attribute name='emailaddress1' />
                                <order attribute='name' descending='false' />
                              </entity>
                            </fetch>";

                    EntityCollection colecao = crmService.RetrieveMultiple(new FetchExpression(query));

                    foreach (var item in colecao.Entities)
                    {
                        Console.WriteLine("Nome: " + item.GetAttributeValue<string>("name"));

                        if (item.Attributes.Contains("telephone1"))
                        {
                            Console.WriteLine("Telefone: " + item.GetAttributeValue<string>("telephone1"));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("A conexão com o Dynamics CRM não foi estabelecida com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao buscar os dados do Dynamics CRM: " + ex.Message);
            }

            Console.ReadKey();
        }
        */
    }
}
