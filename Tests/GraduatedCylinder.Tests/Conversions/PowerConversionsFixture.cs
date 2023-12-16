#if GraduatedCylinder
namespace GraduatedCylinder.Conversions;
#endif
#if Pipette
namespace Pipette.Conversions;
#endif

public class PowerConversionsFixture
{

    [Theory]
    [InlineData(7654.986, PowerUnit.Watts, 6582103.1814273431, PowerUnit.CaloriesPerHour)]
    [InlineData(7654.986, PowerUnit.Watts, 109701.719690456, PowerUnit.CaloriesPerMinute)]
    [InlineData(7654.986, PowerUnit.Watts, 1828.361994841, PowerUnit.CaloriesPerSecond)]
    [InlineData(7654.986, PowerUnit.Watts, 10.265503553707925439184658709937, PowerUnit.Horsepower)]
    [InlineData(7654.986, PowerUnit.Watts, 27557949.6, PowerUnit.JoulesPerHour)]
    [InlineData(7654.986, PowerUnit.Watts, 459299.16, PowerUnit.JoulesPerMinute)]
    [InlineData(7654.986, PowerUnit.Watts, 7654.986, PowerUnit.JoulesPerSecond)]
    [InlineData(7654.986, PowerUnit.Watts, 7.654986, PowerUnit.KiloWatts)]
    [InlineData(7654.986, PowerUnit.Watts, 0.007654986, PowerUnit.MegaWatts)]
    [InlineData(7654.986, PowerUnit.Watts, 7654.986, PowerUnit.NewtonMetersPerSecond)]
    public void PowerConversions(double value1, PowerUnit units1, double value2, PowerUnit units2) {
        new Power(value1, units1).In(units2).ShouldBe(new Power(value2, units2));
        new Power(value2, units2).In(units1).ShouldBe(new Power(value1, units1));
    }

}