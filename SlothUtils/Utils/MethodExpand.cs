using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlothUtils
{
    public static class MethodExpand
    {
        #region Dictionary扩展
        /// <summary>
        /// 尝试直接通过key直接获取TValue的值 : 如果不存在, 返回defaultValue
        /// </summary>
        public static TValue TryGetReturnValue<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default(TValue))
        {
            TValue ret;
            if (!dic.TryGetValue(key, out ret))
            {
                return defaultValue;
            }
            return ret;
        }

        /// <summary>
        /// 尝试获取给定key的value值,如果没有key,则建立默认value的指定key pair
        /// </summary>
        /// <returns>获取到的value值或默认value值</returns>
        public static TValue ForceGetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default(TValue))
            where TValue : new()
        {
            var value = dic.TryGetReturnValue(key, defaultValue);
            if (value == null)
            {
                value = new TValue();
                dic.AddOrReplace(key, value);
            }
            return value;

        }

        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        public static Dictionary<TKey, TValue> TryAddNoReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }
            return dict;
        }
        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

        /// <summary>
        /// 向字典中批量添加键值对
        /// </summary>
        /// <param name="isReplaceExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> dictValues,
            bool isReplaceExisted)
        {
            if (null == dictValues || null == dict)
            {
                return null;
            }
            var it = dictValues.GetEnumerator();
            while (it.MoveNext())
            {
                var item = it.Current;
                if (isReplaceExisted)
                {
                    dict.AddOrReplace(item.Key, item.Value);
                }
                else
                {
                    dict.TryAddNoReplace(item.Key, item.Value);
                }
            }
            return dict;
        }

        /// <summary>
        /// 向字典中批量删除键值对
        /// </summary>
        public static Dictionary<TKey, TValue> RemoveRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> dictValues)
        {
            if (null == dictValues || null == dict)
            {
                return null;
            }
            var it = dictValues.GetEnumerator();
            while (it.MoveNext())
            {
                var item = it.Current;
                dict.Remove(item.Key);
            }
            return dict;
        }
        /// <summary>添加进key-value(list)型字典, 并确保列表非空与不重复添加</summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="tar"></param>
        /// <returns></returns>
        public static Dictionary<T1, List<T2>> AddToList<T1, T2>(this Dictionary<T1, List<T2>> dic, T1 key, T2 tar)
        {
            if (!dic.ContainsKey(key))
                dic.Add(key, new List<T2>());
            List<T2> list = dic[key];
            if (!list.Contains(tar))
                list.Add(tar);
            return dic;
        }

        public static Dictionary<T1, T2> RemoveFromDic<T1, T2>(this Dictionary<T1, T2> dic, Predicate<T2> pred, Action<T2> operation = null)
        {
            Dictionary<T1, T2> deleteDic = new Dictionary<T1, T2>();
            foreach (var key in dic.Keys)
            {
                deleteDic.AddRep(key, dic[key]);
            }
            foreach (var key in deleteDic.Keys)
            {
                dic.Remove(key);
                operation(deleteDic[key]);
            }

            return dic;
        }

        /// <summary>替换/添加, 如果字典中已有则替换值</summary>
        public static Dictionary<T1, T2> AddRep<T1, T2>(this Dictionary<T1, T2> dic, T1 key, T2 value)
        {
            if (dic.ContainsKey(key))
                dic[key] = value;
            else
                dic.Add(key, value);
            return dic;
        }
        #endregion

        #region List 扩展

        public static List<T> Clone<T>(this List<T> list)
        {
            List<T> newList = new List<T>();
            for (int iList = 0, nList = list.Count; iList < nList; iList++)
            {
                newList.Add(list[iList]);
            }
            return newList;
        }

        public static List<T> TryDelete<T>(this List<T> list,T _item)
        {
            if (list.Contains(_item))
            {
                list.Remove(_item);
            }
            return list;
        }

        public static List<T> RemoveFromList<T>(this List<T> list, Predicate<T> pred, Action<T> operation = null)
        {
            List<T> deleteList = new List<T>();
            for (int i = 0, length = list.Count; i < length; i++)
            {
                if (pred(list[i]))
                    deleteList.Add(list[i]);
            }
            for (int i = 0, length = deleteList.Count; i < length; i++)
            {
                list.Remove(deleteList[i]);
                if (operation != null)
                    operation(deleteList[i]);
            }
            return list;
        }

        public static T FindItem<T>(this IEnumerable<T> enu, Predicate<T> judgeFunc)
        {
            foreach (var item in enu)
            {
                if (judgeFunc(item))
                    return item;
            }
            return default(T);
        }
        public static List<T> FindAllItem<T>(this IEnumerable<T> enu, Predicate<T> judgeFunc)
        {
            List<T> list = new List<T>();
            foreach (var item in enu)
            {
                if (judgeFunc(item))
                    list.Add(item);
            }
            return list;
        }
        #endregion

        #region object
        public static string ToJson(this object _obj)
        {
            return "";
        }

        public static T ToObject<T>(this string _strJson)
        {
            return default(T);
        }
        #endregion
    }
}
