using System;
using System.Diagnostics;

namespace SlothUtils
{
    /// <summary>
    /// 简易的计时类
    /// </summary>
    public class Watch : IDisposable
    {
        private string msTestName;
        private int mnTestCount;
        private Stopwatch mWatch;

        public Watch(string name, int count)
        {
            this.msTestName = name;

            this.mnTestCount = count > 0 ? count : 1;

            this.mWatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            this.mWatch.Stop();

            float totalTime = this.mWatch.ElapsedMilliseconds;

            UnityEngine.Debug.Log(string.Format("测试名称：{0}\n总耗时：{1}\n单次耗时：{2}\n测试数量：{3}",
                this.msTestName, totalTime, totalTime / this.mnTestCount, this.mnTestCount));
        }

    }
}

