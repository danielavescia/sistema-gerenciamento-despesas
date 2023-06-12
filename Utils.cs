using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Utils
    {
        //recebe uma string verifica se ela é válida conforme o padrão de regex determinado e retorna um int
        public static int RetornaInt(string regex)
        {
            string input;
            bool isValid;

            do
            {
                input = (Console.ReadLine());
                isValid = Regex.IsMatch(input, regex);

                if (!isValid)
                {
                    Console.WriteLine("Formato incorreto! Tente novamente...");
                }

            } while (!isValid);

            return int.Parse(input);

        }

        //recebe uma string verifica se ela é válida conforme o padrão de regex determinado e retorna um double
        public static double RetornaDouble(string regex)
        {
            string input;
            bool isValid;

            do
            {
                input = (Console.ReadLine());
                isValid = Regex.IsMatch(input, regex);

                if (!isValid)
                {
                    Console.WriteLine("Formato incorreto! Tente novamente...");
                }

            } while (!isValid);

            return double.Parse(input);

        }

        //recebe um padrão de regex e uma string(input) para verificar se o input está no formato solicitado.
        public static bool IsRegexValid(string regex, string input)
        {

            if (Regex.IsMatch(regex, input))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //constroi as mensagen sdo sistema conforme o parametro inserido
        public static StringBuilder RetornaMensagem(String mensagem) { 

            StringBuilder sb = new StringBuilder();
           
            sb.AppendLine();
            sb.Append(".");
            sb.Append('-', mensagem.Length+2);
            sb.Append(".");
            sb.AppendFormat("\n| {0} |\n", mensagem);
            sb.Append(".");
            sb.Append('-', mensagem.Length+2);
            sb.Append(".");
            sb.AppendLine();

            return sb;
        }

    }
}
