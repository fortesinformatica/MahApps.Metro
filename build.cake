var target = Argument("target", "Default");
var url = Argument("url", "");
var key = Argument("key", "");

Task("Build").Does(() =>
{
    MSBuild(@".\src\MahApps.Metro\MahApps.Metro\MahApps.Metro.NET40.csproj", settings =>
        settings
            .UseToolVersion(MSBuildToolVersion.VS2017)
            .SetVerbosity(Verbosity.Minimal)
    );
});

Task("Default").IsDependentOn("Build")
  .Does(() =>
{
var setting = new NuGetPackSettings();
setting.OutputDirectory = "artifacts";
NuGetPack(@".\src\MahApps.Metro\MahApps.Metro\MahApps.Metro.NET40.csproj",setting);
 // Get the path to the package.
var packages = GetFiles("./artifacts/**/*.nupkg");

 // Push the package.
 NuGetPush(packages, new NuGetPushSettings {
     Source = url,
     ApiKey = key
 });
});

RunTarget(target);