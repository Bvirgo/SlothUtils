using System;
using System.IO;
using UnityEngine;

namespace SlothUtils
{
    public class HiDebug
    {
        private static int _fontSize = ((int)(Screen.height * 0.02f));
        private static HiDebugView _instance;
        private static bool _isCallBackSet;
        internal static bool _isOnConsole;
        internal static bool _isOnScreen;
        internal static bool _isOnText;
        private static string _logPath;
        private static string _errorLogPath;

        private static void EnableCallBack()
        {
            if (!_isCallBackSet)
            {
                _isCallBackSet = true;
                Application.logMessageReceivedThreaded += new Application.LogCallback(HiDebug.LogCallBack);
            }
        }
        /// <summary>
        /// 是否开启Debuger，带颜色显示到Cosole
        /// </summary>
        /// <param name="isOn"></param>
        public static void EnableDebuger(bool isOn)
        {
            _isOnConsole = isOn;
        }

        /// <summary>
        /// 调试信息是否显示在屏幕上
        /// </summary>
        /// <param name="isOn"></param>
        public static void EnableOnScreen(bool isOn)
        {
            _isOnScreen = isOn;
            if (_isOnScreen)
            {
                if (_instance == null)
                {
                    GameObject target = new GameObject("HiDebug");
                    _instance = target.AddComponent<HiDebugView>();
                    UnityEngine.Object.DontDestroyOnLoad(target);
                }
                EnableCallBack();
            }
        }

        /// <summary>
        /// 是否自动记录日志信息到本地
        /// </summary>
        /// <param name="isOn"></param>
        public static void EnableOnText(bool isOn)
        {
            _isOnText = isOn;
            if (_isOnText)
            {
                EnableCallBack();
                _logPath = Application.persistentDataPath + "/DebugLog.txt";
                _errorLogPath = Application.persistentDataPath + "/ErrorLog.txt";
                WriteHead();
            }
        }

        private static void WriteHead()
        {
            string strHead = DateTime.Now.ToString("{0}[yyyy.MM.dd HH:mm:ss]");
            strHead = string.Format(strHead, "++++++New Runing At:");
            WriteLogHead(_logPath, strHead);
            WriteLogHead(_errorLogPath, strHead);
        }

        private static void WriteLogHead(string _filePath, string _strHead)
        {
            StreamWriter writer1 = File.AppendText(_filePath);
            writer1.WriteLine("\n\n\n");
            writer1.WriteLine(_strHead);
            writer1.Close();
        }

        private static void LogCallBack(string condition, string stackTrace, LogType type)
        {
            LogInfo logInfo = new LogInfo(condition, stackTrace, type);
            OnText(logInfo);
            OnScreen(logInfo);
        }

        private static void OnScreen(LogInfo logInfo)
        {
            if (_isOnScreen)
            {
                _instance.UpdateLog(logInfo);
            }
        }

        private static void OnText(LogInfo logInfo)
        {
            if (_isOnText)
            {
                string strHead;
                bool bErrorLog = false;
                switch (logInfo.Type)
                {
                    case LogType.Error:
                        strHead = "[Error:]";
                        bErrorLog = true;
                        break;
                    case LogType.Assert:
                        strHead = "[Assert:]";
                        bErrorLog = true;
                        break;
                    case LogType.Warning:
                        strHead = "[Waring:]";
                        break;
                    case LogType.Log:
                        strHead = "[Log:]";
                        break;
                    case LogType.Exception:
                        bErrorLog = true;
                        strHead = "[Exception:]";
                        break;
                    default:
                        strHead = "[Other:]";
                        break;
                }
                StreamWriter writer1 = File.AppendText(_logPath);
                string strTips = string.Format("{0} {1}\nStack:{2}", strHead, logInfo.Condition, logInfo.StackTrace);
                writer1.WriteLine(strTips);
                writer1.Close();
                if (bErrorLog)
                {
                    writer1 = File.AppendText(_errorLogPath);
                    writer1.WriteLine(strTips);
                    writer1.Close();
                }
            }
        }

        public static void SetFontSize(int size)
        {
            _fontSize = size;
        }

        public static int FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
            }
        }
    }
}

