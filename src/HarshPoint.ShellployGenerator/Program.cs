﻿using HarshPoint.ShellployGenerator.Builders;
using System;
using System.IO;
using System.Linq;

namespace HarshPoint.ShellployGenerator
{
    internal static class Program
    {
        private static String ProgramName =>
            Environment.GetCommandLineArgs().FirstOrDefault();

        private static Int32 Main(String[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine($"Usage: {ProgramName} OutputDirectory");
                return 2;
            }

            try
            {
                var writer = new SourceFileWriter(Path.Combine(args[0], "Generated"));
                var script = File.CreateText(Path.Combine(args[0], "Module", "HarshPoint.ShellPloy.psm1"));

                var context = new CommandBuilderContext();
                context.AddBuildersFrom(typeof(Program).Assembly);

                var commands = context.BuildCommands();

                var results = from command in commands
                              let generator = new CommandCodeGenerator(command)

                              select new
                              {
                                  CompileUnit = generator.GenerateCompileUnit(),
                                  command.Aliases,
                                  command.ClassName,
                                  command.Name,
                              };

                foreach (var command in results)
                {
                    Console.WriteLine($"Generating {command.ClassName}...");
                    writer.Write(command.CompileUnit);
                }

                var aliases = from cmd in results
                              from alias in cmd.Aliases
                              select Tuple.Create(alias, cmd.Name);

                foreach (var a in aliases)
                {
                    script.WriteLine($"Set-Alias -Name {a.Item1} -Value {a.Item2}");
                }

                script.WriteLine();
                script.WriteLine("Export-ModuleMember -Alias @(");

                foreach (var a in aliases)
                {
                    script.WriteLine($"    '{a.Item1}'");
                }
                script.WriteLine(")");

                Console.WriteLine("Done.");
                return 0;
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc);
                return 1;
            }
        }
    }
}
