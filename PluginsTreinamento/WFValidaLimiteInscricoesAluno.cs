using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;

namespace PluginsTreinamento
{
    public class WFValidaLimiteInscricoesAluno : CodeActivity
    {
        #region Parametros
        // recebe o usuário do contexto
        [Input("Usuario")]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> usuarioEntrada { get; set; }

        // recebe o contexto
        [Input("AlunoXCurso")]
        [ReferenceTarget("curso_cursoxalunos")]
        public InArgument<EntityReference> RegistroContexto { get; set; }

        [Output("Saida")]
        public OutArgument<string> saida { get; set; }
        #endregion
        protected override void Execute(CodeActivityContext executionContext)
        {
            //Create the context            
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService trace = executionContext.GetExtension<ITracingService>();

            // informação para o Log de Rastreamento de Plugin
            trace.Trace("curso_alunoxcursodisponvel: " + context.PrimaryEntityId);

            // declaro variável com o Guid da entidade primária em uso
            Guid guidAlunoXCurso = context.PrimaryEntityId;

            // informação para o Log de Rastreamento de Plugin
            trace.Trace("guidAlunoXCurso: " + guidAlunoXCurso);

            String fetchAlunoXCursos = "<fetch distinct='false' mapping='logical' output-format='xml-platform' version='1.0'>";
            fetchAlunoXCursos += "<entity name='curso_cursoxalunos' >";
            fetchAlunoXCursos += "<attribute name='curso_cursoxalunosid' />";
            fetchAlunoXCursos += "<attribute name='curso_name' />";
            fetchAlunoXCursos += "<attribute name='curso_emcurso' />";
            fetchAlunoXCursos += "<attribute name='createdon' />";
            fetchAlunoXCursos += "<attribute name='curso_alunos' />";
            fetchAlunoXCursos += "<order descending= 'false' attribute = 'curso_name' />";
            fetchAlunoXCursos += "<filter type= 'and' >";
            fetchAlunoXCursos += "<condition attribute = 'curso_cursoxalunosid' value = '" + guidAlunoXCurso + "' uitype = 'curso_cursoxalunos'  operator= 'eq' />";
            fetchAlunoXCursos += "</filter> ";
            fetchAlunoXCursos += "</entity>";
            fetchAlunoXCursos += "</fetch> ";
            trace.Trace("fetchAlunoXCursos: " + fetchAlunoXCursos);

            var entityAlunoXCursos = service.RetrieveMultiple(new FetchExpression(fetchAlunoXCursos));
            trace.Trace("entityAlunoXCursos: " + entityAlunoXCursos.Entities.Count);

            Guid guidAluno = Guid.Empty;
            foreach (var item in entityAlunoXCursos.Entities)
            {
                string nomeCurso = item.Attributes["curso_name"].ToString();
                trace.Trace("nomeCurso: " + nomeCurso);

                var entityAluno = ((EntityReference)item.Attributes["curso_alunos"]).Id;
                guidAluno = ((EntityReference)item.Attributes["curso_alunos"]).Id;
                trace.Trace("entityAluno: " + entityAluno);
            }

            String fetchAlunoXCursosQtde = "<fetch distinct='false' mapping ='logical' output-format ='xml-platform' version = '1.0'>";
            fetchAlunoXCursosQtde += "<entity name ='curso_cursoxalunos'>";
            fetchAlunoXCursosQtde += "<attribute name= 'curso_cursoxalunosid' />";
            fetchAlunoXCursosQtde += "<attribute name= 'curso_name' />";
            fetchAlunoXCursosQtde += "<attribute name= 'curso_alunos' />";
            fetchAlunoXCursosQtde += "<attribute name= 'createdon' />";
            fetchAlunoXCursosQtde += "<order descending= 'false' attribute = 'curso_name' />";
            fetchAlunoXCursosQtde += "<filter type= 'and' >";
            fetchAlunoXCursosQtde += "<condition attribute= 'curso_alunos' value = '" + guidAluno + "' operator= 'eq' />";
            fetchAlunoXCursosQtde += "</filter>";
            fetchAlunoXCursosQtde += "</entity>";
            fetchAlunoXCursosQtde += "</fetch>";
            trace.Trace("fetchAlunoXCursosQtde: " + fetchAlunoXCursosQtde);
            var entityAlunoXCursosQtde = service.RetrieveMultiple(new FetchExpression(fetchAlunoXCursosQtde));
            trace.Trace("entityAlunoXCursosQtde: " + entityAlunoXCursosQtde.Entities.Count);
            if (entityAlunoXCursosQtde.Entities.Count > 2)
            {
                saida.Set(executionContext, "Aluno excedeu limite de cursos ativos!");
                trace.Trace("Aluno excedeu limite de cursos ativos!");
                throw new InvalidPluginExecutionException("Aluno excedeu limite de cursos ativos!");
            }
        }
    }
}
