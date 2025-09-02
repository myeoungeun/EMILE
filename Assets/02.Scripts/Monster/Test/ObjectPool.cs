using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T prefab;
    private readonly Transform parent;
    private readonly Queue<T> pool = new();

    public ObjectPool(T prefab, int initialCount, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialCount; i++)
        {
            T instance = Object.Instantiate(prefab, parent);
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
        }
    }

    public T Get()
    {
        T instance;
        if (pool.Count > 0)
        {
            instance = pool.Dequeue();
        }
        else
        {
            instance = Object.Instantiate(prefab, parent);
        }

        instance.gameObject.SetActive(true);
        return instance;
    }

    public void Return(T instance)
    {
        instance.transform.parent = parent;
        instance.gameObject.SetActive(false);
        pool.Enqueue(instance);
    }
}