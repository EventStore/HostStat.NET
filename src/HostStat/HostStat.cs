using System;

namespace HostStat {
	/// <summary>
	///     Instances of HostStat allow access to various system statistics.
	/// </summary>
	public class HostStat {
		/// <summary>
		///     Get the total amount of memory installed in this host.
		/// </summary>
		/// <exception cref="HostStatException">
		///     Thrown if there is an error collecting the statistic.
		/// </exception>
		/// <exception cref="PlatformNotSupportedException">
		///     Thrown if there is no implementation for the calling platform.
		/// </exception>
		/// <returns>Total memory size in bytes</returns>
		public ulong GetTotalMemory() {
			var ret = LibHostStat.get_host_total_memory(out var totalMemory);
			return ConvertReturnCode(ret, totalMemory);
		}

		/// <summary>
		///     Get the total amount of free memory on this host.
		/// </summary>
		/// <remarks>
		///     Free memory is a difficult thing to calculate in a cross platform manner, since various operating systems
		///     report the total amount differently, depending on whether they include caches. The output of this method
		///     is intended to answer the question "can I allocate a byte array of size X right now?".
		/// </remarks>
		/// <exception cref="HostStatException">
		///     Thrown if there is an error collecting the statistic.
		/// </exception>
		/// <exception cref="PlatformNotSupportedException">
		///     Thrown if there is no implementation for the calling platform.
		/// </exception>
		/// <returns>Free memory size in bytes</returns>
		public ulong GetFreeMemory() {
			var ret = LibHostStat.get_host_free_memory(out var freeMemory);
			return ConvertReturnCode(ret, freeMemory);
		}

		/// <summary>
		///     Get the 1, 5 and 15 minute load average values for this host.
		/// </summary>
		/// <exception cref="HostStatException">
		///     Thrown if there is an error collecting the statistic.
		/// </exception>
		/// <exception cref="PlatformNotSupportedException">
		///     Thrown if there is no implementation for the calling platform.
		/// </exception>
		/// <returns><see cref="LoadAverages" /> object</returns>
		public LoadAverages GetLoadAverages() {
			var ret = LibHostStat.get_host_load_averages(out var loadAverages);
			return ConvertReturnCode(ret, loadAverages);
		}

		private static T ConvertReturnCode<T>(LibHostStat.HostStatResult returnCode, T value) {
			return returnCode switch {
				LibHostStat.HostStatResult.HostStatSuccess => value,
				LibHostStat.HostStatResult.HostStatError => throw new HostStatException(
					"Error collecting Free Memory statistic"),
				LibHostStat.HostStatResult.HostStatUnavailable => throw new PlatformNotSupportedException(),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}
