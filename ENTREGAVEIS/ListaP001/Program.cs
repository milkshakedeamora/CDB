namespace ListaP001
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Circulo
            double raio = 5.0;
            Circulo circulo = new Circulo(raio);
            Console.WriteLine("Raio do círculo: " + raio);
            Console.WriteLine("Área do círculo: " + circulo.CalcularArea());
            Console.WriteLine("Perímetro do círculo: " + circulo.CalcularPerimetro());
            //ContaBancaria
            ContaBancaria conta = new ContaBancaria(123456, "João", 1000.0);
            Console.WriteLine("Depósito de 500.0 realizado.Saldo:"+conta.depositos(500));            
            Console.WriteLine("Saque de 200.0 realizado.Saldo:"+conta.saques(200));
            //Retangulo
            Retangulo retangulo = new Retangulo(5.0, 3.0);
            Console.WriteLine("Área do retângulo: " + retangulo.CalcularArea());
            Console.WriteLine("Perímetro do retângulo: " + retangulo.CalcularPerimetro());
            //Aluno            
            double[] notas = { 8.5, 7.0, 6.5 };
            Aluno aluno = new Aluno("Lucas",123456, notas);
            Console.WriteLine("Média do aluno: " + aluno.CalcularMedia());
            Console.WriteLine("Situação do aluno: " + aluno.VerificarSituacao());
            //Funcionario
            Funcionario funcionario = new Funcionario("Joaquim", 1560.76, "Programador Backend");
            Console.WriteLine("Salario:"+funcionario.Salario(5473, 15644.78));
            //Produto 
            Produto produto = new Produto("Tenis", 56, 7);
            Console.WriteLine("Total em Estoque: " + produto.ValorTotalEstoque());
            Console.WriteLine("Disponivel: " + produto.DisponivelEmEstoque());
            //Triangulo
            Triangulo triangulo = new Triangulo(3, 5, 6);
            Console.WriteLine("Triângulo Valido: " + triangulo.VerificarTrianguloValido());
            Console.WriteLine("Área do triângulo: " + triangulo.CalcularArea());
            //Carro
            Carro carro = new Carro("N ented", "de carro", 60);
            Console.WriteLine("Velocidade: " + carro.ExibirVelocidadeAtual());
            Console.WriteLine("Acelerar: " + carro.Acelerar(50));
            Console.WriteLine("Frear: " + carro.Frear(30));
            //Paciente 
            List<string> consultas = new List<string> { "Nutricionista", "Psicólogo" };
            Paciente paciente = new Paciente("João", 30, consultas);
            paciente.AdicionarConsulta("Consulta de rotina");
            paciente.AdicionarConsulta("Exame de sangue");
            paciente.AdicionarConsulta("Consulta de retorno");

            paciente.ExibirHistoricoConsultas();



        }
    }
}
