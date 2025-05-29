using UnityEngine;

public class SeCae : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D detector)
    {
        if (detector.gameObject.CompareTag("Player"))
        {
            detector.gameObject.GetComponent<Reaparece>().Reaparecer();
        }
    }

}




