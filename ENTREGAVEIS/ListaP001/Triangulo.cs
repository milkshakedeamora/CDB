using System;

class Triangulo
{
    private double lado1;
    private double lado2;
    private double lado3;

    public Triangulo(double lado1, double lado2, double lado3)
    {
        this.lado1 = lado1;
        this.lado2 = lado2;
        this.lado3 = lado3;
    }

    public bool VerificarTrianguloValido()
    {
        return lado1 + lado2 > lado3 && lado1 + lado3 > lado2 && lado2 + lado3 > lado1;
    }

    public double CalcularArea()
    {
        if(VerificarTrianguloValido())
        {
            double semiPerimetro = (lado1 + lado2 + lado3) / 2;
            return Math.Sqrt(semiPerimetro * (semiPerimetro - lado1) * (semiPerimetro - lado2) * (semiPerimetro - lado3));
        }
        else
        {
            return 0;
        }
        
    }
}

