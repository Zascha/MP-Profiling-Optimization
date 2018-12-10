using MP.ProfilingOptimization.Common;
using ProfileSample.Controllers;
using Xunit;
using Xunit.Abstractions;

namespace MP.ProfilingOptimization.Tests
{
    public class ImageUploadingTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ExecuteManager _executeManager;
        private readonly HomeController _notOptimizedController;
        private readonly HomeOptimizedController _optimizedController;

        public ImageUploadingTests(ITestOutputHelper output)
        {
            _output = output;
            _executeManager = new ExecuteManager();
            _notOptimizedController = new HomeController();
            _optimizedController = new HomeOptimizedController();
        }

        [Fact]
        public void Index_PerformanceTest()
        {
            var notOptimizedResult = _executeManager.ExecuteWithTimePicking(() => _notOptimizedController.Index());
            var optimizedResult = _executeManager.ExecuteWithTimePicking(() => _optimizedController.Index());

            OutputPerformanceResults(optimizedResult, notOptimizedResult);

            Assert.True(optimizedResult < notOptimizedResult);
        }

        // Run apart from the other test
        [Fact]
        public void Convert_PerformanceTest()
        {
            var notOptimizedResult = _executeManager.ExecuteWithTimePicking(() => _notOptimizedController.Convert());
            var optimizedResult = _executeManager.ExecuteWithTimePicking(() => _optimizedController.Convert());

            OutputPerformanceResults(optimizedResult, notOptimizedResult);

            Assert.True(optimizedResult < notOptimizedResult);
        }

        #region Private methods

        private void OutputPerformanceResults(long optimizedResult, long notOptimizedResult)
        {
            _output.WriteLine($"Optimized result: {optimizedResult} ms");
            _output.WriteLine($"Not optimized result: {notOptimizedResult} ms");
            _output.WriteLine($"Faster in {notOptimizedResult / optimizedResult} times.");
        }

        #endregion
    }
}
