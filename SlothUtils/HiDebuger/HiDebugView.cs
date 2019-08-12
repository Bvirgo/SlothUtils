using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlothUtils
{
    public class HiDebugView : MonoBehaviour
    {
        private static float _buttonHeight = 0.05f;
        private static float _buttonWidth = 0.1f;
        private EDisplay _eDisplay;
        private EMouse _eMouse;
        private bool _isErrorOn = true;
        private bool _isLogOn = true;
        private bool _isWarnningOn = true;
        private readonly float _mouseClickTime = 0.2f;
        private float _mouseDownTime;
        private static float _panelHeight = 0.7f;
        private Rect _rect = new Rect(0f, 0f, Screen.width * _buttonWidth, Screen.height * _buttonHeight);
        private Vector2 _scrollLogPosition;
        private Vector2 _scrollStackPosition;
        private LogInfo _stackInfo;
        private List<LogInfo> logInfos = new List<LogInfo>();

        private void Button()
        {
            if (this._eDisplay == EDisplay.Button)
            {
                if (this._rect.Contains(Event.current.mousePosition))
                {
                    if (Event.current.type == EventType.MouseDown)
                    {
                        this._eMouse = EMouse.Down;
                        this._mouseDownTime = Time.realtimeSinceStartup;
                    }
                    else if (Event.current.type == EventType.MouseUp)
                    {
                        this._eMouse = EMouse.Up;
                        if ((Time.realtimeSinceStartup - this._mouseDownTime) < this._mouseClickTime)
                        {
                            this._eDisplay = EDisplay.Panel;
                        }
                    }
                }
                if ((this._eMouse == EMouse.Down) && (Event.current.type == EventType.MouseDrag))
                {
                    this._rect.x = Event.current.mousePosition.x - (this._rect.width / 2f);
                    this._rect.y = Event.current.mousePosition.y - (this._rect.height / 2f);
                }
                GUI.Button(this._rect, "On", this.GetGUISkin(GUI.skin.button, Color.white, TextAnchor.MiddleCenter));
            }
        }

        private Color GetColor(LogType type)
        {
            if (type != LogType.Log)
            {
                if (type == LogType.Warning)
                {
                    return Color.yellow;
                }
                if (type == LogType.Error)
                {
                    return Color.red;
                }
                if (type == LogType.Exception)
                {
                    return Color.red;
                }
            }
            return Color.white;
        }

        private GUIStyle GetGUISkin(GUIStyle guiStyle, Color color, TextAnchor style)
        {
            guiStyle.normal.textColor = color;
            guiStyle.hover.textColor = color;
            guiStyle.active.textColor = color;
            guiStyle.onNormal.textColor = color;
            guiStyle.onHover.textColor = color;
            guiStyle.onActive.textColor = color;
            guiStyle.margin = new RectOffset(0, 0, 0, 0);
            guiStyle.alignment = style;
            guiStyle.fontSize = HiDebug.FontSize;
            return guiStyle;
        }

        private void LogItem()
        {
            for (int i = 0; i < this.logInfos.Count; i++)
            {
                if (this.logInfos[i].Type == LogType.Log)
                {
                    if (this._isLogOn)
                    {
                        goto Label_0061;
                    }
                    continue;
                }
                if (this.logInfos[i].Type == LogType.Warning)
                {
                    if (this._isWarnningOn)
                    {
                        goto Label_0061;
                    }
                    continue;
                }
                if ((this.logInfos[i].Type == LogType.Error) && !this._isErrorOn)
                {
                    continue;
                }
                Label_0061:
                if (GUILayout.Button(this.logInfos[i].Condition, this.GetGUISkin(GUI.skin.button, this.GetColor(this.logInfos[i].Type), TextAnchor.MiddleLeft), new GUILayoutOption[0]))
                {
                    this._stackInfo = this.logInfos[i];
                }
            }
        }

        private void LogWindow(int windowID)
        {
            if (GUI.Button(new Rect(0f, 0f, Screen.width * _buttonWidth, Screen.height * _buttonHeight), "Clear", this.GetGUISkin(GUI.skin.button, Color.white, TextAnchor.MiddleCenter)))
            {
                this.logInfos.Clear();
                this._stackInfo = null;
            }
            if (GUI.Button(new Rect(Screen.width * (1f - _buttonWidth), 0f, Screen.width * _buttonWidth, Screen.height * _buttonHeight), "Close", this.GetGUISkin(GUI.skin.button, Color.white, TextAnchor.MiddleCenter)))
            {
                this._eDisplay = EDisplay.Button;
            }
            int top = GUI.skin.window.padding.top;
            GUIStyle style = this.GetGUISkin(GUI.skin.toggle, Color.white, TextAnchor.UpperLeft);
            this._isLogOn = GUI.Toggle(new Rect(Screen.width * 0.3f, (float)top, Screen.width * _buttonWidth, (Screen.height * _buttonHeight) - top), this._isLogOn, "Log", style);
            GUIStyle style2 = this.GetGUISkin(GUI.skin.toggle, Color.yellow, TextAnchor.UpperLeft);
            this._isWarnningOn = GUI.Toggle(new Rect(Screen.width * 0.5f, (float)top, Screen.width * _buttonWidth, (Screen.height * _buttonHeight) - top), this._isWarnningOn, "Warnning", style2);
            GUIStyle style3 = this.GetGUISkin(GUI.skin.toggle, Color.red, TextAnchor.UpperLeft);
            this._isErrorOn = GUI.Toggle(new Rect(Screen.width * 0.7f, (float)top, Screen.width * _buttonWidth, (Screen.height * _buttonHeight) - top), this._isErrorOn, "Error", style3);
            GUILayout.Space((Screen.height * _buttonHeight) - top);
            this._scrollLogPosition = GUILayout.BeginScrollView(this._scrollLogPosition, new GUILayoutOption[0]);
            this.LogItem();
            GUILayout.EndScrollView();
        }

        private void OnGUI()
        {
            this.Button();
            this.Panel();
        }

        private void Panel()
        {
            if (this._eDisplay == EDisplay.Panel)
            {
                GUI.Window(0, new Rect(0f, 0f, (float)Screen.width, Screen.height * _panelHeight), new GUI.WindowFunction(this.LogWindow), "HiDebug", this.GetGUISkin(GUI.skin.window, Color.white, TextAnchor.UpperCenter));
                GUI.Window(1, new Rect(0f, Screen.height * _panelHeight, (float)Screen.width, Screen.height * (1f - _panelHeight)), new GUI.WindowFunction(this.StackWindow), "Stack", this.GetGUISkin(GUI.skin.window, Color.white, TextAnchor.UpperCenter));
            }
        }

        private void StackItem()
        {
            if (this._stackInfo != null)
            {
                char[] separator = new char[] { '\n' };
                string[] strArray = this._stackInfo.StackTrace.Split(separator);
                for (int i = 0; i < strArray.Length; i++)
                {
                    GUILayout.Label(strArray[i], this.GetGUISkin(GUI.skin.label, this.GetColor(this._stackInfo.Type), TextAnchor.MiddleLeft), new GUILayoutOption[0]);
                }
            }
        }

        private void StackWindow(int windowID)
        {
            this._scrollStackPosition = GUILayout.BeginScrollView(this._scrollStackPosition, new GUILayoutOption[0]);
            this.StackItem();
            GUILayout.EndScrollView();
        }

        public void UpdateLog(LogInfo logInfo)
        {
            this.logInfos.Add(logInfo);
        }

        private enum EDisplay
        {
            Button,
            Panel
        }

        private enum EMouse
        {
            Up,
            Down
        }
    }
}

