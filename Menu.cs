using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Menu
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
            Wait(600);
            //Console.Clear();
            Console.WriteLine();
            Console.WriteLine("    SISTEMA DE GERENCIAMENTO DE DESPESAS    ");
            Console.WriteLine();
            Console.WriteLine(".___________________________________________.");
            Console.WriteLine("|                MENU INICIAL               |");
            Console.WriteLine("|___________________________________________|");
            Console.WriteLine("|1. Gerenciar Contas                        |");
            Console.WriteLine("|2. Gerenciar Transações                    |");
            Console.WriteLine("|3. Painel Geral                            |");
            Console.WriteLine("|___________________________________________|");
            Console.WriteLine();
            Console.WriteLine("Digite a opção desejada:");
        }

        //Imprime Menu da funcionaliade Gerenciar Conta
        public static void ImprimeMenuCase1()
        {
            Wait(600);
            //Console.Clear();
            Console.WriteLine();
            Console.WriteLine("____________________________________________");
            Console.WriteLine("|              GERENCIAR CONTA              |");
            Console.WriteLine("|___________________________________________|");
            Console.WriteLine("|1. Cadastrar conta                         |");
            Console.WriteLine("|2. Remover conta                           |");
            Console.WriteLine("|3. Mesclar contas                          |");
            Console.WriteLine("|4. Voltar ao menu inicial                  |");
            Console.WriteLine("|___________________________________________|");
            Console.WriteLine();
            Console.WriteLine("Digite a opção desejada:");

        }

        //Imprime Menu da funcionaliade Gerenciar Transações
        public static void ImprimeMenuCase2()
        {
            Wait(600);
            //Console.Clear();
            Console.WriteLine();
            Console.WriteLine(".___________________________________________.");
            Console.WriteLine("|            GERENCIAR TRANSACOES           |");
            Console.WriteLine("|___________________________________________|");
            Console.WriteLine("|1. Extrato da conta                        |");
            Console.WriteLine("|2. Incluir transação                       |");
            Console.WriteLine("|3. Editar a última transação               |");
            Console.WriteLine("|4. Transferir fundos                       |");
            Console.WriteLine("|5. Voltar ao menu inicial                  |");
            Console.WriteLine("|___________________________________________|");
            Console.WriteLine();
            Console.WriteLine("Digite a opção desejada:");

        }

        //Imprime Menu da funcionaliade Painel Geral
        public static void ImprimeMenuCase3()
        {
            Wait(600);
            //Console.Clear();
            Console.WriteLine();
            Console.WriteLine(".___________________________________________.");
            Console.WriteLine("|               PAINEL GERAL                |");
            Console.WriteLine("|___________________________________________|");
            Console.WriteLine("|1. Resumo das contas                       |");
            Console.WriteLine("|2. Resumo de receitas e despesas do mês    |");
            Console.WriteLine("|3. Saldo geral dos últimos 6 meses         |");
            Console.WriteLine("|4. Transações com maior valor e menor valor|");
            Console.WriteLine("|5. Voltar ao menu inicial                  |");
            Console.WriteLine("|___________________________________________|");
            Console.WriteLine();
            Console.WriteLine("Digite a opção desejada:");

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

                    isValid = VerificaQuantidadesContas(minhasContas,1);
                    
                    if (isValid)
                    {
                        Conta.RemoverConta(minhasContas);
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
                        Conta.MesclarContas(minhasContas);
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
                        Conta.ExtratoConta(minhasContas);
                    }
                    else
                    {
                        Console.WriteLine("Primeiramente cadastre UMA conta!");
                    }

                    Console.WriteLine("Retornando ao menu inicial...");
                    Wait(1000);
                    break;

                // Incluir transação
                case 2:

                    isValid = VerificaQuantidadesContas(minhasContas, 1);

                    if (isValid)
                    {
                        t = AdicionarTransacao(minhasContas);
                        ultimaTransacao = new Transacao();
                        ultimaTransacao = t;
                    }

                    else
                    {
                        Console.WriteLine("Primeiramente cadastre UMA conta!");
                    }
                    Console.WriteLine("Retornando ao menu inicial...");
                    Wait(1000);
                    break;

                // Editar a última transação
                case 3:

                    if (ultimaTransacao == null)
                    {
                        Console.WriteLine("Primeiro cadastre uma transação");
                        Wait(1000);
                        break;
                    }
                    else
                    {
                        minhasContas = Transacao.EditarTransacao(minhasContas, ultimaTransacao);
                        Console.WriteLine("Retornando ao menu inicial...");
                        Wait(5000);
                        break;
                    }


                // Transferir fundos
                case 4:
                    isValid = VerificaQuantidadesContas(minhasContas, 2);
                    if (isValid)
                    {
                        Conta.TransferirFundos(minhasContas);
                    }
                    else 
                    {
                        Console.WriteLine("Primeiramente cadastre DUAS conta!");
                    }

                    Console.WriteLine("Retornando ao menu inicial...");
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
                        Conta.ImprimeSaldo(minhasContas);
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
                        Conta.ExtratoMensal(minhasContas);
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
                        Conta.ExtratoSemestral(minhasContas);
                    }

                    else
                    {
                        Console.WriteLine("Primeiramente cadastre UMA conta!");
                    }

                    Wait(5000);
                    Console.WriteLine("Retornando ao menu...");
                    break;

                // Retorna a menor e a maior transação apra cada conta cadastrada
                case 4:

                    isValid = VerificaQuantidadesContas(minhasContas, 1);
                    if (isValid)
                    {
                        Conta.TransacaoMinMax(minhasContas);
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

        //loop para a criação de transações
        public Transacao AdicionarTransacao(List<Conta> minhasContas)
        {
            string regex = "^(1|2)$";
            int opcao;
            Transacao t;
          

            do
            {
                t = Transacao.CriarTransacao();

                Conta.ImprimirContasAtivas(minhasContas);
                Console.WriteLine("Digite a id da conta que voce deseja adicionar esta transação:");
                int conta = Conta.RetornaNumeroConta(minhasContas);

                Conta.AdicionaTransacaoConta(minhasContas, t, conta);
                
                Console.WriteLine("Gostaria de adicionar mais transações? Digite:");
                Console.WriteLine("1 - SIM     2 - NÃO");
                opcao = Utilidades.RetornaInt(regex);

            } while (opcao == 1);

            return t;
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


