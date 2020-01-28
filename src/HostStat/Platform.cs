using System.IO;
using System.Runtime.InteropServices;

namespace HostStat {
	internal static class Platform {
		public static DllImportResolver ResolverFor(string toResolve) {
			var platformInfo = GetPlatformConstants();

			string assemblyName = $"lib{toResolve}.{platformInfo.libExt}";
			var assemblyPath = Path.Combine("runtimes", platformInfo.ridPlatform, "native", assemblyName);

			return (libraryName, assembly, searchPath) => {
				// Return the calculated platform assembly path for whichever library we are trying to resolve...
				if (libraryName == toResolve) {
					return NativeLibrary.Load(assemblyPath);
				}

				// ...otherwise fall back to the standard resolution path.
				return NativeLibrary.Load(libraryName, assembly, searchPath);
			};
		}

		private static (string libExt, string ridPlatform) GetPlatformConstants() {
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
				return ("so", "linux");
			}

			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
				return ("dylib", "osx");
			}

			return ("unknown", "unknown");
		}
	}
}
