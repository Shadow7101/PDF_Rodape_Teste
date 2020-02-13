using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste01
{
    class Program
    {
        static void Main(string[] args)
        {
            var pdf = new GeraPdfHtml();


            string titulo = "PSTI - Política de Segurança da Tecnologia da Informação";
            string nome = "Fernando Camillo Neto";
            string cpf = "20701524057";
            string hash = "0CE1D93E5B68F3C7DE0B2243B015512730974235A63FEFD4D20B2F1C4985EBC0C517EBC587B43A21A0A17D0846E2436656349777BD2588B4B70FC1EFF0044632";

            pdf.Gerar(titulo, nome, cpf, hash);
        }
    }
}
