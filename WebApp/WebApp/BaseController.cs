using Contratos;
using Lógica;
using Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp
{
    public class BaseController : Controller
    {
        //protected static IServicioWeb servicio = new MockService();

        protected static IServicioWeb servicio = new LógicaGeneral();
    }
}