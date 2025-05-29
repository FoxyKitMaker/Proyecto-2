using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeFuego : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private float direccion;
    private bool golpe;
    private float tiempoDeVida;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (golpe) return;
        float velocidadMovimiento = velocidad * Time.deltaTime * direccion;
        transform.Translate(velocidadMovimiento, 0, 0);

        tiempoDeVida += Time.deltaTime;
        if (tiempoDeVida > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        golpe = true;
        boxCollider.enabled = false;
        anim.SetTrigger("Explosion");

        if (collision.CompareTag("Trampa"))
            collision.GetComponent<VidaPersonaje>()?.HacerDaño(0.5f);
    }

    public void Dirección(float _direccion)
    {
        tiempoDeVida = 0;
        direccion = _direccion;
        gameObject.SetActive(true);
        golpe = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direccion)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Desactivar()
    {
        gameObject.SetActive(false);
    }
}
