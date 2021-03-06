using HarshPoint;
using HarshPoint.Provisioning;
using HarshPoint.ShellployGenerator;
using HarshPoint.ShellployGenerator.Builders;
using HarshPoint.Tests;
using System;
using Xunit;
using Xunit.Abstractions;
using SMA = System.Management.Automation;

namespace CommandBuilding
{
    public class Mandatory_parameter : SeriloggedTest
    {
        public Mandatory_parameter(ITestOutputHelper output) : base(output)
        {
            var builder = new NewObjectCommandBuilder<TestProvisioner>();
            var command = builder.ToCommand();

            Property = Assert.Single(
                command.Properties,
                p => p.Identifier == "MandatoryParam"
            );
        }

        private PropertyModel Property { get; }

        [Fact]
        public void Has_Mandatory_Parameter_Attribute()
        {
            var synth = Assert.Single(
                Property.ElementsOfType<PropertyModelSynthesized>()
            );

            var attr = Assert.Single(synth.Attributes);
            Assert.Equal(typeof(SMA.ParameterAttribute), attr.AttributeType);
            Assert.Empty(attr.Arguments);

            Assert.Equal(true, attr.Properties["Mandatory"]);
        }

        private sealed class TestProvisioner : HarshProvisioner
        {
            [Parameter(Mandatory = true)]
            public String MandatoryParam { get; set; }
        }
    }
}