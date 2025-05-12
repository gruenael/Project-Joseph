using UnityEngine;
using System.Collections;

public class RotateAroundAnchor : MonoBehaviour
{
    public Transform anchorPoint;
    public float moveDistance = 2f;
    public float moveDuration = 1f;
    public float rotationSpeed = 10f;
    public float startRotationDelay = 0.5f;
    public float resetDuration = 2f;
    public float stopDelay = 60f; // how long ALL objects rotate before stopping

    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isRotating = false;
    private bool isResetting = false;

    private static float globalRotationStartTime;
    private static bool globalRotationActive = false;

    void Start()
    {
        if (anchorPoint == null) return;

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        StartCoroutine(RunLoop());
    }

    IEnumerator RunLoop()
    {
        while (true)
        {
            int index = transform.GetSiblingIndex();

            // Step 1: Move outward
            Vector3 directionFromAnchor = (initialPosition - anchorPoint.position).normalized;
            targetPosition = initialPosition + directionFromAnchor * moveDistance;

            yield return new WaitForSeconds(index * startRotationDelay);
            yield return StartCoroutine(MoveToPosition(targetPosition));

            // Step 2: Wait until all have moved out, only 1 object (index 0) starts the rotation timer
            if (index == 0)
            {
                globalRotationStartTime = Time.time;
                globalRotationActive = true;
            }

            // Step 3: Wait for own turn to start rotating
            yield return new WaitForSeconds(index * startRotationDelay);
            isRotating = true;

            // Step 4: Wait until the shared rotation timer expires
            while (Time.time - globalRotationStartTime < stopDelay)
            {
                yield return null;
            }

            // Step 5: Stop rotating all at once
            isRotating = false;

            // Step 6: Wait until all are done before resetting (only index 0 waits a moment and triggers reset)
            if (index == 0)
                yield return new WaitForSeconds(0.1f);

            yield return StartCoroutine(ResetToInitial());

            // Small delay before next loop
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        Vector3 startPos = transform.position;
        float t = 0;
        while (t < moveDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, target, t / moveDuration);
            yield return null;
        }
    }

    IEnumerator ResetToInitial()
    {
        isResetting = true;
        float t = 0;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (t < resetDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, initialPosition, t / resetDuration);
            transform.rotation = Quaternion.Lerp(startRot, initialRotation, t / resetDuration);
            yield return null;
        }

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        isResetting = false;
    }

    void Update()
{
    if (isRotating && !isResetting && anchorPoint != null)
    {
        transform.RotateAround(anchorPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

}
