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
        private List<Usuario> Usuarios { get; set; }
        private List<Institucion> Instituciones { get; set; }
        private List<Directora> Directores { get; set; }
        private List<Docente> Docentes { get; set; }
        private List<Padre> Padres { get; set; }
        private List<Hijo> Alumnos { get; set; }
        private List<Nota> Notas { get; set; }
        private List<Sala> Salas { get; set; }
        private List<Claves> Claves { get; set; }

        //Path.Combine(Appdomain.CurrentDomain.BaseDirectory, Usuarios);
        //CORRECCION -> Usar AppDomain, para eso en vez de una constante se puede usar una variable readonly
        public readonly string pathUsuarios = $"{AppDomain.CurrentDomain.BaseDirectory}\\Usuarios.txt";
        public readonly string pathInstituciones = $"{AppDomain.CurrentDomain.BaseDirectory}\\Instituciones.txt";
        public readonly string pathDirectores = $"{AppDomain.CurrentDomain.BaseDirectory}\\Directores.txt";
        public readonly string pathDocentes = $"{AppDomain.CurrentDomain.BaseDirectory}\\Docentes.txt";
        public readonly string pathPadres = $"{AppDomain.CurrentDomain.BaseDirectory}\\Padres.txt";
        public readonly string pathAlumnos = $"{AppDomain.CurrentDomain.BaseDirectory}\\Alumnos.txt";
        public readonly string pathNotas = $"{AppDomain.CurrentDomain.BaseDirectory}\\Notas.txt";
        public readonly string pathSalas = $"{AppDomain.CurrentDomain.BaseDirectory}\\Salas.txt";
        public readonly string pathClaves = $"{AppDomain.CurrentDomain.BaseDirectory}\\Claves.txt";

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

        //REVISION
        //Muchas invocaciones al mismo metodo.
        //La validacion de rol debe hacerse al inicio.
        //El array de nulos no es necesario.
        //
        public Resultado AltaDirectora(Directora directora, UsuarioLogueado usuarioLogueado)
        {
            Usuario usuario = null;
            List<string> errores = new List<string>();
            if (LeerUsuarios() == null)
            {
                directora.Id = 1;
            }
            else
                directora.Id = LeerUsuarios().Count + 1;
            if (usuarioLogueado.RolSeleccionado == Roles.Directora) //tiene permiso --> ver si ya está logueado con OTRO ROL
            {
                if (LeerUsuarios() != null)
                    usuario = LeerUsuarios().Where(x => x.Email == directora.Email).FirstOrDefault();
                if (usuario == null || LeerUsuarios() == null) //no está registrado, lo guardo en ambos arrays y guardo nueva clave
                {
                    Random rnd = new Random();
                    Claves nueva = new Claves() { ID = directora.Id, Email = directora.Email, Roles = new Roless[3] { Roless.Directora, Roless.Nulo, Roless.Nulo}, Clave = rnd.Next(100000, 999999).ToString() };//generar clave
                    if (LeerClaves() == null)
                    {
                        Claves = new List<Claves>();
                        Claves.Add(nueva);
                    }
                    else
                    {
                        Claves.Add(nueva);
                    }
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
                    int c = -1;
                    foreach (Roless r in clave.Roles)
                    {
                        c = c + 1;
                        if (r == Roless.Nulo)
                        {
                            clave.Roles[c] = Roless.Directora;
                            break;
                        }
                    }
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
            return new Resultado() { Errores = errores };
        }

        //Idem comentarios directora
        public Resultado AltaDocente(Docente docente, UsuarioLogueado usuarioLogueado)
        {
            Usuario usuario = null;
            List<string> errores = new List<string>();
            if (LeerUsuarios() == null)
            {
                docente.Id = 1;
            }
            else
                docente.Id = LeerUsuarios().Count + 1;
            if (usuarioLogueado.RolSeleccionado == Roles.Directora) //tiene permiso --> ver si ya está logueado con OTRO ROL
            {
                if (LeerUsuarios() != null)
                    usuario = LeerUsuarios().Where(x => x.Email == docente.Email).FirstOrDefault();
                if (usuario == null || LeerUsuarios() == null) //no está registrado, lo guardo en ambos arrays y guardo nueva clave
                {
                    Random rnd = new Random();
                    Claves nueva = new Claves() { ID = docente.Id, Email = docente.Email, Roles = new Roless[3] { Roless.Docente, Roless.Nulo, Roless.Nulo }, Clave = rnd.Next(100000, 999999).ToString() };//generar clave
                    if (LeerClaves() == null)
                    {
                        Claves = new List<Claves>();
                        Claves.Add(nueva);
                    }
                    else
                    {
                        Claves.Add(nueva);
                    }
                    if (LeerUsuarios() == null)
                    {
                        Usuarios = new List<Usuario>();
                        Usuarios.Add(docente);
                    }
                    else
                    {
                        Usuarios.Add(docente);
                    }
                    using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
                    {
                        Writer.Write(JsonConvert.SerializeObject(Usuarios));
                    }
                   
                }
                else //ya está registrado, se agrega ROL en archivo "claves" 
                {
                    Claves clave = LeerClaves().Where(x => x.Email == docente.Email).FirstOrDefault();
                    int c = -1;
                    foreach (Roless r in clave.Roles)
                    {
                        c = c + 1;
                        if (r == Roless.Nulo)
                        {
                            clave.Roles[c] = Roless.Docente;
                            break;
                        }
                    }
                }
                using (StreamWriter Writer = new StreamWriter(pathClaves, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Claves));
                }

                //en ambos casos se agrega a "directores", si no está logueado o si tiene este nuevo rol
                if (LeerDocentes() == null)
                {
                    Docentes = new List<Docente>();
                    Docentes.Add(docente);
                }
                else
                {
                    Docentes.Add(docente);
                }
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

        //Idem comentario directoras
        public Resultado AltaAlumno(Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            Usuario usuario = null;
            List<string> errores = new List<string>();
            if (LeerUsuarios() == null)
            {
                hijo.Id = 1;
            }
            else
                hijo.Id = LeerUsuarios().Count + 1;
            if (usuarioLogueado.RolSeleccionado == Roles.Directora) //tiene permiso 
            {
                if (LeerUsuarios() != null)
                    usuario = LeerUsuarios().Where(x => x.Email == hijo.Email).FirstOrDefault();
                if (usuario == null || LeerUsuarios() == null) //no está registrado, lo guardo en ambos arrays y guardo nueva clave
                {
                    if (LeerUsuarios() == null)
                    {
                        Usuarios = new List<Usuario>();
                        Usuarios.Add(hijo);
                    }
                    else
                    {
                        Usuarios.Add(hijo);
                    }
                    using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
                    {
                        Writer.Write(JsonConvert.SerializeObject(Usuarios));
                    }
                }

                if (LeerAlumnos() == null)
                {
                    Alumnos = new List<Hijo>();
                    Alumnos.Add(hijo);
                }
                else
                {
                    Alumnos.Add(hijo);
                }
                using (StreamWriter Writer = new StreamWriter(pathAlumnos, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Alumnos));
                }
            }
            else //no tiene permiso
            {
                errores.Add("No está autorizado para dar de alta a Alumno");
            }
            return new Resultado() { Errores = errores };
        }

        //Se puede resolver mas facil, cuando no viene nada en el array de hijos, usar la misma variable
        //y cargarla con los hijos de cada sala que llegue y se resuelve una sola vez el alta de sala.
        public Resultado AltaNota(Nota nota, Sala[] salas, Hijo[] hijos, UsuarioLogueado usuarioLogueado)
        { 
            List<string> errores = new List<string>();
            if (LeerAlumnos() == null)
            {
                Alumnos = new List<Hijo>();
            }
            else
            {
                Alumnos = LeerAlumnos();
            }
            if (LeerNotas()==null)
            {
                Notas = new List<Nota>();
            }
            Notas = LeerNotas();
            int cont;
            if (Notas == null)
            {
                Notas = new List<Nota>();
                cont = 0;
            }
            else
                cont = Notas.Count;
            List<Nota> aux = new List<Nota>();
            if (hijos != null && hijos.Length > 0)
            {
                foreach (var item in hijos)
                {
                    cont = cont + 1;
                    Nota nota2 = new Nota();
                    nota2.Comentarios = nota.Comentarios;
                    nota2.Descripcion = nota.Descripcion;
                    nota2.FechaEventoAsociado = nota.FechaEventoAsociado;
                    nota2.Id = cont;
                    nota2.Leida = nota.Leida;
                    nota2.Titulo = nota.Titulo;

                    var hijo = Alumnos.Single(x => x.Id == item.Id);
                    var notasHijo = hijo.Notas == null ? new List<Nota>() : hijo.Notas.ToList();
                    notasHijo.Add(nota2);
                    aux.Add(nota2);
                    hijo.Notas = notasHijo.ToArray();
         
                    using (StreamWriter Writer = new StreamWriter(pathAlumnos, false))
                    {
                        Writer.Write(JsonConvert.SerializeObject(Alumnos));
                    }
                }
            }
            else
            {
                List<Hijo> alumnos = new List<Hijo>();
                foreach (var sala in salas)
                {
                    alumnos.AddRange(Alumnos.Where(x => x.Sala.Id == sala.Id));
                }

                if (salas.Count() == 0) //padre escribe nota a todos sus hijos
                {
                    Padre padre = LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                    foreach (Hijo hijo in padre.Hijos)
                    {
                        alumnos.AddRange(Alumnos.Where(x => x.Id == hijo.Id));
                    }
                }

                foreach (var item in alumnos)
                {
                    cont = cont + 1;
                    Nota nota2 = new Nota();
                    nota2.Comentarios = nota.Comentarios;
                    nota2.Descripcion = nota.Descripcion;
                    nota2.FechaEventoAsociado = nota.FechaEventoAsociado;
                    nota2.Id = cont;
                    nota2.Leida = nota.Leida;
                    nota2.Titulo = nota.Titulo;
                    
                    var notasHijo = item.Notas == null ? new List<Nota>() : item.Notas.ToList();
                    notasHijo.Add(nota2);
                    aux.Add(nota2);
                    item.Notas = notasHijo.ToArray();
                    
                    using (StreamWriter Writer = new StreamWriter(pathAlumnos, false))
                    {
                        Writer.Write(JsonConvert.SerializeObject(Alumnos));
                    }
                }
            }
            //Guardar datos
            Notas.AddRange(aux);
            using (StreamWriter Writer = new StreamWriter(pathNotas, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Notas));
            }
            return new Resultado() { Errores = errores};                     
        }

        //Idem directoras
        public Resultado AltaPadreMadre(Padre padre, UsuarioLogueado usuarioLogueado)
        {
            Usuario usuario = null;
            List<string> errores = new List<string>();
            if (LeerUsuarios() == null)
            {
                padre.Id = 1;
            }
            else
                padre.Id = LeerUsuarios().Count + 1;
            if (usuarioLogueado.RolSeleccionado == Roles.Directora) //tiene permiso --> ver si ya está logueado con OTRO ROL
            {
                if (LeerUsuarios() != null)
                    usuario = LeerUsuarios().Where(x => x.Email == padre.Email).FirstOrDefault();
                if (usuario == null || LeerUsuarios() == null) //no está registrado, lo guardo en ambos arrays y guardo nueva clave
                {
                    Random rnd = new Random();
                    Claves nueva = new Claves() { ID = padre.Id, Email = padre.Email, Roles = new Roless[3] { Roless.Padre, Roless.Nulo, Roless.Nulo }, Clave = rnd.Next(100000, 999999).ToString() };//generar clave
                    if (LeerClaves() == null)
                    {
                        Claves = new List<Claves>();
                        Claves.Add(nueva);
                    }
                    else
                    {
                        Claves.Add(nueva);
                    }
                    if (LeerUsuarios() == null)
                    {
                        Usuarios = new List<Usuario>();
                        Usuarios.Add(padre);
                    }
                    else
                    {
                        Usuarios.Add(padre);
                    }
                    using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
                    {
                        Writer.Write(JsonConvert.SerializeObject(Usuarios));
                    }
                }
                else //ya está registrado, se agrega ROL en archivo "claves" 
                {
                    Claves clave = LeerClaves().Where(x => x.Email == padre.Email).FirstOrDefault();
                    int c = -1;
                    foreach (Roless r in clave.Roles)
                    {
                        c = c + 1;
                        if (r == Roless.Nulo)
                        {
                            clave.Roles[c] = Roless.Padre;
                            break;
                        }
                    }
                }
                //GUARDAR clave nueva o actualizacion de roles
                using (StreamWriter Writer = new StreamWriter(pathClaves, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Claves));
                }
                //en ambos casos se agrega a "directores", si no está logueado o si tiene este nuevo rol
                if (LeerPadres() == null)
                {
                    Padres = new List<Padre>();
                    Padres.Add(padre);
                }
                else
                {
                    Padres.Add(padre);
                }
                using (StreamWriter Writer = new StreamWriter(pathPadres, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Padres));
                }
            }
            else //no tiene permiso
            {
                errores.Add("No  está autorizado para dar de alta a Directora");
            }
            return new Resultado() { Errores = errores };
        }

        //Cuando se usa metodos "OrDefault" siempre se pregunta primero si no es null antes de seguir.
        public Resultado AsignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)
        {
            List<string> errores = new List<string>();
            if (usuarioLogueado.RolSeleccionado == Roles.Directora)
            {
                var salasDocente = docente.Salas != null ? docente.Salas.ToList() : new List<Sala>();

                if (salasDocente.Any(x => x.Id == sala.Id) == false) //si no la tiene ya asignada, la asigna y agrega a lista de salas
                {
                    salasDocente.Add(sala);
                    if (LeerSalas() == null)
                    {
                        LeerSalas().Add(sala);
                    }
                    else
                    {
                        if (LeerSalas().Where(x => x.Id == sala.Id).FirstOrDefault() == null) //si la sala no está en la lista de todas, la agrega
                        {
                            LeerSalas().Add(sala);
                        }
                    }
                }

                docente.Salas = salasDocente.ToArray();
                //escribir en docentes para actualizar salas
                using (StreamWriter Writer = new StreamWriter(pathDocentes, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Docentes));
                }
                return new Resultado();
            }
            else
            {
                errores.Add("No  está autorizado para realizar la acción que desea");
            }
            return new Resultado() { Errores = errores };
        }

        public Resultado AsignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            List<string> errores = new List<string>();
            if (usuarioLogueado.RolSeleccionado == Roles.Directora)
            {
                var hijosPadre = padre.Hijos != null ? padre.Hijos.ToList() : new List<Hijo>();

                if (hijosPadre.Any(x => x.Id == hijo.Id) == false)
                    hijosPadre.Add(hijo);

                padre.Hijos = hijosPadre.ToArray();

                using (StreamWriter Writer = new StreamWriter(pathPadres, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Padres));
                }
            }
            else //no tiene permiso
            {
                errores.Add("No  está autorizado para realizar la acción que desea");
            }
            return new Resultado() { Errores = errores };
        }

        public Resultado DesasignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)
        {
            List<string> errores = new List<string>();

            if (usuarioLogueado.RolSeleccionado == Roles.Directora)
            {
                var salasDocente = docente.Salas != null ? docente.Salas.ToList() : new List<Sala>();

                if (salasDocente.Any(x => x.Id == sala.Id) == true)
                    salasDocente.Remove(sala);

                docente.Salas = salasDocente.ToArray();

                using (StreamWriter Writer = new StreamWriter(pathDocentes, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Docentes));
                }
            }
            else
            {
                errores.Add("No  está autorizado para realizar la acción que desea");
            }
            return new Resultado() { Errores = errores };
        }

        public Resultado DesasignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            List<string> errores = new List<string>();

            if (usuarioLogueado.RolSeleccionado == Roles.Directora)
            {
                var hijosPadre = padre.Hijos != null ? padre.Hijos.ToList() : new List<Hijo>();

                if (hijosPadre.Any(x => x.Id == hijo.Id) == true)
                    hijosPadre.Remove(hijo);

                padre.Hijos = hijosPadre.ToArray();

                using (StreamWriter Writer = new StreamWriter(pathPadres, false))
                {
                    Writer.Write(JsonConvert.SerializeObject(Padres));
                }
            }
            else
            {
                errores.Add("No  está autorizado para realizar la acción que desea");
            }
            return new Resultado() { Errores = errores };
        }

        public Resultado EditarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado)
        {
            Directora nuevaDirectora = new Directora();
            nuevaDirectora = ObtenerDirectoraPorId(usuarioLogueado, id);
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario = LeerUsuarios().Where(x => x.Id == directora.Id).FirstOrDefault();
            Claves nuevaClave = LeerClaves().Where(x => x.ID == directora.Id).FirstOrDefault();

            if (nuevaClave != null)
                nuevaClave.Email = directora.Email; //PROBAR CUANDO ESTÉ HECHO EN ALTA

            //El ID no se deberia editar.
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
            using (StreamWriter Writer = new StreamWriter(pathClaves, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Claves));
            }

            return new Resultado();

        }

        public Resultado EditarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            Hijo nuevoHijo = new Hijo();
            nuevoHijo = ObtenerAlumnoPorId(usuarioLogueado, id);
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario = LeerUsuarios().Where(x => x.Id == hijo.Id).FirstOrDefault();

            //El ID no se deberia editar
            nuevoHijo.Id = id;
            nuevoHijo.Institucion = hijo.Institucion;
            nuevoHijo.Nombre = hijo.Nombre;
            nuevoHijo.FechaNacimiento = hijo.FechaNacimiento;
            nuevoHijo.Sala = hijo.Sala;
            nuevoHijo.ResultadoUltimaEvaluacionAnual = hijo.ResultadoUltimaEvaluacionAnual;
            nuevoHijo.Email = hijo.Email;
            nuevoHijo.Apellido = hijo.Apellido;

            nuevoUsuario.Id = hijo.Id;
            nuevoUsuario.Nombre = hijo.Nombre;
            nuevoUsuario.Apellido = hijo.Apellido;
            nuevoUsuario.Email = hijo.Email;

            using (StreamWriter Writer = new StreamWriter(pathAlumnos, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Alumnos));
            }
            using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Usuarios));
            }

            return new Resultado();

        }

        public Resultado EditarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado)
        {
            Docente nuevoDocente = new Docente();
            nuevoDocente = ObtenerDocentePorId(usuarioLogueado, id);
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario = LeerUsuarios().Where(x => x.Id == docente.Id).FirstOrDefault();
            Claves nuevaClave = LeerClaves().Where(x => x.ID == docente.Id).FirstOrDefault();

            if (nuevaClave != null)
                nuevaClave.Email = docente.Email; 

            nuevoDocente.Id = id;
            nuevoDocente.Apellido = docente.Apellido;
            nuevoDocente.Email = docente.Email;
            nuevoDocente.Nombre = docente.Nombre;
            nuevoDocente.Salas = docente.Salas;

            nuevoUsuario.Id = docente.Id;
            nuevoUsuario.Nombre = docente.Nombre;
            nuevoUsuario.Apellido = docente.Apellido;
            nuevoUsuario.Email = docente.Email;

            using (StreamWriter Writer = new StreamWriter(pathDocentes, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Docentes));
            }
            using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Usuarios));
            }
            using (StreamWriter Writer = new StreamWriter(pathClaves, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Claves));
            }

            return new Resultado();

        }

        public Resultado EditarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            Padre nuevoPadre = new Padre();
            nuevoPadre = ObtenerPadrePorId(usuarioLogueado, id);
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario = LeerUsuarios().Where(x => x.Id == padre.Id).FirstOrDefault();
            Claves nuevaClave = LeerClaves().Where(x => x.ID == padre.Id).FirstOrDefault();

            if (nuevaClave != null)
                nuevaClave.Email = padre.Email; //PROBAR CUANDO ESTÉ HECHO EN ALTA

            nuevoPadre.Id = id;
            nuevoPadre.Apellido = padre.Apellido;
            nuevoPadre.Email = padre.Email;
            nuevoPadre.Nombre = padre.Nombre;
            nuevoPadre.Hijos = padre.Hijos;

            nuevoUsuario.Id = padre.Id;
            nuevoUsuario.Nombre = padre.Nombre;
            nuevoUsuario.Apellido = padre.Apellido;
            nuevoUsuario.Email = padre.Email;

            using (StreamWriter Writer = new StreamWriter(pathPadres, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Padres));
            }
            using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Usuarios));
            }
            using (StreamWriter Writer = new StreamWriter(pathClaves, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Claves));
            }

            return new Resultado();
        }

        public Resultado EliminarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado)
        {
            LeerDirectores().RemoveAll(x => x.Id == directora.Id);
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
            LeerDocentes().RemoveAll(x => x.Id == docente.Id);
            LeerUsuarios().RemoveAll(x => x.Id == docente.Id);
            using (StreamWriter Writer = new StreamWriter(pathDocentes, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Docentes));
            }
            using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Usuarios));
            }
            return new Resultado();

        }

        public Resultado EliminarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            LeerPadres().RemoveAll(x => x.Id == padre.Id);
            LeerUsuarios().RemoveAll(x => x.Id == padre.Id);
            using (StreamWriter Writer = new StreamWriter(pathPadres, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Padres));
            }
            using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Usuarios));
            }
            return new Resultado();
        }

        public Resultado EliminarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            LeerAlumnos().RemoveAll(x => x.Id == hijo.Id);
            LeerUsuarios().RemoveAll(x => x.Id == hijo.Id);
            using (StreamWriter Writer = new StreamWriter(pathAlumnos, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Alumnos));
            }
            using (StreamWriter Writer = new StreamWriter(pathUsuarios, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Usuarios));
            }
            return new Resultado();

        }

        //Podrian tener un metodo similar al de "Leer" que sea "Escribir" y lo invocan siempre que necesiten
        //actualizar un archivo.
        public Resultado MarcarNotaComoLeida(Nota nota, UsuarioLogueado usuarioLogueado)
        {
            List<string> errores = new List<string>();
            if (usuarioLogueado.RolSeleccionado == Roles.Padre)
            {
                Padre nuevopadre = new Padre();
                nuevopadre = LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                Alumnos = LeerAlumnos();
                foreach (Hijo hijo in Alumnos)
                {
                    if (hijo.Notas != null)
                    {
                        Nota nota1 = hijo.Notas.Single(x => x.Id == nota.Id);
                        nota1.Leida = true;
                        LeerNotas().Single(x => x.Id == nota.Id).Leida = true;
                        //escribir en NOTAS y ALUMNOS
                        using (StreamWriter Writer = new StreamWriter(pathNotas, false))
                        {
                            Writer.Write(JsonConvert.SerializeObject(Notas));
                        }
                        using (StreamWriter Writer = new StreamWriter(pathAlumnos, false))
                        {
                            Writer.Write(JsonConvert.SerializeObject(Alumnos));
                        }
                        break;
                    }
                }
            }
            else
                errores.Add("No tiene permiso de realizar la acción");

            return new Resultado() { Errores = errores};
        }

        public Hijo ObtenerAlumnoPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return LeerAlumnos().Where(x => x.Id == id).FirstOrDefault();
        }

        //El filtro cuando no hay alumnos es innecesario
        //La cantidad de registros se calcula mal
        public Grilla<Hijo> ObtenerAlumnos(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            if (LeerAlumnos() == null)
            {
                return new Grilla<Hijo>()
                {
                    Lista = new List<Hijo>().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal) || x.Id.ToString().Contains(busquedaGlobal) || x.Email.Contains(busquedaGlobal))
                                    .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = 0
                };
            }
            else
                return new Grilla<Hijo>()
                {
                    Lista = LeerAlumnos().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal) || x.Id.ToString().Contains(busquedaGlobal) || x.Email.Contains(busquedaGlobal))
                                        .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = LeerAlumnos().Count
                };
        }

        public Nota[] ObtenerCuadernoComunicaciones(int idPersona, UsuarioLogueado usuarioLogueado)
        {
            Nota[] arrayNotas = new Nota[] { };
            if (usuarioLogueado.RolSeleccionado == Roles.Padre) //padre. que vea solo lo de sus hijos.
            {
                Padre nuevoPadre = LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                foreach (Hijo hijo in nuevoPadre.Hijos)
                {
                    if (hijo.Id == idPersona)
                    { //como no logramos que escriba en "Padres", consulta el archivo de Hijos ("Alumnos")
                        Hijo hijoo = LeerAlumnos().Where(x => x.Id == idPersona).FirstOrDefault();
                        arrayNotas = hijoo.Notas == null ? new Nota[] { } : hijoo.Notas.ToArray();
                    }
                }
            }
            if (usuarioLogueado.RolSeleccionado == Roles.Directora) //directora. todos, no se debería validar nada
            {
                Hijo nuevoAlumno = LeerAlumnos().Where(x => x.Id == idPersona).FirstOrDefault();
                arrayNotas = nuevoAlumno.Notas == null ? new Nota[] { } : nuevoAlumno.Notas.ToArray();
            }
            if (usuarioLogueado.RolSeleccionado == Roles.Docente) //docente. ve solo lo de sus clases
            {
                Hijo nuevoAlumno = LeerAlumnos().Where(x => x.Id == idPersona).FirstOrDefault();
                Docente nuevaDocente = LeerDocentes().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                foreach (Sala sala in nuevaDocente.Salas)
                {
                    if (sala.Id == nuevoAlumno.Sala.Id)
                    {
                        arrayNotas = nuevoAlumno.Notas == null ? new Nota[] { } : nuevoAlumno.Notas.ToArray();
                    }
                }
            }

            return arrayNotas;          
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
                    Lista = new List<Directora>().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal) || x.Id.ToString().Contains(busquedaGlobal) || x.Email.Contains(busquedaGlobal))
                                    .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = 0
                };
            }
            else
                return new Grilla<Directora>()
                {
                    Lista = LeerDirectores().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal) || x.Id.ToString().Contains(busquedaGlobal) || x.Email.Contains(busquedaGlobal))
                                        .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = LeerDirectores().Count
                };
        }

        public Docente ObtenerDocentePorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return LeerDocentes().Where(x => x.Id == id).FirstOrDefault();
        }

        public Grilla<Docente> ObtenerDocentes(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            if (LeerDocentes() == null)
            {
                return new Grilla<Docente>()
                {
                    Lista = new List<Docente>().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal) || x.Id.ToString().Contains(busquedaGlobal) || x.Email.Contains(busquedaGlobal))
                                    .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = 0
                };
            }
            else
                return new Grilla<Docente>()
                {
                    Lista = LeerDocentes().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal) || x.Id.ToString().Contains(busquedaGlobal) || x.Email.Contains(busquedaGlobal))
                                        .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = LeerDocentes().Count
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
            return LeerPadres().Where(x => x.Id == id).FirstOrDefault();
        }

        public Grilla<Padre> ObtenerPadres(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            if (LeerPadres() == null)
            {
                return new Grilla<Padre>()
                {
                    Lista = new List<Padre>().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal) || x.Id.ToString().Contains(busquedaGlobal) || x.Email.Contains(busquedaGlobal))
                                    .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = 0
                };
            }
            else
                return new Grilla<Padre>()
                {
                    Lista = LeerPadres().ToArray().Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal) || x.Id.ToString().Contains(busquedaGlobal) || x.Email.Contains(busquedaGlobal))
                                        .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                    CantidadRegistros = LeerPadres().Count
                };
        }

        public Hijo[] ObtenerPersonas(UsuarioLogueado usuarioLogueado)
        {
            /// Obtiene las notas de un cuaderno, si el usuario es padre solo puede obtener cuadernos de sus hijos, 
            ///si es docente de alumnos de sus salas
            /// y si es directora de cualquier alumno de la institucion
            switch (usuarioLogueado.RolSeleccionado)
            {
                case Roles.Padre:
                    {
                        Padre padre = LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                        if (padre.Hijos == null)
                        {
                            padre.Hijos = new Hijo[] { };
                        }
                        return LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault().Hijos;
                    }          
                case Roles.Directora:
                    {
                        if (LeerAlumnos() == null)
                        {
                            Alumnos = new List<Hijo>();
                            return Alumnos.ToArray();
                        }         
                        else
                            return LeerAlumnos().ToArray();
                    }
                case Roles.Docente: //devolver alumnos de todas sus aulas
                    {
                        List<Hijo> lista = new List<Hijo>();
                        Docente docente = LeerDocentes().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                        //ver si es null
                        if (docente.Salas == null)
                        {
                            docente.Salas = new Sala[] { };
                        }
                        foreach (Sala sala in docente.Salas)
                        {
                            foreach (Hijo hijo in LeerAlumnos())
                            {
                                if (hijo.Sala.Id == sala.Id)
                                {
                                    lista.Add(hijo);
                                }
                            }
                        }
                        return lista.ToArray();
                    }
                default:
                    throw new Exception("Rol no implementado");
            }
        }

        //Falta validar que si el usuario es docente, traiga solo las salas donde este asignado.
        public Sala[] ObtenerSalasPorInstitucion(UsuarioLogueado usuarioLogueado)//salas del usuario log
        {
            if (LeerSalas() == null)
            {
                Salas = new List<Sala>();
                return Salas.ToArray();
            }
            else
                return LeerSalas().ToArray();
        }

        public UsuarioLogueado ObtenerUsuario(string email, string clave)
        {
            if (email == "" || clave == "")
                return null;
            else
            {
                if (email != "" && clave != "")
                {
                    Claves datos = LeerClaves().Where(x => x.Email == email && x.Clave == clave).FirstOrDefault();
                    if (datos != null)
                    {
                        Usuario usuario = LeerUsuarios().Where(x => x.Email == email).FirstOrDefault();
                        List<Roles> roles = new List<Roles>();
                        UsuarioLogueado usuarioLog = new UsuarioLogueado();
                        if (datos.Roles.ToList().Contains(Roless.Padre))
                        {
                            roles.Add(Roles.Padre);
                            usuarioLog.RolSeleccionado = Roles.Padre;
                        }
                        if (datos.Roles.ToList().Contains(Roless.Directora))
                        {
                            roles.Add(Roles.Directora);
                            usuarioLog.RolSeleccionado = Roles.Directora;
                        }
                        if (datos.Roles.ToList().Contains(Roless.Docente))
                        {
                            roles.Add(Roles.Docente);
                            usuarioLog.RolSeleccionado = Roles.Docente;
                        }
                        usuarioLog.Roles = roles.ToArray();
                        usuarioLog.Email = email;
                        usuarioLog.Nombre = usuario.Nombre;
                        usuarioLog.Apellido = usuario.Apellido;
                        return usuarioLog;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                    return null;
            }
        }

        public Resultado ResponderNota(Nota nota, Comentario nuevoComentario, UsuarioLogueado usuarioLogueado)
        {
            /*  Si es padre y la nota corresponde a uno de sus hijos
                Si es docente y la nota corresponde a un alumno de su sala
                Si es director y la nota corresponde a un alumno de su institución
            */
            Notas = LeerNotas();
            Alumnos = LeerAlumnos();
            Usuarios = LeerUsuarios();
            nuevoComentario.Usuario = Usuarios.Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
            //agregar la respuesta en el archivo de notas en gral.
            Nota n = Notas.Where(x => x.Id == nota.Id).FirstOrDefault();
            List<Comentario> comentarios = n.Comentarios.Count() == 0 ? new List<Comentario>() : n.Comentarios.ToList();
            comentarios.Add(nuevoComentario);
            n.Comentarios = comentarios.ToArray();
            using (StreamWriter Writer = new StreamWriter(pathNotas, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Notas));
            }

            if (usuarioLogueado.RolSeleccionado == Roles.Padre)
            {
                Padre padre = LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                if (padre.Hijos== null)
                {
                    padre.Hijos = new Hijo[] { };
                }
                foreach (Hijo h in padre.Hijos)
                {
                    foreach(Hijo a in Alumnos)
                    {
                        if (h.Id == a.Id)
                        {
                            if (a.Notas != null)
                            {
                                foreach(Nota notaa in a.Notas)
                                {
                                    if (notaa.Id == nota.Id)
                                    {
                                        notaa.Comentarios = comentarios.ToArray();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (usuarioLogueado.RolSeleccionado == Roles.Docente)
                {
                    Docente docente = LeerDocentes().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                    if (docente.Salas == null)
                    {
                        docente.Salas = new Sala[] { };
                    }
                    foreach (Sala s in docente.Salas)
                    {
                        foreach (Hijo a in Alumnos) 
                        {
                            if (s.Id == a.Sala.Id)
                            {
                                if (a.Notas != null)
                                {
                                    foreach (Nota notaa in a.Notas)
                                    {
                                        if (notaa.Id == nota.Id)
                                        {
                                            notaa.Comentarios = comentarios.ToArray();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else //es DIRECTOR
                {
                    foreach (Hijo h in Alumnos)
                    {
                        if (h.Notas != null)
                        {
                            foreach (Nota notaa in h.Notas)
                            {
                                if (notaa.Id == nota.Id)
                                {
                                    notaa.Comentarios = comentarios.ToArray();
                                }
                            }
                        }
                    }
                }
            }
                             
            using (StreamWriter Writer = new StreamWriter(pathAlumnos, false))
            {
                Writer.Write(JsonConvert.SerializeObject(Alumnos));
            }

            return new Resultado();         
        }
    }
}
