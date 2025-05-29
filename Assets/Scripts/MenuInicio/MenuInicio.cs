using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicio : MonoBehaviour
{
    public void EmpezarJuego()
    {
        SceneManager.LoadScene("Nivel_1");
    }

    public void Salir()
    {
        Application.Quit();
    }

}
