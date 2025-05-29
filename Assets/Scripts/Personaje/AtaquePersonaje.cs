using UnityEngine;

public class AtaquePersonaje : MonoBehaviour
{
    [SerializeField] private float coolDownAtaque;
    [SerializeField] private Transform puntoFuego;
    [SerializeField] private GameObject[] bolasDeFuego;

    private Animator anim;
    private MovimientoPersonaje movimientoPersonaje;
    private float coolDownTiempo = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movimientoPersonaje = GetComponent<MovimientoPersonaje>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTiempo > coolDownAtaque && movimientoPersonaje.PuedeAtacar())
                Ataque();

        coolDownTiempo += Time.deltaTime;
    }

    private void Ataque()
    {
        anim.SetTrigger("Ataque");
        coolDownTiempo = 0;

        bolasDeFuego[BuscarBolaDeFuego()].transform.position = puntoFuego.position;
        bolasDeFuego[BuscarBolaDeFuego()].GetComponent<BolaDeFuego>().Dirección(Mathf.Sign(transform.localScale.x));
    }

    private int BuscarBolaDeFuego()
    {
        for (int i = 0; i < bolasDeFuego.Length; i++)
        {
            if (!bolasDeFuego[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
