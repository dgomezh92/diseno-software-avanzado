using System;
using System.Collections.Generic;

// SRP: Representa el registro de una operación matemática realizada por un usuario.
public class OperacionRegistro
{
    public Guid Id { get; set; }
    public string TipoOperacion { get; set; }
    public List<double> Operandos { get; set; }
    public double Resultado { get; set; }
    public DateTime Fecha { get; set; }
    public string UsuarioId { get; set; }
}
