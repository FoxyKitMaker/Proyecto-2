using UnityEngine;

public class TramapaCaraPincho : DañoEnemigo
{
    [Header("Atributos de los pinchos")]
    [SerializeField] private float velocidad;
    [SerializeField] private float rango;
    [SerializeField] private float retrasoComprobacion;
    [SerializeField] private LayerMask capaJugador;
    private Vector3[] direcciones = new Vector3[4];
    private Vector3 destino;
    private float tiempoComprobacion;
    private bool ataque;

    private void OnEnable()
    {
        Parar();
    }
    private void Update()
    {
        //Mover los pinchos al destino solo si ataca
        if (ataque)
            transform.Translate(destino * Time.deltaTime * velocidad);
        else
        {
            tiempoComprobacion += Time.deltaTime;
            if (tiempoComprobacion > retrasoComprobacion)
                ComprobarJugador();
        }
    }
    private void ComprobarJugador()
    {
        CalcularDireccion();

        //Comprueba si los pinchos ven al jugador en cualquiera de las 4 direcciones
        for (int i = 0; i < direcciones.Length; i++)
        {
            Debug.DrawRay(transform.position, direcciones[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direcciones[i], rango, capaJugador);

            if (hit.collider != null && !ataque)
            {
                ataque = true;
                destino = direcciones[i];
                tiempoComprobacion = 0;
            }
        }
    }
    private void CalcularDireccion()
    {
        direcciones[0] = transform.right * rango; //Dirección Derecha
        direcciones[1] = -transform.right * rango; //Dirección Izquierda
        direcciones[2] = transform.up * rango; //Dirección Arriba
        direcciones[3] = -transform.up * rango; //Dirección Abajo
    }
    private void Parar()
    {
        destino = transform.position; //Asignarle la posición actual del destinatario así no se mueve
        ataque = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Parar(); //Para los pinchos una vez golpean algo
    }
}
