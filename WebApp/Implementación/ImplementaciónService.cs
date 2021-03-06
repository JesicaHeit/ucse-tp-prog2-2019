﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contratos;
using Lógica;

namespace Implementación
{
    public class ImplementaciónService : IServicioWeb
    {
        public static LógicaGeneral Logica { get; set; }
        public ImplementaciónService()
        {
            Logica = new LógicaGeneral();
            Logica.CrearArchivos();
        }

        public Resultado AltaDirectora(Directora directora, UsuarioLogueado usuarioLogueado)
        {
            return Logica.AltaDirectora(directora,usuarioLogueado);       
        }

        public Resultado AltaDocente(Docente docente, UsuarioLogueado usuarioLogueado)
        {
            return Logica.AltaDocente(docente, usuarioLogueado);
        }

        public Resultado AltaAlumno(Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            return Logica.AltaAlumno(hijo, usuarioLogueado);
        }

        public Resultado AltaNota(Nota nota, Sala[] salas, Hijo[] hijos, UsuarioLogueado usuarioLogueado)
        {
            return Logica.AltaNota(nota, salas, hijos, usuarioLogueado);
        }

        public Resultado AltaPadreMadre(Padre padre, UsuarioLogueado usuarioLogueado)
        {
            return Logica.AltaPadreMadre(padre, usuarioLogueado);
        }

        public Resultado AsignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)
        {
            return Logica.AsignarDocenteSala(docente, sala, usuarioLogueado);
        }

        public Resultado AsignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            return Logica.AsignarHijoPadre(hijo, padre, usuarioLogueado);
        }

        public Resultado DesasignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)
        {
            return Logica.DesasignarDocenteSala(docente, sala, usuarioLogueado);
        }

        public Resultado DesasignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            return Logica.DesasignarHijoPadre(hijo, padre, usuarioLogueado);
        }

        public Resultado EditarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado)
        {
            return Logica.EditarDirectora(id, directora, usuarioLogueado);
        }

        public Resultado EditarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            return Logica.EditarAlumno(id, hijo, usuarioLogueado);
        }

        public Resultado EditarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado)
        {
            return Logica.EditarDocente(id, docente, usuarioLogueado);
        }

        public Resultado EditarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            return Logica.EditarPadreMadre(id, padre, usuarioLogueado);
        }

        public Resultado EliminarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado)
        {
            return Logica.EliminarDirectora(id, directora, usuarioLogueado);
        }

        public Resultado EliminarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado)
        {
            return Logica.EliminarDocente(id, docente, usuarioLogueado);
        }

        public Resultado EliminarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            return Logica.EliminarPadreMadre(id, padre, usuarioLogueado);
        }

        public Resultado EliminarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            return Logica.EliminarAlumno(id, hijo, usuarioLogueado);
        }

        public Resultado MarcarNotaComoLeida(Nota nota, UsuarioLogueado usuarioLogueado)
        {
            return Logica.MarcarNotaComoLeida(nota, usuarioLogueado);
        }

        public Hijo ObtenerAlumnoPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return Logica.ObtenerAlumnoPorId(usuarioLogueado, id);
        }

        public Grilla<Hijo> ObtenerAlumnos(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            return Logica.ObtenerAlumnos(usuarioLogueado, paginaActual, totalPorPagina, busquedaGlobal);
        }

        public Nota[] ObtenerCuadernoComunicaciones(int idPersona, UsuarioLogueado usuarioLogueado)
        {
            return Logica.ObtenerCuadernoComunicaciones(idPersona, usuarioLogueado);
        }

        public Directora ObtenerDirectoraPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return Logica.ObtenerDirectoraPorId(usuarioLogueado, id);
        }

        public Grilla<Directora> ObtenerDirectoras(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
              return Logica.ObtenerDirectoras(usuarioLogueado, paginaActual, totalPorPagina, busquedaGlobal);  
        }

        public Docente ObtenerDocentePorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return Logica.ObtenerDocentePorId(usuarioLogueado, id);
        }

        public Grilla<Docente> ObtenerDocentes(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            return Logica.ObtenerDocentes(usuarioLogueado, paginaActual, totalPorPagina, busquedaGlobal);
        }

        public Institucion[] ObtenerInstituciones()
        {
            return Logica.ObtenerInstituciones();
        }

        public string ObtenerNombreGrupo()
        {
            return $"Albrecht - Heit - Llull";
        }

        public Padre ObtenerPadrePorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return Logica.ObtenerPadrePorId(usuarioLogueado, id);
        }

        public Grilla<Padre> ObtenerPadres(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            return Logica.ObtenerPadres(usuarioLogueado, paginaActual, totalPorPagina, busquedaGlobal);
        }

        public Hijo[] ObtenerPersonas(UsuarioLogueado usuarioLogueado)
        {
            return Logica.ObtenerPersonas(usuarioLogueado);
        }

        public Sala[] ObtenerSalasPorInstitucion(UsuarioLogueado usuarioLogueado)
        {
            return Logica.ObtenerSalasPorInstitucion(usuarioLogueado);
        }

        public UsuarioLogueado ObtenerUsuario(string email, string clave)
        {
            return Logica.ObtenerUsuario(email, clave);
        }

        public Resultado ResponderNota(Nota nota, Comentario nuevoComentario, UsuarioLogueado usuarioLogueado)
        {
            return Logica.ResponderNota(nota, nuevoComentario, usuarioLogueado);
        }
    }
}
