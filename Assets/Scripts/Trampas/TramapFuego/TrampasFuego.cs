using System.Collections;
using UnityEngine;

public class TrampasFuego : MonoBehaviour
{
    [SerializeField] private float daño;

    [Header("Tiempos de la trampa de fuego")]
    [SerializeField] private float retrasoActivar;
    [SerializeField] private float tiempoActivo;
    private Animator anim;
    private SpriteRenderer spriteRender;

 
    private bool triggered; //Cuando la tramapa se activa
    private bool activo; //Cuando la trampa esta activa y pude dañar añ jugador

    private VidaPersonaje vidaPersonaje;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (vidaPersonaje != null && activo)
            vidaPersonaje.HacerDaño(daño);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            vidaPersonaje = collision.GetComponent<VidaPersonaje>();

            if (!triggered)
                StartCoroutine(ActivateFiretrap());

            if (activo)
                collision.GetComponent<VidaPersonaje>().HacerDaño(daño);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            vidaPersonaje = null;
    }

    private IEnumerator ActivateFiretrap()
    {
        //Cambia el sprite a rojo para avisar al jugador y activa la trampa
        triggered = true;
        spriteRender.color = Color.red;

        //Espera al delay, activa la trampa, activa la animación, devuelve el color al normal
        yield return new WaitForSeconds(retrasoActivar);
        spriteRender.color = Color.white; //turn the sprite back to its initial color
        activo = true;
        anim.SetBool("Activo", true);

        //Esperar x segundos, desactivar la trampa y resetear todas las variables y el animator
        yield return new WaitForSeconds(tiempoActivo);
        activo = false;
        triggered = false;
        anim.SetBool("Activo", false);
    }
}
