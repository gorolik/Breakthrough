using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
}
