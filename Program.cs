
using Sistema_Gerenciamento_Despesas;

internal class Program
{

    static void Main(string[] args)
    {
        List<Conta> minhasContas = CriarObjetos.CarregarDados();
        Menu menu = new(minhasContas);

    }
}







