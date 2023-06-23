using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gerenciamento_Despesas
{
    public class GerenciamentoTransacoes
    {
        //Método responsável por realizar edição da ultima transação cadastrada 
        public static List<Conta> EditarTransacao(List<Conta> minhasContas, Transacao minhaTransacao)
        {

            int conta, posicaoConta;
            List<Transacao> trans;
            StringBuilder sb;
            Transacao ultimaTransacao;

            posicaoConta = RetornaIdTransacao(minhasContas, minhaTransacao);
            minhasContas[posicaoConta].GetTransacoes().Remove(minhaTransacao);

            sb = Utilidades.RetornaMensagem("     EDITAR ÚLTIMA TRANSAÇÃO     ");
            Console.WriteLine(sb.ToString());
            Console.WriteLine(minhaTransacao.ToString());

            Console.WriteLine("Para alterar os dados da transacao acima:");
            minhaTransacao = Transacao.CriarTransacao();
            minhasContas[posicaoConta].SetTransacao(minhaTransacao);

            Conta.ImprimirContasAtivas(minhasContas);

            Console.WriteLine("TRANSACAO ALTERADA COM SUCESSO!");

            return minhasContas;

        }

        //metodo que pesquisa que permite identificar a localização de uma transação especifica numa das Contas
        public static int RetornaIdTransacao(List<Conta> minhasContas, Transacao minhaTransacao)
        {
            string descricao = minhaTransacao.Descricao;
            double valor = minhaTransacao.Valor;
            int idBanco = 0;

            foreach (Conta c in minhasContas)
            {
                foreach (Transacao t in c.GetTransacoes())
                {
                    if (t.Descricao.Equals(descricao) && (t.Valor == valor))
                    {
                        return idBanco = c.GetId();
                    }
                }
            }
            return idBanco - 1;
        }

        //Método que exibe as maiores transações e menores transações de todas as contas
        public static void TransacaoMenoreMaior(List<Conta> minhasContas)
        {
            Transacao maiorValor, menorValor;

            StringBuilder sb;


            foreach (Conta c in minhasContas)
            {
                List<Transacao> transacoes = c.GetTransacoes();

                // Ordene as transações da conta pelo valor
                List<Transacao> transacoesOrdenadas = transacoes.OrderBy(t => t.Valor).ToList();
                menorValor = transacoesOrdenadas.First();
                maiorValor = transacoesOrdenadas.Last();


                sb = Utilidades.RetornaMensagem($"     TRANSAÇÃO COM MAIOR VALOR NA CONTA ID :{c.GetId().ToString()}");
                Console.WriteLine(sb.ToString());
                Console.WriteLine(maiorValor.ToString());

                sb = Utilidades.RetornaMensagem($"    TRANSAÇÃO COM MENOR VALOR NA CONTA ID :{c.GetId().ToString()}");
                Console.WriteLine(sb.ToString());
                Console.WriteLine(menorValor.ToString());
            }

        }
    }
}
