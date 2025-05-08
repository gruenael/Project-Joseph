using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] string prefsKey;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(prefsKey)) {
            PlayerPrefs.SetFloat(prefsKey, 1f);
            Load();
        } else {
            Load();
        }
    }

    void Update()
    {
        if (volumeSlider.value != PlayerPrefs.GetFloat(prefsKey)) {
            Save();
        }
    
    }

    private void Load() {
        volumeSlider.value = PlayerPrefs.GetFloat(prefsKey);
    }

    private void Save() {
        PlayerPrefs.SetFloat(prefsKey, volumeSlider.value);
    }
}
