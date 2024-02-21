using System;

class Aluno
{
    private string nome;
    private int matricula;
    private double[] notas;

    public Aluno(string nome, int matricula, double[] notas)
    {
        this.nome = nome;
        this.matricula = matricula;
        this.notas = notas;
    }

    public double CalcularMedia()
    {
        double soma = 0;
        foreach (double nota in notas)
        {
            soma += nota;
        }
        return soma / notas.Length;
    }

    public string VerificarSituacao()
    {
        double media = CalcularMedia();
        string situacao = (media >= 7.0)? nome+" Aprovado": nome + " Reprovado";
        return situacao;
    }
}


