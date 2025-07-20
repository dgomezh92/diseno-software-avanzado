using System.Collections.Generic;

// OCP, LSP, ISP: Abstracción precisa y extensible para operaciones matemáticas.
public interface IOperacion
{
    List<double> Operandos { get; }
    double Ejecutar();
    string TipoOperacion { get; }
}
