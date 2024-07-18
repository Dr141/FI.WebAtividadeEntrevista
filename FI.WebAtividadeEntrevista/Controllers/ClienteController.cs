using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using Caelum.Stella.CSharp.Validation;
using System.Text.RegularExpressions;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            try
            {
                BoCliente bo = new BoCliente();
                List<string> resultado = new List<string>();

                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }

                if (new CPFValidator().IsValid(model.CPF))
                {
                    model.Id = bo.Incluir(new Cliente()
                    {
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone,
                        CPF = model.CPF,
                    });
                    resultado.Add("Cadastro do cliente efetuado com sucesso.");

                    if (model.beneficiarios != null && model.beneficiarios.Any())
                    {
                        BoBeneficiario be = new BoBeneficiario();
                        foreach (var benefi in model.beneficiarios)
                        {
                            try
                            {
                                if (new CPFValidator().IsValid(benefi.CPF))
                                    be.Incluir(new Beneficiario { CPF = benefi.CPF, Nome = benefi.Nome, IdCliente = model.Id });
                                else
                                    resultado.Add($"O número do CPF {benefi.CPF} do beneficiário é inválido.");
                            }catch(Exception ex)
                            {
                                resultado.Add(GetMessageException(ex.Message, benefi.CPF, 1));
                            }
                        }
                    }

                    return Json(string.Join(Environment.NewLine, resultado));
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json("Por favor, fornecer um CPF válido.");
                }
            }
            catch (Exception ex) 
            {
                Response.StatusCode = 400;
                return Json(GetMessageException(ex.Message, model.CPF));
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            try
            {
                BoCliente bo = new BoCliente();
                List<string> resultado = new List<string>();

                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }

                if (new CPFValidator().IsValid(model.CPF))
                {
                    bo.Alterar(new Cliente()
                    {
                        Id = model.Id,
                        CEP = model.CEP,
                        Cidade = model.Cidade,
                        Email = model.Email,
                        Estado = model.Estado,
                        Logradouro = model.Logradouro,
                        Nacionalidade = model.Nacionalidade,
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Telefone = model.Telefone,
                        CPF = model.CPF,
                    });
                    resultado.Add("Cadastro do cliente alterado com sucesso.");
                    BoBeneficiario be = new BoBeneficiario();
                    if (model.beneficiarios != null && model.beneficiarios.Any())
                    {
                        foreach (var benefi in model.beneficiarios)
                        {
                            try
                            {
                                if (new CPFValidator().IsValid(benefi.CPF))
                                    be.Alterar(new Beneficiario { CPF = benefi.CPF, Nome = benefi.Nome, Id = benefi.Id, IdCliente = model.Id });
                                else
                                    resultado.Add($"O número do CPF {benefi.CPF} do beneficiário é inválido.");
                            }
                            catch (Exception ex)
                            {
                                resultado.Add(GetMessageException(ex.Message, benefi.CPF, 1));
                            }
                        }

                        var listaBeneficiariosAtual = be.Pesquisa(model.Id).Select(px => new BeneficiarioModel
                        {
                            CPF = px.CPF,
                            Nome = px.Nome,
                            Id = px.Id,
                            IdCliente = px.IdCliente
                        }).ToList();
                        foreach (var excluir in listaBeneficiariosAtual)
                        {
                            try
                            {
                                if (model.beneficiarios.Any(px => px.CPF.Equals(excluir.CPF))) continue;
                                be.Excluir(excluir.Id);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        var listaBeneficiariosAtual = be.Pesquisa(model.Id).Select(px => new BeneficiarioModel
                        {
                            CPF = px.CPF,
                            Nome = px.Nome,
                            Id = px.Id,
                            IdCliente = px.IdCliente
                        }).ToList();
                        foreach (var excluir in listaBeneficiariosAtual)
                        {
                            try
                            {
                                be.Excluir(excluir.Id);
                            }
                            catch { }
                        }
                    }

                    return Json(string.Join(Environment.NewLine, resultado));
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json("Por favor, fornecer um CPF válido.");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(GetMessageException(ex.Message, model.CPF));
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF,
                    beneficiarios = cliente.Beneficiarios != null && cliente.Beneficiarios.Any() ? 
                                    cliente.Beneficiarios.Select(px => new BeneficiarioModel 
                                                                    { 
                                                                        CPF = px.CPF,
                                                                        Nome = px.Nome,
                                                                        Id = px.Id,
                                                                        IdCliente = px.IdCliente
                                                                    }).ToArray() : null,
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private string GetMessageException(string message, string cpf, int index = 0)
        {            
            if (!string.IsNullOrEmpty(cpf) && message.Contains(cpf))
                switch (index)
                {
                    case 0:
                    {
                        return $"O CPF({cpf}) do cliente já encontra-se cadastrado. Por favor, acessar o cadastro do mesmo.";
                    }
                    case 1: 
                    {
                        return $"O CPF({cpf}) do beneficiario já encontra-se cadastrado nesse cliente. E não sera inserido novamente.";
                    }
                }

            return message;
        }
    }
}