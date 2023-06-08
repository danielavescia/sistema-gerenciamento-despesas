
class Program
{
    static void Main(string[] args)
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

                // cadastrar remover e mesclar contas
                case 1:

                    ImprimeMenuCase1();
                    int opcaoMenu1 = int.Parse(Console.ReadLine());

                    break;

                // Exibir e adicionar informações especificas de cada conta como : extrato, adicionar transacoes, transferir fundos, editar ultima transacao
                case 2:

                    ImprimeMenuCase2();
                    int opcaoMenu2 = int.Parse(Console.ReadLine());


                    break;

                //Visualizar resumo das contas, resumo da receita e despesa do mês e verificar saldo dos ultimos 6 meses
                case 3:

                    ImprimeMenuCase3();
                    int opcaoMenu3 = int.Parse(Console.ReadLine());




                    break;

                // Case para sair do sistema
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

    public static void ImprimeMenuInicial()
    {
        Console.WriteLine("____________________________________________");
        Console.WriteLine("    SISTEMA DE GERENCIAMENTO DE DESPESAS    ");
        Console.WriteLine("____________________________________________");
        Console.WriteLine();
        Console.WriteLine("                 MENU INICIAL               ");
        Console.WriteLine("____________________________________________");
        Console.WriteLine("1. Gerenciar Contas");
        Console.WriteLine("2. Gerenciar Transações");
        Console.WriteLine("3. Painel Geral");
        Console.WriteLine("4. Sair do Sistema");
        Console.WriteLine();
        Console.WriteLine("Digite a opção desejada:");
    }

    public static void ImprimeMenuCase1()
    {
        Console.WriteLine();
        Console.WriteLine("____________________________________________");
        Console.WriteLine("             GERENCIAR CONTA                ");
        Console.WriteLine("____________________________________________");
        Console.WriteLine("1. Cadastrar conta");
        Console.WriteLine("2. Remover conta");
        Console.WriteLine("3. Mesclar contas");
        Console.WriteLine();
        Console.WriteLine("Digite a opção desejada:");
    }

    public static void ImprimeMenuCase2()
    {
        Console.WriteLine();
        Console.WriteLine("____________________________________________");
        Console.WriteLine("           GERENCIAR TRANSACOES             ");
        Console.WriteLine("____________________________________________");
        Console.WriteLine("1. Extrato da conta");
        Console.WriteLine("2. Incluir transação");
        Console.WriteLine("3. Editar a última transação");
        Console.WriteLine("4. Transferir fundos");
        Console.WriteLine();
        Console.WriteLine("Digite a opção desejada:");
    }

    public static void ImprimeMenuCase3()
    {
        Console.WriteLine();
        Console.WriteLine("____________________________________________");
        Console.WriteLine("                PAINEL GERAL                ");
        Console.WriteLine("____________________________________________");
        Console.WriteLine("1. Resumo das contas");
        Console.WriteLine("2. Resumo de receitas e despesas do mês");
        Console.WriteLine("3. Editar a última transação");
        Console.WriteLine("4. Resumo de receitas e despesas do mês");
        Console.WriteLine("5. FUNCIONALIDADE X");
        Console.WriteLine();
        Console.WriteLine("Digite a opção desejada:");
    }
}




