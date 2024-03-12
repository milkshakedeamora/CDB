using System;

class Mouse1
{
    static int[] situacao = { 0, 0, 0, 0 };
    private static void inserirMouse(int i)
    {
        situacao[i]++;
    }
    private static int valorTotal()
    {
        return 0 + situacao[0] + situacao[1] + situacao[2] + situacao[3];
    }
    public static string retornarSituacao()
    {
        return $"Situação:\t\t\t\tQuantidade:\tPorcentagem: " +
            $"\n1- Necessita da esfera\t\t\t{situacao[0]}\t\t{(situacao[0] / (double)valorTotal()) * 100}%" +
            $"\n2-Necessita de limpeza\t\t\t{situacao[1]}\t\t{(situacao[1] / (double)valorTotal()) * 100}%" +
            $"\n3-Necessita troca do cabo ou conector\t{situacao[2]}\t\t{(situacao[2] / (double)valorTotal()) * 100} %" +
            $"\n4-Quebrado ou inutilizado\t\t{situacao[3]}\t\t{(situacao[3] / (double)valorTotal()) * 100}%";
    }
    public static bool iniciarPrograma()
    {
        Console.WriteLine("Digite a opção:");
        int i = Convert.ToInt32(Console.ReadLine());
        switch (i)
        {
            case 0:
                Console.WriteLine(retornarSituacao());
                return false;
            case 1:
                inserirMouse(0);
                break;
            case 2:
                inserirMouse(1);
                break;
            case 3:
                inserirMouse(2);
                break;
            case 4:
                inserirMouse(3);
                break;
            default:
                Console.WriteLine("Opção invalida.");
                break;

        }

        return true;
    }

}