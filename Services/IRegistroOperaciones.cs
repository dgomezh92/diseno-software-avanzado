using System.Collections.Generic;

// DIP: Abstracción para el registro de operaciones.
public interface IRegistroOperaciones
{
    void RegistrarOperacion(OperacionRegistro registro);
    List<OperacionRegistro> ObtenerHistorial(string usuarioId);
}
