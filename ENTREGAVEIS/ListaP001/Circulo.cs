using System;

class Circulo
{
    // Atributo para armazenar o raio do círculo
    private double raio;

    // Construtor da classe
    public Circulo(double raio)
    {
        this.raio = raio;
    }

    // Método para calcular a área do círculo
    public double CalcularArea()
    {
        return Math.PI * Math.Pow(raio, 2);
    }

    // Método para calcular o perímetro do círculo (circunferência)
    public double CalcularPerimetro()
    {
        return 2 * Math.PI * raio;
    }
}