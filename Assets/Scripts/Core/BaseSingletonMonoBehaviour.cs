using Core;
using UnityEngine;

public class BaseSingletonMonoBehaviour<T> : BaseMonoBehaviour where T : BaseMonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance != null) return instance;
         
            instance = FindObjectOfType<T>();

            if (instance != null) return instance;
            
            var obj = new GameObject
            {
                name = typeof(T).Name
            };
            instance = obj.AddComponent<T>();
            return instance;
        }
    }

    // Optionally, you can define additional Singleton behavior here, such as Awake or OnDestroy
    // Example:
    /*
    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject); // Optional: Keep the GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy this GameObject
        }
    }
    */
}