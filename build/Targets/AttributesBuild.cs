using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using static Nuke.Common.Tools.Git.GitTasks;

namespace Targets;

partial class Build
{
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = /*IsLocalBuild ? Configuration.Debug :*/ Configuration.Release;

    [Solution] readonly Solution Solution;

    // AbsolutePath DALDirectory => RootDirectory / "Streetcode" / "Streetcode.DAL";
    // AbsolutePath NukeDirectory => RootDirectory / ".nuke" / "temp";
    // AbsolutePath SourceDirectory => RootDirectory / "Streetcode";
    // AbsolutePath OutputDirectory => RootDirectory / "Output";
    // AbsolutePath UnitTestsDirectory => RootDirectory / "Streetcode" / "Streetcode.XUnitTest";
    // AbsolutePath IntegrationTestsDirectory => RootDirectory / "Streetcode" / "Streetcode.XIntegrationTest";
    // AbsolutePath ClientDirectory => (AbsolutePath)"/home/yurtis/Desktop/front";
    // AbsolutePath DbUpdateDirectory => RootDirectory / "Streetcode" / "DbUpdate";

    AbsolutePath DALDirectory => (AbsolutePath)"/Streetcode/Streetcode.DAL";
    AbsolutePath NukeDirectory => (AbsolutePath)"/.nuke/temp";
    AbsolutePath SourceDirectory => (AbsolutePath)"/Streetcode";
    AbsolutePath OutputDirectory => (AbsolutePath)"/Output";
    AbsolutePath UnitTestsDirectory => (AbsolutePath)"/Streetcode/Streetcode.XUnitTest";
    AbsolutePath IntegrationTestsDirectory => (AbsolutePath)"/Streetcode/Streetcode.XIntegrationTest";
    AbsolutePath ClientDirectory => (AbsolutePath)"/home/yurtis/Desktop/front";
    AbsolutePath DbUpdateDirectory => (AbsolutePath)"/Streetcode/DbUpdate";


    static bool GitHasCleanCopy(AbsolutePath path) =>
        !Git($"status --short {path}", logOutput: false).Any();
}

