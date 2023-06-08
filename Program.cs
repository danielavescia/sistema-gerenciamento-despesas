
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

            Console.WriteLine();
            ImprimeMenuInicial();

            opcaoMenu = int.Parse(Console.ReadLine());

            //a partir do switch case a opcao desejada permite a execeução do bloco de código
            switch (opcaoMenu)
            {

                // cadastrar remover e mesclar contas
                case 1:

                    Console.WriteLine("____________________________________________");
                    Console.WriteLine("GERENCIAMENTO DE CONTA");
                    Console.WriteLine("____________________________________________");


                    Console.WriteLine();
                    break;

                // Exibir e adicionar informações especificas de cada conta como : extrato, adicionar transacoes, transferir fundos, editar ultima transacao
                case 2:

                    Console.WriteLine("____________________________________________");
                    Console.WriteLine("GERENCIAR TRANSACOES: ");
                    Console.WriteLine("____________________________________________");


                    break;


                case 3:

                   
                    //Visualizar resumo das contas, resumo da receita e despesa do mês e verificar saldo dos ultimos 6 meses
                    Console.WriteLine("___________________________________________________________________________________");
                    Console.WriteLine("PAINEL GERAL: ");
                    Console.WriteLine("___________________________________________________________________________________");
   

                    break;


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
        Console.WriteLine("=============================== Sistema de Gerencimaneto de Despesas ======================================");
        Console.WriteLine();
        Console.WriteLine("=========== MENU INICIAL ===========");
        Console.WriteLine("1. Gerenciar Contas");
        Console.WriteLine("2. Gerenciar transações");
        Console.WriteLine("3. Painel Geral");
        Console.WriteLine("4. Sair do Sistema");
        Console.WriteLine();
        Console.WriteLine("Digite a opção deseja:");
    }
}




