using Microsoft.AspNetCore.Mvc;
using EscolaEcommerce.Models;
using EscolaEcommerce.Repositorio;

namespace EscolaEcommerce.Controllers
{
    public class CursoController : Controller
    {
        private readonly CursoRepositorio _cursoRepositorio;
        public CursoController(CursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public IActionResult Principal()
        {
            return View(_cursoRepositorio.TodosCursos());
        }

        public IActionResult CadastrarCurso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarCurso(Curso curso)
        {
            _cursoRepositorio.CadastrarCurso(curso);
            return RedirectToAction(nameof(Principal));
        }

        public IActionResult EditarCurso(int id)
        {
            var curso = _cursoRepositorio.ObterCurso(id);

            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarCurso (int id, [Bind("Id, Nome, Descricao")] Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_cursoRepositorio.Atualizar(curso))
                    {
                        return RedirectToAction(nameof(Principal));
                    }
                }

                catch (Exception)
                {
                    ModelState.AddModelError("", "Erro ao editar.");
                    return View(curso);
                }
            }

            return View(curso);
        }

        public IActionResult ExcluirCurso(int id)
        {
            _cursoRepositorio.Excluir(id);
            return RedirectToAction(nameof(Principal));
        }
    }
}
