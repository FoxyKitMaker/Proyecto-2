using UnityEngine;

public class ProyectilEnemigo : DañoEnemigo
{
    [SerializeField] private float velocidad;
    [SerializeField] private float retearTiempo;
    private float tiempoVida;

    public void ActivarProyectil()
    {
        tiempoVida = 0;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        float movementSpeed = velocidad * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        tiempoVida += Time.deltaTime;
        if (tiempoVida > retearTiempo)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //Ejecuta la lógica del script asociado primero
        gameObject.SetActive(false); //Cuando esto golpea algún objeto desactiva la flecha
    }
}
