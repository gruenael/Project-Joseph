using UnityEngine;

public class AudioPreviewerCleaner : MonoBehaviour
{
    void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "AudioPreviewer")
            {
                Destroy(obj);
            }
        }
    }
}
