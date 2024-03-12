using System;
using System.Collections.Generic;

class Mouse2
{
    static List<string>[] situacao = { new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>() };
    private static void inserirMouse(int i, string identificador)
    {
        situacao[i].Add(identificador);
    }
    static string retornarDefeitos(int i)
    {
        if (situacao[i].Count > 0)
        {
            return $"{string.Join(", ", situacao[i])}\nTOTAL: {situacao[i].Count}";
        }
        else
        {
            return $"Nenhum\nTOTAL: {situacao[i].Count}";
        }
    }

    public static string retornarSituacao()
    {
        return "--- MOUSES SEM DEFEITOS ---: " +
            $"\n{retornarDefeitos(0)}" +
            "\n--- MOUSE Necessita da esfera ---" +
            $"\n{retornarDefeitos(1)}" +
            "\n--- MOUSE Necessita de limpeza --- " +
            $"\n{retornarDefeitos(2)}" +
            "\n--- MOUSE Necessita troca do cabo ou conector ---" +
            $"\n{retornarDefeitos(3)}" +
            "\n--- MOUSE Quebrado ou inutilizado --- " +
           $"\n{retornarDefeitos(4)}";
    }
    public static bool iniciarPrograma()
    {
        Console.Write("Digite o identificador:");
        string identificador = Console.ReadLine();
        if (string.IsNullOrEmpty(identificador))
        {
            Console.WriteLine("Identificador não pode ser nulo ou vazio. Tente novamente.");
            return true;
        }
        if (Convert.ToInt32(identificador) == 0)
        {
            Console.WriteLine(retornarSituacao());
            return false;
        }
        Console.WriteLine("Quantos defeitos?");
        int i = Convert.ToInt32(Console.ReadLine());
        if (i == 0)
        {
            situacao[0].Add(identificador);
        }
        else
        {
            for (int j = 0; j < i; j++)
            {
                int def = 0;
                do
                {
                    Console.WriteLine($"Digite o {j + 1}ª defeito:");
                    def = Convert.ToInt32(Console.ReadLine());
                    if (def >= 1 && def <= 4)
                    {
                        inserirMouse(def, identificador);
                    }
                    else
                    {
                        Console.WriteLine("Defeito inválido. Digite novamente.");
                    }
                } while (def > 4 || def < 1);
            }
        }

        return true;
    }

}
