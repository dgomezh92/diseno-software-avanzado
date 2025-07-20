using System.Collections.Generic;
using System.Linq;

// SRP: Solo ejecuta multiplicaciones. LSP: Puede usarse como IOperacion.
public class Multiplicacion : IOperacion
{
    public List<double> Operandos { get; }
    public string TipoOperacion => "Multiplicacion";
    public Multiplicacion(List<double> operandos) => Operandos = operandos;
    public double Ejecutar() => Operandos.Aggregate((a, b) => a * b);
}
