using Microsoft.AspNetCore.Mvc;
using EscolaEcommerce.Models;
using EscolaEcommerce.Repositorio;

namespace EscolaEcommerce.Controllers
{
    public class AlunoController : Controller
    {
        private readonly AlunoRepositorio _alunoRepositorio;
        public AlunoController(AlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public IActionResult Principal()
        {
            return View(_alunoRepositorio.TodosAlunos());
        }

        public IActionResult CadastrarAluno()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarAluno(Aluno aluno)
        {
            int linhasAfetadas = _alunoRepositorio.CadastrarAluno(aluno);
            
            if ( linhasAfetadas > 0)
            {
                TempData["Mensagem"] = "Aluno cadastrado com sucesso.";
                TempData["Classe"] = "alert alert-success";
                return RedirectToAction(nameof(Principal));
            }

            else
            {
                TempData["Mensagem"] = "O aluno já existe no sistema";
                TempData["Classe"] = "alert alert-danger";
                return View();
            }

            
        }

        public IActionResult EditarAluno(int id)
        {
            var aluno = _alunoRepositorio.ObterAluno(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarAluno(int id, [Bind("Rm, Nome, Email, Idade")] Aluno aluno)
        {
            if (id != aluno.Rm)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_alunoRepositorio.Atualizar(aluno))
                    {
                        return RedirectToAction(nameof(Principal));
                    }
                }

                catch (Exception)
                {
                    ModelState.AddModelError("", "Erro ao editar.");
                    return View(aluno);
                }
            }

            return View(aluno);
        }

        public IActionResult ExcluirAluno(int id)
        {
            _alunoRepositorio.Excluir(id);
            return RedirectToAction(nameof(Principal));
        }
    }
}
