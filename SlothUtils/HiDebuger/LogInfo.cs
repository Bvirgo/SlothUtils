using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SlothUtils
{
    public class LogInfo
    {
        public LogInfo(string condition, string stackTrace, LogType type)
        {
            this.Condition = condition;
            this.StackTrace = stackTrace;
            this.Type = type;
        }

        public string Condition { get; private set; }

        public string StackTrace { get; private set; }

        public LogType Type { get; private set; }
    }
}

