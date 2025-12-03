using UnityEngine;

public class TestingInAwake : MonoBehaviour
{
    // Get data by key
    private void Awake()
    {
        // Bridge.storage.Get("level", OnStorageGetCompleted);
    }

    private void OnStorageGetCompleted(bool success, string data)
    {
        // Loading succeeded
        if (success)
        {
            if (data != null)
            {
                Debug.Log(data);
                Debug.loge
            }
            else
            {
                // No data for the key 'level'
            }
        }
        else
        {
            // Error, something went wrong
        }
    }
}
