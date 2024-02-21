using System;

public class Livro
{
    private string nome;
    private string autor;
    private int numeroPaginas;
    private bool disponivel = true;
    public Livro(string nome, string autor, int numeroPaginas)
    {
        this.nome = nome;
        this.autor = autor;
        this.numeroPaginas = numeroPaginas;
    }
    public string Emprestar()
    {
        if (disponivel)
        {
            disponivel = false;
            return "Emprestado";
        }
        return "Não Foi Emprestado.";

    }
    public string Devolver()
    {
        if (!disponivel)
        {
            disponivel = true;
            return "Devolvido.";

        }
        return "Não Precisava ser Devolvido.";


    }
    public string Disponivel()
    {
        if (disponivel) return "Disponivel.";
        return "Indisponivel";
    }
}

