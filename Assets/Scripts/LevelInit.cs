using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInit : MonoBehaviour
{
    public float fadeDuration = 2.0f; // Duración del desvanecimiento en segundos

    private SpriteRenderer spriteRenderer;
    private float timer = 0.0f;
    private bool fading = false;

    void Start()
    {
        // Obtener el componente SpriteRenderer de la imagen
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Iniciar el desvanecimiento
        StartFade();
    }

    void Update()
    {
        if (fading)
        {
            // Incrementar el temporizador
            timer += Time.deltaTime;

            // Calcular el valor alfa basado en el tiempo transcurrido
            float alpha = 1.0f - (timer / fadeDuration);

            // Aplicar el valor alfa al color del material de la imagen
            Color color = spriteRenderer.color;
            color.a = Mathf.Clamp01(alpha);
            spriteRenderer.color = color;

            // Si el desvanecimiento ha terminado, detener el proceso
            if (timer >= fadeDuration)
            {
                fading = false;
            }
        }
    }

    void StartFade()
    {
        // Iniciar el desvanecimiento
        fading = true;
    }
}
