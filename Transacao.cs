using System.Globalization;
using System.Text.RegularExpressions;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Transacao
    {
        protected DateOnly data = new();
        protected string tipo, categoria, descricao;
        protected double valor;
        protected int idBanco;

        //propriedades de acesso
        public DateOnly GetData()
        {
            return data;
        }

        public void SetDate(DateOnly data)
        {
            this.data = data;
        }

        public string GetTipo()
        {
            return tipo;
        }

        public void SetTipo(string tipo)
        {
            this.tipo = tipo;
        }

        public string GetCategoria()
        {
            return categoria;
        }

        public void SetCategoria(string categoria)
        {
            this.categoria = categoria;
        }

        public string GetDescricao()
        {
            return descricao;
        }

        public void SetDescricao(string descricao)
        {
            this.descricao = descricao;
        }

        public double GetValor()
        {
            return valor;
        }

        public void SetValor(double valor)
        {
            this.valor = valor;
        }

        public Transacao(DateOnly data, string tipo, string categoria, string descricao, double valor)
        {
            this.data = data;
            this.tipo = tipo;
            this.categoria = categoria;
            this.descricao = descricao;
            this.valor = valor;

        }
        public override string ToString()
        {
            return
               $@"
               -------------------------------------------
                DADOS REFERENTES DA TRANSACAO:
               -------------------------------------------
               Data {GetData()}
               Tipo: {GetTipo()}
               Categoria: {GetCategoria()}
               Descricao: {GetDescricao()}
               Valor: {GetValor().ToString("N2")}
               --------------------------------------------";
        }

        public static Transacao CriarTransacao()
        {
            string tipo, categoria, descricao;
            double valor;
            DateOnly data;

            //Dados  são obtidos por input para construir objeto Transacao
            Console.WriteLine("Digite os dados solicitados abaixo:");
            Console.WriteLine();

            data = RetornaData();

            tipo = RetornaTipoDespesa();

            Console.WriteLine("Digite o categoria da transacao:");
            categoria = Console.ReadLine();

            Console.WriteLine("Digite o descricao da transacao:");
            descricao = Console.ReadLine();

            valor = RetornaValorDespesa();

            //objeto transacao é construido
            Transacao t = new(data, tipo, categoria, descricao, valor);

            t.ToString();

            return t;
        }

        public static List<Conta> EditarTransacao(List<Conta> minhasContas, Transacao t)
        {
            int conta;

            Console.WriteLine(t.ToString());

            Console.WriteLine("Para alterar os dados da transacao acima:");
            t = CriarTransacao();

            Console.WriteLine("TRANSACAO ALTERADA COM SUCESSO!");
            Console.WriteLine(t.ToString()); //imprime a transação alterada

            conta = Conta.RetornaNumeroConta(minhasContas);
            Conta.AdicionaTransacaoConta(minhasContas, t, conta); // adiciona a nova transição a conta desejada
            Conta.ImprimeSaldo(minhasContas);

            return minhasContas;

        }

        public static string RetornaTipoDespesa()
        {

            int opcaoTipo = 0;

            Console.WriteLine("Digite o numero correspondete ao tipo de transacao que deseja cadastrar:");
            Console.WriteLine("1 - Despesa       2 - Receita  ");

            //loop repete solicitacao de input enquanto o numero nao é 1 ou 2

            while (opcaoTipo == 1 || opcaoTipo == 2)
            {
                opcaoTipo = int.Parse(Console.ReadLine());
            }

            //verificao para designar categoria
            if (opcaoTipo == 1)
            {
                return "Despesa";
            }

            else
                return "Receita";

        }

        public static double RetornaValorDespesa()
        {
            string regex = @"^\d+(\.\d+)?$";
            string input;
            bool isValid;


            Console.WriteLine("Digite o valor da transaçao:");
            do
            {
                input = Console.ReadLine();
                isValid = IsRegexValid(input, regex);

            } while (!isValid);

            return double.Parse(input, CultureInfo.InvariantCulture);
        }

        public static DateOnly RetornaData()
        {
            string regexDia = @"^(0[1-9]|[1-2]\d|3[0-1])$"; // dia - só aceita numeros positivos com 2 digitos entre 1-31
            string regexMes = @"^(0[1-9]|1[0-2])$"; //mes - só aceita numeros positivos com 2 digitos entre 1-12
            string regexAno = @"^\d{4}$"; // ano só aceita numeros com 4 digitos


            Console.WriteLine("Digite o dia da transacao (formato: XX):");
            int dia = RetornaInt(regexDia);

            Console.WriteLine("Digite o mes da transacao (formato: XX):");
            int mes = RetornaInt(regexMes);

            Console.WriteLine("Digite o ano da transacao (formato: XXXX):");
            int ano = RetornaInt(regexAno);

            // objeto data é construido
            return new DateOnly(ano, mes, dia);
        }

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
    }
}

