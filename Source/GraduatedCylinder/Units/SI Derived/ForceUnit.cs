using GraduatedCylinder.Abbreviations;
using GraduatedCylinder.Extensions;
using GraduatedCylinder.Scales;

namespace GraduatedCylinder;

public enum ForceUnit : short
{

    Unspecified = short.MinValue,

    BaseUnit = Newtons,

    [UnitAbbreviation("N")]
    [Scale(1.0)]
    [Extension("Newtons")]
    Newtons = 0,

    [UnitAbbreviation("lbf")]
    [Scale(4.44822)]
    PoundForce = 1,

    [UnitAbbreviation("kgf")]
    [Scale(9.81)]
    KiloGramForce = 2

}