using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Sistema_Gerenciamento_Despesas
{
    public class Menu
    {

        private bool menuEstaAtivo;
        private int opcaoMenu;
        List<Conta> minhasContas;
        Transacao ultimaTransacao;

        public Menu(List<Conta> minhasContasVirtual)
        {
            minhasContas = minhasContasVirtual;
            menuEstaAtivo = true;
            int opcaoMenu;
            Iniciar();
        }

        //chamada de todos os métodos para executar o programa
        public void Iniciar()
        {
            
            while (menuEstaAtivo)
            {
                Transacao t = null;
                ImprimeMenuInicial();
                string regex = "^[1-9][0-9]*$";
                opcaoMenu = Utilidades.RetornaInt(regex);
                Console.WriteLine("Direcionando para a opcao desejada..");

                switch (opcaoMenu)
                {
                    case 1:

                        ImprimeMenuCase1();
                        int opcaoMenu1 = Utilidades.RetornaInt(regex);
                        GerenciarConta(opcaoMenu1, minhasContas);
                        break;
                    case 2:

                        ImprimeMenuCase2();
                        int opcaoMenu2 = Utilidades.RetornaInt(regex);
                        GerenciarTransacoes(opcaoMenu2, minhasContas, t);
                        break;

                    case 3:

                        ImprimeMenuCase3();
                        int opcaoMenu3 = Utilidades.RetornaInt(regex);
                        PainelControle(opcaoMenu3, minhasContas);

                        break;
                    case 4:

                        menuEstaAtivo = false;
                        Console.WriteLine("Encerrando o sistema...");
                        break;

                    default:
                        Console.WriteLine("Digite uma opcao valida!");
                        break;
                }
            }
        }

        //Imprime Menu Inicial
        public static void ImprimeMenuInicial()
        {
            StringBuilder sb = new();
            string [] frases = new string[]
             {
             
                "MENU INICIAL",
                "1. Gerenciar Contas",
                "2. Gerenciar Transações",
                "3. Painel Geral",
                 "Digite a opção desejada:",
             };

            Wait(1000);
            Console.Clear();
           
            sb =  Utilidades.RetornaMenu(frases);
            sb.Insert(0,$"SISTEMA DE GERENCIAMENTO DE DESPESAS{"\n"}");
            Console.WriteLine(sb);
        }
            
        //Imprime Menu da funcionaliade Gerenciar Conta
        public static void ImprimeMenuCase1()
        {
            Wait(1000);
            Console.Clear();
            StringBuilder sb = new();
            string[] frases = new string[]
            {
       
            "GERENCIAR CONTA",
            "1. Cadastrar conta",
            "2. Remover conta",
            "3. Mesclar contas",
            "4. Voltar ao menu inicial",
            "Digite a opção desejada:",
             };

            Wait(1000);
            Console.Clear();
            sb = Utilidades.RetornaMenu(frases);
            Console.WriteLine(sb);

        }

        //Imprime Menu da funcionaliade Gerenciar Transações
        public static void ImprimeMenuCase2()
        {

            Wait(1000);
            Console.Clear();
            StringBuilder sb = new();
            string[] frases = new string[]
            {
            "GERENCIAR TRANSACOES",
            "1. Extrato da conta",
            "2. Incluir transação",
            "3. Editar a última transação",
            "4.Transferir fundos ",
            "5. Voltar ao menu inicial",
            "Digite a opção desejada:",
             };

            Wait(1000);
            Console.Clear();
            sb = Utilidades.RetornaMenu(frases);
            Console.WriteLine(sb);
            Wait(1000);

        }

        //Imprime Menu da funcionaliade Painel Geral
        public static void ImprimeMenuCase3()
        {

            Wait(1000);
            Console.Clear();
            StringBuilder sb = new();
            string[] frases = new string[]
            {
            "PAINEL GERAL",
            "1. Resumo das contas",
            "2. Resumo de receitas e despesas do mês",
            "3. Saldo geral dos últimos 6 meses",
            "4. Transações com maior valor e menor valor",
            "5. Voltar ao menu inicial",
            "Digite a opção desejada:",
             };

            Wait(1000);
            Console.Clear();
            sb = Utilidades.RetornaMenu(frases);
            Console.WriteLine(sb);
      
        }

        //switch case relacionado as funcionalides de Gerencia Conta
        public void GerenciarConta(int opcaoMenu1, List<Conta> minhasContas)
        {
            bool isValid;

            switch (opcaoMenu1)
            {
                // Cadastrar conta
                case 1:
              
                    Conta.CriarConta(minhasContas);
                    Console.WriteLine("Retornando ao menu...");
                    Wait(5000);  
                    break;

                // Remover conta;
                case 2:

                    isValid = VerificaQuantidadesContas(minhasContas, 1);
                    string pergunta = "Digite a Id da conta que deseja remover:";
                    int contaRemover;

                    if (isValid)
                    {
                        contaRemover = Utilidades.RetornaContaEscolhida(minhasContas, pergunta);
                        GerenciamentoConta.RemoverConta(minhasContas, contaRemover);
                    }

                    else 
                    {
                        Console.WriteLine("Primeiramente cadastre UMA conta!");
                    }

                    Console.WriteLine("Retornando ao menu...");
                    Wait(5000);
                    break;

                // Mesclar contas
                case 3:
                    isValid = VerificaQuantidadesContas(minhasContas, 2);

                    if (isValid)
                    {
                        GerenciamentoConta.MesclarContas(minhasContas);
                    }

                    else
                    {
                        Console.WriteLine("Primeiramente cadastre DUAS conta!");
                    }

                    Console.WriteLine("Retornando ao menu...");
                    Wait(5000);
                    break;

                //sair p/ menu inicial
                case 4:
                    Console.WriteLine();
                    Console.WriteLine("Retornando ao menu inicial...");
                    return;

                default:
                    Console.WriteLine("Digite uma opcao valida!");
                    break;

            }
        }

        //switch case relacionado as funcionalides de Gerenciar Transações
        public void GerenciarTransacoes(int opcaoMenu2, List<Conta> minhasContas, Transacao t)
        {
            bool isValid;

            switch (opcaoMenu2)
            {

                // Extrato da conta
                case 1:

                    isValid = VerificaQuantidadesContas(minhasContas, 1);

                    if (isValid)
                    {
                        BalancoConta.ExtratoConta(minhasContas);
                    }
                    else
                    {
                        Console.WriteLine("Primeiramente cadastre UMA conta!");
                    }

                    Console.WriteLine("Retornando ao menu inicial...");
                    Wait(3000);
                    break;

                // Incluir transação
                case 2:

                    isValid = VerificaQuantidadesContas(minhasContas, 1);

                    if (isValid)
                    {
                        t = GerenciamentoConta.AdicionarTransacao(minhasContas);
                        ultimaTransacao = new Transacao();
                        ultimaTransacao = t;
                    }

                    else
                    {
                        Console.WriteLine("Primeiramente cadastre UMA conta!");
                    }
                    Console.WriteLine("Retornando ao menu inicial...");
                    Wait(3000);
                    break;

                // Editar a última transação
                case 3:

                    if (ultimaTransacao == null)
                    {
                        Console.WriteLine("Primeiro cadastre uma transação");
                        Wait(3000);
                        break;
                    }
                    else
                    {
                        minhasContas = GerenciamentoTransacoes.EditarTransacao(minhasContas, ultimaTransacao);
                        Console.WriteLine("Retornando ao menu inicial...");
                        Wait(3000);
                        break;
                    }


                // Transferir fundos
                case 4:
                    isValid = VerificaQuantidadesContas(minhasContas, 2);
                    if (isValid)
                    {
                        BalancoConta.TransferirFundos(minhasContas);
                    }
                    else 
                    {
                        Console.WriteLine("Primeiramente cadastre DUAS conta!");
                    }

                    Console.WriteLine("Retornando ao menu inicial...");
                    Wait(3000);
                    break;

                //sair p/ menu inicial
                case 5:
                    Console.WriteLine("Retornando ao menu inicial...");
                    break;

                default:
                    Console.WriteLine("Digite uma opcao valida!");
                    break;

            }
        }

        //switch case relacionado as funcionalides do Painel Geral
        public static void PainelControle(int opcaoMenu3, List<Conta> minhasContas)
        {
            bool isValid;

            switch (opcaoMenu3)
            {
                // Resumo das contas
                case 1:

                    isValid = VerificaQuantidadesContas(minhasContas, 1);

                    if (isValid)
                    {
                        BalancoConta.ImprimeSaldo(minhasContas);
                    }

                    else 
                    {
                        Console.WriteLine("Primeiramente cadastre UMA conta!");
                    }

                    Console.WriteLine("Retornando ao menu...");
                    Wait(5000);
                    break;

                // Resumo de receitas e despesas do mês
                case 2:

                    isValid = VerificaQuantidadesContas(minhasContas, 1);

                    if (isValid)
                    {
                        BalancoConta.ExtratoMensal(minhasContas);
                    }

                    else
                    {
                        Console.WriteLine("Primeiramente cadastre UMA conta!");
                    }

                    Wait(5000);
                    Console.WriteLine("Retornando ao menu...");
                    break;

                // Saldo geral dos últimos 6 meses
                case 3:

                    isValid = VerificaQuantidadesContas(minhasContas, 1);
                    if (isValid)
                    {
                        BalancoConta.ExtratoSemestral(minhasContas);
                    }

                    else
                    {
                        Console.WriteLine("Primeiramente cadastre UMA conta!");
                    }

                    Wait(5000);
                    Console.WriteLine("Retornando ao menu...");
                    break;

                // Retorna a menor e a maior transação para cada conta cadastrada
                case 4:

                    isValid = VerificaQuantidadesContas(minhasContas, 1);
                    if (isValid)
                    {
                        GerenciamentoTransacoes.TransacaoMenoreMaior(minhasContas);
                    }

                    else
                    {
                        Console.WriteLine("Primeiramente cadastre UMA conta!");
                    }

                    Wait(5000);
                    Console.WriteLine("Retornando ao menu...");
                    break;

                //sair p/ menu inicial
                case 5:
                    Console.WriteLine("Retornando ao menu inicial...");
                    break;

                default:
                    Console.WriteLine("Digite uma opcao valida!");
                    break;
            }
        }

        //tempo entre cada impressão
        public static void Wait(int milissegundos)
        {
            try
            {
                Thread.Sleep(milissegundos);
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        //método que verifica se o número mínimo de contas é compatível com a quantidade de contas existentes
        public static bool VerificaQuantidadesContas(List<Conta> minhasContas, int qntMinimaContas) 
        {

            if (minhasContas.Count < qntMinimaContas)
            {
                return false;
            }

            return true;
        }
    }
}


