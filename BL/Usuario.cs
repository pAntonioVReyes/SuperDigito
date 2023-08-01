using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario) 
        {
            ML.Result result = new ML.Result();

            try 
            {
                using (DL.DesarrolloDataEntities context = new DL.DesarrolloDataEntities())
                {

                    var query = context.UsuarioAdd(usuario.UserName, usuario.Contraseña);

                    if (query > 0) 
                    {
                        result.Correct = true;
                        result.Mensaje = "Registro exitoso \n";
                    }
                }

            }catch(Exception ex) 
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al registrarse \n" + result.Ex;
            }

            return result;
        }
        
        public static ML.Result Delete(int id) 
        {
            ML.Result result = new ML.Result();

            try 
            {
                using (DL.DesarrolloDataEntities context = new DL.DesarrolloDataEntities()) 
                { 
                    var query = context.OperacionDelete(id);

                    if (query > 0) 
                    {
                        result.Correct = true;
                        result.Mensaje = "Usuario eliminado";
                    }

                }

            } catch (Exception ex) 
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al eliminar usuario \n" + result.Ex;
            }

            return result;
        }

        public static ML.Result GetById(int id) 
        {
            ML.Result result = new ML.Result();

            try 
            {
                using (DL.DesarrolloDataEntities context = new DL.DesarrolloDataEntities()) 
                {
                    var query = context.UsuarioGetById(id).FirstOrDefault();

                    if (query != null) 
                    {
                        ML.Usuario usuario = new ML.Usuario(
                            query.IdUsuario, query.UserName, query.Contraseña);

                        result.Object = usuario;

                        if(result.Object != null) result.Correct = true;
                    }
                }

            }catch(Exception ex) 
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al buscar al usuario \n" + result.Ex;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.DesarrolloDataEntities context = new DL.DesarrolloDataEntities()) 
                {
                    var query = context.UsuarioGetAll().ToList();

                    if (query != null) 
                    {
                        result.Objects = new List<object>();

                        foreach (var usuarios in query)
                        {
                            ML.Usuario usuario = new ML.Usuario(
                                usuarios.IdUsuario, usuarios.UserName, usuarios.Contraseña
                                );

                            result.Objects.Add( usuario );
                        }

                        if (result.Objects != null) result.Correct = true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al buscar usuarios \n" + result.Ex;
            }
            return result;
        }

       /* Futuro uso public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al actualizar \n" + result.Ex;
            }

            return result;
        } */
    }
}
