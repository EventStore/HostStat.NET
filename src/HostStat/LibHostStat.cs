using System.Runtime.InteropServices;

namespace HostStat {
	internal static class LibHostStat {
		private const string HostStatLib = "hoststat";

		static LibHostStat() {
			NativeLibrary.SetDllImportResolver(typeof(LibHostStat).Assembly, Platform.ResolverFor(HostStatLib));
		}

		[DllImport(HostStatLib)]
		internal static extern HostStatResult get_host_total_memory(out ulong memorySize);

		[DllImport(HostStatLib)]
		internal static extern HostStatResult get_host_free_memory(out ulong freeMemory);


		[DllImport(HostStatLib)]
		internal static extern HostStatResult get_host_load_averages(out LoadAverages memorySize);

		internal enum HostStatResult {
			HostStatSuccess = 0,
			HostStatError = 1,
			HostStatUnavailable = 2
		}
	}
}
