using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public string InputFieldData { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This object will not be destroyed on scene load
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
