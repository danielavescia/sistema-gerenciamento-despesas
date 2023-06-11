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
            List<Transacao> minhasTransacoes = new List<Transacao>();
            List<Conta> minhasContas = new List<Conta>();

            // Criando a contas     
            minhasContas.Add(new Conta(1, "Banco AZB", "5900", "54489-6", 0.0));
            minhasContas.Add(new Conta(2, "Banco OAOA", "001", "9000-8", 0.0));

            // Criando e adicionado set de transações na lista minhasTransacoes
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 5, 3), "Receita", "Salário", "Salário ref. Fevereiro/2023", 3200.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 5, 3), "Despesa", "Alimentação", "Restaurante", 24.70));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 6), "Despesa", "Casa", "Conserto geladeira", 272.90));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 6), "Despesa", "Energia", "Energia Elétrica", 78.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 7), "Receita", "Freelance", "Serviços de consultoria", 1500.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 7), "Despesa", "Transporte", "Uber", 35.50));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 8), "Despesa", "Lazer", "Ingresso de cinema", 42.00));
            minhasTransacoes.Add(new Transacao(new DateOnly(2023, 3, 8), "Despesa", "Saúde", "Consulta médica", 200.00));

            //adiciona as transações de 0-3 na minhasContas[0] e as transações de 4 - 7 na minhasContas[1]
            for (int i = 0; i < 4; i++) 
            {
                Conta.AdicionaTransacaoConta(minhasContas, minhasTransacoes[i], 0); 
            }

            for(int i = 4; i < 7; i++) 
            {
                Conta.AdicionaTransacaoConta(minhasContas, minhasTransacoes[i], 1);
            }

            return minhasContas; 
        }
       
    }
}
