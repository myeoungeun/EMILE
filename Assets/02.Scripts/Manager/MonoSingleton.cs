using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton <T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                instance = obj.AddComponent<T>();
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null) // 생성을안해도 어느 씬이든 사용할수있게끔 
        {
            if (transform.parent == null)
                DontDestroyOnLoad(gameObject);
            instance = GetComponent<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
