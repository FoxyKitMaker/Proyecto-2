using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{
    [Header("Parámetros Ataque")]
    [SerializeField] private float cooldownAtaque;
    [SerializeField] private float rango;
    [SerializeField] private int daño;

    [Header("Ranged Attack")]
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private GameObject[] bolasFuego;

    [Header("Parámetros collider")]
    [SerializeField] private float distanciaCollider;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Capa jugador")]
    [SerializeField] private LayerMask capaJugador;
    private float tiempoCooldown = Mathf.Infinity;

    //References
    private Animator anim;
    private PatrullaEnemigo patrullaEnemigo;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        patrullaEnemigo = GetComponentInParent<PatrullaEnemigo>();
    }

    private void Update()
    {
        tiempoCooldown += Time.deltaTime;

        //Attack only when player in sight?
        if (JugadorEnVista())
        {
            if (tiempoCooldown >= cooldownAtaque)
            {
                tiempoCooldown = 0;
                anim.SetTrigger("AtaqueDistancia");
            }
        }

        if (patrullaEnemigo != null)
            patrullaEnemigo.enabled = !JugadorEnVista();
    }

    private void RangoAtaque()
    {
        tiempoCooldown = 0;
        bolasFuego[BuscarBolaFuego()].transform.position = puntoDisparo.position;
        bolasFuego[BuscarBolaFuego()].GetComponent<ProyectilEnemigo>().ActivarProyectil();
    }
    private int BuscarBolaFuego()
    {
        for (int i = 0; i < bolasFuego.Length; i++)
        {
            if (!bolasFuego[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool JugadorEnVista()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * rango * transform.localScale.x * distanciaCollider,
            new Vector3(boxCollider.bounds.size.x * rango, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, capaJugador);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * rango * transform.localScale.x * distanciaCollider,
            new Vector3(boxCollider.bounds.size.x * rango, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
