using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class OperacionController : Controller
    {
        [HttpGet]
        public ActionResult Calculadora(ML.Usuario usuario)
        {
            if (usuario.IdUsuario == null || usuario.IdUsuario == 0)
                return View("Modal");

            else 
            {
                ML.Result result = BL.Operacion.GetByUser(usuario.IdUsuario);

                if (result.Correct) 
                {
                    ML.Operacion operacion = new ML.Operacion();

                    operacion.Operaciones = result.Objects;
                    return View(operacion);
                }

                ViewBag.Mensaje = "Error al encontrar usuario"; 
               return View("Modal");
            }
        }

        [HttpPost]

        public ActionResult Calculadora(int idUsuario, string numero) 
        {
            //int id = int.Parse(idUsuario);

            ML.Result result = BL.Operacion.GetByUser(idUsuario);
            if (result.Correct)
            {
                foreach (ML.Operacion operaciones in result.Objects)
                {
                    if (operaciones.Numero == numero)
                    {
                        ML.Result correct = BL.Operacion.Update(operaciones.IdOperacion);

                        if (correct.Correct)
                        {
                            ML.Result resultOperacionesNuevas = BL.Operacion.GetByUser(operaciones.Usuario.IdUsuario);
                            if (resultOperacionesNuevas.Correct)
                            {
                                int resultado = BL.SuperDigito.Calcular(numero);
                                ML.Operacion operacionesNuevas = new ML.Operacion();
                                operacionesNuevas.Operaciones = resultOperacionesNuevas.Objects;

                                ViewBag.Resultado = resultado.ToString();
                                return View(operacionesNuevas);
                            }
                            break;
                        }
                        else
                        {
                            ML.Result resultUsuario = BL.Usuario.GetById(idUsuario);
                            ML.Usuario usuarioEncontrado = new ML.Usuario();
                            usuarioEncontrado = (ML.Usuario)resultUsuario.Object;
                            ViewBag.Mensaje = "Ocurrio un error";
                            return View("Modal", usuarioEncontrado);
                        }
                    }
                    else
                    {
                        int resultado = BL.SuperDigito.Calcular(numero);
                        ML.Operacion operacionNueva = new ML.Operacion();

                        operacionNueva.Numero = numero;
                        operacionNueva.Resultado = resultado;
                        operacionNueva.Usuario = new ML.Usuario();
                        operacionNueva.Usuario.IdUsuario = idUsuario;

                        ML.Result resultNuevo = BL.Operacion.Add(operacionNueva);

                        if (resultNuevo.Correct)
                        {
                            ML.Result resultOperacionesNuevas = BL.Operacion.GetByUser(operacionNueva.Usuario.IdUsuario);
                            if (resultOperacionesNuevas.Correct)
                            {
                                operacionNueva.Operaciones = resultOperacionesNuevas.Objects;
                                ViewBag.Resultado = resultado.ToString();
                                return View(operacionNueva);
                            }
                            else
                            {
                                ML.Result resultUsuario = BL.Usuario.GetById(idUsuario);
                                ML.Usuario usuarioEncontrado = new ML.Usuario();
                                usuarioEncontrado = (ML.Usuario)resultUsuario.Object;
                                ViewBag.Mensaje = "Ocurrio un error";
                                return View("Modal", usuarioEncontrado);
                            }
                        }
                        else
                        {
                            ML.Result resultUsuario = BL.Usuario.GetById(idUsuario);
                            ML.Usuario usuarioEncontrado = new ML.Usuario();
                            usuarioEncontrado = (ML.Usuario)resultUsuario.Object;
                            ViewBag.Mensaje = "Ocurrio un error";
                            return View("Modal", usuarioEncontrado);
                        }
                    }
                }

                return  View("Modal");
            }
            else 
            {
                ML.Result resultUsuario = BL.Usuario.GetById(idUsuario);
                ML.Usuario usuarioEncontrado = new ML.Usuario();
                usuarioEncontrado = (ML.Usuario)resultUsuario.Object;
                ViewBag.Mensaje = "Ocurrio un error";
                return View("Modal", usuarioEncontrado);
            }
            
        }

        [HttpPost]

        public ActionResult Delete(int idOperacion) 
        { 
            ML.Result result = BL.Operacion.Delete(idOperacion);

            if (result.Correct)
            {
                ML.Result resultCorrecto = BL.Operacion.GetById(idOperacion);
                ML.Operacion operacion = new ML.Operacion();
                operacion = (ML.Operacion)resultCorrecto.Object;
                ML.Result usuario = BL.Usuario.GetById(operacion.Usuario.IdUsuario);
                ML.Usuario usuarioEncontrado = (ML.Usuario)usuario.Object;
                ViewBag.Mensaje = "Operación eliminada del historial";
                return View("Modal", usuarioEncontrado);
            }
            else 
            {
                ML.Result resultCorrecto = BL.Operacion.GetById(idOperacion);
                ML.Operacion operacion = new ML.Operacion();
                operacion = (ML.Operacion)resultCorrecto.Object;
                ML.Result usuario = BL.Usuario.GetById(operacion.Usuario.IdUsuario);
                ML.Usuario usuarioEncontrado = (ML.Usuario)usuario.Object;
                ViewBag.Mensaje = result.Mensaje;
                return View("Modal", usuarioEncontrado);
            }
            
        }

        [HttpPost]

        public ActionResult DeleteAll(int idOperacion)
        {
            ML.Result result = BL.Operacion.DeleteAll();

            if (result.Correct)
            {
                ML.Result resultCorrecto = BL.Operacion.GetById(idOperacion);
                ML.Operacion operacion = new ML.Operacion();
                operacion = (ML.Operacion)resultCorrecto.Object;
                ML.Result usuario = BL.Usuario.GetById(operacion.Usuario.IdUsuario);
                ML.Usuario usuarioEncontrado = (ML.Usuario)usuario.Object;
                ViewBag.Mensaje = "Historial eliminado";
                return View("Modal", usuarioEncontrado);
            }
            else
            {
                ML.Result resultCorrecto = BL.Operacion.GetById(idOperacion);
                ML.Operacion operacion = new ML.Operacion();
                operacion = (ML.Operacion)resultCorrecto.Object;
                ML.Result usuario = BL.Usuario.GetById(operacion.Usuario.IdUsuario);
                ML.Usuario usuarioEncontrado = (ML.Usuario)usuario.Object;
                ViewBag.Mensaje = result.Mensaje;
                return View("Modal", usuarioEncontrado);
            }

        }
    }
}