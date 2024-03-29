using System;
using System.Transactions;

public class ContaCorrente : Conta
{
    private decimal taxaManutencao;

    public ContaCorrente() : base(TipoConta.Corrente)
    {
       
    }

   


public decimal TaxaManutencao(decimal quantia)
    {
        return 0.5m;
    }

    public void DescontarTaxa(decimal quantia)
    {
        try
        {
            setSaldo(getSaldo()*(100 - taxaManutencao));
        }catch (Exception ex)
        {
            Console.WriteLine(ex.Message); 
        }
    }




}
