using UnityEngine;
using System.Collections;

public class DelayedGravity : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Turn off gravity to prevent instant fall
        rb.useGravity = false;

        // Start coroutine to enable it after a short delay
        StartCoroutine(EnableGravityAfterDelay());
    }

    IEnumerator EnableGravityAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);  // Wait a fraction of a second
        rb.useGravity = true;
    }
}
