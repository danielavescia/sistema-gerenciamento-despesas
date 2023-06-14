using System.ComponentModel;
using System.Globalization;
using System.Text;
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

        public Transacao() { }

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
            string tipo, categoria, descricao, regex = "^(?!\\s*$).+"; //regex = não pode ser string vazia;
            double valor;
            DateOnly data;

            try
            {
                //Dados  são obtidos por input para construir objeto Transacao
                Console.WriteLine($"{"\n"}Digite os dados solicitados abaixo:");

                data = RetornaData();

                tipo = RetornaTipoDespesa();

                Console.WriteLine($"{"\n"}Digite a CATEGORIA da transação:");
                categoria = Utilidades.RetornaString(regex);

                Console.WriteLine($"{"\n"}Digite o DESCRIÇÃO da transação:");
                descricao = Utilidades.RetornaString(regex);

                valor = RetornaValorTransacao();

                //objeto transacao é construido
                Transacao t = new(data, tipo, categoria, descricao, valor);

                return t;
            }
            catch (Exception) 
            {
                throw new NullReferenceException("Ocorreu um erro na criação da transação! Por favor, tente novamente!");
            }
        }

        public static List<Conta> EditarTransacao(List<Conta> minhasContas, Transacao minhaTransacao)
        {
            
            int conta, posicaoConta;
            List<Transacao> trans;
            StringBuilder sb;

            posicaoConta = RetornaIdTransacao(minhasContas,minhaTransacao);

          
            sb = Utilidades.RetornaMensagem("     EDITAR ÚLTIMA TRANSAÇÃO     ");
            Console.WriteLine(sb.ToString());
            Console.WriteLine(minhaTransacao.ToString());

            Console.WriteLine("Para alterar os dados da transacao acima:");
            minhaTransacao = CriarTransacao();
            
            Conta.ImprimirContasAtivas(minhasContas);
            Console.WriteLine($"{"\n"}Por favor, digite a ID da conta que irá receber as transações:");
            conta = Conta.RetornaNumeroConta(minhasContas);

            int posicaoUltimaTransacao = minhasContas[posicaoConta].GetTransacoes().Count()-1;
            minhasContas[posicaoConta].GetTransacoes().RemoveAt(posicaoUltimaTransacao);
            

            Conta.AdicionaTransacaoConta(minhasContas, minhaTransacao, conta); // adiciona a nova transição a conta desejada
            Conta.ImprimirContasAtivas(minhasContas);

            Console.WriteLine("TRANSACAO ALTERADA COM SUCESSO!");

            return minhasContas;

        }

        
        public static int RetornaIdTransacao(List<Conta> minhasContas, Transacao minhaTransacao) 
        {
            string descricao = minhaTransacao.GetDescricao();
            double valor= minhaTransacao.GetValor();
            int idBanco = 0;

            foreach (Conta c in minhasContas) 
            {
                foreach (Transacao t in c.GetTransacoes()) 
                {
                    if (t.GetDescricao().Equals(descricao) && (t.GetValor() == valor))
                    { 
                        return idBanco = c.GetId();
                    }
                }
            }
            return idBanco-1;
        }

        public static string RetornaTipoDespesa()
        {
            int opcaoTipo;
            string regex = "^(1|2)$";
            Console.WriteLine($"{"\n"}Digite o numero correspondete ao tipo de transacao que deseja cadastrar:");
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

        public static double RetornaValorTransacao()
        {
            string regex = @"^\d+(\.\d+)?$";
            double valorTransacao;

            Console.WriteLine($"{"\n"}Digite o valor da transaçao:");
            valorTransacao = Utilidades.RetornaDouble(regex);

            return valorTransacao;
        }

        public static DateOnly RetornaData()
        {
            string regexDia = @"^(0[1-9]|[1-2]\d|3[0-1])$"; // dia - só aceita numeros positivos com 2 digitos entre 1-31
            string regexMes = @"^(0[1-9]|1[0-2])$"; //mes - só aceita numeros positivos com 2 digitos entre 1-12
            string regexAno = @"^\d{4}$"; // ano só aceita numeros com 4 digitos

            Console.WriteLine($"{"\n"}Digite o DIA da transacao (FORMATO: XX):");
            int dia = Utilidades.RetornaInt(regexDia);

            Console.WriteLine($"{"\n"}Digite o MES da transacao (FORMATO: XX):");
            int mes = Utilidades.RetornaInt(regexMes);

            Console.WriteLine($"{"\n"}Digite o ANO da transacao (FORMATO: XXXX):");
            int ano = Utilidades.RetornaInt(regexAno);

            // objeto data é construido
            return new DateOnly(ano, mes, dia);
        } 
    }
}

