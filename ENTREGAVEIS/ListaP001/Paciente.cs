using System;
using System.Collections.Generic;

class Paciente
{
    private string nome;
    private int idade;
    private List<string> historicoConsultas;

    public Paciente(string nome, int idade, List<string> historicoConsultas)
    {
        this.nome = nome;
        this.idade = idade;
        this.historicoConsultas = historicoConsultas;
    }

    public void AdicionarConsulta(string consulta)
    {
        historicoConsultas.Add(consulta);
    }

    public void ExibirHistoricoConsultas()
    {
        foreach (string consulta in historicoConsultas)
        {
            Console.WriteLine("- " + consulta);
        }
    }
}
