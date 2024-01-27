using System.IO;
using System.Linq;
using UnityEditor;

public class BuildCommands
{
  /// <summary>
  ///   The base path for all builds.
  /// </summary>
  private const string BaseBuildPath = "../../dist/apps/game";

  /// <summary>
  ///   The name of the executable file.
  /// </summary>
  private const string ExecutableName = "Backseat Gaming";

  [MenuItem("Build/Windows")]
  public static void BuildWindows()
  {
    Build("windows", BuildTarget.StandaloneWindows64, ExecutableName + ".exe");
  }

  [MenuItem("Build/MacOS")]
  public static void BuildMacOS()
  {
    Build("macos", BuildTarget.StandaloneOSX, ExecutableName + ".app");
  }

  [MenuItem("Build/Linux")]
  public static void BuildLinux()
  {
    Build("linux", BuildTarget.StandaloneLinux64, ExecutableName + ".x86_64");
  }

  [MenuItem("Build/Android")]
  public static void BuildAndroid()
  {
    Build("android", BuildTarget.Android, ExecutableName + ".apk");
  }

  [MenuItem("Build/iOS")]
  public static void BuildiOS()
  {
    Build("ios", BuildTarget.iOS, "");
  }

  [MenuItem("Build/WebGL")]
  public static void BuildWebGL()
  {
    Build("webgl", BuildTarget.WebGL, "");
  }

  /// <summary>
  ///   Builds the project for the specified platform.
  /// </summary>
  /// <param name="platform">The platform to build for.</param>
  /// <param name="buildTarget">The build target.</param>
  /// <param name="executableName">The name of the executable file.</param>
  private static void Build(string outputDirectoryName, BuildTarget buildTarget, string executableFileName)
  {
    var dirPath = Path.Combine(BaseBuildPath, outputDirectoryName);
    Directory.CreateDirectory(dirPath);

    // Prepare build options
    var buildPlayerOptions = new BuildPlayerOptions
    {
      scenes = EditorBuildSettings.scenes
        .Where(scene => scene.enabled)
        .Select(scene => scene.path)
        .ToArray(),
      locationPathName = Path.Combine(dirPath, executableFileName),
      target = buildTarget,
      options = BuildOptions.None
    };

    // Perform the build
    BuildPipeline.BuildPlayer(buildPlayerOptions);
  }
}
