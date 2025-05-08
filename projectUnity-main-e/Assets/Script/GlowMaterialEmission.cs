using UnityEngine;

public class RainbowGlowEmission : MonoBehaviour
{
    [Header("Material Settings")]
    [SerializeField] private Material glowMaterial;
    [SerializeField] private string emissionColorProperty = "_EmissionColor";

    [Header("Rainbow Settings")]
    [SerializeField] private float colorCycleDuration = 10.0f; // Time for full RGB cycle
    [SerializeField] private float emissionIntensity = 10.0f;
    [SerializeField] private bool smoothTransition = true;

    private float cycleTimer = 0f;
    private readonly Color[] rainbowColors = new Color[]
    {
        Color.red,
        new Color(1, 0.5f, 0), // Orange
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        Color.magenta,
        Color.red // Complete the loop
    };

    private void Start()
    {
        // Initialize with first color
        glowMaterial.SetColor(emissionColorProperty, rainbowColors[0] * emissionIntensity);
    }

    private void Update()
    {
        cycleTimer += Time.deltaTime;
        float progress = Mathf.Repeat(cycleTimer / colorCycleDuration, 1f);

        if (smoothTransition)
        {
            // Smooth RGB spectrum transition
            float hue = Mathf.Repeat(progress, 1f);
            Color currentColor = Color.HSVToRGB(hue, 1f, 1f);
            glowMaterial.SetColor(emissionColorProperty, currentColor * emissionIntensity);
        }
        else
        {
            // Discrete color transition through rainbow array
            float segmentProgress = progress * (rainbowColors.Length - 1);
            int colorIndex = Mathf.FloorToInt(segmentProgress);
            float lerpValue = segmentProgress - colorIndex;

            Color currentColor = Color.Lerp(rainbowColors[colorIndex], 
                                         rainbowColors[colorIndex + 1], 
                                         lerpValue);
            glowMaterial.SetColor(emissionColorProperty, currentColor * emissionIntensity);
        }
    }

    public void SetCycleSpeed(float newDuration)
    {
        colorCycleDuration = Mathf.Max(0.1f, newDuration);
    }

    public void SetIntensity(float newIntensity)
    {
        emissionIntensity = Mathf.Max(0, newIntensity);
    }

    private void OnDestroy()
    {
        // Reset material when this object is destroyed
        if (glowMaterial != null)
        {
            glowMaterial.SetColor(emissionColorProperty, Color.black);
        }
    }
}