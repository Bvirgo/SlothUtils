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
    public static class U3DUtils
    {
        /// <summary>
        /// 添加Collider
        /// </summary>
        /// <param name="decorateObj"></param>
        public static void AddOrReplaceBoxCollider(GameObject decorateObj)
        {
            var mrs = decorateObj.GetComponentsInChildren<Renderer>(true);
            if (mrs.Length != 0)
            {
                foreach (var mr in mrs)
                {
                    var box = mr.gameObject.GetComponent<Collider>();
                    if (box == null)
                    {
                        mr.gameObject.AddComponent<BoxCollider>();
                    }
                }
            }
        }

        /// <summary>
        /// UGUI设置默认Transfrom
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        public static void SetDefaultTransfromWithParent(this Transform child, Transform parent)
        {
            child.SetParent(parent, false);
            child.localPosition = Vector3.zero;
            child.localEulerAngles = Vector3.zero;
            child.localScale = Vector3.one;
            child.SetAsLastSibling();
        }

        /// <summary>
        /// UGUI子对象平铺父对象
        /// </summary>
        /// <param name="go"></param>
        public static void SetTransformExpandParent(this Transform go)
        {
            RectTransform rec = go as RectTransform;
            if (rec != null)
            {
                rec.anchorMin = Vector2.zero;
                rec.anchorMax = Vector2.one;
                rec.offsetMin = rec.offsetMax = Vector2.zero;
            }
        }

        /// <summary>
        /// 获取所有的子对象,包括自身
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static List<Transform> GetAllChilds(this Transform go)
        {
            var childs = new List<Transform>() { go };

            GetAllChilds(go, childs);
            return childs;
        }

        private static void GetAllChilds(Transform go, List<Transform> cs)
        {
            cs = cs ?? new List<Transform>();
            for (int i = 0; i < go.childCount; i++)
            {
                var c = go.GetChild(i);
                if (c != null) cs.Add(c);
                GetAllChilds(c, cs);
            }
        }

        /// <summary>
        /// 鼠标是否在UGUI上
        /// </summary>
        /// <returns></returns>
        public static bool IsUI()
        {
            if (EventSystem.current != null)
            {
                Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                return results.Count > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是指定UI界面
        /// </summary>
        /// <param name="uiName"></param>
        /// <returns></returns>
        public static bool IsCurrentUI(string uiName)
        {
            if (EventSystem.current != null)
            {
                Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

                foreach (var item in results)
                {
                    if (item.gameObject.name.Equals(uiName))
                    {
                        return true;
                    }
                }

                return false;
            }
            else
                return false;
        }

        /// <summary>
        /// 浮点数等值判断
        /// </summary>
        /// <param name="double1"></param>
        /// <param name="double2"></param>
        /// <returns></returns>
        public static bool IsEquals(this float double1, float double2)
        {
            double difference = double1 * .0001;
            return Math.Abs(double1 - double2) <= difference;
        }

        /// <summary>
        /// 点击地面，获取坐标
        /// </summary>
        /// <param name="pos">获取点击到地面上的世界坐标</param>
        /// <param name="groundName">地面层名称</param>
        /// <returns></returns>
        public static bool ClickGround(out Vector3 pos,string groundName="ground")
        {
            pos = Vector3.zero;

            var ly = 1 << LayerMask.NameToLayer(groundName);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            var isClicked = Physics.Raycast(ray, out rayHit, float.MaxValue, ly);
            if (isClicked)
            {
                var go = rayHit.collider.gameObject;
                pos = rayHit.point;
            }
            return isClicked;
        }
    }
}

