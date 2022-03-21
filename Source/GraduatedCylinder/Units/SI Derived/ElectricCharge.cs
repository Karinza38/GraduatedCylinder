﻿namespace GraduatedCylinder;

public partial struct ElectricCharge : IDimension<ElectricCharge, ElectricChargeUnit>
{

    public static ElectricCharge ElementaryCharge { get; } = new ElectricCharge(1.602_176_634e-19, ElectricChargeUnit.Coulomb);

}