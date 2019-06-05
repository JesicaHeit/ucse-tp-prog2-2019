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
        private Usuario[] Usuarios { get; set; }
        private Institucion[] Instituciones { get; set; }
        private Directora[] Directores { get; set; }
        private Docente[] Docentes { get; set; }
        private Padre[] Padres { get; set; }
        private Hijo[] Alumnos { get; set; }
        private Nota[] Notas { get; set; }
        private Sala[] Salas { get; set; }
        private Claves[] Claves { get; set; }

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
            Usuarios = new Usuario[0];
            Instituciones = new Institucion[0];
            Directores = new Directora[0];
            Docentes = new Docente[0];
            Padres = new Padre[0];
            Alumnos = new Hijo[0];
            Notas = new Nota[0];
            Salas = new Sala[0];
            Claves = new Claves[0];
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
        public void LeerUsuarios()
        {
            using (StreamReader leer = new StreamReader(@"C:\Archivos\Usuarios.txt"))
            {
                string contenido = leer.ReadToEnd();
                Usuarios = JsonConvert.DeserializeObject<Usuario[]>(contenido);
            }
        }

        public void LeerDirectores()
        {
            using (StreamReader leer = new StreamReader(@"C:\Archivos\Directores.txt"))
            {
                string contenido = leer.ReadToEnd();
                Directores = JsonConvert.DeserializeObject<Directora[]>(contenido);
            }
        }

        public void LeerDocentes()
        {
            using (StreamReader leer = new StreamReader(@"C:\Archivos\Docentes.txt"))
            {
                string contenido = leer.ReadToEnd();
                Docentes = JsonConvert.DeserializeObject<Docente[]>(contenido);
            }
        }

        public void LeerPadres()
        {
            using (StreamReader leer = new StreamReader(@"C:\Archivos\Padres.txt"))
            {
                string contenido = leer.ReadToEnd();
                Padres = JsonConvert.DeserializeObject<Padre[]>(contenido);
            }
        }

        public void LeerAlumnos()
        {
            using (StreamReader leer = new StreamReader(@"C:\Archivos\Alumnos.txt"))
            {
                string contenido = leer.ReadToEnd();
                Alumnos = JsonConvert.DeserializeObject<Hijo[]>(contenido);
            }
        }

        public void LeerInstituciones()
        {
            using (StreamReader leer = new StreamReader(@"C:\Archivos\Instituciones.txt"))
            {
                string contenido = leer.ReadToEnd();
                Instituciones = JsonConvert.DeserializeObject<Institucion[]>(contenido);
            }
        }

        public void LeerNotas()
        {
            using (StreamReader leer = new StreamReader(@"C:\Archivos\Notas.txt"))
            {
                string contenido = leer.ReadToEnd();
                Notas = JsonConvert.DeserializeObject<Nota[]>(contenido);
            }
        }

        public void LeerSalas()
        {
            using (StreamReader leer = new StreamReader(@"C:\Archivos\Salas.txt"))
            {
                string contenido = leer.ReadToEnd();
                Salas = JsonConvert.DeserializeObject<Sala[]>(contenido);
            }
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
            List<string> errores = new List<string>();
            directora.Id = Directores.Count() + 1;
            if (usuarioLogueado.RolSeleccionado == Roles.Directora) //tiene permiso --> ver si ya está logueado con OTRO ROL
            {   
                Usuario usuario = Usuarios.ToList().Where(x => x.Email == directora.Email).FirstOrDefault();
                if (usuario == null) //no está registrado, lo guardo en ambos arrays y guardo nueva clave
                {
                    Random rnd = new Random();
                    Claves nueva = new Claves() {Email=directora.Email, Roles=new Roless[] {Roless.Directora}, Clave=rnd.Next(100000,999999).ToString() };//generar clave
                    Claves.ToList().Add(nueva); //guardar clave 
                    //guardar en archivo "usuarios"--> UNIFICAR EN UN MÉTODO              
                    Usuarios.ToList().Add(directora);
                    Usuarios.ToArray();               
                    using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
                    {
                        Writer.Write(JsonConvert.SerializeObject(Usuarios));
                    }
                }
                else //ya está registrado, se agrega ROL en archivo "claves" 
                {
                    Claves clave = Claves.ToList().Where(x => x.Email == directora.Email).FirstOrDefault();
                    clave.Roles[clave.Roles.Count()] = Roless.Directora;
                }
                //GUARDAR clave nueva o actualizacion de roles
                using (StreamWriter Writer = new StreamWriter(pathClaves, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Claves));
                }
                //en ambos casos se agrega a "directores", si no está logueado o si tiene este nuevo rol
                Directores.ToList().Add(directora);
                Directores.ToArray();
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
            throw new NotImplementedException();

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
            throw new NotImplementedException();

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
            throw new NotImplementedException();

        }

        public Nota[] ObtenerCuadernoComunicaciones(int idPersona, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Directora ObtenerDirectoraPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            throw new NotImplementedException();
        }

        public Grilla<Directora> ObtenerDirectoras(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            throw new NotImplementedException();

        }

        public Docente ObtenerDocentePorId(UsuarioLogueado usuarioLogueado, int id)
        {
            throw new NotImplementedException();
        }

        public Grilla<Docente> ObtenerDocentes(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            throw new NotImplementedException();

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
            throw new NotImplementedException();

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
            {
                return null;
            }
            else  //ver si la clave es correcta
            {
                Claves datos = Claves.ToList().Where(x => x.Email == email).FirstOrDefault();
                if (datos != null)
                {
                    if (datos.Clave == clave)
                    {
                        Usuario user = Usuarios.ToList().Where(x => x.Email == email).FirstOrDefault();
                        Roles[] roles = new Roles[] { };
                        int c = 0;
                        if (Directores.ToList().Where(x => x.Id == user.Id).FirstOrDefault() != null)
                        {
                            roles[c] = Roles.Directora;
                            c = c + 1;
                        }
                        if (Padres.ToList().Where(x => x.Id == user.Id).FirstOrDefault() != null)
                        {
                            roles[c] = Roles.Padre;
                            c = c + 1;
                        }
                        if (Docentes.ToList().Where(x => x.Id == user.Id).FirstOrDefault() != null)
                        {
                            roles[c] = Roles.Docente;
                            c = c + 1;
                        }
                        return new UsuarioLogueado() { Email = email, Nombre = user.Nombre, Apellido = user.Apellido, Roles = roles };
                    }
                    else                    
                        return null;                
                }
                else
                    return null;
            }
        }

        public Resultado ResponderNota(Nota nota, Comentario nuevoComentario, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

    }
}
