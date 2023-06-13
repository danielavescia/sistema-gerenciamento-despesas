using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Text;
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

            string banco, agencia, numeroConta, regex = "^(? !$).*"; //regex = não pode ser string vazia
            double saldo = 0;
            int id;
            List<Transacao> minhasTransacoes;
            StringBuilder sb;

            //input usuario p/ criacao objeto
            try
            {
                Console.WriteLine("Para criar uma conta adicione as informacoes solicitadas abaixo:");

                Console.WriteLine($"{"\n"}Digite o nome do seu banco:");
                banco = Utilidades.RetornaString(regex);

                Console.WriteLine($"{"\n"}Número da agência:");
                agencia = Utilidades.RetornaString(regex);

                Console.WriteLine($"({"\n"}Número da conta:");
                numeroConta = Utilidades.RetornaString(regex);

                id = AtribuiId(minhasContas);

                Conta c = new(id, banco, agencia, numeroConta, saldo); //criacao objeto conta

                minhasContas.Add(c); // adiciona na lista de contas do usuario

                //construcao da mensagem de criação da conta
                string mensagem = $"A conta do {c.GetBanco()} foi criada com sucesso!";
                sb = Utilidades.RetornaMensagem(mensagem);
                Console.WriteLine(sb);

                return minhasContas;
            }

            catch (Exception)
            {
                throw new NullReferenceException("Ocorreu um erro na criação da sua conta. Tente novamente!");
            }
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
            StringBuilder sb = new();
            string mensagem1 = "LEMBRE-SE QUE AO REMOVER SUA CONTA, VOCE IRA PERDER TODAS AS TRANSACOES RELACIONADAS A ELA";
            string mensagem2 = "VOCE NAO POSSUI NENHUMA CONTA CADASTRADA";

            sb = Utilidades.RetornaMensagem(mensagem1);

            //verificação se lista de contas está vazia
            if (minhasContas.Count == 0)
            {
                sb = Utilidades.RetornaMensagem(mensagem2);
                Console.WriteLine(sb);
            }

            else
            {
                // Imprime as contas ativas p/ usuario decidir pro input qual deseja remover
                ImprimirContasAtivas(minhasContas);
                Console.WriteLine("Digite a Id da conta que deseja remover:");
                contaRemover = RetornaNumeroConta(minhasContas);

                //construção mensagem impressa em tela
                string mensagem3 = $"{minhasContas[contaRemover].id.ToString()} - {minhasContas[contaRemover].banco.ToString()} REMOVIDA COM SUCESSO!";
                sb = Utilidades.RetornaMensagem(mensagem3);
                Console.WriteLine(sb);

                //remove a conta da lista e reatribui as ids as contas existentes
                minhasContas.RemoveAt(contaRemover);
                AtribuiId(minhasContas);
            }

            return minhasContas;
        }

        //metodo que une as transações da conta x na conta y
        public static List<Conta> MesclarContas(List<Conta> minhasContas)
        {
            StringBuilder sb = new();
            int numeroContaRecebe = 0, numeroContaTransfere = 0;

            //bloco que solicita e captura as posições na lista de contas
            ImprimirContasAtivas(minhasContas);
            Console.WriteLine("Por favor, digite a ID da conta que irá receber as transações:");
            numeroContaRecebe = RetornaNumeroConta(minhasContas);

            Console.WriteLine("Agora, digite a ID da conta que irá transferir as transações:");
            numeroContaTransfere = RetornaNumeroConta(minhasContas);


            if (minhasContas[numeroContaRecebe].GetTransacoes() == null && minhasContas[numeroContaTransfere].GetTransacoes() == null)
            {
                sb = Utilidades.RetornaMensagem($"As duas conta não possuem transações");
            }

            else if (minhasContas[numeroContaTransfere].GetTransacoes() == null)
            {
                sb = Utilidades.RetornaMensagem($"A conta do banco de ID: {minhasContas[numeroContaTransfere].id} nao possui transações");
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
                minhasContas[numeroContaTransfere].SetSaldo(0);
                ImprimirContasAtivas(minhasContas);

            }

            return minhasContas;
        }

        //método que imprime contas ativas
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

        //metodo que retorna um número da conta válido
        public static int RetornaNumeroConta(List<Conta> minhasContas)
        {
            string idConta;
            bool isValid = false;

            do
            {
                idConta = Console.ReadLine();
                isValid = IsInputValid(minhasContas, idConta);

            } while (!isValid);

            return int.Parse(idConta) - 1; // pegar a posicao na lista corretamente
        }

        public static Transacao AdicionaTransacaoConta(List<Conta> minhasContas, Transacao t, int numeroConta)
        {

            minhasContas[numeroConta].SetTransacao(t); //adiciona transacao na conta desejada
            CalculaSaldoConta(minhasContas);
            minhasContas[numeroConta].GetTransacoes().OrderBy(t => t.GetData()); //ordena a lista de transacoes

            //criação da mensagem 
            String mensagem1 = $"Esta {t.GetTipo()} no valor de {t.GetValor().ToString("N2")} foi adicionada com sucesso!";
            String mensagem2 = $"Seu novo saldo na conta de ID: {minhasContas[numeroConta].id.ToString()}  é de R$ {minhasContas[numeroConta].saldo.ToString("N2")}";
            StringBuilder sb = new();

            sb = Utilidades.RetornaMensagem(mensagem2);
            sb.Insert(0, mensagem1);

            Console.WriteLine(sb.ToString());

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
                    return false;
                }
            }

            return true;
        }

        public static void ImprimeSaldo(List<Conta> minhasContas)
        {
            StringBuilder sb = new();
            double saldoTotal = CalculaSaldoTotal(minhasContas);

            foreach (Conta c in minhasContas)
            {
                string mensangem1 = $"ID: {c.id.ToString()} {"\n"}|BANCO: {c.banco.ToString()} {"\n"}SALDO: {c.saldo.ToString("N2")}";
                sb = Utilidades.RetornaMensagem(mensangem1);
                Console.WriteLine(sb);
                sb.Clear();
            }

            string mensangem2 = $"SALDO TOTAL: {saldoTotal.ToString("N2")}";
            sb = Utilidades.RetornaMensagem(mensangem2);
            Console.WriteLine(sb.ToString());

        }

        public static double CalculaSaldoTotal(List<Conta> minhasContas)
        {
            double saldoTotal;

            saldoTotal = (minhasContas.Sum(Conta => Conta.GetSaldo()));

            return saldoTotal;
        }

        public static void ExtratoConta(List<Conta> minhasContas)
        {
            StringBuilder sb = new();
            foreach (Conta c in minhasContas)
            {
                c.SetSaldo(0);
                Console.WriteLine(c.ToString());

                foreach (Transacao t in c.GetTransacoes())
                {
                    string saldo = CalculaSaldo(t, c);
                    Console.WriteLine(t.ToString());
                    sb.Append($"SALDO: {saldo}");
                    Console.WriteLine(sb);

                }

                Console.WriteLine();
            }
        }

        public static string CalculaSaldo(Transacao t, Conta c)
        {
            StringBuilder sb;
            
            if (t.GetTipo() == "Despesa")

            {
                double despesa = - t.GetValor();
                c.SetSaldo(c.GetSaldo() + despesa);
            }

            if (t.GetTipo() == "Receita")
            {
                c.SetSaldo(c.GetSaldo() + t.GetValor());
            }
          
            sb = Utilidades.RetornaMensagem($@"SALDO: {c.GetSaldo().ToString("N2")}");

            return sb.ToString();

        }

        public static void ExtratoMensal(List<Conta> minhasContas)
        {
            StringBuilder sb;
            int despesa = 0, receita = 0;
            double saldoDespesa = 0, saldoReceita = 0;
            DateTime dataHoje = DateTime.Now;
            int mesAtual = dataHoje.Month;


            foreach (Conta c in minhasContas)
            {

                foreach (Transacao t in c.GetTransacoes())
                {
                    if (t.GetData().Month == mesAtual)
                    {

                        if (t.GetTipo() == "Despesa")
                        {
                            saldoDespesa = saldoDespesa + t.GetValor();
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
            Console.WriteLine();
            Console.WriteLine($"O MÊS ATUAL: {mesAtual}/{dataHoje.Year} POSSUI UM TOTAL DE:");
            sb = Utilidades.RetornaMensagem($"{receita} TRANSAÇÕES DE RECEITA(S) RESULTANDO NO VALOR DE: R$ {saldoReceita}");
            Console.WriteLine(sb.ToString());
            sb = Utilidades.RetornaMensagem($"{despesa} TRANSAÇÕES DE DESPESA(S) RESULTANDO NO VALOR DE: R$ {saldoDespesa}");
            Console.WriteLine(sb.ToString());
        }

        public static void TransferirFundos(List<Conta> minhasContas) {

            StringBuilder sb;
            int numeroContaRecebe, numeroContaTransfere;
            double valor;
            string regex = @"^\d+(\.\d+)?$";
            DateOnly dataHoje = DateOnly.FromDateTime(DateTime.Now);

            sb = Utilidades.RetornaMensagem("TRANSFERÊNCIA DE FUNDOS"); 
            Console.WriteLine(sb.ToString());
            ImprimirContasAtivas(minhasContas);

            Console.WriteLine($"{"\n"} Digite a ID da conta que gostaria de RECEBER os fundos");
            numeroContaRecebe = RetornaNumeroConta(minhasContas);

            Console.WriteLine($"{"\n"} Digite a ID da conta que gostaria de TRANSFERIR os fundos");
            numeroContaTransfere = RetornaNumeroConta(minhasContas);

            Console.WriteLine($"{"\n"} Digite o VALOR que gostaria de transferir");
            valor = Utilidades.RetornaDouble(regex);


            if (valor > minhasContas[numeroContaTransfere].GetSaldo())
            {
                sb = Utilidades.RetornaMensagem($"Sua conta de ID: {numeroContaTransfere.ToString()} não possui fundos suficientes! Tente novamente...");
                TransferirFundos(minhasContas);
            }

            else
            {
                Transacao tReceber = new(dataHoje, "Receita", "Transferência entre Contas", $"Transferência de valor da conta ID:{numeroContaTransfere.ToString()}", valor);
                Transacao tTransferir = new(dataHoje, "Despesa","Transferência entre Contas", $"Transferência de valor para a conta ID:{numeroContaRecebe.ToString() }", valor);

                AdicionaTransacaoConta(minhasContas, tReceber, numeroContaRecebe);
                AdicionaTransacaoConta(minhasContas, tTransferir, numeroContaTransfere);

            }
        }
    }
}



