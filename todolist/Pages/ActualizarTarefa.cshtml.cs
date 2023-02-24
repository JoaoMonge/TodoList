using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todolist.Models;

namespace todolist.Pages
{
	public class ActualizarTarefaModel : PageModel
    {
        public IEnumerable<Tarefa> tarefas { get; set; }

        public void OnGet()
        {
            TodoContext context = new TodoContext();
            tarefas = context.consultaTarefas();
        }

        public void OnPost()
        {
            TodoContext context = new TodoContext();

            Tarefa Tarefa = new Tarefa()
            {
                Id = Int32.Parse(Request.Form["id"]),
                Descricao = Request.Form["descricao"],
                DataInicio = Request.Form["dataInicio"],
                DataFim = Request.Form["dataFim"]
            };

            string resposta = context.ActualizarTarefa(Tarefa);

            if(resposta == null)
            {
                ViewData["message"] = "Erro na actualização.";
            }
            else
            {
                ViewData["message"] = resposta;
            }

            tarefas = context.consultaTarefas();

        }
    }
}
