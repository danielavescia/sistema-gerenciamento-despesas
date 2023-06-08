using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Sistema_Gerenciamento_Despesas
{
    internal class ContaTest
    {

        [TestMethod]
        public void TestCriarConta_DeveAdicionarContaNaLista()
        {
            // Arrange
            List<Conta> minhasContas = new List<Conta> { };

            ContaTest test1 = new ContaTest();

            // Act
            List<Conta> resultado = Conta.CriarConta(minhasContas);

            // Assert
            Assert.AreEqual(1, resultado.Count);
        }
    }
}
