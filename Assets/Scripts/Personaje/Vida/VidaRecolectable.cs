using UnityEngine;

public class VidaRecolectable : MonoBehaviour
{
    [SerializeField] private float valorVida;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("Player"))
        {
            collision.GetComponent<VidaPersonaje>().A�adirVida(valorVida);
            gameObject.SetActive(false);
        }
    }
}