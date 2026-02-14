using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{

    public void Jugar() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Aquí cargamos la siguiente escena en el orden de construcción del proyecto, lo que generalmente sería la escena del juego después del menú inicial.
    }

    public void Salir() { 
        Debug.Log("Saliendo del juego..."); // Aquí imprimimos un mensaje en la consola para indicar que el jugador ha elegido salir del juego.
        Application.Quit(); // Aquí cerramos la aplicación cuando el jugador elige salir desde el menú inicial.
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
