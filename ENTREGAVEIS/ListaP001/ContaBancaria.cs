using System;
class ContaBancaria
{

    private int numeroConta;
    private string nomeTitular;
    private double saldo;
    public ContaBancaria(int numeroConta, string nomeTitular, double saldo)
    {
        this.numeroConta = numeroConta;
        this.nomeTitular = nomeTitular;
        this.saldo = (saldo > 0) ? saldo : 0;
    }
    public double depositos(double valor)
    {
        if (valor > 0) saldo += valor;
        return saldo;
    }
    public double saques(double valor)
    {
        if (valor <= this.saldo) saldo -= valor;
        return saldo;
    }
}