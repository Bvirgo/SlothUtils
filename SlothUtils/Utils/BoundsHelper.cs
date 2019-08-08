using System.Collections.Generic;
using UnityEngine;

namespace SlothUtils
{
    public class BoundsHelper
    {
        /// <summary>
        /// 获取对象列表的外包围盒
        /// </summary>
        /// <param name="sceneObjects"></param>
        /// <returns></returns>
        public static Bounds GetBounds(List<GameObject> sceneObjects)
        {
            if (sceneObjects == null || sceneObjects.Count == 0)
            {
                Debug.Log("MultiObjBuilder: SceneObject List is Empty.");
                return new Bounds();
            }

            Vector3 center = Vector3.zero;
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
            Bounds[] boundsAll = new Bounds[sceneObjects.Count];
            if (sceneObjects != null)
            {
                for (int i = 0; i < sceneObjects.Count; i++)
                {
                    Bounds b = GetBounds(sceneObjects[i].transform);
                    boundsAll[i] = b;
                    center += b.center;
                }
                if (sceneObjects.Count != 0)
                {
                    center /= sceneObjects.Count;
                }
                bounds = new Bounds(center, Vector3.zero);

                for (int i = 0; i < boundsAll.Length; i++)
                {
                    bounds.Encapsulate(boundsAll[i]);
                }

            }
            return bounds;
        }

        /// <summary>
        /// 获取对象包围盒
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Bounds GetBounds(Transform transform)
        {
            if (transform == null)
            {
                Debug.Log("MultiObjBuilder: SceneObject is Empty.");
                return new Bounds();
            }

            Transform parent = transform;
            Vector3 center = Vector3.zero;
            Renderer[] renders = GetBoundRenderers(parent);

            foreach (Renderer child in renders)
            {
                center += child.bounds.center;
            }
            if (renders.Length != 0)
            {
                center /= renders.Length;
            }
            Bounds bounds = new Bounds(center, Vector3.zero);
            List<Collider> colliders = new List<Collider>();
            if (renders.Length == 0)
            {
                colliders.AddRange(transform.GetComponentsInChildren<Collider>());
            }
            if (colliders.Count > 0)
            {
                foreach (var collider in colliders)
                {
                    center += collider.bounds.center;
                }
                center /= colliders.Count;
            }
            bounds = new Bounds(center, Vector3.zero);
            foreach (Renderer child in renders)
            {
                bounds.Encapsulate(child.bounds);
            }
            foreach (var collider in colliders)
            {
                bounds.Encapsulate(collider.bounds);
            }
            if (bounds.center == Vector3.zero && bounds.size == Vector3.zero)
            {
                bounds.center = transform.position;
                bounds.size = Vector3.one;
            }
            return bounds;
        }

        /// <summary>
        /// 获取角色模型的包围盒(Bounds)
        /// </summary>
        /// <param name="root">根节点</param>
        /// <returns></returns>
        public static Bounds GetCharacterBounds(Transform root)
        {
            if (null == root)
            {
                Debug.Log("[ObjectBoundsMgr] GetPlayerBounds root is Empty.");
                return new Bounds();
            }

            Bounds bounds;
            Vector3 center = Vector3.zero;
            Collider[] colliders = root.GetComponentsInChildren<Collider>();

            if (null != colliders && colliders.Length > 0)
            {

                // 利用模型设置Colliders来计算Bounds
                foreach (var collider in colliders)
                {
                    center += collider.bounds.center;
                    //Debug.LogWarning($"获取到对象：{collider.gameObject.name}");
                }
                center /= colliders.Length;
                bounds = new Bounds(center, Vector3.zero);
                foreach (var collider in colliders)
                {
                    bounds.Encapsulate(collider.bounds);
                }
            }
            else
            {
                // 利用模型的Renderers来计算Bounds
                Renderer[] renderers = GetBoundRenderers(root);
                foreach (Renderer child in renderers)
                {
                    center += child.bounds.center;
                }
                if (renderers.Length != 0)
                {
                    center /= renderers.Length;
                }
                bounds = new Bounds(center, Vector3.zero);
                foreach (Renderer renderer in renderers)
                {
                    bounds.Encapsulate(renderer.bounds);
                }
            }

            return bounds;
        }

        private static Renderer[] GetBoundRenderers(Transform root)
        {
            Renderer[] renders = root.GetComponentsInChildren<Renderer>();

            //屏弊粒子系统的Renderer.
            List<Renderer> listRenderers = new List<Renderer>();
            for (int i = 0; i < renders.Length; i++)
            {

                ParticleSystem ps = renders[i].GetComponent<ParticleSystem>();
                if (ps != null || renders[i].bounds.size == Vector3.zero)
                {
                    continue;
                }
                if (renders[i] is BillboardRenderer)
                {
                    continue;
                }
                listRenderers.Add(renders[i]);
            }

            renders = listRenderers.ToArray();

            return renders;
        }

        /// <summary>
        /// 以左上顶点为第一个点，逆时针记录
        /// </summary>
        /// <param name="bos"></param>
        /// <returns></returns>
        public static List<Vector3> GetBoundsVectors(Bounds bos, float fScale = 1)
        {
            List<Vector3> vecLists = new List<Vector3>();
            Vector3 bSize = bos.extents * fScale;
            vecLists.Add(bos.center + new Vector3(-bSize.x, bSize.y, -bSize.z));
            vecLists.Add(bos.center + new Vector3(bSize.x, bSize.y, -bSize.z));
            vecLists.Add(bos.center + new Vector3(bSize.x, bSize.y, bSize.z));
            vecLists.Add(bos.center + new Vector3(-bSize.x, bSize.y, bSize.z));

            vecLists.Add(bos.center + new Vector3(-bSize.x, -bSize.y, -bSize.z));
            vecLists.Add(bos.center + new Vector3(bSize.x, -bSize.y, -bSize.z));
            vecLists.Add(bos.center + new Vector3(bSize.x, -bSize.y, bSize.z));
            vecLists.Add(bos.center + new Vector3(-bSize.x, -bSize.y, bSize.z));

            return vecLists;
        }

        /// <summary>
        /// 上下，左右，前后 六面中心点
        /// </summary>
        /// <param name="bos"></param>
        /// <returns></returns>
        public static List<Vector3> GetPanelCenterVecs(Bounds bos)
        {
            List<Vector3> vecLists = new List<Vector3>();
            Vector3 bSize = bos.extents;
            vecLists.Add(bos.center + new Vector3(0, bSize.y, 0));
            vecLists.Add(bos.center + new Vector3(0, -bSize.y, 0));
            vecLists.Add(bos.center + new Vector3(-bSize.x, 0, 0));
            vecLists.Add(bos.center + new Vector3(bSize.x, 0, 0));

            vecLists.Add(bos.center + new Vector3(0, 0, -bSize.z));
            vecLists.Add(bos.center + new Vector3(0, 0, bSize.z));
            return vecLists;
        }

        /// <summary>
        /// 判断一个Bound和其他Bounds是否有重叠的部分
        /// </summary>
        /// <param name="existBounds"></param>
        /// <param name="newBound"></param>
        /// <returns></returns>
        public static bool IsBoundsConflict(List<Bounds> existBounds, Bounds bound)
        {
            for (int i = 0; i < existBounds.Count; i++)
            {
                if (bound.Intersects(existBounds[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 俯视四个顶点
        /// </summary>
        /// <param name="bos"></param>
        /// <returns></returns>
        public static List<Vector2> GetLookDownVecs(Bounds bos)
        {
            List<Vector2> vecLists = new List<Vector2>();
            Vector3 bSize = bos.extents;
            Vector2 centerVe2 = new Vector2(bos.center.x, bos.center.z);
            vecLists.Add(centerVe2 + new Vector2(-bSize.x, -bSize.z));
            vecLists.Add(centerVe2 + new Vector2(bSize.x, -bSize.z));
            vecLists.Add(centerVe2 + new Vector2(bSize.x, bSize.z));
            vecLists.Add(centerVe2 + new Vector2(-bSize.x, bSize.z));
            return vecLists;
        }

        /// <summary>
        /// 根据bounds添加适配BoxCollider
        /// </summary>
        /// <param name="go"></param>
        public static void AddCollider(GameObject go)
        {
            Transform parent = go.transform;
            Vector3 postion = parent.position;
            Quaternion rotation = parent.rotation;
            Vector3 scale = parent.localScale;

            Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
            if (renders != null && renders.Length > 0)
            {
                parent.position = Vector3.zero;
                parent.rotation = Quaternion.Euler(Vector3.zero);
                parent.localScale = Vector3.one;

                Collider[] colliders = parent.GetComponentsInChildren<Collider>();
                foreach (Collider child in colliders)
                {
                    GameObject.DestroyImmediate(child);
                }
                Vector3 center = Vector3.zero;

                foreach (Renderer child in renders)
                {
                    center += child.bounds.center;
                }
                center /= renders.Length;
                Bounds bounds = new Bounds(center, Vector3.zero);
                foreach (Renderer child in renders)
                {
                    bounds.Encapsulate(child.bounds);
                }
                BoxCollider boxCollider = parent.gameObject.AddComponent<BoxCollider>();
                boxCollider.center = bounds.center - parent.position;
                boxCollider.size = bounds.size;

                parent.position = postion;
                parent.rotation = rotation;
                parent.localScale = scale;
            }

        }
    }

}
