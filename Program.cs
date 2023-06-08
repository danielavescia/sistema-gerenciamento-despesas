
using Sistema_Gerenciamento_Despesas;
using System.Collections.Generic;

internal class Program
{

    static void Main(string[] args)
    {
        List<Conta> minhasContas = new List<Conta>();
        List<Transacao> minhasTransacoes1 = new List<Transacao>();
        List<Transacao> minhasTransacoes2 = new List<Transacao>();

        // Criando a conta 1
        Conta conta1 = new Conta(1, "Banco AZB", "5900", "54489-6", 0.0);
        minhasContas.Add(conta1);

        //Criando a conta 2
        Conta conta2 = new Conta(2, "Banco OAOA", "001", "9000-8", 0.0);
        minhasContas.Add(conta2);

        // Criando set de transações 1
        Transacao transacao1 = new Transacao(new DateOnly(2023, 3, 5), "Receita", "Salário", "Salário ref. Fevereiro/2023", 3200.00);
        Transacao transacao2 = new Transacao(new DateOnly(2023, 3, 5), "Despesa", "Alimentação", "Restaurante", 24.70);
        Transacao transacao3 = new Transacao(new DateOnly(2023, 3, 6), "Despesa", "Casa", "Conserto geladeira", 272.90);
        Transacao transacao4 = new Transacao(new DateOnly(2023, 3, 6), "Despesa", "Energia", "Energia Elétrica", 78.00);

        // Criando set de transações 2
        Transacao transacao5 = new Transacao(new DateOnly(2023, 3, 7), "Receita", "Freelance", "Serviços de consultoria", 1500.00);
        Transacao transacao6 = new Transacao(new DateOnly(2023, 3, 7), "Despesa", "Transporte", "Uber", 35.50);
        Transacao transacao7 = new Transacao(new DateOnly(2023, 3, 8), "Despesa", "Lazer", "Ingresso de cinema", 42.00);
        Transacao transacao8 = new Transacao(new DateOnly(2023, 3, 8), "Despesa", "Saúde", "Consulta médica", 200.00);

        // Criando a lista de transações 1

        minhasTransacoes1.Add(transacao1);
        minhasTransacoes1.Add(transacao2);
        minhasTransacoes1.Add(transacao3);
        minhasTransacoes1.Add(transacao4);

        // Criando a lista de transações 2

        minhasTransacoes2.Add(transacao5);
        minhasTransacoes2.Add(transacao6);
        minhasTransacoes2.Add(transacao7);
        minhasTransacoes2.Add(transacao8);

        // Colocando um set de Transaçoes na conta 1 
        conta1.setTransacoes(minhasTransacoes1);

        // Colocando um set de Transaçoes na conta 1 
        conta2.setTransacoes(minhasTransacoes2);

        {
            //variaveis relacionadas ao menu
            bool MenuEstaAtivo = true;
            int opcaoMenu;

            //loop permite a repetição da visualização do menu e a escolha da ação que o usuario deseja executar
            while (MenuEstaAtivo)
            {

                ImprimeMenuInicial();
                opcaoMenu = int.Parse(Console.ReadLine());

                //a partir do switch case a opcao desejada permite a execeução do bloco de código
                switch (opcaoMenu)
                {

                    // GERENCIAR CONTA 
                    case 1:

                        ImprimeMenuCase1();
                        int opcaoMenu1 = int.Parse(Console.ReadLine());
                        SwitchCase1(opcaoMenu1, minhasContas);
                        break;

                    // GERENCIAR TRANSACOES
                    case 2:

                        ImprimeMenuCase2();
                        int opcaoMenu2 = int.Parse(Console.ReadLine());
                        SwitchCase2(opcaoMenu2, minhasContas);
                        break;

                    //PAINEL DE CONTROLE
                    case 3:

                        ImprimeMenuCase3();
                        int opcaoMenu3 = int.Parse(Console.ReadLine());
                        SwitchCase3(opcaoMenu3, minhasContas);
                        break;

                    // Sair do sistema
                    case 4:

                        MenuEstaAtivo = false;
                        Console.WriteLine("Encerrando o sistema...");
                        break;


                    default:

                        Console.WriteLine("Digite uma opcao valida!");
                        break;

                }

            }
        }
    }    


            public static void ImprimeMenuInicial()
            {
                Console.WriteLine();
                Console.WriteLine("    SISTEMA DE GERENCIAMENTO DE DESPESAS    ");
                Console.WriteLine();
                Console.WriteLine("____________________________________________");
                Console.WriteLine("|                MENU INICIAL               |");
                Console.WriteLine("|___________________________________________|");
                Console.WriteLine("|1. Gerenciar Contas                        |");
                Console.WriteLine("|2. Gerenciar Transações                    |");
                Console.WriteLine("|3. Painel Geral                            |");
                Console.WriteLine("|___________________________________________|");
                Console.WriteLine();
                Console.WriteLine("Digite a opção desejada:");
            }

            public static void ImprimeMenuCase1()
            {

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

            public static void ImprimeMenuCase2()
            {

                Console.WriteLine();
                Console.WriteLine("____________________________________________");
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

            public static void ImprimeMenuCase3()
            {

                Console.WriteLine();
                Console.WriteLine("____________________________________________");
                Console.WriteLine("|               PAINEL GERAL                |");
                Console.WriteLine("|___________________________________________|");
                Console.WriteLine("|1. Resumo das contas                       |");
                Console.WriteLine("|2. Resumo de receitas e despesas do mês    |");
                Console.WriteLine("|3. Saldo geral dos últimos 6 meses         |");
                Console.WriteLine("|4. FUNCIONALIDADE                          |");
                Console.WriteLine("|5. Voltar ao menu inicial                  |");
                Console.WriteLine("|___________________________________________|");
                Console.WriteLine();
                Console.WriteLine("Digite a opção desejada:");

            }

            public static void SwitchCase1(int opcaoMenu1, List <Conta> minhasContas)
            {
                switch (opcaoMenu1)
                {
                    // Cadastrar conta
                    case 1:
                        Conta.CriarConta(minhasContas);
                        break;

                    // Remover conta;
                    case 2:
                        Conta.RemoverConta(minhasContas);
                        break;

                    // Mesclar contas
                    case 3:
                        Conta.MesclarContas(minhasContas);
                        break;

                    //sair p/ menu inicial
                    case 4:
                        Console.WriteLine("Retornando ao menu inicial...");
                        return;

                    default:
                        Console.WriteLine("Digite uma opcao valida!");
                        break;

                }
            }

            public static void SwitchCase2(int opcaoMenu2, List<Conta> minhasContas)
            {
                switch (opcaoMenu2)
                {
                    // Extrato da conta
                    case 1:

                        break;

                    // Incluir transação
                    case 2:

                        break;

                    // Editar a última transação
                    case 3:

                        break;

                    // Transferir fundos
                    case 4:


                    //sair p/ menu inicial
                    case 5:
                        Console.WriteLine("Retornando ao menu inicial...");
                        return;

                    default:
                        Console.WriteLine("Digite uma opcao valida!");
                        break;

                }
            }

            public static void SwitchCase3(int opcaoMenu3, List<Conta> minhasContas)
            {
                switch (opcaoMenu3)
                {
                    // Resumo das contas
                    case 1:

                        break;

                    // Resumo de receitas e despesas do mês
                    case 2:

                        break;

                    // Saldo geral dos últimos 6 meses
                    case 3:

                        break;

                    // MINHA FUNCIONALIDADE
                    case 4:


                    //sair p/ menu inicial
                    case 5:
                        Console.WriteLine("Retornando ao menu inicial...");
                        return;

                    default:
                        Console.WriteLine("Digite uma opcao valida!");
                        break;

                }
            }
    }







