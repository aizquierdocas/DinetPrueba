using System;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class MovimientoInventario
    {
        [JsonPropertyName("COD_CIA")]
        public string COD_CIA { get; set; }

        [JsonPropertyName("COMPANIA_VENTA_3")]
        public string COMPANIA_VENTA_3 { get; set; }

        [JsonPropertyName("ALMACEN_VENTA")]
        public string ALMACEN_VENTA { get; set; }

        [JsonPropertyName("TIPO_MOVIMIENTO")]
        public string TIPO_MOVIMIENTO { get; set; }

        [JsonPropertyName("TIPO_DOCUMENTO")]
        public string TIPO_DOCUMENTO { get; set; }

        [JsonPropertyName("NRO_DOCUMENTO")]
        public string NRO_DOCUMENTO { get; set; }

        [JsonPropertyName("COD_ITEM_2")]
        public string COD_ITEM_2 { get; set; }

        [JsonPropertyName("PROVEEDOR")]
        public string PROVEEDOR { get; set; }
    }
}