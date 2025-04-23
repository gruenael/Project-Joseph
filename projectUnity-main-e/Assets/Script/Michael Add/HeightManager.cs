using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightManager : MonoBehaviour
{
    public GameObject XROrigin;
    [SerializeField] Slider heightSlider;
    public float minimumHeight = 1f;

    Vector3 defaultHeight;

    // Start is called before the first frame update
    void Start()
    {
        heightSlider.value = 0.35f;
        
        if (!PlayerPrefs.HasKey("height")) {
            PlayerPrefs.SetFloat("height", heightSlider.value);
            defaultHeight = new Vector3(XROrigin.transform.position.x, minimumHeight + heightSlider.value, XROrigin.transform.position.z);
            XROrigin.transform.position = defaultHeight;
            Load();
        } else {
            Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        XROrigin.transform.position = new Vector3(XROrigin.transform.position.x, minimumHeight + heightSlider.value, XROrigin.transform.position.z);
    }

    public void ChangeHeight() {
        XROrigin.transform.position = new Vector3(XROrigin.transform.position.x, minimumHeight + heightSlider.value, XROrigin.transform.position.z);
        Save();
    }

    private void Load() {
        heightSlider.value = PlayerPrefs.GetFloat("height");
    }

    private void Save() {
        PlayerPrefs.SetFloat("height", heightSlider.value);
    }
}
