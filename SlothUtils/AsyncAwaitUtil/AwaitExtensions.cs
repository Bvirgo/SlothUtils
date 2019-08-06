using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace SlothUtils
{
    public static class AwaitExtensions
    {
        public static TaskAwaiter<int> GetAwaiter(this Process process)
        {
            var tcs = new TaskCompletionSource<int>();
            process.EnableRaisingEvents = true;

            process.Exited += (s, e) => tcs.TrySetResult(process.ExitCode);

            if (process.HasExited)
            {
                tcs.TrySetResult(process.ExitCode);
            }

            return tcs.Task.GetAwaiter();
        }

        // Any time you call an async method from sync code, you can either use this wrapper
        // method or you can define your own `async void` method that performs the await
        // on the given Task
        public static async void WrapErrors(this Task task)
        {
            await task;
        }

        /// <summary>
        /// async 兼容 回调
        /// </summary>
        /// <param name="done"></param>
        /// <returns></returns>
        public static async Task<object> Do(this Action<Action<object>> done)
        {
            return await Doing(done);
        }

        /// <summary>
        /// async 兼容 回调
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        public static async Task<object> Do(this AsyncAction aa)
        {
            return await Doing(aa.Task);
        }

        static IEnumerator<object> Doing(Action<Action<object>> done)
        {
            bool bDone = false;
            object o = null;
            while (!bDone)
            {
                done((to) =>
                {
                    bDone = true;
                    o = to;
                });
                yield return null;
            }
            yield return o;
        }
    }

    public class AsyncAction
    {
        public Action<Action<object>> Task;

        public AsyncAction(Action<Action<object>> loadFunc)
        {
            Task = loadFunc;
        }
    }
}

