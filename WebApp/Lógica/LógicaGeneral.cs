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
        private Institucion[] Instituciones { get; set; }
        private Directora[] Directores { get; set; }
        private Docente[] Docentes { get; set; }
        private Padre[] Padres { get; set; }
        private Hijo[] Alumnos { get; set; }
        private Nota[] Notas { get; set; }
        private Sala[] Salas { get; set; }

        public const string pathInstituciones = @"C:\Archivos\Instituciones.txt";
        public const string pathDirectores = @"C:\Archivos\Directores.txt";
        public const string pathDocentes = @"C:\Archivos\Docentes.txt";
        public const string pathPadres = @"C:\Archivos\Padres.txt";
        public const string pathAlumnos = @"C:\Archivos\Alumnos.txt";
        public const string pathNotas = @"C:\Archivos\Notas.txt";
        public const string pathSalas = @"C:\Archivos\Salas.txt";

        public LógicaGeneral()
        {
            Instituciones = new Institucion[0];
            Directores = new Directora[0];
            Docentes = new Docente[0];
            Padres = new Padre[0];
            Alumnos = new Hijo[0];
            Notas = new Nota[0];
            Salas = new Sala[0];
        }

        //CREACIÓN DE ARCHIVOS  
        public void CrearArchivos()
        {
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
        }


        //DESERIALIZACION DE ARCHIVOS
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




        //PONER NOMBRE AL GRUPO
        public string ObtenerNombreGrupo()
        {
            return $"Albrecht - Heit - Llull";
        }

        public UsuarioLogueado ObtenerUsuario (string email, string clave)
        {
            //VER SI LA CONTRASEÑA ESTÁ GUARDADA
            throw new NotImplementedException();
        }

        public Institucion[] ObtenerInstituciones()
        {
            throw new NotImplementedException();
        }

        //ABM DIRECTORA

        public Resultado AltaDirectora(Directora directora, UsuarioLogueado usuarioLogueado)
        {
            
        }

        public Resultado EditarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado EliminarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Sala[] ObtenerSalasPorInstitucion(UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        //ABM DOCENTES

        public Resultado AltaDocente(Docente docente, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado EditarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado EliminarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        //ABM PADRES MADRES

        public Resultado AltaPadreMadre(Padre padre, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado EditarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado EliminarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado AsignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado DesasignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado AsignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado DesasignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Hijo[] ObtenerPersonas(UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Nota[] ObtenerCuadernoComunicaciones(int idPersona, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado AltaNota(Nota nota, Sala[] salas, Hijo[] hijos, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado ResponderNota(Nota nota, Comentario nuevoComentario, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Resultado MarcarNotaComoLeida(Nota nota, UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Grilla<Directora> ObtenerDirectoras(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            throw new NotImplementedException();
        }

        public Grilla<Docente> ObtenerDocentes(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            throw new NotImplementedException();
        }

        public Grilla<Padre> ObtenerPadres(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            throw new NotImplementedException();
        }

        public Grilla<Hijo> ObtenerAlumnos(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            throw new NotImplementedException();
        }

    }
}
