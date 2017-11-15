using UnityEngine;

public class PreserveOnReload : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}