using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositorio;

namespace WebApplication1.Controllers
{
    public class MovimientoInventarioController : Controller
    {
        private readonly IRepositorioMovimientoInventario repositorioMovimientoInventario;

        public MovimientoInventarioController(IRepositorioMovimientoInventario repositorioMovimientoInventario)
        {
            this.repositorioMovimientoInventario = repositorioMovimientoInventario;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var movimientosInventario = await repositorioMovimientoInventario.ObtenerMovimientosInventario();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("Listar", movimientosInventario);
            }

            return View(movimientosInventario);
        }

        [HttpPost]
        public async Task<ActionResult> ObtenerMovimientosInventarioConFiltro(string TIPO_DOCUMENTO, string NRO_DOCUMENTO, string PROVEEDOR)
        {
            var movimientosInventario = await repositorioMovimientoInventario.ObtenerMovimientosInventarioConFiltro(TIPO_DOCUMENTO, NRO_DOCUMENTO, PROVEEDOR);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("Listar", movimientosInventario);
            }

            return View(movimientosInventario);
        }

        [HttpPost]
        public async Task<JsonResult> ObtenerMovimientoInventarioPorId(string COD_CIA,
            string COMPANIA_VENTA_3,
            string ALMACEN_VENTA,
            string TIPO_MOVIMIENTO,
            string TIPO_DOCUMENTO,
            string NRO_DOCUMENTO,
            string COD_ITEM_2)
        {
            try
            {
                var movimientoInventario = await repositorioMovimientoInventario.ObtenerMovimientoInventarioPorId(COD_CIA, COMPANIA_VENTA_3, ALMACEN_VENTA,
                                                                                                   TIPO_MOVIMIENTO, TIPO_DOCUMENTO, NRO_DOCUMENTO, COD_ITEM_2);
                return Json(movimientoInventario);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error al obtener el Movimiento de inventario." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CrearMovimientoInventario([FromForm] MovimientoInventario movimientoInventario)
        {
            int? N_PARM_SAL = null;
            string C_PARM_SAL = string.Empty;

            try
            {
                var respuesta = await repositorioMovimientoInventario.CrearMovimientoInventario(movimientoInventario);
                N_PARM_SAL = respuesta.N_PARM_SAL;
                C_PARM_SAL = respuesta.C_PARM_SAL;
            }
            catch (Exception ex)
            {
                N_PARM_SAL = 0;
                C_PARM_SAL = ex.Message;
            }

            return Json(new { N_PARM_SAL, C_PARM_SAL });
        }



        [HttpPut]
        public async Task<IActionResult> ActualizarMovimientoInventario(string COD_CIA,
            string COMPANIA_VENTA_3,
            string ALMACEN_VENTA,
            string TIPO_MOVIMIENTO,
            string TIPO_DOCUMENTO,
            string NRO_DOCUMENTO,
            string COD_ITEM_2, string PROVEEDOR)
        {
            int? N_PARM_SAL = null;
            string C_PARM_SAL = string.Empty;

            try
            {
                var respuesta = await repositorioMovimientoInventario.ActualizarMovimientoInventario(COD_CIA, COMPANIA_VENTA_3, ALMACEN_VENTA, TIPO_MOVIMIENTO, TIPO_DOCUMENTO, NRO_DOCUMENTO, COD_ITEM_2, PROVEEDOR);
                N_PARM_SAL = respuesta.N_PARM_SAL;
                C_PARM_SAL = respuesta.C_PARM_SAL;
            }
            catch (Exception ex)
            {
                N_PARM_SAL = 0;
                C_PARM_SAL = ex.Message;
            }

            return Json(new { N_PARM_SAL, C_PARM_SAL });
        }
    }
}