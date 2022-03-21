﻿using System.Text.RegularExpressions;

namespace GraduatedCylinder.Text;

internal static class Regexs
{

    public static Regex Pair { get; } = new Regex(
        //12 s
        //-12 s
        //12.12 s
        @"^(?<value>[+-]?\d+(?:\.\d+)?(?:[eE][+-]?\d+)?)\s*?(?<units>\w+)$",
        RegexOptions.Compiled);

    public static Regex ValueOnly { get; } = new Regex(
        //no units
        @"^[+-]?\d+(?:\.\d+)?(?:[eE][+-]?\d+)?$",
        RegexOptions.Compiled);

}