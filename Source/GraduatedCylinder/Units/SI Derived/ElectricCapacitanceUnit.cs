﻿using GraduatedCylinder.Abbreviations;
using GraduatedCylinder.Scales;

namespace GraduatedCylinder;

public enum ElectricCapacitanceUnit : short
{

    Unspecified = short.MinValue,

    BaseUnit = Farad,

    [UnitAbbreviation("pF")]
    [Scale(1e-12)]
    PicoFarad = -12,

    [UnitAbbreviation("nF")]
    [Scale(1e-9)]
    NanoFarad = -9,

    [UnitAbbreviation("µF")]
    [Scale(1e-6)]
    MicroFarad = -6,

    [UnitAbbreviation("mF")]
    [Scale(1e-3)]
    MilliFarad = -3,

    [UnitAbbreviation("F")]
    [Scale(1.0)]
    Farad = 0,

    [UnitAbbreviation("kF")]
    [Scale(1e3)]
    KiloFarad = 3,

    [UnitAbbreviation("MF")]
    [Scale(1e6)]
    MegaFarad = 6

}