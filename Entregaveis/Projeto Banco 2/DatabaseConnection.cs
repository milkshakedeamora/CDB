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
    //ATUALIZANDO ... 
    public static void changeSaldo(int id, double valor)
    {
        using (SqlConnection connection = GetConnection())
        {
            string sql = $"UPDATE tb_conta SET saldo = {setSaldo(id)+valor} WHERE contaId = id";
            SqlCommand command = new SqlCommand(sql, connection);            
            command.ExecuteNonQuery();
        }
    }


    // RETORNANDO ... 
    public static string getTipoConta(int id)
    {
        SqlConnection connection = GetConnection();
        string tipo = null;
        string stringSql = $"SELECT * FROM tb_conta INNER JOIN tb_TipoConta ON tb_conta.contaId = tb_TipoConta.tipoContaId WHERE contaId = {id}";
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
        string tipo = "N";
        string stringSql = $"SELECT * FROM tb_cliente INNER JOIN tb_TipoCliente ON tb_cliente.clienteId = tb_TipoCliente.tipoClienteId WHERE clienteId = {id}";
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


        }

        connection.Close();
        return $"{nome}, Cliente {getTipoCliente(idCliente)}  - Conta {getTipoConta(idConta)}  :{numero} Saldo: R$ {saldo}";

    }
        public static double setSaldo(int id)
    {
        SqlConnection connection = GetConnection();
        double saldo = 0;
        string stringSql = $"SELECT tb_conta WHERE tb_conta.contaId = {id}";
        SqlCommand command = new SqlCommand(stringSql, connection);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            saldo = reader.GetDouble("contaId");
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


    // CRIANDO ... 
    public void CriarConta(string numero, int tipo)
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

    public void CriarCliente(string nome, string cpf, string senha, DateTime dataDeNascimento, int conta)
    {
        using (SqlConnection connection = GetConnection())
        {
            string sql = "INSERT INTO tb_cliente (nome, cpf, senha, tipo, conta) VALUES (@nome, @cpf, @senha,@tipo,@conta)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@cpf", cpf);
            command.Parameters.AddWithValue("@senha",senha);
            //command.Parameters.AddWithValue("@dataDeNascimento", dataDeNascimento);
            command.Parameters.AddWithValue("@tipo",0);
            command.Parameters.AddWithValue("@conta", conta);
            command.ExecuteNonQuery();
        }

    }

    public void CriarContaCorrente(string numero)
    {
        using (SqlConnection connection = GetConnection())
        {
            string sql = "INSERT INTO tb_contaCorrente (numero) VALUES (@numero)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@numero", numero);            
            command.ExecuteNonQuery();
        }

    }
    public void CriarContaPoupanca(string numero)
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
