using UnityEngine;
using UnityEngine.UI;

public class BarraVidaPersonaje : MonoBehaviour
{
    [SerializeField] private VidaPersonaje vidaJugador;
    [SerializeField] private Image barraTotalVida;
    [SerializeField] private Image barraActualVida;

    private void Start()
    {
        barraTotalVida.fillAmount = vidaJugador.VidaActual - 0.1f;
    }
    private void Update()
    {
        barraActualVida.fillAmount = vidaJugador.VidaActual - 0.1f;
    }
}