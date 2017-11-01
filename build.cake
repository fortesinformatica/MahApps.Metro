var target = Argument("target", "Default");
var url = Argument("url", "");
var key = Argument("key", "");

Task("Default")
  .Does(() =>
{
var setting = new NuGetPackSettings();
setting.OutputDirectory = "artifacts";
NuGetPack(@"src\MahApps.Metro\MahApps.Metro\MahApps.Metro.NET40.csproj",setting);
 // Get the path to the package.
var packages = GetFiles("./artifacts/**/*.nupkg");

 // Push the package.
 NuGetPush(packages, new NuGetPushSettings {
     Source = url,
     ApiKey = key
 });
});

RunTarget(target);