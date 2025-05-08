using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [Tooltip("Kecepatan rotasi dalam derajat per detik")]
    public float rotationSpeed = 1.0f;

    private void Update()
    {
        if (RenderSettings.skybox != null)
        {
            float rotation = Time.time * rotationSpeed;
            RenderSettings.skybox.SetFloat("_Rotation", rotation);
        }
    }
}