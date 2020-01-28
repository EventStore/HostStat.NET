using System.Runtime.InteropServices;

namespace HostStat {
	/// <summary>
	///     Represents 1, 5 and 15 minute load averages
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LoadAverages {
		/// <summary>
		///     The 1 minute load average
		/// </summary>
		public readonly double Average1m;

		/// <summary>
		///     The 5 minute load average
		/// </summary>
		public readonly double Average5m;

		/// <summary>
		///     The 15 minute load average
		/// </summary>
		public readonly double Average15m;
	}
}
