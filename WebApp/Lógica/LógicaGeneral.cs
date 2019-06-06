using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contratos;
using System.IO;
using Newtonsoft.Json;

namespace Lógica
{
    public class LógicaGeneral : IServicioWeb
    {
        public List<Usuario> Usuarios { get; set;}
        public List<Institucion> Instituciones { get; set; }
        public List<Directora> Directores { get; set; }
        public List<Docente> Docentes { get; set; }
        public List<Padre> Padres { get; set; }
        public List<Hijo> Alumnos { get; set; }
        public List<Nota> Notas { get; set; }
        public List<Sala> Salas { get; set; }
        public List<Claves> Claves { get; set; }

        public const string pathUsuarios = @"C:\Archivos\Usuarios.txt";
        public const string pathInstituciones = @"C:\Archivos\Instituciones.txt";
        public const string pathDirectores = @"C:\Archivos\Directores.txt";
        public const string pathDocentes = @"C:\Archivos\Docentes.txt";
        public const string pathPadres = @"C:\Archivos\Padres.txt";
        public const string pathAlumnos = @"C:\Archivos\Alumnos.txt";
        public const string pathNotas = @"C:\Archivos\Notas.txt";
        public const string pathSalas = @"C:\Archivos\Salas.txt";
        public const string pathClaves = @"C:\Archivos\Claves.txt";

        public LógicaGeneral()
        {
            Usuarios = new List<Usuario>();
            Instituciones = new List<Institucion>();
            Directores = new List<Directora>();
            Docentes = new List<Docente>();
            Padres = new List<Padre>();
            Alumnos = new List<Hijo>();
            Notas = new List<Nota>();
            Salas = new List<Sala>();
            Claves = new List<Claves>();
        }

        //CREACIÓN DE ARCHIVOS  
        public void CrearArchivos()
        {
            if (!File.Exists(pathUsuarios))
                File.Create(pathUsuarios).Close();
            if (!File.Exists(pathInstituciones))
                File.Create(pathInstituciones).Close();
            if (!File.Exists(pathDirectores))
                File.Create(pathDirectores).Close();
            if (!File.Exists(pathDocentes))
                File.Create(pathDocentes).Close();
            if (!File.Exists(pathPadres))
                File.Create(pathPadres).Close();
            if (!File.Exists(pathAlumnos))
                File.Create(pathAlumnos).Close();
            if (!File.Exists(pathNotas))
                File.Create(pathNotas).Close();
            if (!File.Exists(pathSalas))
                File.Create(pathSalas).Close();
            if (!File.Exists(pathClaves))
                File.Create(pathClaves).Close();
        }

        //DESERIALIZACION DE ARCHIVOS
        public List<Usuario> LeerUsuarios()
        {
            using (StreamReader leer = new StreamReader(pathUsuarios))
            {
                string contenido = leer.ReadToEnd();
                Usuarios = JsonConvert.DeserializeObject<List<Usuario>>(contenido);
            }
            return Usuarios;           
        }

        public List<Directora> LeerDirectores()
        {
            using (StreamReader leer = new StreamReader(pathDirectores))
            {
                    string contenido = leer.ReadToEnd();
                    Directores = JsonConvert.DeserializeObject<List<Directora>>(contenido);
            }
            return Directores;           
        }

        public List<Docente> LeerDocentes()
        {
            using (StreamReader leer = new StreamReader(pathDocentes))
            {
                string contenido = leer.ReadToEnd();
                Docentes = JsonConvert.DeserializeObject<List<Docente>>(contenido);
            }
            return Docentes;
        }

        public List<Padre> LeerPadres()
        {
            using (StreamReader leer = new StreamReader(pathPadres))
            {
                string contenido = leer.ReadToEnd();
                Padres = JsonConvert.DeserializeObject<List<Padre>>(contenido);
            }
            return Padres;
        }

        public List<Hijo> LeerAlumnos()
        {
            using (StreamReader leer = new StreamReader(pathAlumnos))
            {
                string contenido = leer.ReadToEnd();
                Alumnos = JsonConvert.DeserializeObject<List<Hijo>>(contenido);
            }
            return Alumnos;
        }

        public List<Institucion> LeerInstituciones()
        {
            using (StreamReader leer = new StreamReader(pathInstituciones))
            {
                string contenido = leer.ReadToEnd();
                Instituciones = JsonConvert.DeserializeObject<List<Institucion>>(contenido);
            }
            return Instituciones;
        }

        public List<Nota> LeerNotas()
        {
            using (StreamReader leer = new StreamReader(pathNotas))
            {
                string contenido = leer.ReadToEnd();
                Notas = JsonConvert.DeserializeObject<List<Nota>>(contenido);
            }
            return Notas;
        }

        public List<Sala> LeerSalas()
        {
            using (StreamReader leer = new StreamReader(pathSalas))
            {
                string contenido = leer.ReadToEnd();
                Salas = JsonConvert.DeserializeObject<List<Sala>>(contenido);
            }
            return Salas;
        }

        public List<Claves> LeerClaves()
        {
            using (StreamReader leer = new StreamReader(pathClaves))
            {
                string contenido = leer.ReadToEnd();
                Claves = JsonConvert.DeserializeObject<List<Claves>>(contenido);
            }
            return Claves;
        }

        public void GuardarEnArchivo(string path, object[] array)
        {
            using (StreamWriter Writer = new StreamWriter(path, false))
            {
                Writer.Write(JsonConvert.SerializeObject(array));
            }
        }

        public Resultado AltaDirectora(Directora directora, UsuarioLogueado usuarioLogueado)
        {
            Usuario usuario = null;         
            List<string> errores = new List<string>();
            if (LeerDirectores() == null)
            {
                directora.Id = 1;
            }
            else
                directora.Id = LeerDirectores().Count + 1;
            if (usuarioLogueado.RolSeleccionado == Roles.Directora) //tiene permiso --> ver si ya está logueado con OTRO ROL
            {
                if (LeerUsuarios() != null)
                    usuario = LeerUsuarios().Where(x => x.Email == directora.Email).FirstOrDefault();       
                if (usuario == null || LeerUsuarios() == null) //no está registrado, lo guardo en ambos arrays y guardo nueva clave
                {
                    Random rnd = new Random();
                    Claves nueva = new Claves() {Email=directora.Email, Roles=new Roless[] {Roless.Directora}, Clave=rnd.Next(100000,999999).ToString() };//generar clave
                    if (LeerClaves() == null)
                    {
                        Claves = new List<Claves>();
                        Claves.Add(nueva);
                    }
                    else
                    {
                        Claves.Add(nueva);
                    }
                    //guardar en archivo "usuarios"--> UNIFICAR EN UN MÉTODO   
                    if (LeerUsuarios() == null)
                    {
                        Usuarios = new List<Usuario>();
                        Usuarios.Add(directora);
                    }
                    else
                    {
                        Usuarios.Add(directora);
                    }
                    using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
                    {
                        Writer.Write(JsonConvert.SerializeObject(Usuarios));
                    }
                }
                else //ya está registrado, se agrega ROL en archivo "claves" 
                {
                    Claves clave = LeerClaves().Where(x => x.Email == directora.Email).FirstOrDefault();
                    clave.Roles[clave.Roles.Length] = Roless.Directora;
                }
                //GUARDAR clave nueva o actualizacion de roles
                using (StreamWriter Writer = new StreamWriter(pathClaves, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Claves));
                }
                //en ambos casos se agrega a "directores", si no está logueado o si tiene este nuevo rol
                if (LeerDirectores() == null)
                {
                    Directores = new List<Directora>();
                    Directores.Add(directora);            
                }
                else
                {
                    Directores.Add(directora);
                }
                using (StreamWriter Writer = new StreamWriter(pathDirectores, false)) 
                {
                    Writer.Write(JsonConvert.SerializeObject(Directores));
                }
            }
            else //no tiene permiso
            {
                errores.Add("No  está autorizado para dar de alta a Directora");
            }
            return new Resultado() {Errores= errores};
        }

        public Resultado AltaDocente(Docente docente, UsuarioLogueado usuarioLogueado)
        {
            List<string> errores = new List<string>();
            docente.Id = Docentes.Count() + 1;
            if (usuarioLogueado.RolSeleccionado == Roles.Docente) //tiene permiso --> ver si ya está logueado con OTRO ROL
            {

                Usuario usuario = Usuarios.ToList().Where(x => x.Email == docente.Email).FirstOrDefault();
                if (usuario == null) //no está registrado, lo guardo en ambos arrays y guardo nueva clave
                {
                    Random rnd = new Random();
                    Claves nueva = new Claves() { Email = docente.Email, Roles = new Roless[] { Roless.Docente }, Clave = rnd.Next(100000, 999999).ToString() };//generar clave
                    Claves.ToList().Add(nueva); //guardar clave 
                    //guardar en archivo "usuarios"--> UNIFICAR EN UN MÉTODO              
                    Usuarios.ToList().Add(docente);
                    Usuarios.ToArray();
                    using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
                    {
                        Writer.Write(JsonConvert.SerializeObject(Usuarios));
                    }
                }
                else //ya está registrado, se agrega ROL en archivo "claves" 
                {
                    Claves clave = Claves.ToList().Where(x => x.Email == docente.Email).FirstOrDefault();
                    clave.Roles[clave.Roles.Count()] = Roless.Docente;
                }
                //GUARDAR clave nueva o actualizacion de roles
                using (StreamWriter Writer = new StreamWriter(pathClaves, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Claves));
                }
                //en ambos casos se agrega a "docentes", si no está logueado o si tiene este nuevo rol
                Docentes.ToList().Add(docente);
                Docentes.ToArray();
                using (StreamWriter Writer = new StreamWriter(pathDocentes, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Docentes));
                }
            }
            else //no tiene permiso
            {
                errores.Add("No  está autorizado para dar de alta a Docente");
            }
            return new Resultado() { Errores = errores };
        }

        public Resultado AltaAlumno(Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();

        }

        public Resultado AltaNota(Nota nota, Sala[] salas, Hijo[] hijos, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado AltaPadreMadre(Padre padre, UsuarioLogueado usuarioLogueado)
        {
            List<string> errores = new List<string>();
            padre.Id = Directores.Count() + 1;
            if (usuarioLogueado.RolSeleccionado == Roles.Padre) //tiene permiso --> ver si ya está logueado con OTRO ROL
            {
                Usuario usuario = Usuarios.ToList().Where(x => x.Email == padre.Email).FirstOrDefault();
                if (usuario == null) //no está registrado, lo guardo en ambos arrays y guardo nueva clave
                {
                    Random rnd = new Random();
                    Claves nueva = new Claves() { Email = padre.Email, Roles = new Roless[] { Roless.Padre }, Clave = rnd.Next(100000, 999999).ToString() };//generar clave
                    Claves.ToList().Add(nueva); //guardar clave 
                    //guardar en archivo "usuarios"--> UNIFICAR EN UN MÉTODO              
                    Usuarios.ToList().Add(padre);
                    Usuarios.ToArray();
                    using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
                    {
                        Writer.Write(JsonConvert.SerializeObject(Usuarios));
                    }
                }
                else //ya está registrado, se agrega ROL en archivo "claves" 
                {
                    Claves clave = Claves.ToList().Where(x => x.Email == padre.Email).FirstOrDefault();
                    clave.Roles[clave.Roles.Count()] = Roless.Padre;
                }
                //GUARDAR clave nueva o actualizacion de roles
                using (StreamWriter Writer = new StreamWriter(pathClaves, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Claves));
                }
                //en ambos casos se agrega a "padres", si no está logueado o si tiene este nuevo rol
                Padres.ToList().Add(padre);
                Padres.ToArray();
                using (StreamWriter Writer = new StreamWriter(pathPadres, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Padres));
                }
            }
            else //no tiene permiso
            {
                errores.Add("No  está autorizado para dar de alta a Padre/Madre");
            }
            return new Resultado() { Errores = errores };
        }

        public Resultado AsignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado AsignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado DesasignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado DesasignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado EditarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado)
        {
            Directora nuevaDirectora = new Directora();
            nuevaDirectora = ObtenerDirectoraPorId(usuarioLogueado, id);
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario = LeerUsuarios().Where(x => x.Id == directora.Id).FirstOrDefault();

            nuevaDirectora.Id = id;
            nuevaDirectora.Institucion = directora.Institucion;
            nuevaDirectora.Nombre = directora.Nombre;
            nuevaDirectora.FechaIngreso = directora.FechaIngreso;
            nuevaDirectora.Email = directora.Email;
            nuevaDirectora.Cargo = directora.Cargo;
            nuevaDirectora.Apellido = directora.Apellido;
            
            nuevoUsuario.Id = directora.Id;
            nuevoUsuario.Nombre = directora.Nombre;
            nuevoUsuario.Apellido = directora.Apellido;
            nuevoUsuario.Email = directora.Email;
            
            using (StreamWriter Writer = new StreamWriter(pathDirectores, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Directores));
            }
            using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Usuarios));
            }

            return new Resultado();

        }

        public Resultado EditarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();

        }

        public Resultado EditarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();

        }

        public Resultado EditarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();

        }

        public Resultado EliminarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado)
        {            
            LeerDirectores().RemoveAll(x=>x.Id == directora.Id);
            LeerUsuarios().RemoveAll(x => x.Id == directora.Id);
            using (StreamWriter Writer = new StreamWriter(pathDirectores, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Directores));
            }
            using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Usuarios));
            }
            return new Resultado();
        }

        public Resultado EliminarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();

        }

        public Resultado EliminarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();

        }

        public Resultado EliminarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();

        }

        public Resultado MarcarNotaComoLeida(Nota nota, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Hijo ObtenerAlumnoPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            throw new NotImplementedException();
        }

        public Grilla<Hijo> ObtenerAlumnos(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            if (LeerAlumnos() == null)
            {
                return new Grilla<Hijo>()
                {
                    Lista = new List<Hijo>().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                                    .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = 0
                };
            }
            else
                return new Grilla<Hijo>()
                {
                    Lista = LeerAlumnos().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                                        .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = LeerDirectores().Count
                };
        }

        public Nota[] ObtenerCuadernoComunicaciones(int idPersona, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Directora ObtenerDirectoraPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return LeerDirectores().Where(x => x.Id == id).FirstOrDefault();
            
        }

        public Grilla<Directora> ObtenerDirectoras(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            if (LeerDirectores() == null)
            {
                return new Grilla<Directora>()
                {
                    Lista = new List<Directora>().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                                    .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = 0
                };
            }
            else          
                return new Grilla<Directora>()
                {
                    Lista = LeerDirectores().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                                        .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = LeerDirectores().Count
                };
        }

        public Docente ObtenerDocentePorId(UsuarioLogueado usuarioLogueado, int id)
        {
            throw new NotImplementedException();
        }

        public Grilla<Docente> ObtenerDocentes(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            if (LeerDocentes() == null)
            {
                return new Grilla<Docente>()
                {
                    Lista = new List<Docente>().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                                    .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = 0
                };
            }
            else
                return new Grilla<Docente>()
                {
                    Lista = LeerDocentes().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                                        .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = LeerDirectores().Count
                };
        }

        public Institucion[] ObtenerInstituciones()
        {
            throw new NotImplementedException();
        }

        public string ObtenerNombreGrupo()
        {
            return $"Albrecht - Heit - Llull";
        }

        public Padre ObtenerPadrePorId(UsuarioLogueado usuarioLogueado, int id)
        {
            throw new NotImplementedException();
        }

        public Grilla<Padre> ObtenerPadres(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            if (LeerPadres() == null)
            {
                return new Grilla<Padre>()
                {
                    Lista = new List<Padre>().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                                    .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = 0
                };
            }
            else
                return new Grilla<Padre>()
                {
                    Lista = LeerPadres().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                                        .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = LeerDirectores().Count
                };
        }

        public Hijo[] ObtenerPersonas(UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Sala[] ObtenerSalasPorInstitucion(UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public UsuarioLogueado ObtenerUsuario(string email, string clave)
        {
            if (email == "" || clave == "")
                  return null;

              if (email == "directora@ucse.com" && clave == "123456")
                  return new UsuarioLogueado() { Email = email, Nombre = "Usuario", Apellido = "Directora", Roles = new Roles[] { Roles.Directora }, RolSeleccionado = Roles.Directora };
  
            Claves datos = LeerClaves().Where(x => x.Email == email && x.Clave == clave).FirstOrDefault();
            if (datos != null)
            { //NO PONE BIEN EL ROL
                int c = 0;
                UsuarioLogueado usuarioLogueado = new UsuarioLogueado();
                usuarioLogueado.Roles = new Roles[3];
                Usuario usu = LeerUsuarios().Where(x => x.Email == email).FirstOrDefault();
                for (int i = 0; i < datos.Roles.Length; i++)
                {
                    if (datos.Roles[i] == Roless.Padre)
                    {
                        usuarioLogueado.Roles[c] = Roles.Padre;
                        c = c + 1;
                    }
                    if (datos.Roles[i] == Roless.Docente)
                    {
                        usuarioLogueado.Roles[c] = Roles.Docente;
                        c = c + 1;
                    }
                    if (datos.Roles[i] == Roless.Directora)
                    {
                        usuarioLogueado.Roles[c] = Roles.Directora;
                        c = c + 1;
                    }
                }
                return new UsuarioLogueado() { Email = email, Nombre = usu.Nombre, Apellido = usu.Apellido };
            }
            else
            {
                return null;
            }
       
        }

        public Resultado ResponderNota(Nota nota, Comentario nuevoComentario, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

    }
}
