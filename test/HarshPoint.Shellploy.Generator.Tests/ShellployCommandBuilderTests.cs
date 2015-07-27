﻿using HarshPoint.Provisioning;
using HarshPoint.Tests;
using Moq;
using System;
using System.Collections.Immutable;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;
using Xunit.Abstractions;

namespace HarshPoint.ShellployGenerator.Tests
{
    public class ShellployCommandBuilderTests : SeriloggedTest
    {
        public class HarshEmptyTestProvisioner : HarshProvisioner
        {
        }

        public class HarshTestProvisioner : HarshProvisioner
        {
            [Parameter()]
            public String BasicParam { get; set; }

            [Parameter(Mandatory = true)]
            public String MandatoryParam { get; set; }

            [Parameter(ParameterSetName = "A")]
            public String ParamSetA { get; set; }

            [Parameter(ParameterSetName = "B")]
            public String ParamSetB { get; set; }

            [Parameter(ParameterSetName = "A")]
            [Parameter(ParameterSetName = "B", Mandatory = true)]
            public String ParamSetA_BMandatory { get; set; }
        }

        public ShellployCommandBuilderTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public void Creates_command_without_configuration()
        {
            var ns = "MyNamespace";
            var command = new ShellployCommandBuilder<HarshEmptyTestProvisioner>()
                .InNamespace(ns)
                .ToCommand();

            Assert.Equal("New" + nameof(HarshEmptyTestProvisioner) + "Command", command.ClassName);
            Assert.Equal(false, command.HasChildren);
            Assert.Equal(null, command.ParentProvisionerType);
            Assert.Equal(nameof(HarshEmptyTestProvisioner), command.Noun);
            Assert.Equal(typeof(HarshEmptyTestProvisioner), command.ProvisionerType);
            Assert.Equal("New", command.Verb);
            Assert.Equal(ns, command.Namespace);
            Assert.Empty(command.Properties);
        }

        [Fact]
        public void Creates_command_with_children()
        {
            var ns = "MyNamespace";
            var command = new ShellployCommandBuilder<HarshEmptyTestProvisioner>()
                .InNamespace(ns)
                .HasChildren()
                .ToCommand();

            Assert.Equal("New" + nameof(HarshEmptyTestProvisioner) + "Command", command.ClassName);
            Assert.Equal(true, command.HasChildren);
            Assert.Equal(null, command.ParentProvisionerType);
            Assert.Equal(nameof(HarshEmptyTestProvisioner), command.Noun);
            Assert.Equal(typeof(HarshEmptyTestProvisioner), command.ProvisionerType);
            Assert.Equal("New", command.Verb);
            Assert.Equal(ns, command.Namespace);
            Assert.Empty(command.Properties);
        }

        [Fact]
        public void Creates_command_with_parameters()
        {
            var ns = "MyNamespace";
            var command = new ShellployCommandBuilder<HarshTestProvisioner>()
                .InNamespace(ns)
                .ToCommand();

            Assert.Equal("New" + nameof(HarshTestProvisioner) + "Command", command.ClassName);
            Assert.Equal(false, command.HasChildren);
            Assert.Equal(null, command.ParentProvisionerType);
            Assert.Equal(nameof(HarshTestProvisioner), command.Noun);
            Assert.Equal(typeof(HarshTestProvisioner), command.ProvisionerType);
            Assert.Equal("New", command.Verb);
            Assert.Equal(ns, command.Namespace);

            Assert.NotEmpty(command.Properties);
            var properties = command.Properties.ToImmutableDictionary(prop => prop.Name);

            var propName = nameof(HarshTestProvisioner.BasicParam);
            Assert.Equal(typeof(String), properties[propName].Type);
            Assert.Equal(1, properties[propName].ParameterAttributes.Count);
            Assert.Equal(false, properties[propName].ParameterAttributes[0].Mandatory);
            Assert.Equal(null, properties[propName].ParameterAttributes[0].ParameterSet);
            Assert.Equal(null, properties[propName].ParameterAttributes[0].Position);

            propName = nameof(HarshTestProvisioner.MandatoryParam);
            Assert.Equal(typeof(String), properties[propName].Type);
            Assert.Equal(1, properties[propName].ParameterAttributes.Count);
            Assert.Equal(true, properties[propName].ParameterAttributes[0].Mandatory);
            Assert.Equal(null, properties[propName].ParameterAttributes[0].ParameterSet);
            Assert.Equal(null, properties[propName].ParameterAttributes[0].Position);

            propName = nameof(HarshTestProvisioner.ParamSetA);
            Assert.Equal(typeof(String), properties[propName].Type);
            Assert.Equal(1, properties[propName].ParameterAttributes.Count);
            Assert.Equal(false, properties[propName].ParameterAttributes[0].Mandatory);
            Assert.Equal("A", properties[propName].ParameterAttributes[0].ParameterSet);
            Assert.Equal(null, properties[propName].ParameterAttributes[0].Position);

            propName = nameof(HarshTestProvisioner.ParamSetB);
            Assert.Equal(typeof(String), properties[propName].Type);
            Assert.Equal(1, properties[propName].ParameterAttributes.Count);
            Assert.Equal(false, properties[propName].ParameterAttributes[0].Mandatory);
            Assert.Equal("B", properties[propName].ParameterAttributes[0].ParameterSet);
            Assert.Equal(null, properties[propName].ParameterAttributes[0].Position);

            propName = nameof(HarshTestProvisioner.ParamSetA_BMandatory);
            Assert.Equal(typeof(String), properties[propName].Type);
            Assert.Equal(2, properties[propName].ParameterAttributes.Count);
            Assert.Equal(false, properties[propName].ParameterAttributes[0].Mandatory);
            Assert.Equal("A", properties[propName].ParameterAttributes[0].ParameterSet);
            Assert.Equal(null, properties[propName].ParameterAttributes[0].Position);
            Assert.Equal(true, properties[propName].ParameterAttributes[1].Mandatory);
            Assert.Equal("B", properties[propName].ParameterAttributes[1].ParameterSet);
            Assert.Equal(null, properties[propName].ParameterAttributes[1].Position);
        }
    }
}