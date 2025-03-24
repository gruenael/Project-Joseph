using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TestMichael : MonoBehaviour
{
    public GameObject noteBlock;
    XRGrabInteractable grabInteractable;
    public GameObject blueCube;
    public GameObject redCube;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = noteBlock.GetComponent<XRGrabInteractable>();
        blueCube.gameObject.SetActive(false);
        redCube.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectEnter(XRBaseInteractor interactor)
    {
        if (grabInteractable.isSelected)
        {
            // isGripped = true;
            // Debug.Log("Gripped " + gameObject);
            blueCube.gameObject.SetActive(true);
            redCube.gameObject.SetActive(false);
        }
    }

    public void OnSelectExit(XRBaseInteractor interactor)
    {
        if (!grabInteractable.isSelected)
        {
            // isGripped = false;
            // Debug.Log("Let go of " );
            blueCube.gameObject.SetActive(false);
            redCube.gameObject.SetActive(true);
        }
    }

    
}
