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
    public class PrimeiraClasse
    {
        public void HelloWorld()
        {
            Console.WriteLine("Digite seu nome: ");
            string nome = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Hello " + nome);
            Console.ReadKey();
        }
        
    }
}
