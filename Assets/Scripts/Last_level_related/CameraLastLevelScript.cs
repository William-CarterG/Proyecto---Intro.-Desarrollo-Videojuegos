using System.Collections;
using UnityEngine;

public class Camera_last_level_script : MonoBehaviour
{
    public Transform player;
    public float xOffset = 0f;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public float shakeInterval = 5f;

    private Vector3 originalPos;
    private bool isShaking = false;

    void Start()
    {
        originalPos = transform.localPosition;
        StartCoroutine(ShakeRoutine());
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 newPosition = new Vector3(originalPos.x + xOffset, player.position.y, originalPos.z);
            if (!isShaking)
            {
                transform.position = newPosition;
            }
            else
            {
                transform.position = newPosition + GetShakeOffset();
            }
        }
    }

    IEnumerator ShakeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shakeInterval);
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        isShaking = false;
    }

    Vector3 GetShakeOffset()
    {
        float x = Random.Range(-1f, 1f) * shakeMagnitude;
        float y = Random.Range(-1f, 1f) * shakeMagnitude;
        return new Vector3(x, y, 0);
    }
}
