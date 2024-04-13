using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Repositorio
{
    public interface IRepositorioMovimientoInventario
    {
        Task<List<MovimientoInventario>> ObtenerMovimientosInventario();
        Task<MovimientoInventario> ObtenerMovimientoInventarioPorId(string COD_CIA, string COMPANIA_VENTA_3, string ALMACEN_VENTA, string TIPO_MOVIMIENTO, string TIPO_DOCUMENTO, string NRO_DOCUMENTO, string COD_ITEM_2);
        Task<List<MovimientoInventario>> ObtenerMovimientosInventarioConFiltro(string TIPO_DOCUMENTO, string NRO_DOCUMENTO, string PROVEEDOR);
        Task<Response> CrearMovimientoInventario(MovimientoInventario movimientoInventario);
        Task<Response> ActualizarMovimientoInventario(string COD_CIA, string COMPANIA_VENTA_3, string ALMACEN_VENTA, string TIPO_MOVIMIENTO, string TIPO_DOCUMENTO, string NRO_DOCUMENTO, string COD_ITEM_2, string PROVEEDOR);
        
    }

    public class RepositorioMovimientoInventario : IRepositorioMovimientoInventario
    {

        private readonly string connectionString = "";

        public RepositorioMovimientoInventario(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<List<MovimientoInventario>> ObtenerMovimientosInventario()
        {
            List<MovimientoInventario> lista = new List<MovimientoInventario>();

            var conexion = new SqlConnection(connectionString);

            if (conexion.State != ConnectionState.Open) { await conexion.OpenAsync(); }

            SqlDataReader reader = null;

            try
            {
                SqlCommand cmd = new SqlCommand("ConsultarMovimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    lista.Add(new MovimientoInventario
                    {
                        COD_CIA = reader["COD_CIA"].ToString(),
                        COMPANIA_VENTA_3 = reader["COMPANIA_VENTA_3"].ToString(),
                        ALMACEN_VENTA = reader["ALMACEN_VENTA"].ToString(),
                        TIPO_MOVIMIENTO = reader["TIPO_MOVIMIENTO"].ToString(),
                        TIPO_DOCUMENTO = reader["TIPO_DOCUMENTO"].ToString(),
                        NRO_DOCUMENTO = reader["NRO_DOCUMENTO"].ToString(),
                        COD_ITEM_2 = reader["COD_ITEM_2"].ToString(),
                        PROVEEDOR = reader["PROVEEDOR"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    if (!reader.IsClosed) { reader.Close(); }
                }
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed) { conexion.Close(); }
                conexion.Dispose();
            }

            return lista;
        }

        [HttpPost]
        public async Task<MovimientoInventario> ObtenerMovimientoInventarioPorId(string COD_CIA, string COMPANIA_VENTA_3, string ALMACEN_VENTA, string TIPO_MOVIMIENTO, string TIPO_DOCUMENTO, string NRO_DOCUMENTO, string COD_ITEM_2)
        {
            var movimientoInventario = new MovimientoInventario();

            var conexion = new SqlConnection(connectionString);

            if (conexion.State != ConnectionState.Open) { await conexion.OpenAsync(); }

            SqlDataReader reader = null;

            try
            {
                SqlCommand cmd = new SqlCommand("BuscarMovimientoPorID", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@COD_CIA", SqlDbType.VarChar, 50).Value = COD_CIA;
                cmd.Parameters.Add("@COMPANIA_VENTA_3", SqlDbType.VarChar, 50).Value = COMPANIA_VENTA_3;
                cmd.Parameters.Add("@ALMACEN_VENTA", SqlDbType.VarChar, 50).Value = ALMACEN_VENTA;
                cmd.Parameters.Add("@TIPO_MOVIMIENTO", SqlDbType.VarChar, 50).Value = TIPO_MOVIMIENTO;
                cmd.Parameters.Add("@TIPO_DOCUMENTO", SqlDbType.VarChar, 50).Value = TIPO_DOCUMENTO;
                cmd.Parameters.Add("@NRO_DOCUMENTO", SqlDbType.VarChar, 50).Value = NRO_DOCUMENTO;
                cmd.Parameters.Add("@COD_ITEM_2", SqlDbType.VarChar, 50).Value = COD_ITEM_2;

                reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    movimientoInventario.COD_CIA = reader["COD_CIA"].ToString();
                    movimientoInventario.COMPANIA_VENTA_3 = reader["COMPANIA_VENTA_3"].ToString();
                    movimientoInventario.ALMACEN_VENTA = reader["ALMACEN_VENTA"].ToString();
                    movimientoInventario.TIPO_MOVIMIENTO = reader["TIPO_MOVIMIENTO"].ToString();
                    movimientoInventario.TIPO_DOCUMENTO = reader["TIPO_DOCUMENTO"].ToString();
                    movimientoInventario.NRO_DOCUMENTO = reader["NRO_DOCUMENTO"].ToString();
                    movimientoInventario.COD_ITEM_2 = reader["COD_ITEM_2"].ToString();
                    movimientoInventario.PROVEEDOR = reader["PROVEEDOR"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    if (!reader.IsClosed) { reader.Close(); }
                }
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed) { conexion.Close(); }
                conexion.Dispose();
            }

            return movimientoInventario;
        }

        [HttpPost]
        public async Task<List<MovimientoInventario>> ObtenerMovimientosInventarioConFiltro(string TIPO_DOCUMENTO, string NRO_DOCUMENTO, string PROVEEDOR)
        {
            // Validar y asignar cadenas vacías si los parámetros son nulos
            TIPO_DOCUMENTO ??= "";
            NRO_DOCUMENTO ??= "";
            PROVEEDOR ??= "";

            List<MovimientoInventario> lista = new List<MovimientoInventario>();

            var conexion = new SqlConnection(connectionString);

            if (conexion.State != ConnectionState.Open) { await conexion.OpenAsync(); }

            SqlDataReader reader = null;

            try
            {
                SqlCommand cmd = new SqlCommand("ConsultarMovimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 50).Value = TIPO_DOCUMENTO;
                cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 50).Value = TIPO_DOCUMENTO;
                cmd.Parameters.Add("@TipoMovimiento", SqlDbType.VarChar, 50).Value = TIPO_DOCUMENTO;
                cmd.Parameters.Add("@NroDocumento", SqlDbType.VarChar, 50).Value = NRO_DOCUMENTO;

                reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    lista.Add(new MovimientoInventario
                    {
                        COD_CIA = reader["COD_CIA"].ToString(),
                        COMPANIA_VENTA_3 = reader["COMPANIA_VENTA_3"].ToString(),
                        ALMACEN_VENTA = reader["ALMACEN_VENTA"].ToString(),
                        TIPO_MOVIMIENTO = reader["TIPO_MOVIMIENTO"].ToString(),
                        TIPO_DOCUMENTO = reader["TIPO_DOCUMENTO"].ToString(),
                        NRO_DOCUMENTO = reader["NRO_DOCUMENTO"].ToString(),
                        COD_ITEM_2 = reader["COD_ITEM_2"].ToString(),
                        PROVEEDOR = reader["PROVEEDOR"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    if (!reader.IsClosed) { reader.Close(); }
                }
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed) { conexion.Close(); }
                conexion.Dispose();
            }

            return lista;
        }

        [HttpPost]
        public async Task<Response> CrearMovimientoInventario(MovimientoInventario movimientoInventario)
        {
            var output = new Response();

            var conexion = new SqlConnection(connectionString);

            if (conexion.State != ConnectionState.Open) { await conexion.OpenAsync(); }

            SqlDataReader reader = null;

            try
            {
                SqlParameter N_PARM_SAL = new SqlParameter("@N_PARM_SAL", SqlDbType.Int);
                N_PARM_SAL.Direction = ParameterDirection.Output;

                SqlParameter C_PARM_SAL = new SqlParameter("@C_PARM_SAL", SqlDbType.VarChar, 250);
                C_PARM_SAL.Direction = ParameterDirection.Output;

                SqlCommand cmd = new SqlCommand("InsertarMovimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@COD_CIA", SqlDbType.VarChar, 50).Value = movimientoInventario.COD_CIA;
                cmd.Parameters.Add("@COMPANIA_VENTA_3", SqlDbType.VarChar, 50).Value = movimientoInventario.COMPANIA_VENTA_3;
                cmd.Parameters.Add("@ALMACEN_VENTA", SqlDbType.VarChar, 50).Value = movimientoInventario.ALMACEN_VENTA;
                cmd.Parameters.Add("@TIPO_MOVIMIENTO", SqlDbType.VarChar, 50).Value = movimientoInventario.TIPO_MOVIMIENTO;
                cmd.Parameters.Add("@TIPO_DOCUMENTO", SqlDbType.VarChar, 50).Value = movimientoInventario.TIPO_DOCUMENTO;
                cmd.Parameters.Add("@NRO_DOCUMENTO", SqlDbType.VarChar, 50).Value = movimientoInventario.NRO_DOCUMENTO;
                cmd.Parameters.Add("@COD_ITEM_2", SqlDbType.VarChar, 50).Value = movimientoInventario.COD_ITEM_2;
                cmd.Parameters.Add(N_PARM_SAL);
                cmd.Parameters.Add(C_PARM_SAL);

                await cmd.ExecuteNonQueryAsync();

                output.N_PARM_SAL = Convert.ToInt32(N_PARM_SAL.Value.ToString());
                output.C_PARM_SAL = C_PARM_SAL.Value.ToString();

            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    if (!reader.IsClosed) { reader.Close(); }
                }

                output.N_PARM_SAL = 0;
                output.C_PARM_SAL = "Ocurrio un error " + ex.Message;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed) { conexion.Close(); }
                conexion.Dispose();
            }

            return output;
        }

        [HttpPut]
        public async Task<Response> ActualizarMovimientoInventario(string COD_CIA,
            string COMPANIA_VENTA_3,
            string ALMACEN_VENTA,
            string TIPO_MOVIMIENTO,
            string TIPO_DOCUMENTO,
            string NRO_DOCUMENTO,
            string COD_ITEM_2, string PROVEEDOR)
        {
            var output = new Response();

            var conexion = new SqlConnection(connectionString);

            if (conexion.State != ConnectionState.Open) { await conexion.OpenAsync(); }

            SqlDataReader reader = null;

            try
            {
                SqlParameter N_PARM_SAL = new SqlParameter("@N_PARM_SAL", SqlDbType.Int);
                N_PARM_SAL.Direction = ParameterDirection.Output;

                SqlParameter C_PARM_SAL = new SqlParameter("@C_PARM_SAL", SqlDbType.VarChar, 250);
                C_PARM_SAL.Direction = ParameterDirection.Output;

                SqlCommand cmd = new SqlCommand("ActualizarMovimiento", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@COD_CIA", SqlDbType.VarChar, 50).Value = COD_CIA;
                cmd.Parameters.Add("@COMPANIA_VENTA_3", SqlDbType.VarChar, 50).Value = COMPANIA_VENTA_3;
                cmd.Parameters.Add("@ALMACEN_VENTA", SqlDbType.VarChar, 50).Value = ALMACEN_VENTA;
                cmd.Parameters.Add("@TIPO_MOVIMIENTO", SqlDbType.VarChar, 50).Value = TIPO_MOVIMIENTO;
                cmd.Parameters.Add("@TIPO_DOCUMENTO", SqlDbType.VarChar, 50).Value = TIPO_DOCUMENTO;
                cmd.Parameters.Add("@NRO_DOCUMENTO", SqlDbType.VarChar, 50).Value = NRO_DOCUMENTO;
                cmd.Parameters.Add("@COD_ITEM_2", SqlDbType.VarChar, 50).Value = COD_ITEM_2;
                cmd.Parameters.Add("@PROVEEDOR", SqlDbType.VarChar, 50).Value = PROVEEDOR;
                cmd.Parameters.Add(N_PARM_SAL);
                cmd.Parameters.Add(C_PARM_SAL);

                await cmd.ExecuteNonQueryAsync();

                output.N_PARM_SAL = Convert.ToInt32(N_PARM_SAL.Value.ToString());
                output.C_PARM_SAL = C_PARM_SAL.Value.ToString();

            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    if (!reader.IsClosed) { reader.Close(); }
                }

                output.N_PARM_SAL = 0;
                output.C_PARM_SAL = "Ocurrio un error " + ex.Message;
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed) { conexion.Close(); }
                conexion.Dispose();
            }

            return output;
        }

        
    }
}