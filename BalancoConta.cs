using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gerenciamento_Despesas
{
    public class BalancoConta
    {
        //Método que imprime saldo atual
        public static void ImprimeSaldo(List<Conta> minhasContas)
        {
            StringBuilder sb;
            double saldoTotal = CalculaSaldoTotal(minhasContas);

            sb = Utilidades.RetornaMensagem("     RESUMO CONTA     ");
            Console.WriteLine(sb);
            foreach (Conta c in minhasContas)
            {
                //string mensangem1 = $"ID: {c.id.ToString()} {"\n"}|BANCO: {c.banco.ToString()} {"\n"}SALDO: {c.saldo.ToString("N2")}";
                // = Utilidades.RetornaMensagem(mensangem1);
                Console.WriteLine(c.ToString());
            }

            string mensangem2 = $"SALDO TOTAL: {saldoTotal.ToString("N2")}";
            sb = Utilidades.RetornaMensagem(mensangem2);
            Console.WriteLine(sb.ToString());

        }

        //Método que calcula o valor do saldo atual
        public static void CalculaSaldoConta(List<Conta> minhasContas)
        {

            foreach (Conta c in minhasContas)
            {
                Console.WriteLine(c.GetSaldo().ToString());
                c.SetSaldo(0);


                foreach (Transacao t in c.GetTransacoes())
                {
                    CalculaSaldoTransacao(t, c);
                }
            }
        }

        //Método que calcula o valor do saldo de todas as contas presentes na lista de contas
        public static double CalculaSaldoTotal(List<Conta> minhasContas)
        {
            double saldoTotal;

            saldoTotal = (minhasContas.Sum(Conta => Conta.GetSaldo()));

            return saldoTotal;
        }

        //Método que exibe o extrato da conta detalhando a transação e o saldo após a adição de cada uma delas. 
        public static void ExtratoConta(List<Conta> minhasContas)
        {

            StringBuilder sb;
            sb = Utilidades.RetornaMensagem("     EXTRATO DA(S) SUAS CONTA(S)     ");

            foreach (Conta c in minhasContas)
            {
                c.SetSaldo(0);
                Console.WriteLine(c.ToString());

                foreach (Transacao t in c.GetTransacoes())
                {
                    Console.WriteLine(t.ToString());
                    CalculaSaldoTransacao(t, c);
                }

                Console.WriteLine();
            }
        }

        //Método que calcula o saldo conforme o tipo de transação
        public static void CalculaSaldoTransacao(Transacao t, Conta c)
        {
            StringBuilder sb;

            if (t.Tipo == "Despesa")

            {
                double despesa = -t.Valor;
                c.SetSaldo(c.GetSaldo() + despesa);
            }

            if (t.Tipo == "Receita")
            {
                c.SetSaldo(c.GetSaldo() + t.Valor);
            }

            sb = Utilidades.RetornaMensagem($@"SALDO DA CONTA ID {c.GetId()}: {c.GetSaldo().ToString("N2")}");

            Console.WriteLine(sb.ToString());

        }

        //Método que exibe um resumo das transações semestrais conforme a data atual
        public static void ExtratoSemestral(List<Conta> minhasContas)
        {
            StringBuilder sb;
            int despesa = 0, receita = 0;
            bool comparadorDataMaxima, comparadorDataMinima;
            double saldoDespesa = 0, saldoReceita = 0;
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Today);
            DateOnly dataLimiteInferior = dataAtual.AddMonths(-6);

            sb = Utilidades.RetornaMensagem("     EXTRATO SEMESTRAL     ");
            Console.WriteLine(sb.ToString());

            foreach (Conta c in minhasContas)
            {

                foreach (Transacao t in c.GetTransacoes())
                {
                    comparadorDataMaxima = t.Data.CompareTo(dataAtual) <= 0;
                    comparadorDataMinima = t.Data.CompareTo(dataLimiteInferior) >= 0;

                    if (comparadorDataMaxima && comparadorDataMinima)
                    {

                        if (t.Tipo == "Despesa")
                        {
                            saldoDespesa = saldoDespesa + t.Valor;
                            despesa++;
                        }

                        else if (t.Tipo == "Receita")
                        {
                            saldoReceita = (saldoReceita + t.Valor);
                            receita++;
                        }
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"O INTERVALO DE: {dataLimiteInferior.ToString()} À {dataAtual.ToString()} POSSUI UM TOTAL DE:");
            sb = Utilidades.RetornaMensagem($"{receita} TRANSAÇÕES DE RECEITA(S) RESULTANDO NO VALOR DE: R$ {saldoReceita}");
            Console.WriteLine(sb.ToString());
            sb = Utilidades.RetornaMensagem($"{despesa} TRANSAÇÕES DE DESPESA(S) RESULTANDO NO VALOR DE: R$ {saldoDespesa}");
            Console.WriteLine(sb.ToString());
        }

        //método que realiza a transferência de fundos entre contas
        public static void TransferirFundos(List<Conta> minhasContas)
        {

            StringBuilder sb;
            int numeroContaRecebe, numeroContaTransfere;
            double valor;
            string regex = @"^\d+(\.\d+)?$";
            DateOnly dataHoje = DateOnly.FromDateTime(DateTime.Now);

            sb = Utilidades.RetornaMensagem("     TRANSFERÊNCIA DE FUNDOS     ");
            Console.WriteLine(sb.ToString());
            Conta.ImprimirContasAtivas(minhasContas);

            Console.WriteLine($"{"\n"} Digite a ID da conta que gostaria de RECEBER os fundos");
            numeroContaRecebe = Utilidades.RetornaNumeroConta(minhasContas);

            Console.WriteLine($"{"\n"} Digite a ID da conta que gostaria de TRANSFERIR os fundos");
            numeroContaTransfere = Utilidades.RetornaNumeroConta(minhasContas);

            Console.WriteLine($"{"\n"} Digite o VALOR que gostaria de transferir");
            valor = Utilidades.RetornaDouble(regex);


            if (valor > minhasContas[numeroContaTransfere].GetSaldo() || minhasContas[numeroContaTransfere].GetSaldo() == 0)
            {
                sb = Utilidades.RetornaMensagem($"Sua conta de ID: {(numeroContaTransfere + 1).ToString()} não possui fundos suficientes! Tente novamente...");
                Console.WriteLine(sb.ToString());
                return;
            }

            else
            {
                Transacao tReceber = new(dataHoje, "Receita", "Transferência entre Contas", $"Transferência de valor da conta ID:{numeroContaTransfere.ToString()}", valor);
                Transacao tTransferir = new(dataHoje, "Despesa", "Transferência entre Contas", $"Transferência de valor para a conta ID:{numeroContaRecebe.ToString()}", valor);

                GerenciamentoConta.AdicionaTransacaoConta(minhasContas, tReceber, numeroContaRecebe);
                GerenciamentoConta.AdicionaTransacaoConta(minhasContas, tTransferir, numeroContaTransfere);

            }
        }

        //Método que exibe um resumo das transações mensais conforme a data atual
        public static void ExtratoMensal(List<Conta> minhasContas)
        {
            StringBuilder sb;
            int despesa = 0, receita = 0;
            double saldoDespesa = 0, saldoReceita = 0;
            DateOnly dataHoje = DateOnly.FromDateTime(DateTime.Today);
            int mesAtual = dataHoje.Month;

            sb = Utilidades.RetornaMensagem("     EXTRATO MENSAL     ");
            Console.WriteLine(sb.ToString());

            foreach (Conta c in minhasContas)
            {

                foreach (Transacao t in c.GetTransacoes())
                {
                    if (t.Data.Month == mesAtual)
                    {

                        if (t.Tipo == "Despesa")
                        {
                            saldoDespesa = saldoDespesa + t.Valor;
                            despesa++;
                        }

                        else if (t.Tipo == "Receita")
                        {
                            saldoReceita = (saldoReceita + t.Valor);
                            receita++;
                        }
                    }
                }

            }

            Console.WriteLine();
            Console.WriteLine("O MÊS ATUAL POSSUI UM TOTAL DE:");
            sb = Utilidades.RetornaMensagem($"{receita} TRANSAÇÕES DE RECEITA(S) RESULTANDO NO VALOR DE: R$ {saldoReceita}");
            Console.WriteLine(sb.ToString());
            sb = Utilidades.RetornaMensagem($"{despesa} TRANSAÇÕES DE DESPESA(S) RESULTANDO NO VALOR DE: R$ {saldoDespesa}");
            Console.WriteLine(sb.ToString());
        }
    }
}
