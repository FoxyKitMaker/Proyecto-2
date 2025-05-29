using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaPersonaje : MonoBehaviour
{
    [Header ("Vida")]
    [SerializeField] private float vidaInicio;
    public float VidaActual { get; private set; }
    private Animator anim;
    private bool muerto;

    [Header("Frames")]
    [SerializeField] private float duracionFrames;
    [SerializeField] private int numeroDeFlases;
    private SpriteRenderer spriteRender;

    [Header("Components")]
    [SerializeField] private Behaviour[] componentes;
    private bool invulnerable;

    private void Awake()
    {
        VidaActual = vidaInicio;
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }
    public void HacerDaño(float _daño)
    {
        VidaActual = Mathf.Clamp(VidaActual - _daño, 0, vidaInicio);

        if (VidaActual > 0.1f)
        {
            anim.SetTrigger("Golpe");
            StartCoroutine(Invunerabilidad());
        }
        else
        {
            if (!muerto)
            {
                anim.SetTrigger("Muerto");
                GetComponent<MovimientoPersonaje>().enabled = false;

                //Desactiva todos los componentes asociados
                foreach (Behaviour componente in componentes)
                    componente.enabled = false;

                muerto = true;

                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void Desaparecer()
    {
        gameObject.SetActive(false);

    }
    public void AñadirVida(float _valor)
    {
        VidaActual = Mathf.Clamp(VidaActual + _valor, 0, vidaInicio);
    }

    public void AddHealth(float _value)
    {
        VidaActual = Mathf.Clamp(VidaActual + _value, 0, vidaInicio);
    }

    private IEnumerator Invunerabilidad()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numeroDeFlases; i++)
        {
            spriteRender.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(duracionFrames / (numeroDeFlases * 2));
            spriteRender.color = Color.white;
            yield return new WaitForSeconds(duracionFrames / (numeroDeFlases * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    public void Reaparece()
    {
        AddHealth(vidaInicio);
        anim.ResetTrigger("Muerto");
        anim.Play("Idle");
        StartCoroutine(Invunerabilidad());

        //Activa todos los componenets asociados
        foreach (Behaviour component in componentes)
            component.enabled = true;
    }
}

