using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask capaSuelo;
    [SerializeField] private LayerMask capaMuro;
    private Rigidbody2D cuerpo;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float coolDownSaltoMuro;
    private float horizontalInput;

    private void Awake()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Rota el personaje cuando mira a la derecha o a la izquierda
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space) && EstaSuelo())
            SaltoPersonaje();          

        //Parámetros de la animación
        anim.SetBool("Correr", horizontalInput != 0);
        anim.SetBool("Suelo", EstaSuelo());
            
        //Salto de pared
        if (coolDownSaltoMuro > 0.2f)
        {
            cuerpo.velocity = new Vector2(horizontalInput * velocidad, cuerpo.velocity.y);

            if (EstaMuro() && !EstaSuelo())
            {
                cuerpo.gravityScale = 0;
                cuerpo.velocity = Vector2.zero;
            }
            else
                cuerpo.gravityScale = 7;
            if (Input.GetKey(KeyCode.Space))
                SaltoPersonaje();
        }
        else
            coolDownSaltoMuro += Time.deltaTime;
    }

    private void SaltoPersonaje()
    {
        if (EstaSuelo())
        {
            cuerpo.velocity = new Vector2(cuerpo.velocity.x, fuerzaSalto);
            anim.SetTrigger("Salto");
        }
        else if (EstaMuro() && !EstaSuelo())
        {
            if (horizontalInput == 0)
            {
                cuerpo.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                cuerpo.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            coolDownSaltoMuro = 0;
        }
    }

    private bool EstaSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;

    }
    private bool EstaMuro()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, capaMuro);
        return raycastHit.collider != null;
    }

    public bool PuedeAtacar()
    {
        return horizontalInput == 0 && EstaSuelo() && !EstaMuro();
    }
}
