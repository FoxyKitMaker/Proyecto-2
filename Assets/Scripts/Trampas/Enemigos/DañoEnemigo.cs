using UnityEngine;

public class DañoEnemigo : MonoBehaviour
{
    [SerializeField] protected float daño;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<VidaPersonaje>().HacerDaño(daño);
    }
}
