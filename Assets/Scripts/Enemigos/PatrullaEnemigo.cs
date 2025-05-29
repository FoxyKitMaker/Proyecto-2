using UnityEngine;

public class PatrullaEnemigo : MonoBehaviour
{
    [Header("Puntos patrulla")]
    [SerializeField] private Transform ladoIzquierdo;
    [SerializeField] private Transform ladoDerecho;

    [Header("Enemy")]
    [SerializeField] private Transform enemigo;

    [Header("Movement parameters")]
    [SerializeField] private float velocidad;
    private Vector3 escalaInicial;
    private bool moverseIzquierda;

    [Header("Idle Behaviour")]
    [SerializeField] private float duracionIdle;
    private float tiempoIdle;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        escalaInicial = enemigo.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("Movimiento", false);
    }

    private void Update()
    {
        if (moverseIzquierda)
        {
            if (enemigo.position.x >= ladoIzquierdo.position.x)
                MoverEnLaDireccion(-1);
            else
                CambioDireccion();
        }
        else
        {
            if (enemigo.position.x <= ladoDerecho.position.x)
                MoverEnLaDireccion(1);
            else
                CambioDireccion();
        }
    }

    private void CambioDireccion()
    {
        anim.SetBool("Movimiento", false);
        tiempoIdle += Time.deltaTime;

        if (tiempoIdle > duracionIdle)
            moverseIzquierda = !moverseIzquierda;
    }

    private void MoverEnLaDireccion(int _direccion)
    {
        tiempoIdle = 0;
        anim.SetBool("Movimiento", true);

        //Make enemy face direction
        enemigo.localScale = new Vector3(Mathf.Abs(escalaInicial.x) * _direccion
            ,escalaInicial.y, escalaInicial.z);

        //Move in that direction
        enemigo.position = new Vector3(enemigo.position.x + Time.deltaTime * _direccion * velocidad,
            enemigo.position.y, enemigo.position.z);
    }
}
