using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlothUtils
{
    public static class FileUtils
    {
        #region 文件与路径的增加删除创建

        #region 不忽视出错

        /// <summary>
        /// 判断有没有这个文件路径，如果没有则创建它(路径会去掉文件名)
        /// </summary>
        /// <param name="filepath"></param>
        public static void CreatFilePath(string filepath)
        {
            string newPathDir = Path.GetDirectoryName(filepath);

            CreatPath(newPathDir);
        }

        /// <summary>
        /// 判断有没有这个路径，如果没有则创建它
        /// </summary>
        /// <param name="filepath"></param>
        public static void CreatPath(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 删掉某个目录下的所有子目录和子文件，但是保留这个目录
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(string path)
        {
            string[] directorys = Directory.GetDirectories(path);

            //删掉所有子目录
            for (int i = 0; i < directorys.Length; i++)
            {
                string pathTmp = directorys[i];

                if (Directory.Exists(pathTmp))
                {
                    Directory.Delete(pathTmp, true);
                }
            }

            //删掉所有子文件
            string[] files = Directory.GetFiles(path);

            for (int i = 0; i < files.Length; i++)
            {
                string pathTmp = files[i];
                if (File.Exists(pathTmp))
                {
                    File.Delete(pathTmp);
                }
            }
        }

        /// <summary>
        /// 复制文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="sourcePath">待复制的文件夹路径</param>
        /// <param name="destinationPath">目标路径</param>
        public static void CopyDirectory(string sourcePath, string destinationPath)
        {
            DirectoryInfo info = new DirectoryInfo(sourcePath);
            Directory.CreateDirectory(destinationPath);

            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                string destName = Path.Combine(destinationPath, fsi.Name);
                //Debug.Log(destName);

                if (fsi is FileInfo)          //如果是文件，复制文件
                    File.Copy(fsi.FullName, destName);
                else                                    //如果是文件夹，新建文件夹，递归
                {
                    Directory.CreateDirectory(destName);
                    CopyDirectory(fsi.FullName, destName);
                }
            }
        }

        #endregion

        #region 忽视出错 (会跳过所有出错的操作,一般是用来无视权限)
        /// <summary>
        /// 删除所有可以删除的文件
        /// </summary>
        /// <param name="path"></param>
        public static void SafeDeleteDirectory(string path)
        {
            string[] directorys = Directory.GetDirectories(path);

            //删掉所有子目录
            for (int i = 0; i < directorys.Length; i++)
            {
                string pathTmp = directorys[i];

                if (Directory.Exists(pathTmp))
                {
                    SafeDeleteDirectory(pathTmp);
                    try
                    {
                        Directory.Delete(pathTmp, false);
                    }
                    catch
                    {
                        //Debug.LogError(e.ToString());
                    }
                }
            }

            //删掉所有子文件
            string[] files = Directory.GetFiles(path);

            for (int i = 0; i < files.Length; i++)
            {
                string pathTmp = files[i];
                if (File.Exists(pathTmp))
                {
                    try
                    {
                        File.Delete(pathTmp);
                    }
                    catch/*(Exception e)*/
                    {
                        //Debug.LogError(e.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 复制所有可以复制的文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="sourcePath">待复制的文件夹路径</param>
        /// <param name="destinationPath">目标路径</param>
        public static void SafeCopyDirectory(string sourcePath, string destinationPath)
        {
            DirectoryInfo info = new DirectoryInfo(sourcePath);
            Directory.CreateDirectory(destinationPath);

            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                string destName = Path.Combine(destinationPath, fsi.Name);
                //Debug.Log(destName);

                if (fsi is FileInfo)          //如果是文件，复制文件
                    try
                    {
                        File.Copy(fsi.FullName, destName);
                    }
                    catch { }
                else                                    //如果是文件夹，新建文件夹，递归
                {
                    Directory.CreateDirectory(destName);
                    SafeCopyDirectory(fsi.FullName, destName);
                }
            }
        }

        #endregion

        #endregion

        #region 文件名

        //移除拓展名
        public static string RemoveExpandName(string name)
        {
            int dirIndex = name.LastIndexOf(".");

            if (dirIndex != -1)
            {
                return name.Remove(dirIndex);
            }
            else
            {
                return name;
            }
        }

        public static string GetExpandName(string name)
        {
            return name.Substring(name.LastIndexOf(".") + 1, (name.Length - name.LastIndexOf(".") - 1));
        }

        //取出一个路径下的文件名
        public static string GetFileNameByPath(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Name; // text.txt
        }

        //取出一个相对路径下的文件名
        public static string GetFileNameBySring(string path)
        {
            string[] paths = path.Split('/');
            return paths[paths.Length - 1];
        }

        //修改文件名
        public static void ChangeFileName(string path, string newName)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Move(path, newName);
            }
        }

        #endregion

        #region 文件编码

        /// <summary>
        /// 文件编码转换
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <param name="destFile">目标文件，如果为空，则覆盖源文件</param>
        /// <param name="targetEncoding">目标编码</param>
        public static void ConvertFileEncoding(string sourceFile, string destFile, System.Text.Encoding targetEncoding)
        {
            destFile = string.IsNullOrEmpty(destFile) ? sourceFile : destFile;
            Encoding sourEncoding = GetEncodingType(sourceFile);

            System.IO.File.WriteAllText(destFile, System.IO.File.ReadAllText(sourceFile, sourEncoding), targetEncoding);
        }

        /// <summary> 
        /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型 
        /// </summary> 
        /// <param name="FILE_NAME">文件路径</param> 
        /// <returns>文件的编码类型</returns> 
        public static Encoding GetEncodingType(string FILE_NAME)
        {
            FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
            Encoding r = GetEncodingType(fs);
            fs.Close();
            return r;
        }

        /// <summary> 
        /// 通过给定的文件流，判断文件的编码类型 
        /// </summary> 
        /// <param name="fs">文件流</param> 
        /// <returns>文件的编码类型</returns> 
        public static Encoding GetEncodingType(FileStream fs)
        {
            //byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            //byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            //byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM 
            Encoding reVal = Encoding.Default;

            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = Encoding.Unicode;
            }
            r.Close();
            return reVal;

        }

        /// <summary> 
        /// 判断是否是不带 BOM 的 UTF8 格式 
        /// </summary> 
        /// <param name="data"></param> 
        /// <returns></returns> 
        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1;
            //计算当前正分析的字符应还有的字节数 
            byte curByte; //当前分析的字节. 
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前 
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX......1111110X　 
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1 
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }
        #endregion

        #region 文件工具
        public delegate void FileExecuteHandle(string filePath);
        /// <summary>
        /// 递归处理某路径及其他的子目录
        /// </summary>
        /// <param name="path">目标路径</param>
        /// <param name="expandName">要处理的特定拓展名</param>
        /// <param name="handle">处理函数</param>
        public static void RecursionFileExecute(string path, string expandName, FileExecuteHandle handle)
        {
            string[] allUIPrefabName = Directory.GetFiles(path);
            foreach (var item in allUIPrefabName)
            {
                try
                {
                    if (expandName != null)
                    {
                        if (item.EndsWith("." + expandName))
                        {
                            handle(item);
                        }
                    }
                    else
                    {
                        handle(item);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("RecursionFileExecute Error :" + item + " Exception:" + e.ToString());
                }
            }

            string[] dires = Directory.GetDirectories(path);
            for (int i = 0; i < dires.Length; i++)
            {
                RecursionFileExecute(dires[i], expandName, handle);
            }
        }
        #endregion

        #region 获取一个路径下的所有文件

        public static List<string> GetAllFileNamesByPath(string path, string[] expandNames = null)
        {
            List<string> list = new List<string>();

            RecursionFind(list, path, expandNames);

            return list;
        }

        static void RecursionFind(List<string> list, string path, string[] expandNames)
        {
            string[] allUIPrefabName = Directory.GetFiles(path);
            foreach (var item in allUIPrefabName)
            {
                if (ExpandFilter(item, expandNames))
                {
                    list.Add(item);
                }
            }

            string[] dires = Directory.GetDirectories(path);
            for (int i = 0; i < dires.Length; i++)
            {
                RecursionFind(list, dires[i], expandNames);
            }
        }

        static bool ExpandFilter(string name, string[] expandNames)
        {
            if (expandNames == null)
            {
                return true;
            }

            else if (expandNames.Length == 0)
            {
                return !name.Contains(".");
            }

            else
            {
                for (int i = 0; i < expandNames.Length; i++)
                {
                    if (name.EndsWith("." + expandNames[i]))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        #endregion

        #region 文件名相关
        /// <summary>
        /// 获取路径目录，除去文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            int index = fileName.LastIndexOf('/');
            if (index < 0)
                return "";

            return fileName.Substring(0, index);
        }
        /// <summary>
        /// 路径转为标准格式路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetStandardPath(string path)
        {
            int loopNum = 20;
            path = path.Replace(@"\", @"/");
            while (path.IndexOf(@"//") != -1)
            {
                path = path.Replace(@"//", @"/");
                loopNum--;
                if (loopNum < 0)
                {
                    Debug.Log("路径清理失败: " + path);
                    return path;
                }
            }
            return path;
        }

        public static string GetFolderPath(string path, bool fullPath = true)
        {
            path = GetStandardPath(path);
            if (fullPath)//获取全路径
            {
                if (path.LastIndexOf(@"/") == path.Length - 1)
                    return GetFolderPath(path.Substring(0, path.Length - 1));
                else
                    return path.Substring(0, path.LastIndexOf(@"/") + 1);
            }
            else//获取父级文件夹名
            {
                string[] strArr = path.Split('/');

                if (path.LastIndexOf(@"/") == path.Length - 1)
                    return strArr[strArr.Length - 2];
                else
                    return strArr[strArr.Length - 1];
            }
        }

        /// <summary>
        /// 通过全路径，获取文件名（去掉后缀）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="needPostfix">是否需要带后缀</param>
        /// <returns></returns>
        public static string GetFileNameByPath(string path, bool needPostfix = false)
        {
            path = GetStandardPath(path);
            string fileFolderPath = path.Substring(0, path.LastIndexOf(@"/") + 1);

            string fileName = path.Substring(path.LastIndexOf("/") + 1, path.Length - fileFolderPath.Length);
            if (needPostfix)
                return fileName;
            else
                return fileName.Substring(0, fileName.LastIndexOf("."));
        }
        /// <summary>获取文件名后缀</summary>
        public static string GetFilePostfix(string fileName)
        {
            if (fileName == null)
                return null;
            string res;
            if (fileName.IndexOf(".") == -1)
                res = "";
            else
            {
                string[] ss = fileName.Split(new char[1] { '.' });
                res = ss[ss.Length - 1];
            }
            return res;
        }

        /// <summary>
        /// 去掉文件名后缀
        /// </summary>
        /// <param name="_strName"></param>
        /// <returns></returns>
        public static string GetFilePrefix(string _strName)
        {
            string strName = _strName;
            int nIndex = strName.LastIndexOf('.');
            if (nIndex > 0 && nIndex < strName.Length - 1)
            {
                strName = strName.Substring(0, strName.LastIndexOf('.'));
            }

            return strName;
        }

        public static string GetParentFolderPath(string path, bool fullPath = true)
        {
            path = GetStandardPath(path);
            if (fullPath)//获取全路径
            {
                if (path.LastIndexOf(@"/") == path.Length - 1)
                    return GetFolderPath(path.Substring(0, path.Length - 1));
                else
                    return path.Substring(0, path.LastIndexOf(@"/") + 1);
            }
            else//获取父级文件夹名
            {
                string[] strArr = path.Split('/');
                return strArr[strArr.Length - 2];
            }
        }
        #endregion

        #region 文件读写相关
        public static byte[] LoadFile(FileInfo fInfo)
        {
            return LoadFile(fInfo.FullName);
        }
        public static byte[] LoadFile(string path) //@path)
        {
            if (File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    //创建文件长度缓冲区
                    byte[] bytes = new byte[fileStream.Length];
                    fileStream.Seek(0, SeekOrigin.Begin);
                    //读取文件
                    fileStream.Read(bytes, 0, (int)fileStream.Length);
                    return bytes;
                }
            }
            else
            {
                return null;
            }
        }

        public static string LoadFile2String(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 读取文本：PC模式，移动模式
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadTextFile(string path)
        {
            if (path.Contains("file://"))
            {
                WWW w3 = new WWW(path);

                while (!w3.isDone)
                    System.Threading.Thread.Sleep(1);

                if (w3.error == null)
                    return w3.text;
                else
                {
                    return null;
                }
            }
            else
            {
                try
                {
                    return File.ReadAllText(path);
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }

        public static void WriteTextFile(string path, string text)
        {
            File.WriteAllText(path, text);
        }

        /// <summary>
        /// 带时间戳保存
        /// </summary>
        /// <param name="_strMsg"></param>
        /// <param name="_strFileName"></param>
        /// <param name="_strType"></param>
        /// <param name="_bAppend"></param>
        public static void SaveInfo(string _strMsg, string _strFileName = "CityEditorInfo", string _strType = ".txt", bool _bAppend = false)
        {
            if (!Directory.Exists(GetDataPath() + "SaveData/"))
                Directory.CreateDirectory(GetDataPath() + "SaveData/");

            string strPath = GetDataPath() + "SaveData/" + _strFileName + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + _strType;

            UTF8Encoding utf8BOM = new UTF8Encoding(true);
            StreamWriter sw = new StreamWriter(strPath, true, utf8BOM);
            sw.WriteLine(_strMsg);
            sw.Close();
        }

        /// <summary>
        /// 不加时间戳
        /// </summary>
        /// <param name="_strMsg"></param>
        /// <param name="_strFileName"></param>
        /// <param name="_strType"></param>
        /// <param name="_bAppend"></param>
        public static void SimpleSaveInfo(string _strMsg, string _strFileName = "CityEditorInfo", string _strType = ".txt", bool _bAppend = false)
        {
            if (!Directory.Exists(GetDataPath() + "SaveData/"))
                Directory.CreateDirectory(GetDataPath() + "SaveData/");

            string strPath = GetDataPath() + "SaveData/" + _strFileName + _strType;

            UTF8Encoding utf8BOM = new UTF8Encoding(true);
            StreamWriter sw = new StreamWriter(strPath, true, utf8BOM);
            sw.WriteLine(_strMsg);
            sw.Close();
        }

        #endregion

        #region 文件存在性
        public static bool IsDirExists(string path)
        {
            return Directory.Exists(path);
        }

        public static bool IsFileExists(string path)
        {
            return File.Exists(path);
        }
        #endregion

        #region 平台相关
        /// <summary>
        /// 获取平台dataPath
        /// </summary>
        /// <returns></returns>
        public static string GetDataPath()
        {
            string path = "";
            if (UnityEngine.Application.platform == RuntimePlatform.Android || UnityEngine.Application.platform == RuntimePlatform.IPhonePlayer)
            {
                path = UnityEngine.Application.persistentDataPath + "/";
            }
            else if (UnityEngine.Application.platform == RuntimePlatform.WindowsPlayer)
            {
                path = UnityEngine.Application.dataPath + "/";
            }
            else if (UnityEngine.Application.platform == RuntimePlatform.WindowsEditor)
            {
                path = UnityEngine.Application.dataPath + "/../";
            }
            else
            {
                path = UnityEngine.Application.persistentDataPath + "/";
            }

            return path;
        }
        #endregion

        #region 类型判断
        public static bool IsPic(string fileName)
        {
            string postFix = GetFilePostfix(fileName);
            return postFix == "png"
                || postFix == "PNG"
                || postFix == "jpg"
                || postFix == "JPG"
                || postFix == "jpeg"
                || postFix == "JPEG";
        }

        public static bool IsFbx(string fileName)
        {
            string postFix = GetFilePostfix(fileName);
            return postFix.ToLower().Equals("fbx");
        }

        public static bool IsAB(string fileName)
        {
            string postFix = GetFilePostfix(fileName);
            return postFix.ToLower().Equals("assetbundle");
        }
        #endregion
    }
}
