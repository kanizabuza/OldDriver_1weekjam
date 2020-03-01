using UnityEngine;

public class Singleton : MonoBehaviour
{
    static bool existsInstance = false;

    void Awake()
    {
        if (existsInstance) {
            Destroy(gameObject);
            return;
        }

        existsInstance = true;
        DontDestroyOnLoad(gameObject);
    }
}
