using System.Text.RegularExpressions;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Conta
    {
        //atributos 
        protected int id; //atributo visual p/ identificar as contas em situações que envolvem escolhas
        protected string banco, agencia, numeroConta;
        protected double saldo; // relacionado ao saldo da conta
        protected double saldoTotal; // relacionado ao saldo de todas as contas
        protected List<Transacao> minhastransacoes = new();
        protected Transacao trans;


        // Propriedades (getters e setters) 
        public int GetId()
        {
            return id;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public string GetBanco()
        {
            return banco;
        }

        public void SetBanco(string banco)
        {
            this.banco = banco;
        }

        public string GetAgencia()
        {
            return agencia;
        }

        public void SetAgencia(string agencia)
        {
            this.agencia = agencia;
        }

        public string GetNumeroConta()
        {
            return numeroConta;
        }

        public void SetNumeroConta(string numeroConta)
        {
            this.numeroConta = numeroConta;
        }

        public double GetSaldo()
        {

            return saldo;
        }

        public void SetSaldo(double saldo)
        {
            this.saldo = saldo;
        }


        public List<Transacao> GetTransacoes()
        {
            return minhastransacoes;
        }

        public void SetTransacoes(List<Transacao> mt)
        {
            minhastransacoes = mt;
        }

        public void SetTransacao(Transacao mt)
        {
            trans = mt;
            minhastransacoes.Add(mt);
        }


        //Construtor objeto Conta
        public Conta(int id, string banco, string agencia, string numeroConta, double saldo)
        {
            this.id = id;
            this.banco = banco;
            this.agencia = agencia;
            this.numeroConta = numeroConta;
            this.saldo = saldo;
            List<Transacao> transacao = new();
        }

        //override ToString para retornar os dados da Conta
        public override string ToString()
        {
            return
               $@"
               CONTA ID: {GetId()}
               Banco: {GetBanco()}
               Agencia: {GetAgencia()}
               Conta: {GetNumeroConta()}
               Saldo: {GetSaldo().ToString("N2")}
                ";
        }

        //método que cria Contas e adiciona a uma Lista de Contas
        public static List<Conta> CriarConta(List<Conta> minhasContas)
        {

            String banco, agencia, numeroConta;
            double saldo = 0;
            int id;
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

            Conta c = new(id, banco, agencia, numeroConta, saldo); //criacao objeto conta

            minhasContas.Add(c); // adiciona na lista de contas do usuario

            Console.WriteLine($"CONTA CRIADA COM SUCESSO!");
            ImprimirContasAtivas(minhasContas);

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
                minhasContas[i].SetId(i + 1);
            }

            return ultimaId + 1;
        }

        //método que remove a conta selecionada pelo usuario
        public static List<Conta> RemoverConta(List<Conta> minhasContas)
        {
            int contaRemover;

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

                Console.WriteLine("Digite a Id da conta que deseja remover:");
                contaRemover = RetornaNumeroConta(minhasContas);

                Console.WriteLine();
                Console.WriteLine(".---------------------------------------------------------------------------------------------------------------.");
                Console.WriteLine($"|CONTA: {minhasContas[contaRemover].id.ToString()} - {minhasContas[contaRemover].banco.ToString()} REMOVIDA COM SUCESSO!");
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

            int numeroContaRecebe = 0, numeroContaTransfere = 0;

            Console.WriteLine("Por favor, digite a ID da conta que irá receber as transações:");
            numeroContaRecebe = RetornaNumeroConta(minhasContas);

            Console.WriteLine("Agora, digite a ID da conta para a qual você transferir as transações:");
            numeroContaTransfere = RetornaNumeroConta(minhasContas);

            if (minhasContas[numeroContaRecebe].GetTransacoes() == null)
            {
                Console.WriteLine();
                Console.WriteLine(".---------------------------------------------------------------------------------.");
                Console.WriteLine($"|A conta do banco de ID: {minhasContas[numeroContaRecebe].id.ToString()} nao possui transacoes|");
                Console.WriteLine(".--------------------------------------------------------------------------------.");
                Console.WriteLine();
            }

            else if (minhasContas[numeroContaTransfere].GetTransacoes() == null)
            {
                Console.WriteLine();
                Console.WriteLine(".----------------------------------------------------------.");
                Console.WriteLine($"|A conta do banco de ID: {minhasContas[numeroContaTransfere].id.ToString()} nao possui transacoes|");
                Console.WriteLine(".---------------------------------------------------------.");
                Console.WriteLine();
            }

            else
            {
                //conta 1 recebera a uniao de transações da conta 2
                foreach (Transacao t in minhasContas[numeroContaTransfere].GetTransacoes())
                {

                    AdicionaTransacaoConta(minhasContas, t, numeroContaRecebe);
                }

                //remove todas transacoes referentes a conta 1
                minhasContas[numeroContaTransfere].GetTransacoes().Clear();
                ImprimirContasAtivas(minhasContas);

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
                c.SetSaldo(0);
                foreach (Transacao t in c.GetTransacoes())
                {

                    if (t.GetTipo() == "Despesa")
                    {
                        c.SetSaldo(c.GetSaldo() - Math.Abs(t.GetValor()));
                    }

                    if (t.GetTipo() == "Receita")
                    {
                        c.SetSaldo(c.GetSaldo() + t.GetValor());
                    }
                }
            }
        }

        public static int RetornaNumeroConta(List<Conta> minhasContas)
        {
            string idConta;
            bool isValid = false;

            ImprimirContasAtivas(minhasContas);

            do
            {
                idConta = Console.ReadLine();
                isValid = IsInputValid(minhasContas, idConta);

            } while (isValid);

            return int.Parse(idConta) - 1; // pegar a posicao na lista corretamente
        }

        public static Transacao AdicionaTransacaoConta(List<Conta> minhasContas, Transacao t, int numeroConta)
        {

            minhasContas[numeroConta].SetTransacao(t); //adiciona transacao na conta desejada
            CalculaSaldoConta(minhasContas);
            minhasContas[numeroConta].GetTransacoes().OrderBy(t => t.GetData()); //ordena a lista de transacoes


            Console.WriteLine(numeroConta.ToString());
            Console.WriteLine(minhasContas[numeroConta].ToString());

            Console.WriteLine(t.ToString());
            Console.WriteLine($"ESTA TRANSAÇÃO FOI ADICIONADA COM SUCESSO!");
            Console.WriteLine();
            Console.WriteLine(".-------------------------------------------------.");
            Console.WriteLine($"|Seu novo saldo na conta de ID: {minhasContas[numeroConta].id} é de R$ {minhasContas[numeroConta].saldo}|");
            Console.WriteLine(".-------------------------------------------------.");

            return t;

        }

        //método que verifica o input
        public static bool IsInputValid(List<Conta> minhasContas, string input)
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
            double saldoTotal = CalculaSaldoTotal(minhasContas);
            foreach (Conta c in minhasContas)
            {

                Console.WriteLine(".----------------------------------.");
                Console.WriteLine($"| ID: {c.id.ToString()}           |");
                Console.WriteLine($"| BANCO: {c.banco.ToString()}     |");
                Console.WriteLine($"| SALDO: {c.saldo.ToString("N2")} |");
                Console.WriteLine(".----------------------------------.");
                Console.WriteLine();

            }

            Console.WriteLine(".-----------------------------------------------.");
            Console.WriteLine($"|  SALDO TOTAL: {saldoTotal.ToString("N2")}    |");
            Console.WriteLine(".-----------------------------------------------.");
            Console.WriteLine();
        }

        public static double CalculaSaldoTotal(List<Conta> minhasContas)
        {
            double saldoTotal;

            saldoTotal = (minhasContas.Sum(Conta => Conta.GetSaldo()));

            return saldoTotal;
        }

        public static void ExtratoConta(List<Conta> minhasContas)
        {
            foreach (Conta c in minhasContas)
            {
                c.SetSaldo(0);
                Console.WriteLine(c.ToString());

                foreach (Transacao t in c.GetTransacoes())
                {
                    string saldo = CalculaSaldo(t, c);
                    Console.WriteLine(t.ToString() + "\n" + saldo);
                    Console.WriteLine();

                }

                Console.WriteLine();
            }
        }

        public static string CalculaSaldo(Transacao t, Conta c)
        {
            if (t.GetTipo() == "Despesa")
            {
                c.SetSaldo(c.GetSaldo() - Math.Abs(t.GetValor()));
            }

            if (t.GetTipo() == "Receita")
            {
                c.SetSaldo(c.GetSaldo() + t.GetValor());
            }

            return

                $@"                SALDO: {c.GetSaldo().ToString("N2")}
               -------------------------------------------";
        }

        public static void ResumoReceitasDespesas(List<Conta> minhasContas)
        {
            DateTime dataHoje = DateTime.Now;
            int mesAtual = dataHoje.Month;
            int despesa = 0, receita = 0;
            double saldoDespesa = 0, saldoReceita = 0;

            foreach (Conta c in minhasContas)
            {

                foreach (Transacao t in c.GetTransacoes())
                {
                    if (t.GetData().Month == mesAtual)
                    {

                        if (t.GetTipo() == "Despesa")
                        {
                            saldoDespesa = Math.Abs(saldoDespesa + t.GetValor());
                            despesa++;
                        }

                        else if (t.GetTipo() == "Receita")
                        {
                            saldoReceita = (saldoReceita + t.GetValor());
                            receita++;
                        }
                    }
                }
            }
            Console.WriteLine($"O mês atual tem:{"\n"}Total de {despesa} despesa(s):{saldoDespesa}{"\n"}Total de {receita} receita(s):{saldoReceita}");
        }
    }
}



