using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace sistema_regulacao.Controllers
{
    public class HomeController : Controller
    {
        public static List<dynamic> pendencias = new List<dynamic>();
        public static List<dynamic> erros = new List<dynamic>();
        public static List<dynamic> fila = new List<dynamic>();
        public static List<dynamic> avisar = new List<dynamic>();
        public static List<dynamic> avisados = new List<dynamic>();

        public static int id = 0;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(string nome, string cpf, string telefone,
            string mae, string cns, string peso, string altura,
            string unidade, string especialidade, string medico, string prioridade)
        {
            id++;

            bool prioridadeBool = prioridade == "on";

            var paciente = new
            {
                Id = id,
                Nome = nome,
                Cpf = cpf,
                Telefone = telefone,
                Mae = mae,
                Cns = cns,
                Peso = peso,
                Altura = altura,
                Unidade = unidade,
                Especialidade = especialidade,
                Medico = medico,
                Prioridade = prioridadeBool
            };

            pendencias.Add(paciente);

            // 🔥 prioridade vai pro topo
            pendencias = pendencias
                .OrderByDescending(p => p.Prioridade)
                .ToList();

            return RedirectToAction("Pendencias");
        }

        public IActionResult Pendencias()
        {
            return View(pendencias);
        }

        public IActionResult Aprovar(int id)
        {
            var p = pendencias.FirstOrDefault(x => x.Id == id);

            if (p != null)
            {
                fila.Add(p);
                pendencias.Remove(p);
            }

            fila = fila.OrderByDescending(x => x.Prioridade).ToList();

            return RedirectToAction("Pendencias");
        }

        public IActionResult Erro(int id)
        {
            var p = pendencias.FirstOrDefault(x => x.Id == id);

            if (p != null)
            {
                erros.Add(p);
                pendencias.Remove(p);
            }

            return RedirectToAction("Pendencias");
        }

        public IActionResult Fila()
        {
            return View(fila);
        }

        public IActionResult Agendar(int id)
        {
            var p = fila.FirstOrDefault(x => x.Id == id);

            if (p != null)
            {
                avisar.Add(p);
                fila.Remove(p);
            }

            return RedirectToAction("Fila");
        }

        public IActionResult Agendados()
        {
            ViewBag.Avisar = avisar;
            ViewBag.Avisados = avisados;
            return View();
        }

        public IActionResult Avisado(int id)
        {
            var p = avisar.FirstOrDefault(x => x.Id == id);

            if (p != null)
            {
                avisados.Add(p);
                avisar.Remove(p);
            }

            return RedirectToAction("Agendados");
        }

        public IActionResult Erros()
        {
            return View(erros);
        }
    }
}