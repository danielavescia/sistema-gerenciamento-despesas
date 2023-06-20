using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Utilidades

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
                    throw new FormatException("Valor inserido é inválido! Digite novamente um número inteiro:");
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
                input = Console.ReadLine();
                isValid = Regex.IsMatch(input, regex);

                if (!isValid)
                {
                    throw new FormatException("Valor inválido! Digite novamente o valor desejado:");
                }

            } while (!isValid);

            return double.Parse(input, CultureInfo.InvariantCulture);

        }

        //recebe uma string verifica se ela é válida conforme o padrão de regex determinado e retorna uma string
        public static string RetornaString(string regex)
        {
            string input;
            bool isValid;

            do
            {
                input = (Console.ReadLine());
                isValid = Regex.IsMatch(input, regex);

                if (!isValid)
                {
                    throw new FormatException("Formato inválido! Digite novamente:");
                }

            } while (!isValid);

            return input;

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
            
            StringBuilder sb = new();

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

        public static StringBuilder RetornaMenu(string [] frases)
        {
            StringBuilder sb = new();

            if (frases == null)
            {
                throw new ArgumentNullException("Adicione strings ao array para imprimir as mensagens corretamente!");
            }

            else
            {
               
                int indiceMaiorPalavra = RetornaMaiorString(frases);
                int larguraMenu = frases[indiceMaiorPalavra].Length;

                sb.AppendLine($".{new string('_', larguraMenu + 2)}.");
                sb.AppendFormat("| {0,-" + larguraMenu + "} |\n", frases[0].Trim());
                sb.AppendLine($".{new string('_', larguraMenu + 2)}.");

                for (int i = 1; i < frases.Length - 1; i++)
                {
                    sb.AppendFormat("| {0,-" + larguraMenu + "} |\n", frases[i].Trim());
                }
                sb.AppendLine($"|{new string('_', larguraMenu + 2)}|");
                sb.AppendLine();
                sb.AppendLine(frases[frases.Length - 1]);
            }
            return sb;

        }

        public static int RetornaMaiorString(string[] frases) 
        {
            int indiceMaiorPalavra;

              string maiorPalavra = frases[0] ;

                foreach (string s in frases)
                {
                    if (s.Length > maiorPalavra.Length)
                        maiorPalavra = s;
                }

                indiceMaiorPalavra = Array.IndexOf(frases, maiorPalavra);
            
            return indiceMaiorPalavra;
        }
    }
}
