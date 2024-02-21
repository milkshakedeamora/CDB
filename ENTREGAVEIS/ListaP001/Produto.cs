using System;

public class Produto
{
    private string nome;
    private double preco;
    private int quantidadeEstoque;
    public Produto(string nome, double preco, int quantidadeEstoque)
    {
        this.nome = nome;
        this.preco = preco;
        this.quantidadeEstoque = (quantidadeEstoque > 0) ? quantidadeEstoque : 0;
    }

    public double ValorTotalEstoque()
    {
        return preco * quantidadeEstoque;
    }

    public bool DisponivelEmEstoque()
    {
        return (quantidadeEstoque > 0);
    }
    public bool DisponivelEmEstoque(int quantidade)
    {
        return (quantidadeEstoque >= quantidade);
    }
}
