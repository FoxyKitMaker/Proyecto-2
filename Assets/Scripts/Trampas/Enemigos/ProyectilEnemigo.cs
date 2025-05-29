using UnityEngine;

public class ProyectilEnemigo : Da�oEnemigo
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
        base.OnTriggerEnter2D(collision); //Ejecuta la l�gica del script asociado primero
        gameObject.SetActive(false); //Cuando esto golpea alg�n objeto desactiva la flecha
    }
}
