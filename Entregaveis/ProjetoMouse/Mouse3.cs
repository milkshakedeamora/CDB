using System;
using System.Collections.Generic;
using System.Linq;

public enum Defeito
{
    NecessitaEsfera = 0,
    NecessitaLimpeza = 1,
    NecessitaTrocaDoCaboOuConector = 2,
    QuebradoOuInutilizado = 4,
}

public class Mouse
{
    public int id { get; private set; }
    public List<Defeito> defeitos { get; private set; }

    public Mouse(int id)
    {
        this.id = id;
        this.defeitos = inserirDefeitos();
    }

    List<Defeito> inserirDefeitos()
    {
        List<Defeito> defeitos = new List<Defeito>();
        Console.Write("Quantos defeitos?");
        int numDefeitos = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < numDefeitos; i++)
        {
            Console.Write($"Digite o {i + 1}ª defeito:");
            int def = Convert.ToInt32(Console.ReadLine());
            defeitos.Add((Defeito)def);
        }
        return defeitos;
    }
}

public class MousePrincipal
{
    static List<Mouse> mouses = new List<Mouse>();

    public static void inserirMouse()
    {
        Console.Write("Digite o identificador:");
        int id = Convert.ToInt32(Console.ReadLine());
        if (mouses.Any(mouse => mouse.id == id))
        {
            Console.WriteLine("Um mouse com este identificador já existe. Tente novamente.");
            return;
        }
        Mouse mouse = new Mouse(id);
        mouses.Add(mouse);
    }

    public static string retornarMouses()
    {
        int totalMouses = mouses.Count;
        int mousesSemDefeito = mouses.Count(mouse => mouse.defeitos.Count == 0);
        int mousesComUmDefeito = mouses.Count(mouse => mouse.defeitos.Count == 1);

        double porcentagemSemDefeito = ((double)mousesSemDefeito / totalMouses) * 100;
        double porcentagemComUmDefeito = ((double)mousesComUmDefeito / totalMouses) * 100;

        return $"Relatório – Resumo" +
            $"\nQuantidade de mouses cadastrados: {totalMouses}" +
            $"\n% de mouses sem defeito: {porcentagemSemDefeito}%" +
            $"\n% de mouses com apenas um defeito: {porcentagemComUmDefeito}%";
    }

    public static bool iniciarPrograma()
    {
        
        
            Console.WriteLine("1-Inserir Mouse");
            Console.WriteLine("2-Retornar Mouse");
            Console.WriteLine("3-Sair");
            Console.WriteLine("Digite a opção:");
            int i = Convert.ToInt32(Console.ReadLine());
            switch (i)
            {
                case 1:
                    inserirMouse();
                    break;
                case 2:
                    Console.WriteLine(retornarMouses());
                    break;
                case 3:
                    return false;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        
        return true;
    }
}
