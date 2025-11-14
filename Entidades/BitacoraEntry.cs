using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Entidades
{
    public class BitacoraEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Usuario { get; set; } = "";

        public string Modulo { get; set; } = "";

        public string Accion { get; set; } = "";

        public string? IpCliente { get; set; }

        // ✅ Necesario para MongoBitacora
        public string? Extra { get; set; }
    }
}
