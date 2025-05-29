using UnityEngine;

public class SaltoPersonaje : MonoBehaviour
{
    public float fuerzaSalto = 10f; 
    public LayerMask sueloMask; 
    public Transform pies; 

    private Rigidbody2D rb;
    private Animator animator;
    private bool enSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        enSuelo = Physics2D.OverlapCircle(pies.position, 0.1f, sueloMask);

        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            animator.SetTrigger("Saltar");
        }
        animator.SetBool("EnSuelo", enSuelo);
    }
}
