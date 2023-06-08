using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gerenciamento_Despesas
{
    internal class Transacao
    {
       private DateOnly data = new DateOnly();
       private string tipo, categoria, descricao;
       private double valor;

        //propriedades de acesso
        public DateOnly getData() 
        { 
            return data;
        }

        public void setDate(DateOnly data) 
        { 
            this.data = data;
        }

        public string getTipo() 
        {
            return tipo;
        }

        public void setTipo(string tipo) 
        { 
            this.tipo = tipo;
        }

        public string getCategoria() 
        { 
            return categoria;
        }

        public void setCategoria(string categoria) 
        { 
            this.categoria = categoria;
        }

        public string getDescricao() 
        {
            return descricao;
        }

        public void setDescricao(string descricao) 
        { 
            this.descricao = descricao;
        }

        public double getValor() 
        {
            return valor;
        }

        public void setValor(double valor) 
        {
            this.valor = valor;
        }

        public Transacao(DateOnly data, string tipo, string categoria, string descricao, double valor) 
        {
            this.data= data;
            this.tipo = tipo;
            this.categoria = categoria;
            this.descricao = descricao;
            this.valor = valor;
        }

        public override string ToString()
        {
            return 
               $@"
               Data {getData}
               Tipo: {getTipo}
               Categoria: {getCategoria}
               Descricao: {getDescricao}
               Valor: {getValor}
                ";
        }
    }
}
