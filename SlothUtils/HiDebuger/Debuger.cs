using System;
using UnityEngine;

namespace SlothUtils
{
    public static class Debuger
    {
        private static string GetTime()
        {
            return (DateTime.Now.ToString("[yyyy.MM.dd HH:mm:ss]") + ": {0}");
        }

        public static void Log(object obj)
        {
            if (HiDebug._isOnConsole)
            {
                Debug.Log("<color=green>" + string.Format(GetTime(), obj) + "</color>");
            }
        }

        public static void LogError(object obj)
        {
            if (HiDebug._isOnConsole)
            {
                Debug.LogError("<color=red>" + string.Format(GetTime(), obj) + "</color>");
            }
        }

        public static void LogWarning(object obj)
        {
            if (HiDebug._isOnConsole)
            {
                Debug.LogWarning("<color=blue>" + string.Format(GetTime(), obj) + "</color>");
            }
        }
    }
}

