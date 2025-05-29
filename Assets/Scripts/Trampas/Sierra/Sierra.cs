using UnityEngine;

public class Sierra : MonoBehaviour
{
    [SerializeField] private float distanciaMovimiento;
    [SerializeField] private float velocidad;
    [SerializeField] private float daño;
    private bool movimientoIzquierda;
    private float ladoIzquierdo;
    private float ladoDerecho;

    private void Awake()
    {
        ladoIzquierdo = transform.position.x - distanciaMovimiento;
        ladoDerecho = transform.position.x + distanciaMovimiento;
    }

    private void Update()
    {
        if (movimientoIzquierda)
        {
            if (transform.position.x > ladoIzquierdo)
            {
                transform.position = new Vector3(transform.position.x - velocidad * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movimientoIzquierda = false;
        }
        else
        {
            if (transform.position.x < ladoDerecho)
            {
                transform.position = new Vector3(transform.position.x + velocidad * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movimientoIzquierda = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("Player"))
        {
            collision.GetComponent<VidaPersonaje>().HacerDaño(daño);
        }
    }
}