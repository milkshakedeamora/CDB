using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Projeto_banco
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Cliente, Conta> clientesContas = new Dictionary<Cliente, Conta>();
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
                            Conta conta = RetornarConta();
                            Console.WriteLine("Valor para transferencia:");
                            int valor  = int.Parse(Console.ReadLine());
                            conta.Transferir(valor);
                            atualizarTipo(conta);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Erro: {ex.Message}");
                        }
                        break;
                    case 3:
                        try
                        {
                            Conta conta = RetornarConta();
                            Console.WriteLine("Valor para deposito:");
                            int valor = int.Parse(Console.ReadLine());
                            conta.Depositar(valor);
                            atualizarTipo(conta);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Erro: {ex.Message}");
                        }
                        break;
                    case 4:
                        try
                        {
                            Conta conta = RetornarConta();                            
                            Saldo(conta);
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
                
                Console.Write("Digite o nome completo: ");
                string nome = Console.ReadLine();
                Console.Write("Digite o CPF (sem pontos ou traços): ");
                string cpf = Console.ReadLine();
                try { 
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
                Console.Write("Tipo da Conta(1-Corrente/2-Poupança): ");
                int tipoConta = int.Parse(Console.ReadLine());                
                Conta conta = tipoConta == 1 ? new ContaCorrente() : new ContaPoupanca();
                Cliente cliente = new Cliente(nome, cpf,conta);

                clientesContas.Add(cliente, conta);
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
            Conta RetornarConta()
            {
                Console.WriteLine("Digite o número da sua conta:");
                string numeroConta = Console.ReadLine();

                foreach (Conta conta in clientesContas.Values)
                {
                    if (conta.getNumero() == numeroConta)
                    {
                        return conta;
                    }
                }

                throw new ArgumentException("Conta não encontrada");
            }

            void Saldo(Conta conta)
            {
                Cliente cliente = clientesContas.FirstOrDefault(pair => pair.Value == conta).Key;
               
               Console.WriteLine($"Cliente: {cliente.retornarInformações()} - Saldo R$ {conta.getSaldo()}");
                
            }

            void atualizarTipo(Conta conta)
            {
                Cliente cliente = clientesContas.FirstOrDefault(pair => pair.Value == conta).Key;
                if (conta.getSaldo() >= 15000)
                {
                    cliente.setTipoCliente(TipoCliente.Premium);
                }else if (conta.getSaldo() >= 5000)
                {
                    cliente.setTipoCliente(TipoCliente.Super);
                }
                else
                {
                    cliente.setTipoCliente(TipoCliente.Comum);
                }
            }

        } 
    }
}
