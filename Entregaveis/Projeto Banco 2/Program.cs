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

            int numeroConta = 6000;
            int opcao;

            do
            {
                Console.WriteLine("Menu de Operações Bancárias");
                Console.WriteLine("--------------------------");
                Console.WriteLine("1. Cadastrar nova Conta");
                Console.WriteLine("2. Transferir Dinheiro");
                Console.WriteLine("3. Depositar Dinheiro");
                Console.WriteLine("4. Consultar Saldo");
                Console.WriteLine("5. Sacar Dinheiro");
                Console.WriteLine("6. Alterar Dados Cliente");
                Console.WriteLine("7. Excluir Conta");
                Console.WriteLine("8. Sair");
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
                                Transferir(login());                            
                            
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Erro: {ex.Message}");
                        }
                        break;
                    case 3:
                        try
                        {
                           depositar(login() );
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
                        try
                        {
                            sacar(login());
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Erro: {ex.Message}");
                        }
                        break;
                    case 6:
                        try
                        {
                            AlterarDados(login());
                        }
                        catch(Exception ex)
                        {
                            Console.Error.WriteLine($"Erro: {ex.Message}");
                        }
                        break;
                    case 7:
                        try
                        {
                            Console.WriteLine("Certeza? Digite 1:");
                            opcao = int.Parse(Console.ReadLine());
                            if (opcao == 1)
                            {
                                Console.WriteLine("Desculpe por não atender seus padrões. Digite seus dados pela ultima vez:");
                                DatabaseConnection.ExcluirContaCliente(login());

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Erro: {ex.Message}");
                        }
                        
                        
                        break;
                    case 8:
                        Console.WriteLine("Obrigado por utilizar nossos serviços.");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
                atualizarTipo();
            } while (opcao != 8);

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
                DatabaseConnection.CriarConta(numero, (tipoConta-1));
                if (tipoConta == 1)
                {
                    
                    DatabaseConnection.CriarContaCorrente(numero);
                    
                }
                else
                {
                    
                   DatabaseConnection.CriarContaPoupanca(numero);
                    
                }
                int idConta = DatabaseConnection.setIdConta(numero);                
                DatabaseConnection.CriarCliente(nome, cpf, senha, data, idConta);

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
            
            void Transferir(int idRementente)
            {
                Console.Write("Digite o valor para transferencia: ");
                double valor = double.Parse(Console.ReadLine());
                if (Convert.ToDecimal(valor) > DatabaseConnection.getSaldo(idRementente))
                {
                    throw new ArgumentException("Saldo Insuficiente");
                }
                if (valor < 0)
                {
                    throw new ArgumentException("Valor invalido.");
                }
                Console.Write("Digite o numero da Conta: ");
                string conta = Console.ReadLine();
                int idDestinatario = DatabaseConnection.setIdConta(conta);
                if (idDestinatario == -1)
                {
                    throw new ArgumentException("Conta Não Encontrada.");

                }
                DatabaseConnection.AlterarSaldo(idRementente, valor*-1);
                DatabaseConnection.AlterarSaldo(idDestinatario, valor);
            }
            bool depositar(int id)
            {
                Console.Write("Digite o valor para deposito: ");
                double valor = double.Parse(Console.ReadLine());
                if (valor>0)
                {
                    DatabaseConnection.AlterarSaldo(id, valor);
                    return true;
                }
                throw new ArgumentException("Valor Invalido.");
            }
            bool sacar(int id)
            {
                Console.Write("Digite o valor para saque: ");
                double valor = double.Parse(Console.ReadLine());
                if (Convert.ToDecimal(valor)<= DatabaseConnection.getSaldo(id)) {
                    DatabaseConnection.AlterarSaldo(id, (valor * -1)); return true;
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
                
                if (DatabaseConnection.setSenha(id) != senha)
                    {
                        throw new ArgumentException("Senha Errada.");
                    }
                return id;

                
                
            }

            void atualizarTipo()
            {
                DatabaseConnection.AtualizarTipoCliente();
            }

            void AlterarDados(int id)
            {
                Console.WriteLine("Menu de Opções");
                Console.WriteLine("--------------------------");
                Console.WriteLine("1. Nome");
                Console.WriteLine("2. Senha");
                Console.WriteLine("3. Conta Poupança/Corrente");                
                Console.WriteLine();
                Console.Write("Digite a opção desejada: ");
                int escolha = int.Parse(Console.ReadLine());

                Console.WriteLine();

                switch (escolha)
                {
                    case 1:
                        Console.Write("Digite o novo nome:");
                        string n = Console.ReadLine();                        
                        DatabaseConnection.AlterarDado(id, "nome", n);
                        break;
                    case 2:
                        Console.Write("Digite a nova senha: ");
                        string senha = Console.ReadLine();
                        DatabaseConnection.AlterarDado(id, "senha", senha);
                        break;
                    case 3:
                        DatabaseConnection.mudarTipoConta(id);
                        break;
                    default:
                        Console.WriteLine("Opção incorreta.");
                        break;
                }
            }


        }
    }
}
