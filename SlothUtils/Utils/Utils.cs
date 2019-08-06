using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SlothUtils
{
    public static class Utils
    {
        #region String
        public static bool HasChinese(this string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        public static bool IsLegelString(string tarStr)
        {
            return (tarStr != "" &&
                tarStr != "null" &&
                tarStr != null);
        }
        /// <summary>
        /// 纯数字
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        public static bool IsInt(string _str)
        {
            Regex regex = new Regex(@"^\d+$");

            // 区号纯数字
            return regex.IsMatch(_str);
        }

        /// <summary>
        /// 只允许是：字母，数字，汉字
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        public static bool IsCharOrIntOrChinese(string _str)
        {
            return Regex.IsMatch(_str, @"^[A-z0-9\u4e00-\u9fa5]+$");
        }
        #endregion

        #region Equal
        public static bool IsEqual(string _o1, string _o2)
        {
            return _o1.Equals(_o2);
        }

        public static bool IsEqual(int _o1, int _o2)
        {
            return _o1.Equals(_o2);
        }

        public static bool IsEqual(float _o1, float o2)
        {
            float fdis = _o1 - o2;
            fdis = fdis < 0 ? -fdis : fdis;
            return fdis < 0.001;
        }

        public static bool IsNull(string _o)
        {
            return string.IsNullOrEmpty(_o);
        }
        #endregion

        #region 资源加载相关


        /// <summary>
        /// 根据URL，加载Texture
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static IEnumerator LoadTexture(string url, Action<Texture2D> cb)
        {
            //这里的url可以是web路径也可以是本地路径file://  
            WWW www = new WWW(url);
            //挂起程序段，等资源下载完成后，继续执行下去  
            yield return www;

            //判断是否有错误产生  
            if (string.IsNullOrEmpty(www.error))
            {
                //把下载好的图片回调给调用者  
                cb.Invoke(www.texture);
                //释放资源  
                www.Dispose();
            }
        }

        /// <summary>
        /// 根据URL ，记载AB
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ab"></param>
        /// <returns></returns>
        public static IEnumerator LoadAssetBundle(string url, Action<AssetBundle> ab)
        {
            WWW www = new WWW(url);
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                ab.Invoke(www.assetBundle);
                www.Dispose();
            }
        }
        #endregion

        #region 资源本地保存
        /// <summary>
        /// 流，存为Png格式图片
        /// </summary>
        /// <param name="incomingTexture"></param>
        /// <param name="pathName">全路径</param>
        public static void SaveTextureFile(Texture2D incomingTexture, string pathName)
        {
            byte[] bytes = incomingTexture.EncodeToPNG();
            string dir = Path.GetDirectoryName(pathName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllBytes(pathName, bytes);
        }

        /// <summary>
        /// 保存RenderTexture，到本地
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool SaveRenderTextureToPNG(RenderTexture rt, string filename)
        {
            RenderTexture prev = RenderTexture.active;
            RenderTexture.active = rt;
            Texture2D png = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
            png.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

            SaveTextureFile(png, filename);

            Texture2D.DestroyImmediate(png);
            png = null;
            RenderTexture.active = prev;
            return true;

        }
        #endregion
        
        #region Transform相关

        /// <summary>
        /// 整体缩放
        /// </summary>
        /// <param name="tar"></param>
        /// <param name="multi"></param>
        /// <returns></returns>
        public static Vector3 GetScaledVector(this Vector3 tar, float multi)
        {
            return new Vector3(tar.x * multi, tar.y * multi, tar.z * multi);
        }


        /// <summary>
        /// 遍历Transform
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="Act">(Transform tf, Transform tfParent)</param>
        public static void RecurTraverseTransform(Transform tf, Action<Transform, Transform> Act)
        {
            int count = tf.childCount;
            if (0 == count)
            {
                Act(tf, tf);
                return;
            }
            for (int i = 0; i < count; i++)
            {
                var child = tf.GetChild(i);
                if (Act != null)
                {
                    Act(child, tf);
                }
                RecurTraverseTransform(child, Act);
            }
        }
        public static Vector3 GetMoveInput(bool allowArrow = true)
        {
            float kz = 0;
            if (allowArrow)
            {
                kz += Input.GetKey(KeyCode.UpArrow) ? 1f : 0f * 1f;
                kz += Input.GetKey(KeyCode.DownArrow) ? -1f : 0f * 1f;
            }
            kz += Input.GetKey(KeyCode.W) ? 1f : 0f * 1f;
            kz += Input.GetKey(KeyCode.S) ? -1f : 0f * 1f;
            float kx = 0;
            if (allowArrow)
            {
                kx -= Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f * 1f;
                kx -= Input.GetKey(KeyCode.RightArrow) ? 1f : 0f * 1f;
            }
            kx -= Input.GetKey(KeyCode.A) ? -1f : 0f * 1f;
            kx -= Input.GetKey(KeyCode.D) ? 1f : 0f * 1f;
            return new Vector3(kx, 0, kz);
        }

        /// <summary>获取朝目标方向前进后退左右平移后, 坐标的改变</summary>
        /// <param name="rotH">水平面旋转方向</param>
        /// <param name="dirX">左右移动距离</param>
        public static Vector3 MoveTowards(float rotH, float dirX, float dirZ)
        {
            float dx = dirX * Mathf.Sin(rotH) + dirZ * Mathf.Cos(rotH);
            float dz = dirX * Mathf.Cos(rotH) - dirZ * Mathf.Sin(rotH);
            return new Vector3(dx, 0, dz);
        }
        public static Vector3 MoveTowards(float rotH, Vector3 dir)
        {
            float dx = dir.x * Mathf.Sin(rotH) + dir.z * Mathf.Cos(rotH);
            float dz = dir.x * Mathf.Cos(rotH) - dir.z * Mathf.Sin(rotH);
            return new Vector3(dx, 0, dz);
        }

        /// 遍历Transform子集并执行operate
        /// 用例 1: SongeUtil.forAllChildren(gameObject,tar => {tar.transform.position = Vector3.zero;});
        /// </summary>
        public static void ForAllChildren(GameObject target, Action<GameObject> operate, bool includeTarget = true)
        {
            if (target == null)
                return;

            if (includeTarget)
                operate(target);
            for (int i = 0, length = target.transform.childCount; i < length; i++)
            {
                Transform childTran = target.transform.GetChild(i);
                operate(childTran.gameObject);
                ForAllChildren(childTran.gameObject, operate, false);
            }
        }

        /// <summary>
        /// 递归向上查找第一个挂有指定脚本的对象；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tf"></param>
        /// <returns></returns>
        public static GameObject FindComponentInParent<T>(Transform tf) where T : Component
        {
            if (tf != null)
            {
                var g = tf.GetComponent<T>();
                if (g != null )
                {
                    return g.gameObject;
                }
                else if (tf.parent != null)
                {
                    var c = tf.parent.GetComponent<T>();
                    if (c != null)
                    {
                        return c.gameObject;
                    }
                    else
                    {
                        return FindComponentInParent<T>(tf.parent);
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 射线检测相关
        /// <summary>
        /// 获取鼠标点中的Mono对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetMonoByMouse<T>() where T : MonoBehaviour
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;
            if (Physics.Raycast(ray, out rh, 99999f))
            {
                return rh.collider.GetComponentInParent<T>();
            }
            return null;
        }

        /// <summary>
        /// 按层进行射线检测
        /// </summary>
        /// <param name="hit">导出碰撞信息</param>
        /// <param name="LayerNameList">层名称列表</param>
        /// <param name="targetCamera">目标相机</param>
        /// <returns>是否碰撞</returns>
        public static bool RayCastByLayer(ref RaycastHit hit, string[] LayerNameList = null, Camera targetCamera = null)
        {
            if (targetCamera == null)
                targetCamera = Camera.main;
            Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] raycastHitArr;
            if (LayerNameList == null)
                raycastHitArr = Physics.RaycastAll(ray);
            else
                raycastHitArr = Physics.RaycastAll(ray, 999999, LayerMask.GetMask(LayerNameList));
            foreach (RaycastHit rayCastHit in raycastHitArr)
            {
                hit = rayCastHit;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取鼠标点击位置
        /// </summary>
        /// <param name="_strGroundLay">检测层</param>
        /// <returns></returns>
        public static Vector3 GetWorldPosByMouse(string _strGroundLay)
        {
            Vector3 vPos = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, int.MaxValue, LayerMask.NameToLayer(_strGroundLay)))
            {
                vPos = hit.point;
            }
            return vPos;
        }

        /// <summary>
        /// Get Sphere Colliders
        /// </summary>
        /// <param name="_vPos"></param>
        /// <param name="_fDis"></param>
        /// <param name="_strTag"></param>
        /// <returns></returns>
        public static List<GameObject> GetAroundGameObject(Vector3 _vPos, float _fDis, string _strTag = "")
        {
            List<GameObject> pGo = new List<GameObject>();
            _fDis = _fDis < 0 ? 0 : _fDis;
            bool bCheckTag = !string.IsNullOrEmpty(_strTag);

            var cs = Physics.OverlapSphere(_vPos, _fDis);
            for (int i = 0; i < cs.Length; i++)
            {
                var c = cs[i];
                if (bCheckTag && c.gameObject.tag.Equals(_strTag) || !bCheckTag)
                {
                    pGo.Add(c.gameObject);
                }
            }
            return pGo;
        }
        #endregion

        #region Math

        /// <summary>获取法向量</summary>
        public static Vector3 GetNormalVector(Vector3 va, Vector3 vb, Vector3 vc)
        {
            //平面方程Ax+BY+CZ+d=0 行列式计算
            float A = va.y * vb.z + vb.y * vc.z + vc.y * va.z - va.y * vc.z - vb.y * va.z - vc.y * vb.z;
            float B = -(va.x * vb.z + vb.x * vc.z + vc.x * va.z - vc.x * vb.z - vb.x * va.z - va.x * vc.z);
            float C = va.x * vb.y + vb.x * vc.y + vc.x * va.y - va.x * vc.y - vb.x * va.y - vc.x * vb.y;
            //float D = -(va.x * vb.y * vc.z + vb.x * vc.y * va.z + vc.x * va.y * vb.z - va.x * vc.y * vb.z - vb.x * va.y * vc.z - vc.x * vb.y * va.z);
            float E = Mathf.Sqrt(A * A + B * B + C * C);
            Vector3 res = new Vector3(A / E, B / E, C / E);
            return (res);
        }

        /// <summary>
        /// Takes a Vector3 and turns it into a Vector2
        /// </summary>
        /// <returns>The vector2.</returns>
        /// <param name="target">The Vector3 to turn into a Vector2.</param>
        public static Vector2 Vector3ToVector2(Vector3 target)
        {
            return new Vector2(target.x, target.y);
        }

        /// <summary>
        /// Takes a Vector2 and turns it into a Vector3 with a null z value
        /// </summary>
        /// <returns>The vector3.</returns>
        /// <param name="target">The Vector2 to turn into a Vector3.</param>
        public static Vector3 Vector2ToVector3(Vector2 target)
        {
            return new Vector3(target.x, target.y, 0);
        }

        /// <summary>
        /// Takes a Vector2 and turns it into a Vector3 with the specified z value 
        /// </summary>
        /// <returns>The vector3.</returns>
        /// <param name="target">The Vector2 to turn into a Vector3.</param>
        /// <param name="newZValue">New Z value.</param>
        public static Vector3 Vector2ToVector3(Vector2 target, float newZValue)
        {
            return new Vector3(target.x, target.y, newZValue);
        }

        /// <summary>
        /// Rounds all components of a Vector3.
        /// </summary>
        /// <returns>The vector3.</returns>
        /// <param name="vector">Vector.</param>
        public static Vector3 RoundVector3(Vector3 vector)
        {
            return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
        }

        /// <summary>
        /// Returns a random vector3 from 2 defined vector3.
        /// </summary>
        /// <returns>The vector3.</returns>
        /// <param name="min">Minimum.</param>
        /// <param name="max">Maximum.</param>
        public static Vector3 RandomVector3(Vector3 minimum, Vector3 maximum)
        {
            return new Vector3(UnityEngine.Random.Range(minimum.x, maximum.x),
                                             UnityEngine.Random.Range(minimum.y, maximum.y),
                                             UnityEngine.Random.Range(minimum.z, maximum.z));
        }

        /// <summary>
        /// Rotates a point around the given pivot.
        /// </summary>
        /// <returns>The new point position.</returns>
        /// <param name="point">The point to rotate.</param>
        /// <param name="pivot">The pivot's position.</param>
        /// <param name="angle">The angle we want to rotate our point.</param>
        public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angle)
        {
            angle = angle * (Mathf.PI / 180f);
            var rotatedX = Mathf.Cos(angle) * (point.x - pivot.x) - Mathf.Sin(angle) * (point.y - pivot.y) + pivot.x;
            var rotatedY = Mathf.Sin(angle) * (point.x - pivot.x) + Mathf.Cos(angle) * (point.y - pivot.y) + pivot.y;
            return new Vector3(rotatedX, rotatedY, 0);
        }

        /// <summary>
        /// Rotates a point around the given pivot.
        /// </summary>
        /// <returns>The new point position.</returns>
        /// <param name="point">The point to rotate.</param>
        /// <param name="pivot">The pivot's position.</param>
        /// <param name="angles">The angle as a Vector3.</param>
        public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angle)
        {
            // we get point direction from the point to the pivot
            Vector3 direction = point - pivot;
            // we rotate the direction
            direction = Quaternion.Euler(angle) * direction;
            // we determine the rotated point's position
            point = direction + pivot;
            return point;
        }

        /// <summary>
        /// Rotates a point around the given pivot.
        /// </summary>
        /// <returns>The new point position.</returns>
        /// <param name="point">The point to rotate.</param>
        /// <param name="pivot">The pivot's position.</param>
        /// <param name="angles">The angle as a Vector3.</param>
        public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion quaternion)
        {
            // we get point direction from the point to the pivot
            Vector3 direction = point - pivot;
            // we rotate the direction
            direction = quaternion * direction;
            // we determine the rotated point's position
            point = direction + pivot;
            return point;
        }

        /// <summary>
        /// Computes and returns the angle between two vectors, on a 360° scale
        /// </summary>
        /// <returns>The <see cref="System.Single"/>.</returns>
        /// <param name="vectorA">Vector a.</param>
        /// <param name="vectorB">Vector b.</param>
        public static float AngleBetween(Vector2 vectorA, Vector2 vectorB)
        {
            float angle = Vector2.Angle(vectorA, vectorB);
            Vector3 cross = Vector3.Cross(vectorA, vectorB);

            if (cross.z > 0)
            {
                angle = 360 - angle;
            }

            return angle;
        }

        /// <summary>
        /// Returns the sum of all the int passed in parameters
        /// </summary>
        /// <param name="thingsToAdd">Things to add.</param>
        public static int Sum(params int[] thingsToAdd)
        {
            int result = 0;
            for (int i = 0; i < thingsToAdd.Length; i++)
            {
                result += thingsToAdd[i];
            }
            return result;
        }

        /// <summary>
        /// Returns the result of rolling a dice of the specified number of sides
        /// </summary>
        /// <returns>The result of the dice roll.</returns>
        /// <param name="numberOfSides">Number of sides of the dice.</param>
        public static int RollADice(int numberOfSides)
        {
            return (UnityEngine.Random.Range(1, numberOfSides));
        }

        /// <summary>
        /// Returns a random success based on X% of chance.
        /// Example : I have 20% of chance to do X, Chance(20) > true, yay!
        /// </summary>
        /// <param name="percent">Percent of chance.</param>
        public static bool Chance(int percent)
        {
            return (UnityEngine.Random.Range(0, 100) <= percent);
        }

        /// <summary>
        /// Moves from "from" to "to" by the specified amount and returns the corresponding value
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="amount">Amount.</param>
        public static float Approach(float from, float to, float amount)
        {
            if (from < to)
            {
                from += amount;
                if (from > to)
                {
                    return to;
                }
            }
            else
            {
                from -= amount;
                if (from < to)
                {
                    return to;
                }
            }
            return from;
        }


        /// <summary>
        /// Remaps a value x in interval [A,B], to the proportional value in interval [C,D]
        /// </summary>
        /// <param name="x">The value to remap.</param>
        /// <param name="A">the minimum bound of interval [A,B] that contains the x value</param>
        /// <param name="B">the maximum bound of interval [A,B] that contains the x value</param>
        /// <param name="C">the minimum bound of target interval [C,D]</param>
        /// <param name="D">the maximum bound of target interval [C,D]</param>
        public static float Remap(float x, float A, float B, float C, float D)
        {
            float remappedValue = C + (x - A) / (B - A) * (D - C);
            return remappedValue;
        }

        public static float RoundToClosest(float value, float[] possibleValues)
        {
            if (possibleValues.Length == 0)
            {
                return 0f;
            }

            float closestValue = possibleValues[0];

            foreach (float possibleValue in possibleValues)
            {
                if (Mathf.Abs(closestValue - value) > Mathf.Abs(possibleValue - value))
                {
                    closestValue = possibleValue;
                }
            }
            return closestValue;

        }

        #endregion

        #region Unity相关
        /// <summary>去除Unity实例化物体后添加的" (Instance)"字段</summary>
        public static string RemovePostfix_Instance(string str)
        {
            string backstr = " (Instance)";
            while (str.EndsWith(backstr))
                str = str.Substring(0, str.Length - backstr.Length);
            return str;
        }

        public static void RemoveChildren(Transform _tf)
        {
            if (_tf != null)
            {
                for (int i = _tf.childCount - 1; i >= 0; i--)
                {
                    GameObject obj = _tf.GetChild(i).gameObject;

                    GameObject.Destroy(obj);
                }
            }
        }

        /// <summary>
        /// 添加MeshCollider
        /// </summary>
        /// <param name="_obj"></param>
        public static void AddMeshCollider(GameObject _obj)
        {
            ForAllChildren(_obj, tar =>
            {
                if (tar.GetComponent<MeshFilter>() != null)
                {
                    if (tar.GetComponent<Collider>() != null)
                        UnityEngine.Object.Destroy(tar.GetComponent<Collider>());
                    tar.AddComponent<MeshCollider>();
                }
            });
        }

        /// <summary>
        /// Copy Component Value To Destination
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            System.Type type = original.GetType();
            var dst = destination.GetComponent(type) as T;
            if (!dst) dst = destination.AddComponent(type) as T;
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic) continue;
                field.SetValue(dst, field.GetValue(original));
            }
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
                prop.SetValue(dst, prop.GetValue(original, null), null);
            }
            return dst as T;
        }

        #endregion

        #region UGUI相关
        /// <summary>
        /// 判断鼠标是否在UI上
        /// </summary>
        /// <returns></returns>
        public static bool IsUI()
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //实例化点击事件
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            //将点击位置的屏幕坐标赋值给点击事件
            eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

            List<RaycastResult> results = new List<RaycastResult>();
            //向点击处发射射线
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            return results.Count > 0;
        }

        /// <summary>
        /// Mouse Is On InputField
        /// </summary>
        /// <returns></returns>
        public static bool IsOnInputField()
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //实例化点击事件
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            //将点击位置的屏幕坐标赋值给点击事件
            eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

            List<RaycastResult> results = new List<RaycastResult>();
            //向点击处发射射线
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            for (int i = 0; i < results.Count; i++)
            {
                GameObject obj = results[i].gameObject;
                if (obj.GetComponent<InputField>() != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 鼠标是否在UGUI上
        /// </summary>
        public static bool IsOnUI
        {
            get { return IsUI(); }
        }

        /// <summary>
        /// 设置RectTransform
        /// </summary>
        /// <param name="_tf"></param>
        /// <param name="_fWidth">宽度</param>
        /// <param name="_fHeight">高度</param>
        public static void ResetRectTransform(Transform _tf, float _fWidth = -1, float _fHeight = -1)
        {
            if (_tf != null)
            {
                RectTransform rtf = _tf.GetComponent<RectTransform>();

                if (-1 == _fWidth)
                {
                    _fWidth = rtf.sizeDelta.x;
                }

                if (-1 == _fHeight)
                {
                    _fHeight = rtf.sizeDelta.y;
                }

                if (rtf != null)
                {
                    rtf.sizeDelta = new Vector2(_fWidth, _fHeight);
                }
            }
        }

        /// <summary>
        /// 生成Sprite
        /// </summary>
        /// <param name="_tx"></param>
        /// <returns></returns>
        public static Sprite GetSprite(Texture2D _tx)
        {
            Sprite spr = null;
            if (_tx != null)
            {
                spr = Sprite.Create(_tx, new Rect(0, 0, _tx.width, _tx.height), Vector2.zero);
            }
            return spr;
        }

        /// <summary>
        /// Button Set Show Name
        /// </summary>
        /// <param name="_btn"></param>
        /// <param name="_strName"></param>
        public static void SetBtnName(UnityEngine.UI.Button _btn, string _strName)
        {
            if (_btn != null)
            {
                Text tx = _btn.GetComponentInChildren<Text>();
                tx.text = _strName;
            }
        }

        /// <summary>
        /// 设置滑动手感
        /// </summary>
        /// <param name="_tf"></param>
        /// <param name="_nLenth"></param>
        public static void ResetScrollSensitivity(Transform _tf, int _nLenth)
        {
            if (_tf != null && _nLenth > 0)
            {
                ScrollRect sr = _tf.GetComponent<ScrollRect>();
                if (sr != null)
                {
                    int nSss = _nLenth % 500;
                    nSss = nSss > 1 ? nSss : 1;
                    sr.scrollSensitivity = nSss;
                }
            }
        }

        #endregion

        #region Bounds
        /// <summary>
        /// Get Collider Bounds
        /// </summary>
        /// <param name="theObject"></param>
        /// <returns></returns>
        public static Bounds GetColliderBounds(GameObject theObject)
        {
            Bounds returnBounds;

            // if the object has a collider at root level, we base our calculations on that
            if (theObject.GetComponent<Collider>() != null)
            {
                returnBounds = theObject.GetComponent<Collider>().bounds;
                return returnBounds;
            }

            // if the object has a collider2D at root level, we base our calculations on that
            if (theObject.GetComponent<Collider2D>() != null)
            {
                returnBounds = theObject.GetComponent<Collider2D>().bounds;
                return returnBounds;
            }

            // if the object contains at least one Collider we'll add all its children's Colliders bounds
            if (theObject.GetComponentInChildren<Collider>() != null)
            {
                Bounds totalBounds = theObject.GetComponentInChildren<Collider>().bounds;
                Collider[] colliders = theObject.GetComponentsInChildren<Collider>();
                foreach (Collider col in colliders)
                {
                    totalBounds.Encapsulate(col.bounds);
                }
                returnBounds = totalBounds;
                return returnBounds;
            }

            // if the object contains at least one Collider2D we'll add all its children's Collider2Ds bounds
            if (theObject.GetComponentInChildren<Collider2D>() != null)
            {
                Bounds totalBounds = theObject.GetComponentInChildren<Collider2D>().bounds;
                Collider2D[] colliders = theObject.GetComponentsInChildren<Collider2D>();
                foreach (Collider2D col in colliders)
                {
                    totalBounds.Encapsulate(col.bounds);
                }
                returnBounds = totalBounds;
                return returnBounds;
            }

            returnBounds = new Bounds(Vector3.zero, Vector3.zero);
            return returnBounds;
        }

        /// <summary>
        /// Get Render Mesh Bounds
        /// </summary>
        /// <param name="theObject"></param>
        /// <returns></returns>
        public static Bounds GetRendererBounds(GameObject theObject)
        {
            Bounds returnBounds;

            // if the object has a renderer at root level, we base our calculations on that
            if (theObject.GetComponent<Renderer>() != null)
            {
                returnBounds = theObject.GetComponent<Renderer>().bounds;
                return returnBounds;
            }

            // if the object contains at least one renderer we'll add all its children's renderer bounds
            if (theObject.GetComponentInChildren<Renderer>() != null)
            {
                Bounds totalBounds = theObject.GetComponentInChildren<Renderer>().bounds;
                Renderer[] renderers = theObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    totalBounds.Encapsulate(renderer.bounds);
                }
                returnBounds = totalBounds;
                return returnBounds;
            }

            returnBounds = new Bounds(Vector3.zero, Vector3.zero);
            return returnBounds;
        }

        #endregion
        
        #region 鼠标操作
        public static float GetRotateY(Vector3 dir)
        {
            return Mathf.Atan2(dir.x, dir.z) / Mathf.PI * 180;
        }
        //示例 eularAngle = new Vector3( -GetRotatV, GetRotateY, 0)
        /// <summary>获取仰角</summary>
        public static float GetRotatV(Vector3 dir)
        {
            float len2 = dir.x * dir.x + dir.z * dir.z;
            float len = Mathf.Sqrt(len2);
            float atan = Mathf.Atan2(dir.y, len);
            return atan / Mathf.PI * 180;
        }

        /// <summary>获取鼠标在水平面的投影点</summary>
        public static Vector3 GetMousePointOnHorizontal(Camera tarCamera = null)
        {
            if (tarCamera == null)
                tarCamera = Camera.main;
            Ray ray = tarCamera.ScreenPointToRay(Input.mousePosition);
            float ratio = -ray.origin.y / ray.direction.y;
            var ret = ray.origin + ray.direction * ratio;
            return ret;
        }
        /// <summary>
        /// 获取鼠标在指定层碰撞点
        /// </summary>
        /// <param name="_vPos"></param>
        /// <param name="_nLayer"></param>
        /// <returns></returns>
        public static Vector3 GetPointOnLayer(string _strLayer)
        {
            int nLayer = LayerMask.NameToLayer(_strLayer);
            Vector3 vPos = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] pHit = Physics.RaycastAll(ray, int.MaxValue);
            for (int i = 0; i < pHit.Length; i++)
            {
                RaycastHit hit = pHit[i];
                if (hit.collider.gameObject.layer == nLayer)
                {
                    vPos = hit.point;
                    break;
                }
            }
            return vPos;
        }

        #endregion

    }
}

