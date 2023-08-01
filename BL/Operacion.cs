using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Operacion
    {
        public static ML.Result Add(ML.Operacion operacion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.DesarrolloDataEntities context = new DL.DesarrolloDataEntities()) 
                {
                    var query = context.OperacionAdd(operacion.Numero, operacion.Resultado, operacion.Usuario.IdUsuario);

                    if (query > 0) 
                    {
                        result.Correct = true;
                        result.Mensaje = "Operación registrada en el historial";
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al hacer la operación \n" + result.Ex;
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
                        result.Mensaje = "Operación eliminada del historial";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al eliminar el registro de la operación \n" + result.Ex;
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
                    var query = context.OperacionGetById(id).FirstOrDefault();

                    if (query != null) 
                    {
                        ML.Operacion operacion = new ML.Operacion
                            (query.IdOperacion, query.Numero, 
                            query.Resultado.Value, query.IdUsuario, 
                            query.UserName, query.FechaConsulta.Value
                            );

                        result.Object = operacion;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al buscar la operación \n" + result.Ex;
            }
            return result;
        }

        public static ML.Result GetByUser(int id)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.DesarrolloDataEntities context = new DL.DesarrolloDataEntities()) 
                {
                    var query = context.OperacionGetByUser(id).ToList();

                    if (query != null) 
                    {
                        result.Objects = new List<object>();

                        foreach (var operaciones in query) 
                        {
                            ML.Operacion operacion = new ML.Operacion
                                ( operaciones.IdOperacion, operaciones.Numero,
                                    operaciones.Resultado.Value, operaciones.IdUsuario, 
                                    operaciones.UserName,operaciones.FechaConsulta.Value
                                );

                            result.Objects.Add(operacion);
                        }

                        if(result.Objects != null) result.Correct= true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al buscar operaciones \n" + result.Ex;
            }
            return result;
        }

        public static ML.Result Update(int id) 
        {
            ML.Result result = new ML.Result();

            try 
            {
                using (DL.DesarrolloDataEntities context = new DL.DesarrolloDataEntities()) 
                {
                    var query = context.OperacionFechaUpdate(id);

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Mensaje = "Consulta actualizada";
                    }
                }
            } catch (Exception ex) 
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al actualizar \n" + result.Ex;
            }

            return result;
        }

        public static ML.Result DeleteAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.DesarrolloDataEntities context = new DL.DesarrolloDataEntities())
                {
                    var query = context.OperacionDeleteAll();

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Mensaje = "Historial Eliminado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Mensaje = "Error al eliminar el historial \n" + result.Ex;
            }

            return result;
        }
    }
}
