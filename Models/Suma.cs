using System.Collections.Generic;
using System.Linq;

// SRP: Solo ejecuta sumas. LSP: Puede usarse como IOperacion.
public class Suma : IOperacion
{
    public List<double> Operandos { get; }
    public string TipoOperacion => "Suma";
    public Suma(List<double> operandos) => Operandos = operandos;
    public double Ejecutar() => Operandos.Sum();
}
