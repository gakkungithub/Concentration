using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    SetupInstance();
                }
                else
                {
                    string typeName = typeof(T).Name;

                    Debug.Log("[Singleton] " + typeName + " instance already created: " +
                        _instance.gameObject.name);
                }
            }

            return _instance;
        }
    }

    // �V�[����������Singleton�Ƃ���
    protected static bool isSceneinSingleton = false;

    public virtual void Awake()
    {
        RemoveDuplicates();
    }

    private static void SetupInstance()
    {
        // lazy instantiation
        _instance = (T)FindObjectOfType(typeof(T));

        if (_instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;

            _instance = gameObj.AddComponent<T>();
            // �V�[����������Singleton�Ƃ���
            if (!isSceneinSingleton)
            {
                DontDestroyOnLoad(gameObj);
            }
        }
    }

    private void RemoveDuplicates()
    {
        if (_instance == null)
        {
            _instance = this as T;
            // �V�[����������Singleton�Ƃ���
            if (!isSceneinSingleton)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
