using System.Collections.Generic;
using System.Linq;

// SRP: Solo ejecuta divisiones. LSP: Puede usarse como IOperacion.
public class Division : IOperacion
{
    public List<double> Operandos { get; }
    public string TipoOperacion => "Division";
    public Division(List<double> operandos) => Operandos = operandos;
    public double Ejecutar() => Operandos.Aggregate((a, b) => a / b);
}
