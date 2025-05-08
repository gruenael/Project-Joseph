using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReleaseGripBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public Material nearMaterial;
    public bool isColliding;
    public bool isReleased;

    //Michael Add
    VRNoteBlock noteScript;
    public bool isGripped;

    void Start()
    {
        noteScript = GetComponentInParent<VRNoteBlock>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering " + this.gameObject.name);
        if (other.GetComponent<HandAnimator>().isGripping) isGripped = true;
        if (!isColliding)
        {
            isColliding = true;
            StartCoroutine(IE_CheckForRelease(other));
        }
    }

    public IEnumerator IE_CheckForRelease(Collider collision)
    {
        GetComponent<MeshRenderer>().sharedMaterial = nearMaterial;
        yield return new WaitUntil(() => collision.GetComponent<HandAnimator>().isGripping == false);
        // Michael add
        if (isGripped) {
            isReleased = true;
            isColliding = false;
            isGripped = false;
            noteScript.EndOfLifetime(isReleased);
            Debug.Log("Released Grip " + isReleased);
        }
        // End Michael add

        float time = 0;
        while (time <= 1)
        {
            foreach (var material in GetComponent<MeshRenderer>().materials)
            {
                material.SetFloat("_cutoff", time / 1);
                yield return null;
            }
            time += Time.deltaTime;
            yield return null;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        isReleased = false;
        noteScript.PlaySFX(noteScript.hitClip);
    }
}

