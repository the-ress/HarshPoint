﻿using HarshPoint.Provisioning.Implementation;
using System.Text.RegularExpressions;

namespace HarshPoint.ShellployGenerator.Commands
{
    internal abstract class HarshPointCommandBuilder<T> : CommandBuilder<T>
        where T : HarshProvisionerBase
    {
        public HarshPointCommandBuilder()
        {
            Aliases.Add(
                Regex.Replace(ProvisionerType.Name, "^Harsh(Modify)?", "")
            );

            Namespace = "HarshPoint.Shellploy";
            ImportedNamespaces.Add("HarshPoint.Provisioning");

            Parameter("Fields").Rename("Field");
            Parameter("Lists").Rename("List");
        }
    }
}