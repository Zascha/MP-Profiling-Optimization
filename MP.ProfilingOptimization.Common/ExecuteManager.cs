using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace MP.ProfilingOptimization.Common
{
    public class ExecuteManager
    {
        public long ExecuteWithTimePicking<T>(Func<T> controllerMethod)
        {
            return PickTime(controllerMethod);
        }

        public long ExecuteWithTimePicking(Func<ActionResult> controllerMethod)
        {
            return PickTime(controllerMethod);
        }

        #region Private methods

        private long PickTime<T>(Func<T> func)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            func();

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        #endregion
    }
}
