using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Xsl;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Transacao
    {
        protected DateOnly data = new();
        protected string tipo, categoria, descricao;
        protected double valor;
      
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

            try
            {
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
            catch (Exception e) 
            {
                throw new Exception("Ocorreu um erro...");
            }
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
            int opcaoTipo;
            string regex = "^(1|2)$";
            Console.WriteLine("Digite o numero correspondete ao tipo de transacao que deseja cadastrar:");
            Console.WriteLine("1 - Despesa       2 - Receita  ");

            //loop repete solicitacao de input enquanto o numero nao é 1 ou 2

            opcaoTipo = Utilidades.RetornaInt(regex);

            //Conforme o input um case é de3terminado
            switch (opcaoTipo)
            {

                case 1:

                    return "Despesa";

                case 2:

                    return "Receita";

                default:

                    Console.WriteLine("Esta opção não é válida!");
                    return RetornaTipoDespesa();
            }
            
        }
        
 

        public static double RetornaValorDespesa()
        {
            string regex = @"^\d+(\.\d+)?$";
            double despesa;

            Console.WriteLine("Digite o valor da transaçao:");
            despesa = Utilidades.RetornaDouble(regex);

            return despesa;
        }

        public static DateOnly RetornaData()
        {
            string regexDia = @"^(0[1-9]|[1-2]\d|3[0-1])$"; // dia - só aceita numeros positivos com 2 digitos entre 1-31
            string regexMes = @"^(0[1-9]|1[0-2])$"; //mes - só aceita numeros positivos com 2 digitos entre 1-12
            string regexAno = @"^\d{4}$"; // ano só aceita numeros com 4 digitos


            Console.WriteLine("Digite o dia da transacao (formato: XX):");
            int dia = Utilidades.RetornaInt(regexDia);

            Console.WriteLine("Digite o mes da transacao (formato: XX):");
            int mes = Utilidades.RetornaInt(regexMes);

            Console.WriteLine("Digite o ano da transacao (formato: XXXX):");
            int ano = Utilidades.RetornaInt(regexAno);

            // objeto data é construido
            return new DateOnly(ano, mes, dia);
        }

       
    }
}

