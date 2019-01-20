using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using wpena.lucasian.Models;
using wpena.lucasian.Services;

namespace wpena.lucasian.Controllers
{
    [RoutePrefix("api/banco")]
    public class BancoCuentasController : ApiController
    {
        // GET: api/Banco
        [HttpGet]
        public IEnumerable<Banco> obtenerBancos()
        {
            return BancoRepositorio.Bancos.Select(x => x.banco);
        }

        // GET: api/Banco/5
        [HttpGet]
        public Banco obtenerBancos(int id)
        {
            if (BancoRepositorio.Bancos.Any(x => x.banco != null && x.banco.id == id))
            {
                return BancoRepositorio.Bancos.Find(x => x.banco != null && x.banco.id == id).banco;
            }
            else
            {
                return null;
            }
        }

        // POST: api/Banco
        [HttpPost]
        public ApiRespuesta crearBanco([FromBody]string Nombre)
        {
            try
            {
                Banco banco = new Banco() { nombre = Nombre };
                BancoRepositorio.confirmarBanco(banco);
                return new ApiRespuesta()
                {
                    status = "ok",
                    message = "registro ok",
                    id = banco.id
                };
            }
            catch (Exception ex)
            {
                return new ApiRespuesta()
                {
                    status = "error",
                    message = ex.Message
                };
            }
        }

        // PUT: api/Banco/5
        [HttpPut]
        public ApiRespuesta actualizarBanco(int id, [FromBody]Banco Banco)
        {
            try
            {
                BancoRepositorio.confirmarBanco(Banco, "U", id);
                return new ApiRespuesta()
                {
                    status = "ok",
                    message = "registro ok",
                    id = Banco.id
                };
            }
            catch (Exception ex)
            {
                return new ApiRespuesta()
                {
                    status = "error",
                    message = ex.Message
                };
            }
        }

        // DELETE: api/Banco/5
        [HttpDelete]
        public ApiRespuesta eliminarBanco(int id)
        {
            try
            {
                Banco banco = new Banco();
                BancoRepositorio.confirmarBanco(banco, "D", id);
                return new ApiRespuesta()
                {
                    status = "ok",
                    message = "registro ok",
                    id = banco.id
                };
            }
            catch (Exception ex)
            {
                return new ApiRespuesta()
                {
                    status = "error",
                    message = ex.Message
                };
            }
        }

        // GET: api/Banco/cuenta
        [Route("{id_banco:int}/cuenta")]
        [HttpGet]
        public IEnumerable<Cuenta> obtenerCuentas(int id_banco)
        {
            if (BancoRepositorio.Bancos.Any(x => x.banco.id == id_banco))
            {
                return BancoRepositorio.Bancos.Find(x => x.banco.id == id_banco).cuentas;
            }
            else
            {
                return new List<Cuenta>();
            }
        }

        // GET: api/Banco/5/cuenta/3
        [Route("{id_banco:int}/cuenta/{id:int}")]
        [HttpGet]
        public Cuenta obtenerCuenta(int id_banco, int id)
        {
            if (BancoRepositorio.Bancos.Any(x => x.banco.id == id_banco))
            {
                return BancoRepositorio.Bancos.Find(x => x.banco.id == id_banco).cuentas.Find(x => x.id == id) ?? new Cuenta();
            }
            else
            {
                return null;
            }
        }

        // POST: api/Banco/cuenta
        [Route("{id_banco:int}/cuenta")]
        [HttpPost]
        public ApiRespuesta crearCuenta(int id_banco, [FromBody]string Nombre)
        {
            try
            {
                Cuenta cuenta = new Cuenta() { nombre = Nombre };
                BancoRepositorio.confirmarCuenta(id_banco, cuenta);
                return new ApiRespuesta()
                {
                    status = "ok",
                    message = "registro ok",
                    id = cuenta.id
                };
            }
            catch (Exception ex)
            {
                return new ApiRespuesta()
                {
                    status = "error",
                    message = ex.Message
                };
            }
        }

        // PUT: api/Banco/5/cuenta/3
        [Route("{id_banco:int}/cuenta/{id:int}")]
        [HttpPut]
        public ApiRespuesta actualizarCuenta(int id_banco, int id, [FromBody]Cuenta cuenta)
        {
            try
            {
                BancoRepositorio.confirmarCuenta(id_banco, cuenta, "U", id);
                return new ApiRespuesta()
                {
                    status = "ok",
                    message = "registro ok",
                    id = cuenta.id
                };
            }
            catch (Exception ex)
            {
                return new ApiRespuesta()
                {
                    status = "error",
                    message = ex.Message
                };
            }
        }

        // DELETE: api/Banco/5/cuenta/3
        [Route("{id_banco:int}/cuenta/{id:int}")]
        [HttpDelete]
        public ApiRespuesta eliminarCuenta(int id_banco, int id)
        {
            try
            {
                Cuenta cuenta = new Cuenta();
                BancoRepositorio.confirmarCuenta(id_banco, cuenta, "D", id);
                return new ApiRespuesta()
                {
                    status = "ok",
                    message = "registro ok",
                    id = cuenta.id
                };
            }
            catch (Exception ex)
            {
                return new ApiRespuesta()
                {
                    status = "error",
                    message = ex.Message
                };
            }
        }

        /// <summary>
        /// clase de respuesta
        /// </summary>
        public class ApiRespuesta
        {
            public string status { get; set; }
            public string message { get; set; }
            public int? id { get; set; }
        }
    }
}
