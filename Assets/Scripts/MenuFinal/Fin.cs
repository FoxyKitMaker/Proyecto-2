using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fin : MonoBehaviour
{
    public void PantallaTitulo()
    {
        SceneManager.LoadScene("Pantalla_titulo");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

}
