using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gerenciamento_Despesas
{
    internal class CriarObjetos
    {
        public static List<Conta> CarregarDados()
        {

            //cricao dos dados de exemplo solicitado
            List<Transacao> minhasTransacoes = new();
            List<Conta> minhasContas = new();

            // Criando a contas     
            minhasContas.Add(new Conta(1, "Banco AZB", "5900", "54489-6", 0.0));
            minhasContas.Add(new Conta(2, "Banco OAOA", "001", "9000-8", 0.0));

            // Criando e adicionado set de transações na lista minhasTransacoes
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 5, 3), "Receita", "Salário", "Salário ref. Fevereiro/2023", 3200.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 5, 3), "Despesa", "Alimentação", "Restaurante", 24.70));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 6, 6), "Despesa", "Casa", "Conserto geladeira", 272.90));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 6, 6), "Despesa", "Energia", "Energia Elétrica", 78.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 6, 7), "Receita", "Freelance", "Serviços de consultoria", 1500.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 2, 7), "Despesa", "Transporte", "Uber", 35.50));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 2, 8), "Despesa", "Lazer", "Ingresso de cinema", 42.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 8), "Despesa", "Saúde", "Consulta médica", 200.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 9), "Despesa", "Alimentação", "Supermercado", 120.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 9), "Despesa", "Educação", "Livros didáticos", 85.50));

            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 2, 10), "Receita", "Venda", "Objeto usado", 2550.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 10), "Despesa", "Casa", "Aluguel", 800.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 11), "Despesa", "Telecomunicações", "Telefone celular", 60.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 11), "Receita", "Investimento", "Dividendos", 200.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 4, 12), "Despesa", "Lazer", "Assinatura de streaming", 15.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 4, 12), "Despesa", "Saúde", "Medicamentos", 40.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 6, 13), "Receita", "Freelance", "Serviços de design", 800.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 6, 13), "Despesa", "Transporte", "Passagem de ônibus", 10.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 6, 14), "Despesa", "Lazer", "Ingresso de show", 80.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 6, 14), "Despesa", "Educação", "Cursos online", 150.00));

            //adiciona as transações de 0-9 na minhasContas[0] e as transações de 10 - 19 na minhasContas[1]
            for (int i = 0; i <= 9; i++) 
            {
                Conta.AdicionaTransacaoConta(minhasContas, minhasTransacoes[i], 0); 
            }

            for(int i = 10; i <= 19; i++) 
            {
                Conta.AdicionaTransacaoConta(minhasContas, minhasTransacoes[i], 1);
            }

            return minhasContas; 
        }
       
    }
}
