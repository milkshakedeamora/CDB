using System.Globalization;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projeto_Banco_Part2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbConnection = new DatabaseConnection();
            int numeroConta = 4000;
            int opcao;

            do
            {
                Console.WriteLine("Menu de Operações Bancárias");
                Console.WriteLine("--------------------------");
                Console.WriteLine("1. Cadastrar nova Conta");
                Console.WriteLine("2. Transferir Dinheiro");
                Console.WriteLine("3. Depositar Dinheiro");
                Console.WriteLine("4. Consultar Saldo");
                Console.WriteLine("5. Sair");
                Console.WriteLine();

                Console.Write("Digite a opção desejada: ");
                opcao = int.Parse(Console.ReadLine());

                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        Cadastrar();
                        break;
                    case 2:
                        try
                        {
                            int a = login();
                            if (a!=-1)
                            {
                                Console.WriteLine("Valor para transferencia:");
                                int valor = int.Parse(Console.ReadLine());
                                Transferir(valor,a);
                                

                            }
                            
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Erro: {ex.Message}");
                        }
                        break;
                    case 3:
                        try
                        {
                            int a = login();
                            if (a != -1)
                            {
                                Console.WriteLine("Valor para deposito:");
                                int valor = int.Parse(Console.ReadLine());
                                depositar(valor,a);


                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Erro: {ex.Message}");
                        }
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine(DatabaseConnection.getDados(login()));
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Erro: {ex.Message}");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Obrigado por utilizar nossos serviços.");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (opcao != 5);

            void Cadastrar()
            {

                Console.Write("Digite o nome: ");
                string nome = Console.ReadLine();
                Console.Write("Digite o CPF (sem pontos ou traços): ");
                string cpf = Console.ReadLine();
                
                try
                {
                    if (!ValidarCPF(cpf))
                    {
                        throw new ArgumentException("CPF invalido");

                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Erro: {ex.Message}");
                    return;
                }
                Console.Write("Data de aniversario: ");
                DateTime data = receberAniversario();
                Console.Write("Tipo da Conta(1-Corrente/2-Poupança): ");
                int tipoConta = int.Parse(Console.ReadLine());
                Console.Write("Digite a senha a ser usada: ");
                string senha = Console.ReadLine();
                string numero = gerarNumero();
                dbConnection.CriarConta(numero, (tipoConta-1));
                if (tipoConta == 1)
                {
                    
                    dbConnection.CriarContaCorrente(numero);
                    
                }
                else
                {
                    
                    dbConnection.CriarContaPoupanca(numero);
                    
                }
                int idConta = DatabaseConnection.setIdConta(numero);
                Console.WriteLine(idConta);
                dbConnection.CriarCliente(nome, cpf, senha, data, idConta);

            }
            DateTime receberAniversario()
            {
                Console.Write("Data de aniversario (formato dd/MM/yyyy): ");
                string input = Console.ReadLine();

                DateTime data;
                try
                {
                    data = DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Data inválida. Insira a data no formato dd/MM/yyyy.");
                    return DateTime.Today; 
                }
                return data;

            }
            string gerarNumero()
            {
                Random random = new Random();
                int numeroAleatorio = random.Next(0, 6);
                char letra = (char)('A' + numeroAleatorio);                
                string numero = numeroConta++.ToString() + letra;
                Console.WriteLine($"Conta Criada. Número da Conta: {numero}");
                return numero;
            }

            bool ValidarCPF(string cpf)
            {
                if (cpf.Length != 11)
                {
                    return false;
                }

                foreach (char c in cpf)
                {
                    if (!char.IsDigit(c))
                    {
                        return false;
                    }
                }

                return true;
            }
            
            void Transferir(double valor,int idRementente)
            {
                Console.Write("Digite o numero da Conta: ");
                string conta = Console.ReadLine();
                int idDestinatario = DatabaseConnection.setIdConta(conta);
                if (idDestinatario == -1)
                {
                    throw new ArgumentException("Conta Não Encontrada.");

                }
                depositar(valor, idDestinatario);
                sacar(valor, idRementente);
            }
            bool depositar(double valor, int id)
            {
                if (valor>0)
                {
                    DatabaseConnection.changeSaldo(id, valor);
                    return true;
                }
                throw new ArgumentException("Valor Invalido.");
            }
            bool sacar(double valor, int id)
            {
                if (valor<=DatabaseConnection.setSaldo(id)) {
                    DatabaseConnection.changeSaldo(id, (valor * -1)); return true;
                }
                throw new ArgumentException("Saldo Insuficiente");
            }

           int login()
            {
                Console.Write("Digite o numero da Conta: ");
                string conta = Console.ReadLine();
                Console.Write("Digite sua senha: ");
                string senha = Console.ReadLine();
                int id = DatabaseConnection.setIdConta(conta);
                if(id == -1)
                {
                    throw new ArgumentException("Conta Invalida.");

                }
                return id;
            }

            void atualizarTipo()
            {
               
            }

        }
    }
}
