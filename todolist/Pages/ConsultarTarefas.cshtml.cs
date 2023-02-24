using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todolist.Models;

namespace todolist.Pages
{
	public class ConsultarTarefasModel : PageModel
    {
        public IEnumerable<Tarefa> tarefas { get; set; }

        public void OnGet()
        {
            TodoContext context = new TodoContext();
            tarefas = context.consultaTarefas();

        }
    }
}
