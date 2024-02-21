using System;
public class Funcionario
{
    private string nome;
    private double salario;
    private string cargo;
    public Funcionario(string nome, double salario, string cargo)
    {
        this.nome = nome;
        this.salario = salario;
        this.cargo = cargo;
    }
    public double Salario(double descontos, double beneficios)
    {
        return salario += beneficios - descontos;
    }
}
