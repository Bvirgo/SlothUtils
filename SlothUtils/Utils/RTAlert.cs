using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlothUtils
{
    public class RTAlert : MonoSingleton<RTAlert>
    {
        private Rect mRect;
        private float mfW = 260;
        private float mfH = 200;
        bool mbShow;
        private Action mActDone;
        private Action mActCancel;
        private string mstrMsg;
        
        void Awake()
        {
            mRect = new Rect(250, 70, mfW, mfH);
            mbShow = false;
        }

        public void Show(string _strMsg, Action _cbDone = null, Action _cbCancel = null)
        {
            mstrMsg = _strMsg;
            mActCancel = _cbCancel;
            mActDone = _cbDone;
            mbShow = true;
        }

        void OnGUI()
        {
            if (mbShow) mRect = GUI.Window(0, mRect, DoMyWindow, "Alert");
        }

        void DoMyWindow(int windowID)
        {

            GUI.TextArea(new Rect(5, 20, mfW - 10, mfH - 70), mstrMsg);

            if (GUI.Button(new Rect(mfW - 90, mfH - 45, 80, 30), "Cancel"))
            {
                mbShow = false;
                if (mActCancel != null)
                {
                    mActCancel();
                    mActCancel = null;
                }
            }

            if (GUI.Button(new Rect(mfW - 250, mfH - 45, 80, 30), "OK"))
            {
                mbShow = false;
                if (mActDone != null)
                {
                    mActDone();
                    mActDone = null;
                }
            }
            GUI.DragWindow(new Rect(0, 0, mfW, mfH));
        }
    }
}

