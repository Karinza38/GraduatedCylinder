﻿using System;
using System.Runtime.InteropServices;
using GraduatedCylinder.Units;

namespace GraduatedCylinder
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Torque : IDimension<Torque, TorqueUnit>, IComparable<Torque>, IEquatable<Torque>
    {

        private readonly float _value;
        private readonly TorqueUnit _units;

        public Torque(float value, TorqueUnit units) {
            _value = value;
            _units = units;
        }

        public TorqueUnit Units => _units;

        public float Value => _value;

        public int CompareTo(Torque other) {
            int unitsComparison = _units.CompareTo(other._units);
            if (unitsComparison != 0) {
                return unitsComparison;
            }
            return _value.CompareTo(other._value);
        }

        public bool Equals(Torque other) {
            return CompareTo(other) == 0;
        }

        public override bool Equals(object? obj) {
            return obj is Torque other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine((int)_units, _value);
        }

        public Torque In(TorqueUnit units) {
            throw new NotImplementedException();
        }

    }
}