using Xunit;

namespace HostStat.Tests {
	public class HostStatTests {
		public HostStatTests() {
			_sut = new HostStat();
		}

		private readonly HostStat _sut;

		[Fact]
		public void can_get_host_cpu_load_averages() {
			var loadAverages = _sut.GetLoadAverages();
			Assert.True(loadAverages.Average1m > 0);
			Assert.True(loadAverages.Average5m > 0);
			Assert.True(loadAverages.Average15m > 0);
		}

		[Fact]
		public void can_get_host_free_memory() {
			var freeMemory = _sut.GetFreeMemory();
			Assert.True(freeMemory > 0);
		}

		[Fact]
		public void can_get_host_total_memory() {
			var totalMemory = _sut.GetTotalMemory();
			Assert.True(totalMemory > 0);
		}
	}
}
