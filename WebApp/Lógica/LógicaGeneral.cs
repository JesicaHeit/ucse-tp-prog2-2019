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
        public List<Usuario> Usuarios { get; set; }
        public List<Institucion> Instituciones { get; set; }
        public List<Directora> Directores { get; set; }
        public List<Docente> Docentes { get; set; }
        public List<Padre> Padres { get; set; }
        public List<Hijo> Alumnos { get; set; }
        public List<Nota> Notas { get; set; }
        public List<Sala> Salas { get; set; }
        public List<Claves> Claves { get; set; }

        //Path.Combine(Appdomain.CurrentDomain.BaseDirectory, Usuarios);
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
                    Claves nueva = new Claves() { ID = directora.Id, Email = directora.Email, Roles = new Roless[] { Roless.Directora }, Clave = rnd.Next(100000, 999999).ToString() };//generar clave
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
                    clave.Roles.ToList().Add(Roless.Directora);
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
                    Claves nueva = new Claves() { ID = docente.Id, Email = docente.Email, Roles = new Roless[] { Roless.Docente }, Clave = rnd.Next(100000, 999999).ToString() };//generar clave
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
                    clave.Roles[clave.Roles.Length] = Roless.Docente;
                }
                //GUARDAR clave nueva o actualizacion de roles
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

        public Resultado AltaNota(Nota nota, Sala[] salas, Hijo[] hijos, UsuarioLogueado usuarioLogueado)
        {
            // se ejecuta una vez. si es directora o docente, que lo mande a todas las salas 
            //o a alguna particular. una o mas . salas llega cuando es directora o docente hijos [] cuando el usuario seleccione.
            //si se queda en salas genera una nota por cada alumno de la s sala s. SIEMPRE SE GUARDA EN HIJOS.
            List<string> errores = new List<string>();
            if ((usuarioLogueado.RolSeleccionado == Roles.Directora)||(usuarioLogueado.RolSeleccionado == Roles.Docente))//directora o docente
            {
                if (salas != null && hijos == null) //seleccionó solo salas
                {
                    foreach (Sala s in salas)
                    {
                        if (salas.ToList().Contains(s)) //si la sala fue seleccionada, agrega a cada uno de sus alumnos, la nota 
                        {
                            foreach (Hijo alumno in LeerAlumnos())
                            {
                                if (alumno.Sala == s)
                                {
                                    if (alumno.Notas == null)
                                    {
                                        alumno.Notas = new Nota[] { };
                                    }
                                    alumno.Notas.ToList().Add(nota);
                                    alumno.Notas.ToArray();
                                }
                            }                    
                        }
                    }
                }
                else // seleccionó salas y alumnos de esas salas
                {
                    foreach (Sala s in salas)
                    {
                        if (salas.ToList().Contains(s)) // si la sala fue seleccionada, agrega a cada uno de sus alumnos seleccionados
                        {
                            foreach (Hijo alumno in LeerAlumnos())
                            {
                                if (alumno.Sala == s && hijos.ToList().Contains(alumno))
                                {  
                                    if (alumno.Notas == null)
                                    {
                                        alumno.Notas = new Nota[] { };
                                    }
                                    alumno.Notas.ToList().Add(nota);
                                    alumno.Notas.ToArray();
                                }
                            }
                        }
                    }
                }              
            }
            else // es padre y seleccionó a alguno de sus hijos o todos
            {
                // buscar hijos de ese padre
                Padre padre = LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                Hijo[] hijoss = padre.Hijos;
                foreach (Hijo hijo in hijoss)
                {
                    if (hijos.Contains(hijo))
                    {
                        if (hijo.Notas == null)
                        {
                            hijo.Notas = new Nota[] { };
                        }
                        hijo.Notas.ToList().Add(nota);
                        hijo.Notas.ToArray();
                    }
                }
            }
            //Guardar datos
            List<Nota> notas = LeerNotas();
            if (notas == null)
            {
                notas = new List<Nota>();
                notas.Add(nota);
            }
            else
            {
                notas.Add(nota);
            }

            using (StreamWriter Writer = new StreamWriter(pathNotas, false))
            {
                Writer.Write(JsonConvert.SerializeObject(notas));
            }

            using (StreamWriter Writer = new StreamWriter(pathAlumnos, false))
            {
                Writer.Write(JsonConvert.SerializeObject(LeerAlumnos()));
            }
            return new Resultado() { Errores = errores};

            /*
             * if (hijos != null && hijos.Length > 0)
            {
                foreach (var item in hijos)
                {
                    var hijo = _alumnos.Single(x => x.Id == item.Id);
                    var notasHijo = hijo.Notas == null ? new List<Nota>() : hijo.Notas.ToList();

                    notasHijo.Add(nota);

                    hijo.Notas = notasHijo.ToArray();
                }
            }
            else
            {
                List<Hijo> alumnos = new List<Hijo>();
                foreach (var sala in salas)
                {
                    alumnos.AddRange(_alumnos.Where(x => x.Sala.Id == sala.Id));
                }

                foreach (var item in alumnos)
                {
                    var notasHijo = item.Notas == null ? new List<Nota>() : item.Notas.ToList();

                    notasHijo.Add(nota);

                    item.Notas = notasHijo.ToArray();                    
                }
            }

            return new Resultado();
             */
        }

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
                    Claves nueva = new Claves() { ID = padre.Id, Email = padre.Email, Roles = new Roless[] { Roless.Padre }, Clave = rnd.Next(100000, 999999).ToString() };//generar clave
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
                    clave.Roles[clave.Roles.Length] = Roless.Padre;
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
                nuevaClave.Email = docente.Email; //PROBAR CUANDO ESTÉ HECHO EN ALTA

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

        public Resultado MarcarNotaComoLeida(Nota nota, UsuarioLogueado usuarioLogueado)
        {
            if (usuarioLogueado.RolSeleccionado == Roles.Padre)
            {
                Padre nuevopadre = new Padre();
                nuevopadre = LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                foreach (Hijo hijo in nuevopadre.Hijos)
                {
                    Nota nota1 = hijo.Notas.Where(x => x.Id == nota.Id).FirstOrDefault();
                    if (nota1 != null)
                    {
                        nota1.Leida = true;
                        break;
                    }
                }
            }
            return new Resultado();
        }

        public Hijo ObtenerAlumnoPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return LeerAlumnos().Where(x => x.Id == id).FirstOrDefault();
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
                    CantidadRegistros = LeerAlumnos().Count
                };
        }

        public Nota[] ObtenerCuadernoComunicaciones(int idPersona, UsuarioLogueado usuarioLogueado)
        {
            Nota[] arrayNotas = new Nota[] { };
            if (usuarioLogueado.RolSeleccionado == Roles.Padre) //padre. que vea solo lo de sus hijos.
            {
                int c = 0;
                Padre nuevoPadre = LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                foreach (Hijo hijo in nuevoPadre.Hijos)
                {
                    if (hijo.Id == idPersona)
                    {
                        foreach (Nota nota in Notas)
                        {
                            foreach (Nota nota2 in hijo.Notas)
                            {
                                if (nota == nota2)
                                {
                                    arrayNotas[c] = nota;
                                    c = c + 1;
                                }
                            }
                        }
                    }
                }
            }
            if (usuarioLogueado.RolSeleccionado == Roles.Directora) //directora. todos, no se debería validar nada
            {
                int c = 0;
                Hijo nuevoAlumno = LeerAlumnos().Where(x => x.Id == idPersona).FirstOrDefault();
                foreach (Nota nota in Notas)
                {
                    foreach (Nota nota2 in nuevoAlumno.Notas)
                    {
                        if (nota == nota2)
                        {
                            arrayNotas[c] = nota;
                            c = c + 1;
                        }
                    }
                }
            }
            if (usuarioLogueado.RolSeleccionado == Roles.Docente) //docente. ve solo lo de sus clases
            {
                int c = 0;
                Hijo nuevoAlumno = LeerAlumnos().Where(x => x.Id == idPersona).FirstOrDefault();
                Docente nuevaDocente = LeerDocentes().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                foreach (Sala sala in nuevaDocente.Salas)
                {
                    if (nuevoAlumno.Sala == sala)
                    {
                        foreach(Nota nota in Notas)
                        {
                            foreach (Nota nota2 in nuevoAlumno.Notas)
                            {
                                if (nota == nota2)
                                {
                                    arrayNotas[c] = nota;
                                    c = c + 1;
                                }
                            }
                          
                        }
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
            return LeerDocentes().Where(x => x.Id == id).FirstOrDefault();
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
                    CantidadRegistros = LeerPadres().Count
                };
        }

        public Hijo[] ObtenerPersonas(UsuarioLogueado usuarioLogueado)
        {
            switch (usuarioLogueado.RolSeleccionado)
            {
                case Roles.Padre:
                    return LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault().Hijos;
                case Roles.Directora:
                    return LeerAlumnos().ToArray();
                case Roles.Docente: //devolver alumnos de todas sus aulas
                    {
                        List<Hijo> lista = new List<Hijo>();
                        Docente docente = LeerDocentes().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                        foreach (Sala sala in docente.Salas)
                        {
                            foreach (Hijo hijo in LeerAlumnos())
                            {
                                if (hijo.Sala == sala)
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
            /*if (email == "directora@ucse.com" && clave == "123456")
                  return new UsuarioLogueado() { Email = email, Nombre = "Usuario", Apellido = "Directora", Roles = new Roles[]
                  { Roles.Directora }, RolSeleccionado = Roles.Directora }; */

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
                            usuarioLog.RolSeleccionado = Roles.Padre;     // esto después SE CAMBIA, acá se usa si es solo 1 ROL
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
            List<string> errores = new List<string>();
            if (usuarioLogueado.RolSeleccionado == Roles.Padre)
            {
                Padre nuevopadre = new Padre();
                nuevopadre = LeerPadres().Where(x => x.Email == usuarioLogueado.Email).FirstOrDefault();
                foreach (Hijo hijo in nuevopadre.Hijos) //buscar nota dentro de los arrays de notas de cada hijo
                {                                          
                    Nota nota1 = hijo.Notas.Where(x => x.Id == nota.Id).FirstOrDefault();
                    if (nota1 != null)
                    {
                        nota1.Comentarios.ToList().Add(nuevoComentario);
                        break;
                    }
                }
            }
            else
            {
                if (usuarioLogueado.RolSeleccionado == Roles.Docente) //ver alumnos de sus salas
                {
                    Docente docente = new Docente();
                }
                else
                {
                    if (usuarioLogueado.RolSeleccionado == Roles.Directora) //ver todos los alumnos de la institución
                    {

                    }
                }
            }          
            return new Resultado() { Errores = errores};
        }
    }
}
