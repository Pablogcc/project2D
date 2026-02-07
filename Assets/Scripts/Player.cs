using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // El Start sucede cuando inicias el juego.
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Aquí accedemos al componente Rigidbody2D del personaje en Unity.
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
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer); // Aquí sabemos si el personaje está tocando el suelo utilizando un círculo de verificación en la posición del groundCheck.
    }
}
