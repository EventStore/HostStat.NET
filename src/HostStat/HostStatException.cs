using System;

namespace HostStat {
	/// <summary>
	///     Exception indicating an error collecting a particular statistic.
	/// </summary>
	public class HostStatException : ApplicationException {
		public HostStatException(string message) : base(message) {
		}
	}
}
