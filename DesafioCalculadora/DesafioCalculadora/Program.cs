
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;

double n1, n2;

Console.WriteLine("DESAFIO DA CALCULADORA");
string continuar;
do
{
    calculadora();
    Console.WriteLine("Deseja continuar?\n1-Sim\n2-Não");
    continuar = Console.ReadLine();
    if (continuar != "2" && continuar != "1")
    {
        Console.WriteLine("Opção Inválida.");
        continuar = "1";
    }
}
while (continuar.Equals("1"));
void calculadora()
{
    Console.WriteLine("1 - Digite 1 para digitar um ano para descobrir ser bisexto \n" +
    "2 - Digite 2 para digitar uma conta matematica \n" +
    "3 - Digite 3 para escolher uma opção matematica e realizar a operação ");
    int opcao = int.Parse(Console.ReadLine());
    switch (opcao)
    {
        case 1:
            Console.WriteLine("ANO BISSEXTO ? \n Digite o ano:");
            int ano = int.Parse(Console.ReadLine());
            string resultado = (ano % 4 == 0 && ano % 100 != 0) || (ano % 400 == 0) ? "ANO BISSEXTO" : "ANO NÃO BISSEXTO";
            Console.WriteLine(resultado);
            break;

        case 2:
            Console.WriteLine("EXPRESSÃO MÁTEMATICA \n Operações aceitas:\n " +
                "+ (soma) \n - (subtração) \n * (multiplicação) \n \\ (divisão) \n % (porcentagem) \n" +
                "Digite na ordem (primeiro numero)-espaço-(operação)-espaço-(segundo numero) \n" +
                "Digite sua expressão:");
            string expressao = Console.ReadLine();
            string[] partes = expressao.Split(" ");

            if (partes.Length != 3)
            {
                Console.WriteLine("Expressão inválida. Digite na ordem (primeiro numero)-espaço-(operação)-espaço-(segundo numero)");
            }

            n1 = double.Parse(partes[0]);
            n2 = double.Parse(partes[2]);
            Console.WriteLine($"Resultado:{calcularOperacao(n1, n2, partes[1])}");

            break;

        case 3:
            Console.WriteLine("OPERAÇÃO MÁTEMATICA \\n Operações aceitas:\\n " +
                "+ (soma) \n - (subtração) \n * (multiplicação) \n \\ (divisão) \n % (porcentagem) \n" +
                "Digite sua operação de acordo com os sinais (+ - * \\ %)");
            string operador = Console.ReadLine();
            Console.WriteLine("Digite o primeiro numero:");
            n1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Digite o segundo numero:");
            n2 = double.Parse(Console.ReadLine());
            Console.WriteLine($"Resultado:{calcularOperacao(n1, n2, operador)}");
            break;
            ;

        default:
            Console.WriteLine("Opção Inválida.");
            break;
    }
}
double calcularOperacao(double n1, double n2, string operatore) {
    switch (operatore)
    {
        case "+":
            return n1 + n2;
        case "-":
            return n1 - n2;
        case "*":
            return n1 * n2;
        case "/":
            double resultado = n2!=0 ? n1/n2: -1;
            return resultado;
        case "%":
            return (n1 * n2) / 100;
        default:
            return -502;
    }

}


