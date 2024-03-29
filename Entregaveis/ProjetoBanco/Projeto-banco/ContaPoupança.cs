using System;

public class ContaPoupanca : Conta
{
    private decimal taxaRendimento;

    public ContaPoupanca() : base(TipoConta.Poupança)
    {
       
    }


public decimal TaxaRendimento(decimal quantia)
    {
        return taxaRendimento;
    }

    public void AcrescentarRendimento(decimal quantia)
    {
        try
        {
            setSaldo(getSaldo() * (100+taxaRendimento));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    

    
}

