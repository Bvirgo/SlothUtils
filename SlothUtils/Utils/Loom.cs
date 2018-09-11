using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;
namespace SlothUtils
{
    public class Loom : MonoBehaviour
    {
        public static int maxThreads = 8;
        static int numThreads;

        private static Loom _current;
        private static System.Object locker = new object();
        public static Loom Current
        {
            get
            {
                Initialize();
                return _current;
            }
        }

        public void Initial()
        {
            Loom.Initialize();
        }

        void Awake()
        {
            _current = this;
            initialized = true;
        }

        static bool initialized;

        static void Initialize()
        {
            if (!initialized)
            {
                if (!Application.isPlaying)
                    return;
                initialized = true;
                var g = new GameObject("Loom");
                _current = g.AddComponent<Loom>();

                DontDestroyOnLoad(g);
            }

        }

        private List<Action> _actions = new List<Action>();
        private Action _updateAction;
        public struct DelayedQueueItem
        {
            public float time;
            public Action action;
        }
        private List<DelayedQueueItem> _delayed = new List<DelayedQueueItem>();

        List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();

        public static void OnMainThreadUpdate(Action action)
        {
            lock (locker)
            {
                Current._updateAction += action;
            }
        }

        public static void QueueOnMainThread(Action action)
        {
            QueueOnMainThread(action, 0f);
        }
        public static void QueueOnMainThread(Action action, float time)
        {
            lock (locker)
            {
                if (Current == null)
                {
                    return;
                }
                if (time != 0)
                {
                    Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action });
                }
                else
                {
                    Current._actions.Add(action);
                }
            }
        }

        public static Thread RunAsync(Action a)
        {
            Initialize();
            while (numThreads >= maxThreads)
            {
                Thread.Sleep(1);
            }
            Interlocked.Increment(ref numThreads);
            ThreadPool.QueueUserWorkItem(RunAction, a);
            return null;
        }

        private static void RunAction(object action)
        {
            try
            {
                ((Action)action)();
            }
            catch
            {
            }
            finally
            {
                Interlocked.Decrement(ref numThreads);
            }

        }


        void OnDisable()
        {
            if (_current == this)
            {
                _current = null;
            }
        }



        // Use this for initialization  
        void Start()
        {

        }

        List<Action> _currentActions = new List<Action>();

        // Update is called once per frame  
        void Update()
        {
            lock (locker)
            {
                _currentActions.Clear();
                _currentActions.AddRange(_actions);
                _actions.Clear();
                foreach (var a in _currentActions)
                {
                    a();
                }
                _currentDelayed.Clear();
                _currentDelayed.AddRange(_delayed.Where(d => d.time <= Time.time));
                foreach (var item in _currentDelayed)
                {
                    _delayed.Remove(item);
                }
                foreach (var delayed in _currentDelayed)
                {
                    delayed.action();
                }
                {
                    if (_updateAction != null)
                    {
                        _updateAction();
                    }
                }
            }
        }
    }

}
