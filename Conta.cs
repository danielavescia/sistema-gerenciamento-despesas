using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Conta
    {
        //atributos 
        protected int id; //atributo isual p/ escolher 
        protected string banco, agencia, numeroConta;
        protected double saldo;
        protected List<Transacao> minhastransacoes;

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
        public override string ToString()
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

            id = AtribuiId(minhasContas);

            Conta c = new Conta(id, banco, agencia, numeroConta, saldo); //criacao objeto conta

            minhasContas.Add(c); // adiciona na lista de contas do usuario

            return minhasContas;
        }

        //método que cria um id conforme a quantidade de contas existentes na lista de contas 
        public static int AtribuiId(List<Conta> minhasContas)
        {
            if (minhasContas == null || minhasContas.Count == 0)
            {
                return 1;
            }

            int ultimaId = minhasContas.Count;

            for (int i = 0; i < ultimaId; i++)
            {
                minhasContas[i].setId(i + 1);
            }

            return ultimaId + 1;
        }

        //método que remove a conta selecionada pelo usuario
        public static List<Conta> RemoverConta(List<Conta> minhasContas)
        {
            int contaRemover = 0;
            string contaInput;
            bool isValid = false;

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

            }

            else
            {
                do
                {
                    Console.WriteLine("Digite a Id da conta que deseja remover:");
                    contaInput = Console.ReadLine();
                    isValid = isInputValid(minhasContas, contaInput); //validacao do input

                } while (isValid);

                contaRemover = int.Parse(contaInput) - 1;  //id fornecida -1 == posicao na lista pq lista começa 0


                Console.WriteLine();
                Console.WriteLine(".---------------------------------------------------------------------------------------------------------------.");
                Console.WriteLine($"| CONTA: {minhasContas[contaRemover].getId()} - {minhasContas[contaRemover].getBanco()} REMOVIDA COM SUCESSO!  |");
                Console.WriteLine(".---------------------------------------------------------------------------------------------------------------.");
                Console.WriteLine();
                minhasContas.RemoveAt(contaRemover); //remove a conta da lista conforme
                AtribuiId(minhasContas);
            }

            return minhasContas;
        }

        //metodo que uni as transações da conta 2 na conta 1
        public static List<Conta> MesclarContas(List<Conta> minhasContas)
        {
            bool isValid = false;
            string idConta1 = " ", idConta2 = " ";
            int numeroConta1 = 0, numeroConta2 = 0;

            ImprimirContasAtivas(minhasContas);

            do
            {
                Console.WriteLine("Por favor, digite a ID da conta para a qual você deseja receber as transações:");
                idConta1 = (Console.ReadLine());
                isValid = isInputValid(minhasContas, idConta1); //validacao do input

            } while (isValid);

            do
            {
                Console.WriteLine("Agora, digite a segunda ID da conta para a qual você deseja unificar as transações:");
                idConta2 = Console.ReadLine();
                isValid = isInputValid(minhasContas, idConta1); //validacao do input

            } while (isValid);

            numeroConta1 = int.Parse(idConta1) - 1; //diminuir -1 para pegar o index correto na lista
            numeroConta2 = int.Parse(idConta2) - 1;

            //listas recebem as transações de cada conta especificada
            List<Transacao> lista1 = minhasContas[numeroConta1].getTransacoes();
            List<Transacao> lista2 = minhasContas[numeroConta2].getTransacoes();

            if (lista1 == null)
            {
                Console.WriteLine();
                Console.WriteLine(".--------------------------------------------------------.");
                Console.WriteLine($"|A conta do banco de ID: {idConta1} nao possui transacoes|");
                Console.WriteLine(".--------------------------------------------------------.");
                Console.WriteLine();

            }
            else if (lista2 == null)
            {
                Console.WriteLine();
                Console.WriteLine(".---------------------------------------------------------.");
                Console.WriteLine($"|A conta do banco de ID: {idConta2} nao possui transacoes|");
                Console.WriteLine(".---------------------------------------------------------.");
                Console.WriteLine();

            }
            else
            {
                List<Transacao> mesclada = lista1.Concat(lista2).ToList(); //as transacoes da lista 2 são unidas com as da lista 1 
                lista1 = mesclada.OrderBy(Transacao => Transacao.getData()).ToList(); //lista 1 receberá a lista unida e ordenada por data crescente

                minhasContas[numeroConta1].setTransacoes(lista1); //aqui a conta 1 recebera a uniao de transações da conta 1 e 2

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
            foreach (Conta c in minhasContas)
            {
                foreach (Transacao t in c.getTransacoes())
                {

                    if (t.getCategoria().Equals("Despesa"))
                    {
                        c.setSaldo(c.saldo - t.getValor());
                    }

                    if (t.getCategoria().Equals("Receita"))
                    {
                        c.setSaldo(c.saldo + t.getValor());
                    }
                }
            }
        }

        public static Transacao AdicionaTransacaoConta(List<Conta> minhasContas, Transacao t)
        {
            string idConta = " ";
            int numeroConta = 0;
            double saldo = 0;
            bool isValid = false;

            ImprimirContasAtivas(minhasContas);

            Console.WriteLine("Digite a id da conta que voce gostaria de adicionar esta transacao:");

            do
            {
                idConta = Console.ReadLine();
                isValid = isInputValid(minhasContas, idConta);

            } while (isValid);

            numeroConta = int.Parse(idConta) - 1;
            minhasContas[numeroConta].getTransacoes().Add(t); //adiciona transacao na conta desejada
            minhasContas[numeroConta].getTransacoes().OrderBy(t => t.getData()).ToList(); //ordena a lista de transacoes
            CalculaSaldoConta(minhasContas);
            t.setIdBanco(numeroConta); // adiciona Id banco

            Console.WriteLine(t.ToString);
            Console.WriteLine($"ESTA TRANSAÇÃO FOI ADICIONADA COM SUCESSO!");
            Console.WriteLine();
            Console.WriteLine(".--------------------------------------------------------------------------------------.");
            Console.WriteLine($"|Seu novo saldo na conta de ID: {idConta} é de R$ {minhasContas[numeroConta].getSaldo}|");
            Console.WriteLine(".--------------------------------------------------------------------------------------.");
            
            return t;

        }

        //método que verifica o input
        public static bool isInputValid(List<Conta> minhasContas, string input)
        {

            string regex = "[^0-9]"; // regex que permite qualquer caracter exceto numeros

            if (Regex.IsMatch(regex, input))
            {
                Console.WriteLine("Só são aceitos numeros!");
                return false;
            }

            else
            {
                int numberInput = int.Parse(input) - 1; // corrige para o indice da lista
                int intervaloMaximo = minhasContas.Count;

                if (numberInput < 0 || numberInput > intervaloMaximo)
                {
                    return true;
                }
            }

            return false;
        }

        public static void ImprimeSaldo(List<Conta> minhasContas)
        {
            double saldoTotal;
            CalculaSaldoConta(minhasContas);

            foreach (Conta c in minhasContas)
            {
  
                Console.WriteLine(".--------------------------------------------.");
                Console.WriteLine($"| ID: {c.getId()}                             |");
                Console.WriteLine($"| BANCO: {c.getBanco()}                       |");
                Console.WriteLine($"| SALDO: {c.getSaldo()}                       | ");
                Console.WriteLine(".--------------------------------------------.");
                Console.WriteLine();


            }
            saldoTotal = CalculaSaldoTotal(minhasContas);
            Console.WriteLine(".--------------------------------------------.");
            Console.WriteLine($"|SALDO TOTAL: {saldoTotal}                  | ");
            Console.WriteLine(".--------------------------------------------.");
            Console.WriteLine();
        }

        public static double  CalculaSaldoTotal(List<Conta> minhasContas)
        {
            double saldoTotal = 0;

            saldoTotal = minhasContas.Sum(Conta => Conta.getSaldo());

            return saldoTotal;
        }
    }
}


