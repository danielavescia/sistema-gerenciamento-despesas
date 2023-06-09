
using Sistema_Gerenciamento_Despesas;
using System.Collections.Generic;

internal class Program
{

    static void Main(string[] args)
    {

        List<Conta> minhasContas = CriarObjetos.CarregarDados();
        Menu menu = new Menu(minhasContas);

    }
}







