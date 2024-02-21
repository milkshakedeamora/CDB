using System;

class Carro
{
    private string marca;
    private string modelo;
    private double velocidadeAtual;

    public Carro(string marca, string modelo, double velocidadeAtual)
    {
        this.marca = marca;
        this.modelo = modelo;
        this.velocidadeAtual = (velocidadeAtual > 0) ? velocidadeAtual : 0;
    }

    public double Acelerar(double valor)
    {
        return velocidadeAtual += valor;
        return velocidadeAtual;
    }

    public double Frear(double valor)
    {
        return velocidadeAtual = (velocidadeAtual - valor > 0) ? velocidadeAtual - valor : 0;
        return velocidadeAtual;
    }

    public string ExibirVelocidadeAtual()
    {
        return +velocidadeAtual + " km/h";
    }
}
