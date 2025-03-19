using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMichael : MonoBehaviour
{
    public InputActionReference menuButton;
    // Start is called before the first frame update
    void Start()
    {
        // menuButton.action.started += ButtonPressed;
    }

    void ButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Button Pressed");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(PlayerPrefs.GetFloat("musicVolume"));
        if (menuButton.action.triggered)
        {
            Debug.Log("Button Pressed");
        }
    }

    
}
