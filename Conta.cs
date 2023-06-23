using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace Sistema_Gerenciamento_Despesas
{
    public class Conta
    {
        //atributos 
        protected int id; //atributo visual p/ identificar as contas em situações que envolvem escolhas
        protected string banco, agencia, numeroConta;
        protected double saldo; // relacionado ao saldo da conta
        protected double saldoTotal; // relacionado ao saldo de todas as contas
        protected List<Transacao> minhastransacoes = new();
        protected Transacao trans;


        // Propriedades (getters e setters) 
        public int GetId()
        {
            return id;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public string GetBanco()
        {
            return banco;
        }

        public void SetBanco(string banco)
        {
            this.banco = banco;
        }

        public string GetAgencia()
        {
            return agencia;
        }

        public void SetAgencia(string agencia)
        {
            this.agencia = agencia;
        }

        public string GetNumeroConta()
        {
            return numeroConta;
        }

        public void SetNumeroConta(string numeroConta)
        {
            this.numeroConta = numeroConta;
        }

        public double GetSaldo()
        {

            return saldo;
        }

        public void SetSaldo(double saldo)
        {
            this.saldo = saldo;
        }

        public List<Transacao> GetTransacoes()
        {
            return minhastransacoes;
        }

        public void SetTransacoes(List<Transacao> mt)
        {
            minhastransacoes = mt;
        }

        public void SetTransacao(Transacao mt)
        {
            trans = mt;
            minhastransacoes.Add(mt);
        }

        //Construtor objeto Conta
        public Conta(int id, string banco, string agencia, string numeroConta, double saldo)
        {
            this.id = id;
            this.banco = banco;
            this.agencia = agencia;
            this.numeroConta = numeroConta;
            this.saldo = saldo;
            List<Transacao> transacao = new();
        }

        //override ToString para retornar os dados da Conta
        public override string ToString()
        {
            return
               $@"
               CONTA ID: {GetId()}
               Banco: {GetBanco()}
               Agencia: {GetAgencia()}
               Conta: {GetNumeroConta()}
               Saldo: {GetSaldo().ToString("N2")}
                ";
        }

        //método que cria Contas e adiciona a uma Lista de Contas
        public static List<Conta> CriarConta(List<Conta> minhasContas)
        {

            string banco, agencia, numeroConta, regex = "^^(?!$).*"; //regex = não pode ser string vazia
            double saldo = 0;
            int id;
            List<Transacao> minhasTransacoes;
            StringBuilder sb;

            //input usuario p/ criacao objeto
            try
            {
                sb = Utilidades.RetornaMensagem("     CRIAR CONTA     ");
                Console.WriteLine(sb.ToString());
                Console.WriteLine("Para criar uma conta adicione as informacoes solicitadas abaixo:");

                Console.WriteLine($"{"\n"}Digite o nome do seu banco:");
                banco = Utilidades.RetornaString(regex);

                Console.WriteLine($"{"\n"}Número da agência:");
                agencia = Utilidades.RetornaString(regex);

                Console.WriteLine($"{"\n"}Número da conta:");
                numeroConta = Utilidades.RetornaString(regex);

                id = AtribuiId(minhasContas);

                Conta c = new(id, banco, agencia, numeroConta, saldo); //criacao objeto conta

                minhasContas.Add(c); // adiciona na lista de contas do usuario

                //construcao da mensagem de criação da conta
                string mensagem = $"A conta do {c.GetBanco()} foi criada com sucesso!";
                sb = Utilidades.RetornaMensagem(mensagem);
                Console.WriteLine(sb);

                return minhasContas;

            }
            catch (Exception)
            {
                throw new NullReferenceException("Ocorreu um erro na criação da sua conta. Tente novamente!");
            }
        }

        //método que cria um id conforme a quantidade de contas existentes na lista de contas 
        public static int AtribuiId(List<Conta> minhasContas)
        {
            if (minhasContas == null || minhasContas.Count == 0)
            {
                return 1;
            }

            int ultimaId = minhasContas.Count;

            for (int i = 0; i < ultimaId; i++)
            {
                minhasContas[i].SetId(i + 1);
            }

            return ultimaId + 1;
        }

        //Método que imprime contas ativas
        public static void ImprimirContasAtivas(List<Conta> minhasContas)

        {
            Console.WriteLine("Estas são suas contas ativas:");

            foreach (Conta c in minhasContas)
            {
                Console.WriteLine(c.ToString());
            }
        }

        //Método que retorna um posição válida de uma conta de interesse na lista de contas
        public static int RetornaNumeroConta(List<Conta> minhasContas)
        {
            string idConta;
            int intervaloMaximo = minhasContas.Count;
            int numeroId;

            do
            {
                string regex = "^^[0-9]+$"; // regex que permite qualquer caracter exceto numeros
                numeroId = Utilidades.RetornaInt(regex);

                if (numeroId < 0 || numeroId > intervaloMaximo)
                {
                    Console.WriteLine("O número se encontra fora do intervalo das IDs de contas existentes");
                }

            } while (numeroId < 0 || numeroId > intervaloMaximo);

            return numeroId - 1; // pegar a posicao na lista corretamente 
        }

       

       
    }
}



