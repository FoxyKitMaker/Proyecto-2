using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaparece : MonoBehaviour
{
    private Transform checkpointActual;
    private VidaPersonaje vidaPersonaje;

    private void Awake()
    {
        vidaPersonaje = GetComponent<VidaPersonaje>();
    }

    public void Reaparecer()
    {
        vidaPersonaje.Reaparece(); //Restaura la vida del personaje y resetea las animaciones
        transform.position = checkpointActual.position; //Mueve al jugador al checkpoint
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            checkpointActual = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Aparece");
        }
    }
}
