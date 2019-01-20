using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wpena.lucasian.Models;

namespace wpena.lucasian.Services
{
    /// <summary>
    /// clase de almacenamiento de bancos en memoria.
    /// </summary>
    public static class BancoRepositorio
    {        
        static List<BancoCuentas> bancos = new List<BancoCuentas>();
        /// <summary>
        /// Listado de bancos en memoria
        /// </summary>
        public static List<BancoCuentas> Bancos { get => bancos; }

        /// <summary>
        /// Realiza la creación,actualización y eliminación para el banco
        /// </summary>
        /// <param name="banco">Banco a operar</param>
        /// <param name="Accion">Acción[I: creación,U: actualización:, D: eliminación]</param>
        /// <param name="id">id del banco</param>
        public static void confirmarBanco(Banco banco, string Accion = "I", int? id = null)
        {
            switch (Accion)
            {
                case "U":
                    if (bancos.Any(x => x.banco.id == (id ?? banco.id)))
                    {
                        BancoCuentas bancoCuenta = bancos.Find(x => x.banco.id == (id ?? banco.id));
                        bancoCuenta.banco = banco;
                    }
                    else
                    {
                        throw new Exception("El banco no existe");
                    }
                    break;
                case "D":
                    if (bancos.Any(x => x.banco.id == (id ?? banco.id)))
                    {
                        BancoCuentas bancoCuenta = bancos.Find(x => x.banco.id == (id ?? banco.id));
                        if (bancoCuenta.cuentas.Any())
                        {
                            throw new Exception("El banco contiene cuentas");
                        }
                        bancos.Remove(bancoCuenta);
                    }
                    else
                    {
                        throw new Exception("El banco no existe");
                    }
                    break;
                default:
                    Random rand = new Random();
                    do
                    {
                        id = rand.Next(1, int.MaxValue);
                    } while (bancos.Any(x => x.banco.id == id.Value));

                    banco.id = id.Value;
                    bancos.Add(new BancoCuentas
                    {
                        banco = banco,
                        cuentas = new List<Cuenta>()
                    });
                    break;
            }
        }


        /// <summary>
        /// Realiza la creación,actualización y eliminación para la cuenta según el banco
        /// </summary>
        /// <param name="id_banco">id del banco</param>
        /// <param name="banco">cuenta a operar</param>
        /// <param name="Accion">Acción[I: creación,U: actualización:, D: eliminación]</param>
        /// <param name="id">id de la cuenta</param>
        public static void confirmarCuenta(int id_banco, Cuenta Cuenta, string Accion = "I", int? id = null)
        {
            if (bancos.Any(x => x.banco.id == id_banco))
            {
                BancoCuentas bancoCuenta = bancos.Find(x => x.banco.id == id_banco);
                switch (Accion)
                {
                    case "U":
                        if (bancoCuenta.cuentas.Any(x => x.id == (id ?? Cuenta.id)))
                        {
                            bancoCuenta.cuentas.RemoveAll(x => x.id == (id ?? Cuenta.id));
                            bancoCuenta.cuentas.Add(Cuenta);
                        }
                        else
                        {
                            throw new Exception("la cuenta no esta asociada al banco");
                        }
                        break;
                    case "D":
                        if (bancoCuenta.cuentas.Any(x => x.id == (id ?? Cuenta.id)))
                        {
                            bancoCuenta.cuentas.RemoveAll(x => x.id == (id ?? Cuenta.id));
                        }
                        else
                        {
                            throw new Exception("la cuenta no esta asociada al banco");
                        }
                        break;
                    default:
                        Random rand = new Random();
                        do
                        {
                            id = rand.Next(1, int.MaxValue);
                        } while (bancoCuenta.cuentas.Any(x => x.id == id.Value));

                        Cuenta.id = id.Value;
                        bancoCuenta.cuentas.Add(Cuenta);
                        break;
                }

            }
            else
            {
                throw new Exception("El banco no existe");
            }
            
        }
    }
}