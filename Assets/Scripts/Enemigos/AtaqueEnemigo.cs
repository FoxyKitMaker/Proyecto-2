using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEnemigo : MonoBehaviour
{
    [Header("Parámetros Ataque")]
    [SerializeField] private float cooldownAtaque;
    [SerializeField] private float rango;
    [SerializeField] private float daño;

    [Header("Parámetros collider")]
    [SerializeField] private float distanciaCollider;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Capa jugador")]
    [SerializeField] private LayerMask capaJugador;
    private float tiempoCooldown = Mathf.Infinity;

    //References
    private Animator anim;
    private VidaPersonaje vidaPersonaje;
    private PatrullaEnemigo patrullaEnemigo;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        patrullaEnemigo = GetComponentInParent<PatrullaEnemigo>();
    }

    private void Update()
    {
        tiempoCooldown += Time.deltaTime;

        //Atacar solo cuando el jugador esté en su campo de visión
        if (JugadorEnVista())
        {
            if (tiempoCooldown >= cooldownAtaque)
            {
                tiempoCooldown = 0;
                anim.SetTrigger("AtaqueFisico");
            }
        }

        if (patrullaEnemigo != null)
            patrullaEnemigo.enabled = !JugadorEnVista();
    }

    private bool JugadorEnVista()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rango * transform.localScale.x * distanciaCollider,
            new Vector3(boxCollider.bounds.size.x * rango, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, capaJugador);

        if (hit.collider != null)
            vidaPersonaje = hit.transform.GetComponent<VidaPersonaje>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rango * transform.localScale.x * distanciaCollider,
            new Vector3(boxCollider.bounds.size.x * rango, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DañoJugador()
    {
        if (JugadorEnVista())
            vidaPersonaje.HacerDaño(daño);
    }
}
