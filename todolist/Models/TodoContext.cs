using System;
using MySql.Data.MySqlClient;

namespace todolist.Models;

public class TodoContext
{

    private string ConnectionString { get; set; }

    public TodoContext()
	{
        ConnectionString = "server=localhost;port=3306;database=todolist;user=root;password=P@ssw0rd";
    }

    // Permite obter a connecção com o MySQL
    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

    public List<Tarefa> consultaTarefas() {

        //Lista para guardar as tarefas num formato que seja válido para programar facilmente. 
        List<Tarefa> tarefas = new List<Tarefa>();

        //Estabelecer uma ligação com a base de dados
        using ( MySqlConnection conn = GetConnection())
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Tarefa;", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarefas.Add(new Tarefa()
                    {
                        Id = reader.GetInt32("id"),
                        Descricao = reader.GetString("descricao"),
                        DataInicio = reader.GetString("dataInicio"),
                        DataFim = reader.GetString("dataFim")
                    });

                }
            } 
               
        }

        return tarefas;

    }

    public void criarTarefa(String descricao,String dataInicio,String dataFim) {


        using (MySqlConnection conn = GetConnection())
        {
            //Abrimos uma coneção com a base de dados
            conn.Open();

            //Criamos uma query de update 
            MySqlCommand query = new MySqlCommand("INSERT INTO Tarefa (Descricao,datainicio,datafim) VALUES (@descricao,@datainicio,@datafim)", conn);

            //Para evitar o SQL Injection usamos o mecanismo AddWithValue em vez de colocarmos directamente na string da Query
            query.Parameters.AddWithValue("@descricao", descricao);
            query.Parameters.AddWithValue("@datainicio", dataInicio);
            query.Parameters.AddWithValue("@datafim", dataFim);

            //Como as queries de update não devolvem dados usamos o execute non query em vez do Reader como no exemplo ListAll
            query.ExecuteNonQuery();

            //Fechamos a ligação com a base de dados
            conn.Close();
        }
    }

    public void apagarTarefa(int id)
    {
        using (MySqlConnection conn = GetConnection())
        {
            //Abrimos uma coneção com a base de dados
            conn.Open();

            //Criamos uma query de update 
            MySqlCommand query = new MySqlCommand("DELETE FROM Tarefa WHERE id=@id;", conn);

            //Para evitar o SQL Injection usamos o mecanismo AddWithValue em vez de colocarmos directamente na string da Query
            query.Parameters.AddWithValue("@id", id);

            //Como as queries de update não devolvem dados usamos o execute non query em vez do Reader como no exemplo ListAll
            query.ExecuteNonQuery();

            //Fechamos a ligação com a base de dados
            conn.Close();
        }
    }

    public string ActualizarTarefa(Tarefa tarefa)
    {
        using (MySqlConnection conn = GetConnection())
        {
            //Abrimos uma coneção com a base de dados
            conn.Open();

            //Criamos uma query de update 
            MySqlCommand query = new MySqlCommand("UPDATE Tarefa SET descricao=@descricao, dataInicio=STR_TO_DATE(@dataInicio, \"%d/%m/%Y %T\"), dataFim=STR_TO_DATE(@dataFim, \"%d/%m/%Y %T\") WHERE id=@id;", conn);

            //Para evitar o SQL Injection usamos o mecanismo AddWithValue em vez de colocarmos directamente na string da Query
            query.Parameters.AddWithValue("@id", tarefa.Id);
            query.Parameters.AddWithValue("@descricao", tarefa.Descricao);
            query.Parameters.AddWithValue("@dataInicio", tarefa.DataInicio);
            query.Parameters.AddWithValue("@dataFim", tarefa.DataFim);
            //Como as queries de update não devolvem dados usamos o execute non query em vez do Reader como no exemplo ListAll

            try
            {
                if (query.ExecuteNonQuery() == 1)
                {
                    return "Actualização efectuada com sucesso";
                }
                else
                {
                    return null;
                }
            }catch (Exception exp){
                return null;
            }

            //Fechamos a ligação com a base de dados
            conn.Close();
        }
    }
}