using System;
namespace todolist.Models;

public class Tarefa
{
	public int Id { get; set; }
	public string Descricao { get; set; }
	public string DataInicio { get; set; }
	public string DataFim { get; set; }
}

