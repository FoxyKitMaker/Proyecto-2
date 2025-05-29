using UnityEngine;
using UnityEngine.SceneManagement;
public class FinNivel_1 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D detector)
    {
        if (detector.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Nivel_2");
        }
    }
}
