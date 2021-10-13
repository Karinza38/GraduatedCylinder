using GraduatedCylinder.Scales;

namespace GraduatedCylinder.Units
{
    public enum ElectricResistanceUnit : short
    {

        Unspecified = short.MinValue,

        BaseUnit = Ohm,

        [UnitAbbreviation("yΩ")]
        [Scale(1e-24f)]
        Yoctoohm = -24,

        [UnitAbbreviation("zΩ")]
        [Scale(1e-21f)]
        Zeptoohm = -21,

        [UnitAbbreviation("aΩ")]
        [Scale(1e-18f)]
        Attoohm = -18,

        [UnitAbbreviation("fΩ")]
        [Scale(1e-15f)]
        Femtoohm = -15,

        [UnitAbbreviation("pΩ")]
        [Scale(1e-12f)]
        Picoohm = -12,

        [UnitAbbreviation("nΩ")]
        [Scale(1e-9f)]
        Nanoohm = -9,

        [UnitAbbreviation("µΩ")]
        [Scale(1e-6f)]
        Microohm = -6,

        [UnitAbbreviation("mΩ")]
        [Scale(1e-3f)]
        Milliohm = -3,

        [UnitAbbreviation("cΩ")]
        [Scale(1e-2f)]
        Centiohm = -2,

        [UnitAbbreviation("dΩ")]
        [Scale(1e-1f)]
        Deciohm = -1,

        [UnitAbbreviation("Ω")]
        [Scale(1.0f)]
        Ohm = 0,

        [UnitAbbreviation("daΩ")]
        [Scale(10f)]
        Dekaohm = 1,

        [UnitAbbreviation("hΩ")]
        [Scale(1e2f)]
        Hectoohm = 2,

        [UnitAbbreviation("kΩ")]
        [Scale(1e3f)]
        Kiloohm = 3,

        [UnitAbbreviation("MΩ")]
        [Scale(1e6f)]
        Megaogm = 6,

        [UnitAbbreviation("GΩ")]
        [Scale(1e9f)]
        Gigaogm = 9,

        [UnitAbbreviation("TΩ")]
        [Scale(1e12f)]
        Teraohm = 12,

        [UnitAbbreviation("PΩ")]
        [Scale(1e15f)]
        Petaohm = 15,

        [UnitAbbreviation("EΩ")]
        [Scale(1e18f)]
        Exaohm = 18,

        [UnitAbbreviation("ZΩ")]
        [Scale(1e21f)]
        Zettaohm = 21,

        [UnitAbbreviation("YΩ")]
        [Scale(1e24f)]
        Yottaohm = 24

    }
}