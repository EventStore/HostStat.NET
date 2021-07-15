using System.IO;
using System.Runtime.InteropServices;

namespace HostStat {
	internal static class Platform {
		public static DllImportResolver ResolverFor(string toResolve) {
			var platformInfo = GetPlatformConstants();

			var assemblyLocation = typeof(Platform).Assembly.Location;
			var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

			string libraryName = $"lib{toResolve}.{platformInfo.libExt}";

			var runtimesDirectory = Path.Combine(assemblyDirectory, "runtimes", platformInfo.ridPlatform, "native",
				libraryName);

			// When `dotnet publish` is used, the library is copied next to the published assembly
			var publishDirectory = Path.Combine(assemblyDirectory, libraryName);

			var libraryPath = GetLibraryPath(runtimesDirectory, publishDirectory);

			return (libraryName, assembly, searchPath) => {
				// Return the calculated platform assembly path for whichever library we are trying to resolve...
				if (libraryName == toResolve) {
					return NativeLibrary.Load(libraryPath);
				}

				// ...otherwise fall back to the standard resolution path.
				return NativeLibrary.Load(libraryName, assembly, searchPath);
			};
		}

		private static string GetLibraryPath(params string[] libraryPaths) {
			foreach (var libraryPath in libraryPaths) {
				if (File.Exists(libraryPath)) {
					return libraryPath;
				}
			}

			throw new FileNotFoundException(
				$"The native library was not found in any of the possible locations: {string.Join(',', libraryPaths)}");
		}

		private static (string libExt, string ridPlatform) GetPlatformConstants() {
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {

				if (RuntimeInformation.OSArchitecture == Architecture.Arm64)
					return ("so", "linux-arm64");

				return ("so", "linux");
			}

			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
				return ("dylib", "osx");
			}

			return ("unknown", "unknown");
		}
	}
}
