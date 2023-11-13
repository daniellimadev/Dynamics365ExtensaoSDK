using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginsTreinamento
{
    public class PluginAccountPostOperation : IPlugin
    {
        // método requerido para execução do Plugin recebendo como parâmetro os dados do provedor de serviço O referências
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                // Variável contendo o contexto da execução
                IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                // Variável contendo o Service Factory da Organização
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                // Variável contendo o Service Admin que estabele os serviços de conexão com o Dataverse
                IOrganizationService serviceAdmin = serviceFactory.CreateOrganizationService(null);
                // Variável do Trace que armazena informações de LOG
                ITracingService trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

                // Verifica se contém dados para o destino e se corresponde a uma Entity
                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
                {
                    // Variável do tipo Entity herdando a entidade do contexto
                    Entity entidadeContexto = (Entity)context.InputParameters["Target"];

                    if (!entidadeContexto.Contains("websiteurl"))
                    {
                        throw new InvalidPluginExecutionException("Campo website principal é obrigatório");
                    }

                    // variavel para nova entidade TASK vazia
                    var Task = new Entity("task");

                    // atribuição dos atributos para novo registro da entidade TASK
                    Task.Attributes["ownerid"] = new EntityReference("systemuser", context.UserId);
                    Task.Attributes["regardingobjectid"] = new EntityReference("account", context.PrimaryEntityId);
                    Task.Attributes["subject"] = "Visite nosso site " + entidadeContexto["websiteurl"];
                    Task.Attributes["description"] = "TASK criada via plugin post operation";

                    serviceAdmin.Create(Task); //execute o método create para criar a entidade TASK
                }
            }
            catch (InvalidPluginExecutionException ex)
            {
                throw new InvalidPluginExecutionException("Erro ocorrido: " + ex.Message);
            }
        }
    }
}
