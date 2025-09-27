using ControleChamadosMVC.DAO;
using ControleChamadosMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleChamadosMVC.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                var lista = dao.Listagem();
                return View(lista);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";
                UsuarioDAO dao = new UsuarioDAO();
                UsuarioViewModel model = new UsuarioViewModel
                {
                    Id = dao.ProximoId()
                };
                return View("Form", model);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Save(UsuarioViewModel usuario, string Operacao)
        {
            try
            {
                ValidaDados(usuario, Operacao);

                if (ModelState.IsValid)
                {
                    UsuarioDAO dao = new UsuarioDAO();
                    if (Operacao == "I")
                        dao.Inserir(usuario);
                    else
                        dao.Alterar(usuario);

                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.Operacao = Operacao;
                    return View("Form", usuario);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        private void ValidaDados(UsuarioViewModel usuario, string operacao)
        {
            ModelState.Clear();

            UsuarioDAO dao = new UsuarioDAO();
            if (usuario.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            else
            {
                if (operacao == "I" && dao.Consulta(usuario.Id) != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");
                if (operacao == "A" && dao.Consulta(usuario.Id) == null)
                    ModelState.AddModelError("Id", "Usuário não existe.");
            }

            if (string.IsNullOrEmpty(usuario.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");
        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                UsuarioDAO dao = new UsuarioDAO();
                var model = dao.Consulta(id);

                if (model == null)
                    return RedirectToAction("index");
                else
                    return View("Form", model);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                dao.Excluir(id);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }
    }
}
