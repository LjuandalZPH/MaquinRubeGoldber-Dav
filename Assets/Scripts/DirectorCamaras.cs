using System.Collections;
using UnityEngine;

[System.Serializable]
public struct DatosCamara
{
    public Camera camara;          // La cámara de este plano
    public float duracion;         // Cuánto tiempo dura este plano en segundos
    public Transform objetivoGiro; // El punto alrededor del cual girará (opcional)
    public float velocidadGiro;    // Velocidad a la que gira en esta toma
}

public class DirectorCamaras : MonoBehaviour
{
    public DatosCamara[] secuenciaDeTOMAS; // Lista personalizada de cada plano

    void Start()
    {
        if (secuenciaDeTOMAS.Length > 0)
        {
            StartCoroutine(EjecutarSecuenciaCinematica());
        }
    }

    IEnumerator EjecutarSecuenciaCinematica()
    {
        // 1. Apagamos todas las cámaras al inicio
        foreach (var toma in secuenciaDeTOMAS)
        {
            if (toma.camara != null)
                toma.camara.enabled = false;
        }

        // 2. Recorremos cada toma de la secuencia paso a paso
        for (int i = 0; i < secuenciaDeTOMAS.Length; i++)
        {
            DatosCamara tomaActual = secuenciaDeTOMAS[i];

            if (tomaActual.camara == null) continue;

            // Encendemos la cámara de esta toma
            tomaActual.camara.enabled = true;

            float tiempoTranscurrido = 0f;

            // Mantenemos esta cámara activa durante su tiempo configurado
            while (tiempoTranscurrido < tomaActual.duracion)
            {
                // Si esta toma tiene un objetivo asignado, la cámara rota a su alrededor
                if (tomaActual.objetivoGiro != null)
                {
                    tomaActual.camara.transform.RotateAround(tomaActual.objetivoGiro.position, Vector3.up, tomaActual.velocidadGiro * Time.deltaTime);
                    tomaActual.camara.transform.LookAt(tomaActual.objetivoGiro.position);
                }

                tiempoTranscurrido += Time.deltaTime;
                yield return null;
            }

            // Apagamos la cámara actual antes de pasar a la siguiente
            tomaActual.camara.enabled = false;
        }

        Debug.Log("¡Secuencia cinemática finalizada con éxito!");
    }
}