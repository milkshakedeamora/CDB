using Microsoft.Data.SqlClient;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class DatabaseConnection
{
    
   public static SqlConnection GetConnection()

{    SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=db_projetoBanco;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    connection.Open();
    return connection;
}


    public static void CloseConnection(SqlConnection connection)
    {
        connection.Close();
    }

    //EXCLUINDO .... 
    public static void ExcluirContaCorrentePoupanca(int id)
    {
        using (SqlConnection connection = GetConnection())
        {
            // Excluir conta corrente
            string stringSql = $"DELETE FROM tb_contaCorrente WHERE numero IN (SELECT numero FROM tb_conta WHERE contaId = {id})";
            SqlCommand command = new SqlCommand(stringSql, connection);
            command.ExecuteNonQuery();

            // Excluir conta poupança
            stringSql = $"DELETE FROM tb_contaPoupanca WHERE numero IN (SELECT numero FROM tb_conta WHERE contaId = {id})";
            command = new SqlCommand(stringSql, connection);
            command.ExecuteNonQuery();
        }
    }
    public static void ExcluirContaCliente(int id)
    {
        using (SqlConnection connection = GetConnection())
        {
            

            // Excluir cliente
            string stringSql = $"DELETE FROM tb_cliente WHERE conta = {id}";
            SqlCommand command = new SqlCommand(stringSql, connection);
            command.ExecuteNonQuery();
            ExcluirContaCorrentePoupanca(id);

            // Excluir conta
            stringSql = $"DELETE FROM tb_conta WHERE contaId = {id}";
            command = new SqlCommand(stringSql, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }


    //ATUALIZANDO ... 
    public static void mudarTipoConta(int id)
    {
        string tipo;
        SqlConnection connection = GetConnection();
        if (getTipoConta(id) == "corrente")
        {
            ExcluirContaCorrentePoupanca(id);
            CriarContaPoupanca(getNumero(id));            
            string stringSql = "UPDATE tb_conta SET tipo = 1";
            SqlCommand command = new SqlCommand(stringSql, connection);
            command.ExecuteNonQuery();

        }
        else
        {
            ExcluirContaCorrentePoupanca(id);
            CriarContaCorrente(getNumero(id));
            string stringSql = "UPDATE tb_conta SET tipo = 0";
            SqlCommand command = new SqlCommand(stringSql, connection);
            command.ExecuteNonQuery();

        }
        CloseConnection(connection);
    }

    public static void AlterarDado(int id, string dado, string novoValor)
    {
        using (SqlConnection connection = GetConnection())
        {
            string sql = $"UPDATE tb_cliente SET {dado} = @novoValor WHERE conta IN (SELECT contaId FROM tb_conta WHERE contaId = @id)";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@novoValor", novoValor);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }

    public static void AlterarSaldo(int id, double valor)
    {
        using (SqlConnection connection = GetConnection())
        {
            
          
            string sql = "UPDATE tb_conta SET saldo = @novoSaldo WHERE contaId = @id";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@novoSaldo", Convert.ToDecimal(valor)+getSaldo(id));
                command.Parameters.AddWithValue("@id", id);                
                command.ExecuteNonQuery();
            }
        }
    }

    public static void AtualizarTipoCliente()
    {
            SqlConnection connection = GetConnection();
            string stringSql = "UPDATE tb_cliente SET tipo = 0";
            SqlCommand command = new SqlCommand(stringSql, connection);
            command.ExecuteNonQuery();
            stringSql = @"UPDATE tb_cliente SET tipo = 1 WHERE conta IN (SELECT contaId FROM tb_conta WHERE saldo >= 5000 AND saldo < 15000)";
            command = new SqlCommand(stringSql, connection);
            command.ExecuteNonQuery();
            stringSql = @"UPDATE tb_cliente SET tipo = 2 WHERE conta IN (SELECT contaId FROM tb_conta WHERE saldo >= 15000)";
            command = new SqlCommand(stringSql, connection);
            command.ExecuteNonQuery();
            CloseConnection(connection);
        
    }




    // RETORNANDO ... 
    public static string getTipoConta(int id)
    {
        SqlConnection connection = GetConnection();
        string tipo = null;
        string stringSql = $"SELECT * FROM tb_conta INNER JOIN tb_TipoConta ON tb_conta.tipo = tb_TipoConta.tipoContaId WHERE contaId = {id}";
        SqlCommand command = new SqlCommand(stringSql, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            tipo = reader.GetString("conta");
        }

        connection.Close();
        return tipo;

    }
    public static string getTipoCliente(int id)
    {
        SqlConnection connection = GetConnection();
        string tipo = null;
        string stringSql = $"SELECT * FROM tb_cliente INNER JOIN tb_TipoCliente ON tb_cliente.tipo = tb_TipoCliente.tipoClienteId WHERE clienteId = {id}";
        SqlCommand command = new SqlCommand(stringSql, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            tipo = reader.GetString("cliente");
        }

        connection.Close();
        return tipo;
        
    }
    public static string getDados(int id)

    {
        SqlConnection connection = GetConnection();
        string nome = null;
        string numero = null;
        decimal saldo = 0;
        int idCliente = -1;
        int idConta = -1;
        DateTime data = DateTime.MinValue;
        string stringSql = $"SELECT * FROM tb_cliente INNER JOIN tb_conta ON tb_cliente.conta = tb_conta.contaId WHERE tb_conta.contaId = {id}";
        SqlCommand command = new SqlCommand(stringSql, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            nome = reader.GetString("nome");
            numero = reader["numero"].ToString();
            saldo = reader.GetDecimal("saldo");
            idCliente = reader.GetInt32("clienteId");
            idConta = reader.GetInt32("contaId");
            data = reader.GetDateTime("dataNascimento");



        }

        connection.Close();
        return $"{nome} ({data}), Cliente {getTipoCliente(idCliente)}  - Conta {getTipoConta(idConta)}:{numero} - Saldo: R$ {saldo}";

    }
    public static decimal getSaldo(int id)
    {
        SqlConnection connection = GetConnection();
        decimal saldo = 0;
        string stringSql = $"SELECT saldo FROM tb_conta WHERE contaId = {id}";
        SqlCommand command = new SqlCommand(stringSql, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            saldo = reader.GetDecimal(0);
        }

        connection.Close();
        return saldo;
    }


    public static string setSenha(int id)
    {
        SqlConnection connection = GetConnection();
        string senha = null;        
        string stringSql = $"SELECT * FROM tb_cliente INNER JOIN tb_conta ON tb_conta.contaId = tb_cliente.conta WHERE tb_conta.contaId = {id}"; 
        SqlCommand command = new SqlCommand(stringSql, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read()) 
        {
            senha = reader["senha"].ToString(); 
        }
        
        connection.Close();
        return senha;
    }

    public static int setIdConta(string numero)
    {
        int accountId = -1;

        try
        {
            using (SqlConnection connection = GetConnection())
            {
                string sql = "SELECT contaId FROM tb_conta WHERE numero= @numero";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@numero", numero);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        accountId = reader.GetInt32(0);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Error: {ex.Message}");
        }

        return accountId;
    }


    public static int setCliente(string cpf)
    {
        SqlConnection connection = GetConnection();
        int cliente = -1;
        string stringSql = $"SELECT * FROM tb_cliente WHERE tb_cliente.cpf = {cpf}";
        SqlCommand command = new SqlCommand(stringSql, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            cliente = reader.GetInt32(0);
        }

        CloseConnection(connection);
        return cliente;
    }

    public static string getNumero(int id)
    {
        SqlConnection connection = GetConnection();
        string numero = null;
        string stringSql = $"SELECT numero FROM tb_conta WHERE tb_conta.contaId = {id}";
        SqlCommand command = new SqlCommand(stringSql, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            numero = reader.GetString(0);
        }

        CloseConnection(connection);
        return numero;
    }

    // CRIANDO ... 
    public static void CriarConta(string numero, int tipo)
    {
        using (SqlConnection connection = GetConnection())
        {
            string sql = "INSERT INTO tb_conta (numero, saldo, tipo) VALUES (@numero, @saldo, @tipo)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@numero",numero);
            command.Parameters.AddWithValue("@saldo", 0);
            command.Parameters.AddWithValue("@tipo",tipo);
            command.ExecuteNonQuery();
        }
        
    }

    public static void CriarCliente(string nome, string cpf, string senha, DateTime dataDeNascimento, int conta)
    {
        using (SqlConnection connection = GetConnection())
        {
            string sql = "INSERT INTO tb_cliente (nome, cpf, senha, dataNascimento, tipo, conta) VALUES (@nome, @cpf, @senha,@dataDeNascimento, @tipo,@conta)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@cpf", cpf);
            command.Parameters.AddWithValue("@senha",senha);
            command.Parameters.AddWithValue("@dataDeNascimento", dataDeNascimento);
            command.Parameters.AddWithValue("@tipo",0);
            command.Parameters.AddWithValue("@conta", conta);
            command.ExecuteNonQuery();
        }

    }

    public static void CriarContaCorrente(string numero)
    {
        using (SqlConnection connection = GetConnection())
        {
            string sql = "INSERT INTO tb_contaCorrente (numero) VALUES (@numero)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@numero", numero);            
            command.ExecuteNonQuery();
        }

    }
    public static void CriarContaPoupanca(string numero)
    {
        using (SqlConnection connection = GetConnection())
        {
            string sql = "INSERT INTO tb_contaCorrente (numero) VALUES (@numero)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@numero", numero);
            command.ExecuteNonQuery();
        }

    }
}
