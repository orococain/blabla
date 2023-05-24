using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenShake : MonoBehaviour
{
    public AnimationCurve Curve;
    public void  shake(float amount)
    {
        StartCoroutine(Shaking());
    }
    
    
    public float shakingMagnitude = 0.1f;
    public float shakingDuration = 0.5f;
    IEnumerator Shaking()
    {
        Vector3 startingPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakingDuration)
        {
            elapsedTime += Time.deltaTime;
            float magnitude = Curve.Evaluate(shakingDuration - elapsedTime) / shakingDuration;

            float x = Mathf.Lerp(-1f, 1f, Random.value) * shakingMagnitude * magnitude;
            float y = Mathf.Lerp(-1f, 1f, Random.value) * shakingMagnitude * magnitude;

            transform.position = startingPosition + new Vector3(x, y, 0f) * magnitude;

            yield return null;
        }

        transform.position = startingPosition;
    }
}
