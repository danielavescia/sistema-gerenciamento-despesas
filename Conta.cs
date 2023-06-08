using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Conta
    {
        //atributos 
        private int id;
        private string banco, agencia, numeroConta;
        private double saldo;
        private List<Transacao> minhastransacoes;

        // Propriedades (getters e setters) 
        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public string getBanco()
        {
            return banco;
        }

        public void setBanco(string banco)
        {
            this.banco = banco;
        }

        public string getAgencia()
        {
            return agencia;
        }

        public void setAgencia(string agencia)
        {
            this.agencia = agencia;
        }

        public string getNumeroConta()
        {
            return numeroConta;
        }

        public void setNumeroConta(string numeroConta)
        {
            this.numeroConta = numeroConta;
        }

        public double getSaldo()
        {
            return saldo;
        }

        public void setSaldo(double saldo)
        {
            this.saldo = saldo;
        }

        public List<Transacao> getTransacoes()
        {
            return minhastransacoes;
        }

        public void setTransacoes(List<Transacao> minhastransacoes)
        {
            this.minhastransacoes = minhastransacoes;
        }

        //Construtor objeto Conta
        public Conta(int id, string banco, string agencia, string numeroConta, double saldo)
        {
            this.id = id;
            this.banco = banco;
            this.agencia = agencia;
            this.numeroConta = numeroConta;
            this.saldo = saldo;
            List<Transacao> t = new List<Transacao>(); 
        }

        //override ToString para retornar os dados da Conta
        public  override string ToString()
        {
            return
               $@"
               CONTA ID: {getId()}
               Banco: {getBanco()}
               Agencia: {getAgencia()}
               Conta: {getNumeroConta()}
               Saldo: {getSaldo()}
                ";
        }

        //método que cria Contas e adiciona a uma Lista de Contas
        public static List<Conta> CriarConta(List<Conta> minhasContas)
        {

            String banco = null, agencia = null, numeroConta = null;
            double saldo = 0;
            int id = 0;
            List<Transacao> minhasTransacoes;

            //input usuario p/ criacao objeto
            Console.WriteLine();
            Console.WriteLine("Para criar uma conta adicione as informacoes solicitadas abaixo:");
            Console.WriteLine();

            Console.WriteLine("Digite o nome do seu banco:");
            banco = Console.ReadLine();

            Console.WriteLine("Agencia:");
            agencia = Console.ReadLine();

            Console.WriteLine("Numero da conta:");
            numeroConta = Console.ReadLine();

            id = criaId(minhasContas);

            Conta c = new Conta(id, banco, agencia, numeroConta, saldo); //criacao objeto conta

            minhasContas.Add(c); // adiciona na lista de contas do usuario

            return minhasContas;

        }

        //método que cria um id conforme a quantidade de contas existentes na lista de contas 
        public static int criaId(List<Conta> minhasContas)
        {
            int id;

            if (minhasContas == null)
            {
                return id = 1;
            }

            else
            {
                id = minhasContas.Count + 1; 
            }
            return id;
        }

        //método que remove a conta selecionada pelo usuario
        public static List<Conta> RemoverConta(List<Conta> minhasContas)
        {
            ImprimirContasAtivas(minhasContas); // Imprime as contas ativas
            
            Console.WriteLine();
            Console.WriteLine(".------------------------------------------------------------------------------------------.");
            Console.WriteLine("|LEMBRE-SE QUE AO REMOVER SUA CONTA, VOCE IRA PERDER TODAS AS TRANSACOES RELACIONADAS A ELA|");
            Console.WriteLine(".------------------------------------------------------------------------------------------.");
            Console.WriteLine();

            if (minhasContas == null)
            {
                Console.WriteLine();
                Console.WriteLine(".----------------------------------------.");
                Console.WriteLine("|VOCE NAO POSSUI NENHUMA CONTA CADASTRADA|");
                Console.WriteLine(".----------------------------------------.");
                Console.WriteLine();

            } else {

                Console.WriteLine("Digite a Id da conta que deseja remover:");
                int contaRemover = int.Parse(Console.ReadLine()) - 1; //id fornecida -1 == posicao na lista pq lista começa 0
                Console.WriteLine();

             
                Console.WriteLine($"| Conta: {minhasContas[contaRemover].getId()} - {minhasContas[contaRemover].getBanco()} removida com sucesso!");
                Console.WriteLine();
                minhasContas.RemoveAt(contaRemover); //remove a conta da lista conforme
            }

            return minhasContas;
        }

        //metodo que uni as transações da conta 2 na conta 1
        public static List<Conta> MesclarContas(List<Conta> minhasContas) 
        {
            ImprimirContasAtivas(minhasContas);
           
            Console.WriteLine("Digite a id da conta que recebera as transacoes da outra conta:");
            int idConta1 = int.Parse(Console.ReadLine()); 

            Console.WriteLine("Digite a id da conta que deseja unificar as transacoes com a primeira:");
            int idConta2 = int.Parse(Console.ReadLine());

            //listas recebem as transações de cada conta especificada
            List<Transacao> lista1 = minhasContas[idConta1 - 1].getTransacoes(); 
            List<Transacao> lista2 = minhasContas[idConta2 - 1].getTransacoes(); //diminuir -1 para pegar o index correto na lista

            if (lista1 == null)
            {
                Console.WriteLine();
                Console.WriteLine(".--------------------------------------------------------.");
                Console.WriteLine($"|A conta do banco de ID: {idConta1} nao possui transacoes|");
                Console.WriteLine(".--------------------------------------------------------.");
                Console.WriteLine();

            } else if (lista2 == null) {
                Console.WriteLine();
                Console.WriteLine(".---------------------------------------------------------.");
                Console.WriteLine($"|A conta do banco de ID: {idConta2} nao possui transacoes|");
                Console.WriteLine(".---------------------------------------------------------.");
                Console.WriteLine();

            } else {
                List<Transacao> mesclada = lista1.Concat(lista2).ToList(); //as transacoes da lista 2 são unidas com as da lista 1 
                lista1 = mesclada.OrderBy(Transacao => Transacao.getData()).ToList(); //lista 1 receberá a lista unida e ordenada por data crescente

                minhasContas[idConta1].setTransacoes(lista1); //aqui a conta 1 recebera a uniao de transações da conta 1 e 2
            }

            return minhasContas;
        }

        public static void ImprimirContasAtivas(List<Conta> minhasContas)
        {
            Console.WriteLine("Estas são suas contas ativas:");

            foreach (Conta c in minhasContas)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public static void CalculaSaldoConta(List<Conta> minhasContas)
        {

            foreach (Conta c in minhasContas) {

                foreach (Transacao t in c.getTransacoes()) {

                    if (t.getCategoria().Equals("Despesa"))
                    {

                        c.saldo = Math.Abs(c.saldo - t.getValor());
                    }

                    else {

                        c.saldo =c.saldo + t.getValor();
                    }

                }       
            }
        }
    }
}


