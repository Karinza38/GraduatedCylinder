using DigitalHammer.Testing;
using Xunit;

namespace GraduatedCylinder.Conversions;

public class AngularAccelerationConversionsFixture
{

    [Theory]
    [InlineData(3 * Math.PI, AngularAccelerationUnit.RadiansPerSquareSecond, 1.5, AngularAccelerationUnit.RevolutionsPerSquareSecond)]
    public void AngularAccelerationConversions(double value1,
                                               AngularAccelerationUnit units1,
                                               double value2,
                                               AngularAccelerationUnit units2) {
        new AngularAcceleration(value1, units1).In(units2).ShouldBe(new AngularAcceleration(value2, units2));
        new AngularAcceleration(value2, units2).In(units1).ShouldBe(new AngularAcceleration(value1, units1));
    }

}