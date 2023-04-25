using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        private int numero { get; set; }
        private string titular { get; set; }
        private double saldoConta { get; set; }

        private const double taxaSaque = 3.50;

        public ContaBancaria(int numero, string titular, double depositoInicial = 0)
        {
            this.numero = numero;
            this.titular = titular;
            this.saldoConta = depositoInicial;
            GetInformacoesConta();
        }

        internal void Deposito(double quantia)
        {
            Console.WriteLine("Dados da conta atualizados:");
            this.saldoConta += quantia;
            GetInformacoesConta(true);
        }

        internal void Saque(double quantia)
        {
            Console.WriteLine("Dados da conta atualizados:");
            this.saldoConta -= (quantia + taxaSaque);
            GetInformacoesConta(true);
        }

        public void GetInformacoesConta(bool eAtualizacao = false)
        {
            Console.WriteLine();
            if (!eAtualizacao)
                Console.WriteLine("Dados da conta:");
            else
                Console.WriteLine("Dados da conta atualizados:");

            var saldoFormat = saldoConta.ToString("N2");
            Console.WriteLine($@"Conta {numero}, Titular: {titular}, Saldo: $ {saldoFormat}");
        }
    }
}
