using System;
public enum TipoCliente
{
    Comum = 0,
    Super = 1,
    Premium = 2
}

public class Cliente
{
    private int id;
    private string cpf;
    private string nome;
    private DateTime dataNascimento;
    private TipoCliente tipo;
    private Conta conta;
    
    public string retornarInformações()
    {
        return $"{nome}, conta {tipo}";
    }
    public void setTipoCliente(TipoCliente tipo)
    {
        this.tipo = tipo ;
    }
    public Cliente(string nome, string cpf, Conta conta)
    {
        this.nome = nome;
        this.cpf = cpf;
        tipo = TipoCliente.Comum;
    }

}