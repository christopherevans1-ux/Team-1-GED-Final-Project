using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Replay : MonoBehaviour
{
    public Movement movement;
    public Rigidbody2D rb;

    private List<Vector2> positions = new List<Vector2>();
    private bool isReplaying = false;
    private void FixedUpdate()
    {
        if(!isReplaying && positions.Count < 3000)
            positions.Add(transform.position);
    }

    public void PlayReplay()
    {
        if (!isReplaying && positions.Count > 1)
            StartCoroutine(ReplayFunction());
    }

    IEnumerator ReplayFunction()
    {
        isReplaying = true;

        Vector2 originalPosition = transform.position;
        Vector2 originalVelocity = rb.linearVelocity;

        movement.enabled = false;
        rb.simulated = false;

        foreach(Vector2 position in positions)
        {
            transform.position = position;
            yield return new WaitForFixedUpdate();
        }

        transform.position = originalPosition;
        rb.position = originalPosition;
        rb.simulated = true;
        rb.linearVelocity = originalVelocity;

        movement.enabled = true;
        isReplaying = false;
    }
}
