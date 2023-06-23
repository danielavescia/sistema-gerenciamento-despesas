using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gerenciamento_Despesas
{
    public class GerenciamentoConta
    {
        //método que remove a conta selecionada pelo usuario
        public static List<Conta> RemoverConta(List<Conta> minhasContas, int contaRemover)
        {
            
            StringBuilder sb;
            string mensagem1 = "LEMBRE-SE QUE AO REMOVER SUA CONTA, VOCE IRA PERDER TODAS AS TRANSACOES RELACIONADAS A ELA";
            string mensagem2 = "VOCE NAO POSSUI NENHUMA CONTA CADASTRADA";

            sb = Utilidades.RetornaMensagem("     REMOVER CONTA     ");
            Console.WriteLine(sb.ToString());
            sb = Utilidades.RetornaMensagem(mensagem1);

            //verificação se lista de contas está vazia
            if (minhasContas.Count == 0)
            {
                sb = Utilidades.RetornaMensagem(mensagem2);
                Console.WriteLine(sb);
            }

            else { 
       
                //construção mensagem impressa em tela
                string mensagem3 = $"{minhasContas[contaRemover].GetId().ToString()} - {minhasContas[contaRemover].GetBanco().ToString()} REMOVIDA COM SUCESSO!";
                sb = Utilidades.RetornaMensagem(mensagem3);
                Console.WriteLine(sb);

                //remove a conta da lista e reatribui as ids as contas existentes
                minhasContas.RemoveAt(contaRemover);
                Conta.AtribuiId(minhasContas);
            }

            return minhasContas;
        }

        //metodo que une as transações da conta x na conta y
        public static List<Conta> MesclarContas(List<Conta> minhasContas)
        {
            StringBuilder sb;
            int numeroContaRecebe, numeroContaTransfere;

            sb = Utilidades.RetornaMensagem("     MESCLAR CONTA     ");
            //bloco que solicita e captura as posições na lista de contas
            Conta.ImprimirContasAtivas(minhasContas);
            Console.WriteLine("Por favor, digite a ID da conta que irá receber as transações:");
            numeroContaRecebe = Utilidades.RetornaNumeroConta(minhasContas);

            Console.WriteLine("Agora, digite a ID da conta que irá transferir as transações:");
            numeroContaTransfere = Utilidades.RetornaNumeroConta(minhasContas);


            if (minhasContas[numeroContaRecebe].GetTransacoes().Count == null && minhasContas[numeroContaTransfere].GetTransacoes() == null)
            {
                sb = Utilidades.RetornaMensagem($"As duas conta não possuem transações");
            }

            else if (minhasContas[numeroContaTransfere].GetTransacoes() == null)
            {
                sb = Utilidades.RetornaMensagem($"A conta do banco de ID: {minhasContas[numeroContaTransfere].GetId()} nao possui transações");
            }

            else
            {
                //conta 1 recebera a uniao de transações da conta 2
                foreach (Transacao t in minhasContas[numeroContaTransfere].GetTransacoes())
                {
                    GerenciamentoConta.AdicionaTransacaoConta(minhasContas, t, numeroContaRecebe);
                }

                //remove todas transacoes referentes a conta 1
                minhasContas[numeroContaTransfere].GetTransacoes().Clear();
                minhasContas[numeroContaTransfere].SetSaldo(0);
                Conta.ImprimirContasAtivas(minhasContas);

            }

            return minhasContas;
        }

        //Método que adiciona uma transação na Lista de Transações de uma conta específica
        public static Transacao AdicionaTransacaoConta(List<Conta> minhasContas, Transacao t, int numeroConta)
        {
            StringBuilder sb = new();
            String mensagem2 = $"          Esta {t.Tipo} foi adicionada com sucesso!          ";

            minhasContas[numeroConta].SetTransacao(t); //adiciona transacao na conta desejada
            minhasContas[numeroConta].GetTransacoes().OrderBy(t => t.Data); //ordena a lista de transacoes

            //criação da mensagem
            Console.WriteLine(t.ToString());
            sb = Utilidades.RetornaMensagem(mensagem2);
            Console.WriteLine(sb.ToString());
            BalancoConta.CalculaSaldoTransacao(t, minhasContas[numeroConta]);

            return t;

        }

        //loop para a criação de transações
        public static Transacao AdicionarTransacao(List<Conta> minhasContas)
        {
            string regex = "^(1|2)$";
            int opcao;
            Transacao t;

            do
            {
                t = Transacao.CriarTransacao();

                Conta.ImprimirContasAtivas(minhasContas);
                Console.WriteLine("Digite a id da conta que voce deseja adicionar esta transação:");
                int conta = Utilidades.RetornaNumeroConta(minhasContas);

                GerenciamentoConta.AdicionaTransacaoConta(minhasContas, t, conta);

                Console.WriteLine("Gostaria de adicionar mais transações? Digite:");
                Console.WriteLine("1 - SIM     2 - NÃO");
                opcao = Utilidades.RetornaInt(regex);

            } while (opcao == 1);

            return t;
        }
    }
}
