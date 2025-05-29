using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPersonaje : MonoBehaviour
{
    public float velocidadSeguimiento = 2f;
    public float movimientoY = 1f;
    public Transform objetivo;

    public void Update()
    {
        Vector3 newPos = new Vector3(objetivo.position.x, objetivo.position.y + movimientoY, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, velocidadSeguimiento * Time.deltaTime);
    }
}