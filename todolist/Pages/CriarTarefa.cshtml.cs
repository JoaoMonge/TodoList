using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todolist.Models;

namespace todolist.Pages
{
	public class CriarTarefaModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            string descricao = Request.Form["descricao"];
            string dataInicio = Request.Form["dateInicio"];
            string dataFim = Request.Form["dateFim"];

            TodoContext context = new TodoContext();
            context.criarTarefa(descricao, dataInicio, dataFim);

        }
    }
}
