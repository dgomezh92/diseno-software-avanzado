using System.Collections.Generic;
using System.Linq;

// SRP: Solo ejecuta restas. LSP: Puede usarse como IOperacion.
public class Resta : IOperacion
{
    public List<double> Operandos { get; }
    public string TipoOperacion => "Resta";
    public Resta(List<double> operandos) => Operandos = operandos;
    public double Ejecutar() => Operandos.Aggregate((a, b) => a - b);
}
