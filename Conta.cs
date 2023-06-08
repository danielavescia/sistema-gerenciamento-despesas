using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Conta
    {
        private int id;
        private string banco, agencia, numeroConta;
        private double saldo;
        private List<Transacao> minhastransacoes;

        // Propriedades (getters e setters) 
        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public string getBanco()
        {
            return banco;
        }

        public void setBanco(string banco)
        {
            this.banco = banco;
        }

        public string getAgencia()
        {
            return agencia;
        }

        public void setAgencia(string agencia)
        {
            this.agencia = agencia;
        }

        public string getNumeroConta()
        {
            return numeroConta;
        }

        public void setNumeroConta(string numeroConta)
        {
            this.numeroConta = numeroConta;
        }

        public double getSaldo()
        {
            return saldo;
        }

        public void setSaldo(double saldo)
        {
            this.saldo = saldo;
        }

        //Construtor objeto Conta
        public Conta(int id, string banco, string agencia, string numeroConta, double saldo)
        {
            this.id = id;
            this.banco = banco;
            this.agencia = agencia;
            this.numeroConta = numeroConta;
            this.saldo = saldo;

        }

        //override do ToString para os dados da Conta
        public override string ToString()
        {
            return
               $@"
               CONTA {getId}
               Banco: {getBanco}
               Agencia: {getAgencia}
               Conta: {getNumeroConta}
               Saldo: {getSaldo}
                ";
        }


        //método que cria Contas e adiciona a uma Lista de Contas
        public List<Conta> CriarConta(List<Conta> minhasContas)
        {
            String banco = null, agencia = null, numeroConta = null;
            double saldo = 0;
            int id = 0;

            Console.WriteLine("Para criar uma conta adicione as informacoes solicitadas abaixo:");

            Console.WriteLine("Digite o nome do seu banco:");
            banco = Console.ReadLine();

            Console.WriteLine("Agencia:");
            agencia = Console.ReadLine();

            Console.WriteLine("Numero da conta:");
            numeroConta = Console.ReadLine();

            id = criaId(minhasContas);

            Conta c = new Conta(id, banco, agencia, numeroConta, saldo);

            minhasContas.Add(c);

            return minhasContas;

        }

        //método que cria um id conforme a quantidade de contas existentes
        public int criaId(List<Conta> minhasContas)
        {

            if (minhasContas == null)
            {
                return id = 1;
            }

            else
            {
                id = minhasContas.Count + 1;
            }
            return id;
        }


        public List<Conta> RemoverConta(List<Conta> minhasContas)
        {
            Console.WriteLine("Estas são suas contas ativas:");

            foreach (Conta c in minhasContas)
            {
                c.ToString();
            }

            Console.WriteLine("Lembre-se ao remover sua conta você irá perder todos os dados relacionados a ela");
            Console.WriteLine("Digite o Id da conta que deseja remover:");
            int contaRemover = int.Parse(Console.ReadLine());

            minhasContas.RemoveAt(contaRemover);

            Console.WriteLine($" Conta: {getId} removida com sucesso!");

            return minhasContas;
        }
        //public List<Conta> mesclarContas(List<Conta> minhasContas)
    }

}
