using System.Collections.Generic;
using LiteDB;

// SRP: Solo gestiona persistencia de operaciones en LiteDB.
namespace Diseño_avanzado.Services
{
    public class RegistroOperacionesLiteDB : IRegistroOperaciones
{
    private readonly string _dbPath;
    public RegistroOperacionesLiteDB(string dbPath) => _dbPath = dbPath;

    public void RegistrarOperacion(OperacionRegistro registro)
    {
        using var db = new LiteDatabase(_dbPath);
        var col = db.GetCollection<OperacionRegistro>("operaciones");
        col.Insert(registro);
    }

    public List<OperacionRegistro> ObtenerHistorial(string usuarioId)
    {
        using var db = new LiteDatabase(_dbPath);
        var col = db.GetCollection<OperacionRegistro>("operaciones");

        return col.Find(x => x.UsuarioId == usuarioId).ToList();
    }
}
}
