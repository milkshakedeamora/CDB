using System;

public class Retangulo
{
    private double largura;
    private double altura;

    public Retangulo(double largura, double altura)
    {
        this.largura = largura;
        this.altura = altura;

    }

    public double CalcularArea()
    {
        return largura * altura;
    }
    public double CalcularPerimetro()
    {
        return (largura * 2) + (2 * altura);
    }
}
