using UnityEngine;

public class ColorTransition : MonoBehaviour
{
    public float transitionSpeed = 1f;  // Speed of color transition
    private Renderer[] childRenderers;

    void Start()
    {
        childRenderers = GetComponentsInChildren<Renderer>();

        // Enable emission for all materials
        foreach (Renderer renderer in childRenderers)
        {
            if (renderer != null && renderer.material.HasProperty("_EmissionColor"))
            {
                renderer.material.EnableKeyword("_EMISSION");
            }
        }
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * transitionSpeed, 1f);
        Color lerpedColor = Color.Lerp(Color.blue, Color.red, t);
        Color emissionColor = lerpedColor * 0.5f;  // Increase intensity for glow

        foreach (Renderer renderer in childRenderers)
        {
            if (renderer != null && renderer.material.HasProperty("_EmissionColor"))
            {
                renderer.material.color = lerpedColor; // Base color
                renderer.material.SetColor("_EmissionColor", emissionColor); // Emission (glow)
            }
        }
    }
}
