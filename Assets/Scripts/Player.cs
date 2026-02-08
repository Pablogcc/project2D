using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5; // Esta variable es la velocidad a la que se moverá el personaje.
    private Rigidbody2D rb2D; // Esta variable es una referencia al componente Rigidbody2D del personaje, que se utiliza para aplicar física al movimiento.(Accedemos al componente de fisicas del personaje)

    public float move; // Esta variable es para el movimiento del personaje

    public float jumpForce = 4; // Esta variable es la fuerza con la que el personaje saltará.
    private bool isGrounded; // Esta variable es para verificar si el personaje está tocando el suelo, lo que le permitirá saltar.
    public Transform groundCheck; // Esta variable es para saber si el presonaje está tocando el suelo.
    public float groundRadius = 0.1f; // Esta variable es el radio del círculo que se usará para verificar si el personaje está tocando el suelo.
    public LayerMask groundLayer; // Esta variable es para especificar qué capas se consideran suelo para el personaje.

    private Animator animator; // Esta variable es para controlar las animaciones del personaje.

    private int coins;
    public TMP_Text textCoins;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // El Start sucede cuando inicias el juego.
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Aquí accedemos al componente Rigidbody2D del personaje en Unity.
        animator = GetComponent<Animator>(); // Aquí accedemos al componente Animator del personaje en Unity.
    }

    // Update is called once per frame
    // El Update sucede cada frame, es decir, cada vez que se refresca la pantalla.
    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal"); // Aquí obtenemos el valor del eje horizontal (teclas A y D o flechas izquierda y derecha) para determinar la dirección del movimiento.
        rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y); // Aquí aplicamos la velocidad al Rigidbody2D del personaje, multiplicando el valor del movimiento por la velocidad establecida. El movimiento vertical se mantiene sin cambios.

        // Aquí haremos que el personaje mire hacia la dirección que se está moviendo.
        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);

        // Aquí haremos la función de saltar.
        //Primero verificamos que le damos a la barra espaciadora y que el personaje esté tocando el suelo (isGrounded) para permitirle saltar.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
        }

        animator.SetFloat("Speed", Mathf.Abs(move)); // Aquí actualizamos el parámetro "Speed" del Animator con el valor absoluto del movimiento para controlar las animaciones de caminar o correr. Abs hace que el valor sea positivo, sin importar la dirección del movimiento.
        animator.SetFloat("VerticalVelocity", rb2D.linearVelocity.y); // Aquí actualizamos el parámetro "VerticalVelocity" del Animator con la velocidad vertical del Rigidbody2D para controlar las animaciones de salto o caída.
        animator.SetBool("isGrounded", isGrounded); // Aquí actualizamos el parámetro "isGrounded" del Animator con el valor de isGrounded para controlar las animaciones relacionadas con estar en el suelo o en el aire.


    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer); // Aquí sabemos si el personaje está tocando el suelo utilizando un círculo de verificación en la posición del groundCheck.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coins++;
            textCoins.text = coins.ToString();
        }

        if (collision.transform.CompareTag("Spikes"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.transform.CompareTag("Barrel"))
        {
            Vector2 knockbackDir = (rb2D.position - (Vector2)collision.transform.position).normalized;
            rb2D.linearVelocity = Vector2.zero; // Detener el movimiento actual
            rb2D.AddForce(knockbackDir * 3, ForceMode2D.Impulse); // Aplicar una fuerza de retroceso

            BoxCollider2D[] colliders = collision.gameObject.GetComponents<BoxCollider2D>();

            foreach (BoxCollider2D col in colliders)
            {
                col.enabled = false; // Desactivar el collider para evitar colisiones adicionales
            }

            collision.GetComponent<Animator>().enabled = true; // Activar la animación de destrucción del barril
            Destroy(collision.gameObject, 0.5f);
        }
    }
}
