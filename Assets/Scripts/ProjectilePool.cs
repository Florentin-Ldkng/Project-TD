using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    private static GameObject _ProjectileEmpty;

    private static Dictionary<GameObject, ObjectPool<GameObject>> _objPool;
    private static Dictionary<GameObject, GameObject> _prefabLink;

    private void Awake()
    {
        _objPool = new Dictionary<GameObject, ObjectPool<GameObject>>();
        _prefabLink = new Dictionary<GameObject, GameObject>();

        _ProjectileEmpty = GameObject.Find("Pool");
    }

    private static void CreatePool(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () => CreateObj(prefab, pos, rot),
            actionOnGet: OnGetObj,
            actionOnRelease: OnReleaseObj,
            actionOnDestroy: OnDestroyObj);

        _objPool.Add(prefab, pool);
    }

    private static GameObject CreateObj(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        prefab.SetActive(false);

        GameObject obj = Instantiate(prefab,pos, rot);

        prefab.SetActive(true);

        obj.transform.SetParent(_ProjectileEmpty.transform);

        return obj;
    }

    private static void OnGetObj(GameObject gameObject)
    {

    }

    private static void OnReleaseObj(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private static void OnDestroyObj(GameObject gameObject)
    {
        if(_prefabLink.ContainsKey(gameObject))
        {
            _prefabLink.Remove(gameObject);
        }
    }

    public static GameObject SpawnObject(GameObject objToSpawn, Vector3 pos, Quaternion rot)
    {
        if (!_objPool.ContainsKey(objToSpawn))
        {
            CreatePool(objToSpawn, pos, rot);
        }

        GameObject obj = _objPool[objToSpawn].Get();

        if (obj != null)
        {
            if (!_prefabLink.ContainsKey(obj))
            {
                _prefabLink.Add(obj, objToSpawn);
            }
        }

        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);

        return obj;
    }

    public static void ReturnObjToPool(GameObject gameObject)
    {
        if (_prefabLink.TryGetValue(gameObject, out GameObject prefab))
        {
            if(gameObject.transform.parent != _ProjectileEmpty)
            {
                gameObject.transform.SetParent(_ProjectileEmpty.transform);
            }

            if(_objPool.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            {
                pool.Release(gameObject);
            }
        }
        else
        {
            Debug.LogError($"Trying to Release unpooled Obj: {gameObject.name}");
        }
    }
}
