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
        public DateOnly Data
        {
            get { return data; }
            set { data = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public double Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        //método construtor default
        public Transacao() { }

        //método construtor com atributos
        public Transacao(DateOnly data, string tipo, string categoria, string descricao, double valor)
        {
            this.data = data;
            this.tipo = tipo;
            this.categoria = categoria;
            this.descricao = descricao;
            this.valor = valor;

        }

        //método que retorna uma representação em formato de string dos dados da Transação.
        public override string ToString()
        {
            return
               $@"
               -------------------------------------------
                DADOS REFERENTES DA TRANSACAO:
               -------------------------------------------
               Data {Data}
               Tipo: {Tipo}
               Categoria: {Categoria}
               Descricao: {Descricao}
               Valor: {Valor.ToString("N2")}
               --------------------------------------------";
        }

        //método que cria um objeto Transação
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

                tipo = RetornaTipoTransacao();

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

        //Método responsável por realizar edição da ultima transação cadastrada 
        public static List<Conta> EditarTransacao(List<Conta> minhasContas, Transacao minhaTransacao)
        {
            
            int conta, posicaoConta;
            List<Transacao> trans;
            StringBuilder sb;
            Transacao ultimaTransacao;

            posicaoConta = RetornaIdTransacao(minhasContas,minhaTransacao); 
            minhasContas[posicaoConta].GetTransacoes().Remove(minhaTransacao);

            sb = Utilidades.RetornaMensagem("     EDITAR ÚLTIMA TRANSAÇÃO     ");
            Console.WriteLine(sb.ToString());
            Console.WriteLine(minhaTransacao.ToString());

            Console.WriteLine("Para alterar os dados da transacao acima:");
            minhaTransacao = CriarTransacao();
            minhasContas[posicaoConta].SetTransacao(minhaTransacao);

            Conta.ImprimirContasAtivas(minhasContas);

            Console.WriteLine("TRANSACAO ALTERADA COM SUCESSO!");

            return minhasContas;

        }

        //metodo que pesquisa que permite identificar a localização de uma transação especifica numa das Contas
        public static int RetornaIdTransacao(List<Conta> minhasContas, Transacao minhaTransacao) 
        {
            string descricao = minhaTransacao.Descricao;
            double valor= minhaTransacao.Valor;
            int idBanco = 0;

            foreach (Conta c in minhasContas) 
            {
                foreach (Transacao t in c.GetTransacoes()) 
                {
                    if (t.Descricao.Equals(descricao) && (t.Valor == valor))
                    { 
                        return idBanco = c.GetId();
                    }
                }
            }
            return idBanco-1;
        }

        //Método que por meio do input determina qual o tipo da transação 
        public static string RetornaTipoTransacao()
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
                    return RetornaTipoTransacao();
            }
            
        }
        //Método que retorna um valor válido como valor da transação
        public static double RetornaValorTransacao()
        {
            string regex = @"^\d+(\.\d+)?$";
            double valorTransacao;

            Console.WriteLine($"{"\n"}Digite o valor da transaçao:");
            valorTransacao = Utilidades.RetornaDouble(regex);

            return valorTransacao;
        }

        //Método que retorna uma data válida como valor da transação
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

