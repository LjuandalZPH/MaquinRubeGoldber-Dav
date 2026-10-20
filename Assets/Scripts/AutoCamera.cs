using System.Collections;
using UnityEngine;

public class SecuenciaCamara : MonoBehaviour
{
    public Transform objetivoGiro;   // El punto alrededor del cual va a girar
    public Transform objetivoNuevo;  // A dónde se moverá o mirará después
    public float velocidadGiro = 30f;
    public float tiempoDeGiro = 5f;  // Cuántos segundos estará girando

    void Start()
    {
        // Iniciamos la secuencia automáticamente al darle al Play
        StartCoroutine(RutinaCinematica());
    }

    IEnumerator RutinaCinematica()
    {
        // --- PASO 1: Girar durante un tiempo determinado ---
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoDeGiro)
        {
            if (objetivoGiro != null)
            {
                transform.RotateAround(objetivoGiro.position, Vector3.up, velocidadGiro * Time.deltaTime);
                transform.LookAt(objetivoGiro.position);
            }

            tiempoTranscurrido += Time.deltaTime;
            yield return null; // Espera al siguiente frame
        }

        // --- PASO 2: Hacer otra cosa después del tiempo ---
        Debug.Log("¡El tiempo de giro terminó! Cambiando de acción...");

        // Aquí puedes hacer que mire a otro objeto, se mueva, etc.
        if (objetivoNuevo != null)
        {
            transform.LookAt(objetivoNuevo.position);
        }
    }
}