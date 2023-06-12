namespace Sistema_Gerenciamento_Despesas
{
    internal class Menu
    {

        private bool menuEstaAtivo;
        private int opcaoMenu;
        List<Conta> minhasContas;

        public Menu(List<Conta> minhasContasVirtual)
        {
            minhasContas = minhasContasVirtual;
            menuEstaAtivo = true;
            int opcaoMenu;
            Iniciar();
        }

        public void Iniciar()
        {

            while (menuEstaAtivo)
            {

                ImprimeMenuInicial();
                opcaoMenu = int.Parse(Console.ReadLine());
                Console.WriteLine("Direcionando para a opcao desejada..");

                switch (opcaoMenu)
                {
                    case 1:
                        ImprimeMenuCase1();
                        int opcaoMenu1 = int.Parse(Console.ReadLine());
                        GerenciarConta(opcaoMenu1, minhasContas);
                        break;
                    case 2:
                        Transacao t = null;
                        ImprimeMenuCase2();
                        int opcaoMenu2 = int.Parse(Console.ReadLine());
                        GerenciarTransacoes(opcaoMenu2, minhasContas, t);
                        break;

                    case 3:

                        ImprimeMenuCase3();
                        int opcaoMenu3 = int.Parse(Console.ReadLine());
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

        public static void ImprimeMenuInicial()
        {
            Wait(600);
            Console.Clear();
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

        public static void ImprimeMenuCase1()
        {
            Wait(600);
            Console.Clear();
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
            Wait(600);
            Console.Clear();
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

        public static void ImprimeMenuCase3()
        {
            Wait(600);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(".___________________________________________.");
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

        public void GerenciarConta(int opcaoMenu1, List<Conta> minhasContas)
        {
            switch (opcaoMenu1)
            {
                // Cadastrar conta
                case 1:
                    Conta.CriarConta(minhasContas);
                    Wait(5000);
                    Console.WriteLine("Retornando ao menu...");
                    break;

                // Remover conta;
                case 2:
                    Conta.RemoverConta(minhasContas);
                    Wait(5000);
                    Console.WriteLine("Retornando ao menu...");
                    break;

                // Mesclar contas
                case 3:
                    Conta.MesclarContas(minhasContas);
                    Wait(5000);
                    Console.WriteLine("Retornando ao menu...");
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

        public void GerenciarTransacoes(int opcaoMenu2, List<Conta> minhasContas, Transacao t)
        {

            switch (opcaoMenu2)
            {

                // Extrato da conta
                case 1:

                    Conta.ExtratoConta(minhasContas);
                    Console.WriteLine("Retornando ao menu inicial...");
                    Wait(1000);
                    break;

                // Incluir transação
                case 2:

                    t = AdicionarTransacao(minhasContas);
                    t.ToString();
                    Console.WriteLine("Retornando ao menu inicial...");
                    Wait(1000);
                    break;

                // Editar a última transação
                case 3:

                    if (t == null)
                    {
                        Console.WriteLine("Primeiro cadastre uma transação");
                    }
                    else
                    {

                        minhasContas = Transacao.EditarTransacao(minhasContas, t);
                        Console.WriteLine("Retornando ao menu inicial...");

                    }
                    break;

                // Transferir fundos
                case 4:


                //sair p/ menu inicial
                case 5:
                    Console.WriteLine("Retornando ao menu inicial...");
                    break;

                default:
                    Console.WriteLine("Digite uma opcao valida!");
                    break;

            }
        }

        public static void PainelControle(int opcaoMenu3, List<Conta> minhasContas)
        {
            switch (opcaoMenu3)
            {
                // Resumo das contas
                case 1:

                    Conta.ImprimeSaldo(minhasContas);
                    Console.WriteLine("Retornando ao menu...");
                    Wait(5000);
                    break;

                // Resumo de receitas e despesas do mês
                case 2:
                    Conta.ResumoReceitasDespesas(minhasContas);
                    Wait(5000);
                    Console.WriteLine("Retornando ao menu...");
                    break;

                // Saldo geral dos últimos 6 meses
                case 3:

                    break;

                // MINHA FUNCIONALIDADE
                case 4:


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
        public Transacao AdicionarTransacao(List<Conta> minhasContas)
        {
            String regex = "^(1|2)$";
            int opcao;
            Transacao t;

            do
            {
                t = Transacao.CriarTransacao();

                Console.WriteLine("Digite a id da conta que voce deseja adicionar esta transação:");
                int conta = Conta.RetornaNumeroConta(minhasContas);

                Conta.AdicionaTransacaoConta(minhasContas, t, conta);

                Console.WriteLine("Gostaria de adicionar mais transações? Digite: ");
                Console.WriteLine("1 - SIM     2 - NÃO");
                opcao = Transacao.RetornaInt(regex);

            } while (opcao == 1);

            return t;
        }
    }
}


