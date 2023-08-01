using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ML.Usuario usuario) 
        {
            bool usuarioValidacion = false, nombreEncontrado = false;

            if (usuario != null || usuario.UserName != "") 
            {
                ML.Result resultUsuario = BL.Usuario.GetAll();

                foreach (var usuarioList in resultUsuario.Objects) 
                {
                    ML.Usuario usuarios = new ML.Usuario();
                    usuarios = (ML.Usuario)usuarioList;

                    if (usuario.UserName == usuarios.UserName) 
                    {
                        nombreEncontrado = true;
                        if (usuario.Contraseña == usuarios.Contraseña) 
                        { 
                            usuarioValidacion = true;
                            usuario = usuarios;
                        }
                        else
                        {
                            ViewBag.Mensaje = "Contraseña incorrecta";
                            return View();
                        } 
                    }
                    if (nombreEncontrado == true) break;
                }
                if (usuarioValidacion == false) 
                {
                    ViewBag.Mensaje = "Nombre de usuario incorrecto";
                    return View();
                } 

                return RedirectToAction("Calculadora", "Operacion", usuario);
            }
            else 
            {
                ViewBag.Mensaje = "Llene todos los campos";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Registro() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(ML.Usuario usuario, string password) 
        {
            if (usuario.Contraseña == password)
            {
                ML.Result result = BL.Usuario.Add(usuario);

                if (!result.Correct)
                {
                    ViewBag.Mensaje = result.Mensaje;
                    return RedirectToAction("Login");

                }
                else
                {
                    ViewBag.Mensaje = result.Mensaje;
                    return View();
                }
            }
            else 
            {
                ViewBag.Mensaje = "Las contraseñas no son iguales";
                return View();
            }
            
        }
    }
}