using System;
public enum TipoConta
{
    Corrente = 0,
    Poupança = 1
}
public abstract class Conta
{
    private static int numeroConta = 1569;
    private int contaId;
    private string numero;
    private decimal saldo;
    private TipoConta tipo;
    

   public string getNumero()
    {
        return numero;
    }
    public decimal getSaldo()
    {
        return saldo;
    }
    public void setSaldo(decimal valor)
    {
        if (tipo == TipoConta.Poupança && valor<0)
        {
            throw new ArgumentException($"Impossibilidade.");
        }
        else
        {
            this.saldo = valor;
        }
    }

    public Conta(TipoConta tipo)
    {
        Random random = new Random();
        int numeroAleatorio = random.Next(0, 6);
        char letra = (char)('A' + numeroAleatorio);
        saldo = 0;
        this.tipo = tipo;
        numero = numeroConta++.ToString() + letra;
        Console.WriteLine($"Conta Criada. Número da Conta: {this.numero}");
    }


    public void Depositar(decimal quantia)
    {
        saldo += quantia;
        Console.WriteLine("Deposito com Sucesso.");
    }

    public void Transferir(decimal quantia)
    {
        if (this.saldo < quantia)
        {
            throw new ArgumentException($"Saldo insuficiente. Saldo atual: R$ {this.saldo}");
        }

        this.saldo -= quantia;

        Console.WriteLine($"Transferência de R$ {quantia} realizada com sucesso.");
        Console.WriteLine($"Saldo atual: R$ {this.saldo}");
    }


}



