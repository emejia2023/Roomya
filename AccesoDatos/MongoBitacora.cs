using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Entidades;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace AccesoDatos
{
    public class MongoBitacora : IBitacoraService
    {
        private readonly IMongoCollection<BitacoraEntry> _col;

        public MongoBitacora(IConfiguration cfg)
        {
            var cs = cfg["Mongo:Url"] ?? "mongodb://localhost:27017";
            var cli = new MongoClient(cs);
            var dbName = cfg["Mongo:Database"] ?? "roomya";
            var db = cli.GetDatabase(dbName);
            _col = db.GetCollection<BitacoraEntry>("bitacora");
        }



        // Mantiene compatibilidad con la versión anterior
        public Task InsertAsync(BitacoraEntry e) => _col.InsertOneAsync(e);

        public Task<List<BitacoraEntry>> ListAsync()
            => _col.Find(_ => true)
                   .SortByDescending(x => x.Fecha)
                   .Limit(200)
                   .ToListAsync();

        // Nuevo método que usará la capa de negocio
        public async Task RegistrarAsync(string modulo, string accion, string usuario, string? ipCliente = null, object? extra = null)
        {
            var entry = new BitacoraEntry
            {
                Fecha = System.DateTime.UtcNow,
                Usuario = usuario ?? "sistema",
                Modulo = modulo,
                Accion = accion,
                IpCliente = ipCliente,
                Extra = extra != null ? extra.ToString() : null
            };

            await _col.InsertOneAsync(entry);
        }
    }
}

