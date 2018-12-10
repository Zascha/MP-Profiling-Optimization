using MP.ProfilingOptimization.Task1;
using System;
using MP.ProfilingOptimization.Common;
using Xunit;
using Xunit.Abstractions;

namespace MP.ProfilingOptimization.Tests
{
    public class PasswordHashGeneratorTests
    {
        private const string Password = "Password";

        private readonly ITestOutputHelper _output;
        private readonly ExecuteManager _executeManager;
        private readonly PasswordHashGenerator _hashGenerator;
        private readonly Random _random;

        private readonly byte[] _salt;

        public PasswordHashGeneratorTests(ITestOutputHelper output)
        {
            _output = output;

            _hashGenerator = new PasswordHashGenerator();
            _executeManager = new ExecuteManager();
            _random = new Random();

            _salt = GenerateSaltBytes();
        }

        [Fact]
        public void GeneratePasswordHashUsingSalt_PerformanceTest()
        {
            var notOptimizedResult = _executeManager.ExecuteWithTimePicking(() => _hashGenerator.GeneratePasswordHashUsingSalt_NotOptimized(Password, _salt));
            var optimizedResult = _executeManager.ExecuteWithTimePicking(() => _hashGenerator.GeneratePasswordHashUsingSalt_Optimized(Password, _salt));

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

        private byte[] GenerateSaltBytes()
        {
            var array = new byte[36];

            _random.NextBytes(array);

            return array;
        }

        #endregion
    }
}
