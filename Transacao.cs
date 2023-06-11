using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Transacao
    {
        protected DateOnly data = new DateOnly();
        protected string tipo, categoria, descricao;
        protected double valor;
        protected int idBanco;

        //propriedades de acesso
        public DateOnly getData() 
        { 
            return data;
        }

        public void setDate(DateOnly data) 
        { 
            this.data = data;
        }

        public string getTipo() 
        {
            return tipo;
        }

        public void setTipo(string tipo) 
        { 
            this.tipo = tipo;
        }

        public string getCategoria() 
        { 
            return categoria;
        }

        public void setCategoria(string categoria) 
        { 
            this.categoria = categoria;
        }

        public string getDescricao() 
        {
            return descricao;
        }

        public void setDescricao(string descricao) 
        { 
            this.descricao = descricao;
        }

        public double getValor() 
        {
            return valor;
        }

        public void setValor(double valor) 
        {
            this.valor = valor;
        }

        public int getidBanco() 
        {
            return idBanco;       
        }

        public void setIdBanco(int idBanco) 
        {
            this.idBanco = idBanco;
        }

        public Transacao(DateOnly data, string tipo, string categoria, string descricao, double valor) 
        {
            this.data= data;
            this.tipo = tipo;
            this.categoria = categoria;
            this.descricao = descricao;
            this.valor = valor;
            idBanco = 0;
        }
        public override string ToString()
        {
            return
               $@"
               -------------------------------------------
                DADOS REFERENTES DA TRANSACAO:
               -------------------------------------------
               Data {getData().ToString()}
               Tipo: {getTipo().ToString()}
               Categoria: {getCategoria().ToString()}
               Descricao: {getDescricao().ToString()}
               Valor: {getValor().ToString()}
               --------------------------------------------
                ";
        }

        public Transacao CriarTransacao() 
        {

            string tipo = null, categoria = null, descricao = null;
            double valor =0;
            int dia = 0, mes = 0, ano = 0;
            Transacao t = null;


            //Dados  são obtidos por input para construir objeto Transacao
            Console.WriteLine("Digite os dados solicitados abaixo:");
            
            Console.WriteLine("Digite o dia da transacao:");
            dia = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o mes da transacao:");
            mes = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o ano da transacao:");
            ano = int.Parse(Console.ReadLine());

            // objeto data é construido
            DateOnly data = new DateOnly(dia, mes, ano);

            tipo = RetornaTipoDespesa();

            Console.WriteLine("Digite o categoria da transacao:");
            categoria = Console.ReadLine();

            Console.WriteLine("Digite o categoria da transacao:");
            descricao = Console.ReadLine();

            valor = RetornaValorDespesa();

            //objeto transacao é construido
            t = new Transacao(data, tipo, categoria, descricao, valor);

            return t;
        }

        public List<Conta> EditarTransacao(List<Conta> minhasContas, Transacao t) 
        {
            int conta;

            Console.WriteLine(t.ToString());

            Console.WriteLine("Para alterar os dados da transacao acima:");
            t = CriarTransacao();

            Console.WriteLine("TRANSACAO ALTERADA COM SUCESSO!");
            Console.WriteLine(t.ToString()); //imprime a transação alterada

            conta = Conta.CapturaNumeroConta(minhasContas);
            Conta.AdicionaTransacaoConta(minhasContas, t, conta); // adiciona a nova transição a alguma outra conta
            Conta.ImprimeSaldo(minhasContas);

            return minhasContas;

        }

        public string RetornaTipoDespesa() 
        {

            int opcaoTipo = 0;
            string tipo = " ";

            Console.WriteLine("Digite o numero correspondete ao tipo de transacao que deseja cadastrar:");
            Console.WriteLine("1 - Despesa       2 - Receita  ");

            //loop repete solicitacao de input enquanto o numero nao é 1 ou 2
            do
            {
                opcaoTipo = int.Parse(Console.ReadLine());

            } while (opcaoTipo != 1 || opcaoTipo != 2); 

            //verificao para designar categoria
            if (opcaoTipo == 1)
            {
                return tipo = "Despesa";
            }

            if (opcaoTipo == 2)
            {
                return tipo = "Receita";
            }

            return tipo;
        }


        public double RetornaValorDespesa() 
        {
            string regex = @"^[0 - 9] + (\.[0 - 9]+)?$";
            string input;
            bool isValid = true;
            double valor = 0;

            Console.WriteLine("Digite o valor da transaçao:");

            do
            {
                input = Console.ReadLine();
                isValid = Regex.IsMatch(input, regex);

            } while (!isValid);

            return valor = int.Parse(input);
        }
    }
}
