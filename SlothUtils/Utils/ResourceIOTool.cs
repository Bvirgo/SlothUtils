﻿using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using Object = UnityEngine.Object;

namespace SlothUtils
{
    /// <summary>
    /// 资源读取器，负责从不同路径读取资源
    /// </summary>
    public class ResourceIOTool : MonoSingleton<ResourceIOTool>
    {
        #region 读操作
        public static string ReadStringByFile(string path)
        {
            StringBuilder line = new StringBuilder();
            try
            {
                if (!File.Exists(path))
                {
                    Debug.Log("path dont exists ! : " + path);
                    return "";
                }

                StreamReader sr = File.OpenText(path);
                line.Append(sr.ReadToEnd());

                sr.Close();
                sr.Dispose();
            }
            catch (Exception e)
            {
                Debug.Log("Load text fail ! message:" + e.Message);
            }

            return line.ToString();
        }

        public static string ReadStringByBundle(string path)
        {
            AssetBundle ab = AssetBundle.LoadFromFile(path);

            TextAsset ta = (TextAsset)ab.mainAsset;

            string content = ta.ToString();
            ab.Unload(true);

            return content;
        }

        public static string ReadStringByResource(string path)
        {
            path = FileUtils.RemoveExpandName(path);
            TextAsset text = (TextAsset)Resources.Load(path);

            if (text == null)
            {
                return "";
            }
            else
            {
                return text.text;
            }
        }

        public static Object ReadObjectByResource(string path)
        {
            path = FileUtils.RemoveExpandName(path);
            Object obj = Resources.Load(path);
            return obj;
        }

        public static byte[] ReadBytesByPath(string filePath)
        {
            try
            {
                FileInfo fileTmp = new FileInfo(filePath);
                if (fileTmp.Exists)
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    int len = (int)fs.Length;
                    byte[] data = new byte[len];
                    fs.Close();
                    return data;
                }
                return null;
            }
            catch (FileNotFoundException e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }


        public static Texture2D ReadTextureByFile(string path, int width, int height)
        {
            //创建文件读取流
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(0, SeekOrigin.Begin);
            //创建文件长度缓冲区
            byte[] bytes = new byte[fileStream.Length];
            //读取文件
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            //释放文件读取流
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;

            //创建Texture
            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(bytes);

            return texture;
        }

        public static void ResourceLoadAsync(string path, LoadCallBack callback)
        {
            Instance.MonoLoadMethod(path, callback);
        }

        public void MonoLoadMethod(string path, LoadCallBack callback)
        {
            StartCoroutine(MonoLoadByResourcesAsync(path, callback));
        }

        LoadState m_loadState = new LoadState();
        public IEnumerator MonoLoadByResourcesAsync(string path, LoadCallBack callback)
        {
            ResourceRequest status = Resources.LoadAsync(path);

            while (!status.isDone)
            {
                m_loadState.UpdateProgress(status);
                callback(m_loadState, null);

                yield return 0;
            }

            m_loadState.UpdateProgress(status);
            callback(m_loadState, status.asset);
        }

        /// <summary>
        /// 异步加载单个assetsbundle
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public static void AssetsBundleLoadAsync(string path, AssetBundleLoadCallBack callback)
        {
            Instance.MonoLoadAssetsBundleMethod(path, callback);
        }

        public void MonoLoadAssetsBundleMethod(string path, AssetBundleLoadCallBack callback)
        {
            StartCoroutine(MonoLoadByAssetsBundleAsync(path, callback));
        }

        public IEnumerator MonoLoadByAssetsBundleAsync(string path, AssetBundleLoadCallBack callback)
        {
#if !UNITY_WEBGL
            AssetBundleCreateRequest status = AssetBundle.LoadFromFileAsync(path);
            LoadState loadState = new LoadState();

            while (!status.isDone)
            {
                loadState.UpdateProgress(status);
                callback(loadState, null);

                yield return 0;
            }
            if (status.assetBundle != null)
            {
                status.assetBundle.name = path;
            }

            loadState.UpdateProgress(status);
            callback(loadState, status.assetBundle);
#else
        WWW www = new WWW(path);
        LoadState loadState = new LoadState();

        while (!www.isDone)
        {
            loadState.UpdateProgress(www);
            callback(loadState, null);

            yield return 0;
        }
        if (www.assetBundle != null)
        {
            www.assetBundle.name = path;
        }

        loadState.UpdateProgress(www);
        callback(loadState, www.assetBundle);
#endif
        }

        /// <summary>
        /// 异步加载WWW
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public static void WWWLoadAsync(string url, WWWLoadCallBack callback)
        {
            Instance.MonoLoadWWWethod(url, callback);
        }

        public void MonoLoadWWWethod(string url, WWWLoadCallBack callback)
        {
            StartCoroutine(MonoLoadByWWWAsync(url, callback));
        }

        public IEnumerator MonoLoadByWWWAsync(string url, WWWLoadCallBack callback)
        {
            WWW www = new WWW(url);
            LoadState loadState = new LoadState();

            while (!www.isDone)
            {

                loadState.UpdateProgress(www);
                callback(loadState, www);

                yield return 0;
            }

            loadState.UpdateProgress(www);
            callback(loadState, www);
        }

        #endregion

        #region 写操作
#if !UNITY_WEBGL || UNITY_EDITOR
        //web Player 不支持写操作
        public static void WriteStringByFile(string path, string content)
        {
            byte[] dataByte = Encoding.GetEncoding("UTF-8").GetBytes(content);

            CreateFile(path, dataByte);
        }

        public static void WriteTexture2DByFile(string path, Texture2D texture)
        {
            byte[] dataByte = texture.EncodeToPNG();

            CreateFile(path, dataByte);
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Debug.Log("File:[" + path + "] dont exists");
            }
        }


        public static void CreateFile(string path, byte[] byt)
        {
            try
            {
                FileUtils.CreatFilePath(path);
                File.WriteAllBytes(path, byt);
            }
            catch (Exception e)
            {
                Debug.LogError("File Create Fail! \n" + e.Message);
            }
        }

#endif

        #endregion
    }

    #region 回调声明
    public delegate void AssetBundleLoadCallBack(LoadState state, AssetBundle bundlle);
    public delegate void WWWLoadCallBack(LoadState loadState, WWW www);
    public delegate void LoadCallBack(LoadState loadState, object resObject);
    public class LoadState
    {
        private static LoadState completeState;

        public static LoadState CompleteState
        {
            get
            {
                if (completeState == null)
                {
                    completeState = new LoadState();
                    completeState.isDone = true;
                    completeState.progress = 1;
                }
                return completeState;
            }
        }

        //public object asset;
        public bool isDone;
        public float progress;

        public void UpdateProgress(ResourceRequest resourceRequest)
        {
            isDone = resourceRequest.isDone;
            progress = resourceRequest.progress;
        }

        public void UpdateProgress(AssetBundleCreateRequest assetBundleCreateRequest)
        {
            isDone = assetBundleCreateRequest.isDone;
            progress = assetBundleCreateRequest.progress;
        }

        public void UpdateProgress(WWW www)
        {
            isDone = www.isDone;
            progress = www.progress;
        }

    }

    #endregion
}

