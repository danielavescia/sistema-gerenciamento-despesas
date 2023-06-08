﻿using System;
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
        //atributos 
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

        public List<Transacao> getTransacoes() 
        {
            return minhastransacoes;
        }

        public void setTransacoes(List<Transacao> minhastransacoes) 
        { 
            this.minhastransacoes = minhastransacoes;
        }

        //Construtor objeto Conta
        public Conta(int id, string banco, string agencia, string numeroConta, double saldo, List<Transacao> minhasTransacoes)
        {
            this.id = id;
            this.banco = banco;
            this.agencia = agencia;
            this.numeroConta = numeroConta;
            this.saldo = saldo;
            this.minhastransacoes = minhasTransacoes;

        }

        //override ToString para retornar os dados da Conta
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
            List<Transacao> minhasTransacoes = null;

            //input usuario p/ criacao objeto
            Console.WriteLine("Para criar uma conta adicione as informacoes solicitadas abaixo:");

            Console.WriteLine("Digite o nome do seu banco:");
            banco = Console.ReadLine();

            Console.WriteLine("Agencia:");
            agencia = Console.ReadLine();

            Console.WriteLine("Numero da conta:");
            numeroConta = Console.ReadLine();

            id = criaId(minhasContas);

            Conta c = new Conta(id, banco, agencia, numeroConta, saldo, minhasTransacoes);

            minhasContas.Add(c);

            return minhasContas;

        }

        //método que cria um id conforme a quantidade de contas existentes na lista de contas 
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

        //método que remove a conta selecionada pelo usuario
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

            Console.WriteLine($" Conta: {getId} - {getBanco} removida com sucesso!");

            return minhasContas;
        }

        //metodo que uni as transações da conta 2 na conta 1
        public List<Conta> mesclarContas(List<Conta> minhasContas) 
        {
            Console.WriteLine("Estas são suas contas ativas:");

            foreach (Conta c in minhasContas)
            {
                c.ToString();
            }

            Console.WriteLine("Digite a id da conta que receberá as transações da outra conta:");
            int idConta1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a id da conta que deseja unificar as transacoes com a primeira:");
            int idConta2 = int.Parse(Console.ReadLine());

            List<Transacao> lista1 = minhasContas[idConta1].getTransacoes(); //listas recebem as transações de cada conta especificada
            List<Transacao> lista2 = minhasContas[idConta2].getTransacoes();

            List<Transacao> mesclada = lista1.Concat(lista2).ToList(); //as transacoes da lista 2 são unidas com as da lista 1 
            lista1 = mesclada.OrderBy(Transacao => Transacao.getData()).ToList(); //lista 1 receberá a lista unida e ordenada por data crescente

            minhasContas[idConta1].setTransacoes(lista1); //aqui a conta 1 recebera a uniao de transações da conta 1 e 2


            return minhasContas;
        }
    }

}
