using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public AudioSource previewSource;
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", 1f);
            Load();
        } else {
            Load();
        }

        ApplyVolume();
    }

    void Update()
    {
        if (volumeSlider.value != PlayerPrefs.GetFloat("musicVolume")) {
            Save();
            ApplyVolume(); // Update previewSource volume in real time
        }
    }

    public void ChangeVolume() {
        Save();
        ApplyVolume();
    }

    private void Load() {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save() {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    private void ApplyVolume() {
    float volumeValue = volumeSlider.value;

    if (previewSource != null)
    {
        // Scale down preview volume to 20% of slider value (adjust as needed)
        previewSource.volume = volumeValue * 0.20f;
    }

    // Keep or remove this depending on whether you want global volume to be affected
    AudioListener.volume = volumeValue;
}

}
