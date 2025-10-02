using ControleChamadosMVC.DAO;
using ControleChamadosMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleChamadosMVC.Controllers
{
    public class ChamadoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                ChamadoDAO dao = new ChamadoDAO();
                var lista = dao.Listagem().OrderBy(c => c.Id).OrderByDescending(c => c.Situacao == 1).ToList();
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
                ViewBag.Operacao = "C";
                ChamadoDAO dao = new ChamadoDAO();
                UsuarioDAO usuarioDao = new UsuarioDAO();

                ChamadoViewModel model = new ChamadoViewModel
                {
                    Id = dao.ProximoId(),
                    DataAbertura = DateTime.Now,
                    Situacao = 1
                };

                ViewBag.Usuarios = usuarioDao.Listagem()
                                    .Select(u => new SelectListItem
                                    {
                                        Value = u.Id.ToString(),
                                        Text = u.Nome
                                    }).ToList();

                return View("FormCreate", model);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Save(ChamadoViewModel chamado, string Operacao)
        {
            try
            {
                ValidaDados(chamado, Operacao);

                if (ModelState.IsValid)
                {                    
                    ChamadoDAO dao = new ChamadoDAO();
                    if (Operacao == "I")
                        dao.Inserir(chamado);
                    else
                        dao.Alterar(chamado);

                    return RedirectToAction("index");
                }
                else
                {                    
                    if (Operacao == "AT")
                    {
                        UsuarioDAO usuarioDao = new UsuarioDAO();
                        ViewBag.Operacao = Operacao;

                        ViewBag.Usuarios = usuarioDao.Listagem()
                                    .Select(u => new SelectListItem
                                    {
                                        Value = u.Id.ToString(),
                                        Text = u.Nome
                                    }).ToList();

                        return View("FormAtender", chamado);
                    }
                    else if (Operacao == "C")
                    {
                        UsuarioDAO usuarioDao = new UsuarioDAO();
                        ViewBag.Operacao = Operacao;

                        ViewBag.Usuarios = usuarioDao.Listagem()
                                    .Select(u => new SelectListItem
                                    {
                                        Value = u.Id.ToString(),
                                        Text = u.Nome
                                    }).ToList();

                        ViewBag.Operacao = Operacao;
                        return View("FormCreate", chamado);
                    }
                    else
                    {
                        UsuarioDAO usuarioDao = new UsuarioDAO();
                        ViewBag.Operacao = Operacao;

                        ViewBag.Usuarios = usuarioDao.Listagem()
                                    .Select(u => new SelectListItem
                                    {
                                        Value = u.Id.ToString(),
                                        Text = u.Nome
                                    }).ToList();

                        ViewBag.Operacao = Operacao;
                        return View("Form", chamado);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        private void ValidaDados(ChamadoViewModel chamado, string operacao)
        {
            ModelState.Clear();

            ChamadoDAO dao = new ChamadoDAO();
            if (chamado.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            else
            {
                if (operacao == "I" && dao.Consulta(chamado.Id) != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");
                if (operacao == "A" && dao.Consulta(chamado.Id) == null)
                    ModelState.AddModelError("Id", "Chamado não existe.");
            }

            if (string.IsNullOrEmpty(chamado.DescricaoProblema))
                ModelState.AddModelError("DescricaoProblema", "Preencha a descrição do problema.");

            if (chamado.Situacao != 1 && chamado.Situacao != 2)
                ModelState.AddModelError("Situacao", "Situação inválida.");

            if (operacao == "C" && chamado.DataAbertura != DateTime.Today)
                ModelState.AddModelError("DataAbertura", "Data inválida, escolha o dia atual.");

            if (operacao == "AT" && chamado.DataAtendimento is null)
                ModelState.AddModelError("DataAtendimento", "Preencha a data de atendimento.");

            if (chamado.DataAtendimento < chamado.DataAbertura)
                ModelState.AddModelError("DataAtendimento", "Data de Atendimento menor que a Data de Abertura.");

            if (operacao == "AT" && chamado.DescricaoAtendimento is null)
            {
               ModelState.AddModelError("DescricaoAtendimento", "Adicione uma descrição para o atendimento.");               
            }

            if (operacao == "AT" && chamado.Situacao == 1)
            {
                ModelState.AddModelError("Situacao", "A situação deve estar como atendido.");
            }

            if (operacao == "AT" && chamado.UsuarioId is null)
            {
                ModelState.AddModelError("UsuarioId", "Selecione um usuário.");
            }

        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                ChamadoDAO dao = new ChamadoDAO();
                UsuarioDAO usuarioDao = new UsuarioDAO();

                var model = dao.Consulta(id);

                if (model == null)
                    return RedirectToAction("index");

                ViewBag.Usuarios = usuarioDao.Listagem()
                                .Select(u => new SelectListItem
                                {
                                    Value = u.Id.ToString(),
                                    Text = u.Nome
                                }).ToList();

                
                return View("Form", model);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Atender(int id)
        {
            try
            {
                ViewBag.Operacao = "AT";
                ChamadoDAO dao = new ChamadoDAO();
                UsuarioDAO usuarioDao = new UsuarioDAO();

                var model = dao.Consulta(id);

                if (model == null)
                    return RedirectToAction("index");

                ViewBag.Usuarios = usuarioDao.Listagem()
                                .Select(u => new SelectListItem
                                {
                                    Value = u.Id.ToString(),
                                    Text = u.Nome
                                }).ToList();


                return View("FormAtender", model);
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
                ChamadoDAO dao = new ChamadoDAO();
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
