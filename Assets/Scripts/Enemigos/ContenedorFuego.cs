using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenedorFuego : MonoBehaviour
{
    [SerializeField] private Transform enemigo;

    private void Update()
    {
        transform.localScale = enemigo.localScale;
    }
}
