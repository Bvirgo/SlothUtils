using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Profiling;
#if UNITY_5_5_OR_NEWER
using UnityEngine.Profiling;
#endif

#pragma warning disable
namespace SlothUtils
{
    /*Eg:
     * MemoryManager.Instance.OnTrigger();
     * MemoryManager.Instance.OnShow();
     * MemoryManager.Instance.OnRegister(()=>{"Special Hander For youself"});
     */
    public class MemoryManager:MonoSingleton<MemoryManager>
    {
        /// <summary>
        /// 是否允许动态加载
        /// </summary>
        public  bool s_allowDynamicLoad = true;

        /// <summary>
        /// 最大允许的内存使用量
        /// </summary>
        public  int s_MaxMemoryUse = 170;

        /// <summary>
        /// 最大允许的堆内存使用量
        /// </summary>
        public  int s_MaxHeapMemoryUse = 50;

        private bool mbShowInfo = false;

        private bool mbTigger = false;

        private List<Action> mpFreeMemory;

        void Awake()
        {
            mpFreeMemory = new List<Action>();
        }

        #region Public
        public void OnShow()
        {
            mbShowInfo = true;
        }

        public void OnHide()
        {
            mbShowInfo = false;
        }

        public void OnTrigger(bool _bTrigger = true)
        {
            mbTigger = _bTrigger;
            Debug.LogFormat("<color=green>{0} Memory Monitor</color>", mbTigger ? "Open" : "Close");
        }

        /// <summary>
        /// Register Handler For Free Memory
        /// </summary>
        /// <param name="_cbFree"></param>
        public void OnRegister(Action _cbFree)
        {
            if (!mpFreeMemory.Contains(_cbFree))
            {
                mpFreeMemory.Add(_cbFree);
            }
        }
        #endregion


        void Update()
        {
            if (mbTigger)
            {
#if !UNITY_EDITOR
                //内存管理
                MonitorMemorySize();
#else

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F3))
            {
                FreeHeapMemory();
            }
#endif

#endif
            }

        }

         void OnGUI()
        {
            if (mbShowInfo)
            {
                GUILayout.Label("总内存：" + ByteToM(Profiler.GetTotalAllocatedMemory()).ToString("F") + "M");
                GUILayout.Label("堆内存：" + ByteToM(Profiler.GetMonoUsedSize()).ToString("F") + "M");
            }
        }

        /// <summary>
        /// 释放内存
        /// </summary>
        public  void FreeMemory()
        {
            //GlobalEvent.DispatchEvent(MemoryEvent.FreeMemory);

            ////清空对象池
            //GameObjectManager.CleanPool();

            //GameObjectManager.CleanPool_New();

            ////清空缓存的UI
            //UIManager.DestroyAllHideUI();

            FreeHeapMemory();

            Resources.UnloadUnusedAssets();
            //GC
            GC.Collect();
        }

        /// <summary>
        /// 释放堆内存
        /// </summary>
        private  void FreeHeapMemory()
        {
            foreach (var item in mpFreeMemory)
            {
                if (item != null) item();
            }
            //GlobalEvent.DispatchEvent(MemoryEvent.FreeHeapMemory);

            //DataManager.CleanCache();
            //ConfigManager.CleanCache();
            //RecordManager.CleanCache();
        }

        #region 内存监控

        // 字节到兆
        //const float ByteToM = 0.000001f;

         bool s_isFreeMemory = false;
         bool s_isFreeMemory2 = false;

         bool s_isFreeHeapMemory = false;
         bool s_isFreeHeapMemory2 = false;

        /// <summary>
        /// 用于监控内存
        /// </summary>
        /// <param name="tag"></param>
         void MonitorMemorySize()
        {
            if (ByteToM(Profiler.GetTotalReservedMemory()) > s_MaxMemoryUse * 0.7f)
            {
                if (!s_isFreeMemory)
                {
                    s_isFreeMemory = true;
                    FreeMemory();
                }

                if (ByteToM(Profiler.GetMonoHeapSize()) > s_MaxMemoryUse)
                {
                    if (!s_isFreeMemory2)
                    {
                        s_isFreeMemory2 = true;
                        FreeMemory();
                        Debug.LogError("总内存超标告警 ！当前总内存使用量： " + ByteToM(Profiler.GetTotalAllocatedMemory()) + "M");
                    }
                }
                else
                {
                    s_isFreeMemory2 = false;
                }
            }
            else
            {
                s_isFreeMemory = false;
            }

            if (ByteToM(Profiler.GetMonoUsedSize()) > s_MaxHeapMemoryUse * 0.7f)
            {
                if (!s_isFreeHeapMemory)
                {
                    s_isFreeHeapMemory = true;
                }

                if (ByteToM(Profiler.GetMonoUsedSize()) > s_MaxHeapMemoryUse)
                {
                    if (!s_isFreeHeapMemory2)
                    {
                        s_isFreeHeapMemory2 = true;
                        Debug.LogError("堆内存超标告警 ！当前堆内存使用量： " + ByteToM(Profiler.GetMonoUsedSize()) + "M");
                    }
                }
                else
                {
                    s_isFreeHeapMemory2 = false;
                }
            }
            else
            {
                s_isFreeHeapMemory = false;
            }
        }

        #endregion

         float ByteToM(uint byteCount)
        {
            return (float)(byteCount / (1024.0f * 1024.0f));
        }
    }
}
