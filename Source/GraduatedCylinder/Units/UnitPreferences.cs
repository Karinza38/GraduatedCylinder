﻿namespace GraduatedCylinder;

public partial class UnitPreferences
{

    public int Precision { get; set; } = 2;

    public static UnitPreferences GetAmericanEnglishUnits() {
        return new UnitPreferences {
            AccelerationUnit = AccelerationUnit.MilePerHourPerSecond,
            AngleUnit = AngleUnit.Degree,
            AreaUnit = AreaUnit.SquareMiles,
            EnergyUnit = EnergyUnit.KiloCalories,
            ForceUnit = ForceUnit.PoundForce,
            JerkUnit = JerkUnit.MilesPerSecondCubed,
            LengthUnit = LengthUnit.Mile,
            MassUnit = MassUnit.Pounds,
            MassDensityUnit = MassDensityUnit.PoundsPerCubicFeet,
            MassFlowRateUnit = MassFlowRateUnit.PoundsPerSecond,
            MomentumUnit = MomentumUnit.PoundsMilesPerHour,
            PowerUnit = PowerUnit.Horsepower,
            PressureUnit = PressureUnit.PoundsPerSquareInch,
            SpeedUnit = SpeedUnit.MilesPerHour,
            TemperatureUnit = TemperatureUnit.Fahrenheit,
            TimeUnit = TimeUnit.Hours,
            TorqueUnit = TorqueUnit.FootPounds,
            VolumeUnit = VolumeUnit.GallonsUSLiquid,
            VolumetricFlowRateUnit = VolumetricFlowRateUnit.GallonsUsPerHour
        };
    }

    public static UnitPreferences GetBritishEnglishUnits() {
        return new UnitPreferences {
            AccelerationUnit = AccelerationUnit.MilePerHourPerSecond,
            AngleUnit = AngleUnit.Degree,
            AreaUnit = AreaUnit.SquareMiles,
            EnergyUnit = EnergyUnit.KiloCalories,
            ForceUnit = ForceUnit.PoundForce,
            JerkUnit = JerkUnit.MilesPerSecondCubed,
            LengthUnit = LengthUnit.Mile,
            MassUnit = MassUnit.Pounds,
            MassDensityUnit = MassDensityUnit.PoundsPerCubicFeet,
            MassFlowRateUnit = MassFlowRateUnit.PoundsPerSecond,
            MomentumUnit = MomentumUnit.PoundsMilesPerHour,
            PowerUnit = PowerUnit.Horsepower,
            PressureUnit = PressureUnit.PoundsPerSquareInch,
            SpeedUnit = SpeedUnit.MilesPerHour,
            TemperatureUnit = TemperatureUnit.Fahrenheit,
            TorqueUnit = TorqueUnit.FootPounds,
            VolumeUnit = VolumeUnit.GallonsUK,
            VolumetricFlowRateUnit = VolumetricFlowRateUnit.GallonsUsPerSecond
        };
    }

    public static UnitPreferences GetCultureUnits(string cultureCode) {
        cultureCode = cultureCode.ToLowerInvariant();
        switch (cultureCode) {
            case "en":
            case "en-us":
                return GetAmericanEnglishUnits();
            case "en-gb":
                return GetBritishEnglishUnits();
            case "es":
            case "es-mx":
            case "fr":
            case "fr-ca":
            default:
                return GetMetricUnits();
        }
    }

    public static UnitPreferences GetMetricUnits() {
        return new UnitPreferences {
            AccelerationUnit = AccelerationUnit.KilometerPerHourPerSecond,
            AreaUnit = AreaUnit.SquareKilometer,
            EnergyUnit = EnergyUnit.NewtonMeters,
            JerkUnit = JerkUnit.KiloMetersPerSecondCubed,
            LengthUnit = LengthUnit.Kilometer,
            PowerUnit = PowerUnit.Kilowatts,
            SpeedUnit = SpeedUnit.KilometersPerHour,
            VolumeUnit = VolumeUnit.Liters
        };
    }

}