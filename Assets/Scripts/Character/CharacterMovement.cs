using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rigidbody = null;
    private float upForce = 250f; // valor del salto al ser presionado
    private float force = 10f; // valor del movimiento al ser presionado
    private PlayerInput playerInput = null; // creamos una variable playerinput que registra las teclas presionadas por el jugador
    private Vector2 input = new Vector2(); // este input registrara la posicion que estamos tocando para mostrarla en el log.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); // inicializa el rigidbody
        playerInput = GetComponent<PlayerInput>(); //inicializamos el playerInput
    }

    // Update is called once per frame
    void Update()
    {
        input = playerInput.actions["Movement"].ReadValue<Vector2>(); // aqui le indicamos que lea el movimiento que mapeamos en el input que creamos antes
        Debug.Log(input);
    }

    private void FixedUpdate() 
    {
        rigidbody.AddForce(new Vector3(input.x, 0f, input.y) * force); //aqui lo que le indicamos que se aplicara el update solo en ese momento   
    }

    public void Jump(InputAction.CallbackContext callbackContext) // Se hace el llamado al evento de salto y al agregar la libreria permite acceder a la informacion de fase en la que se encuentra.
    {
        //if (callbackContext.phase == InputActionPhase.Performed) Si la fase es Performed, quiere decir que se presiono el boton.
        if (callbackContext.performed)
        {
            rigidbody.AddForce(Vector3.up * upForce); // aqui el evento hace que el rigidbody se eleve segun la fuerza de l upForce
            Debug.Log("Jumping"); // comando de log para ver el salto.
        }
        Debug.Log(callbackContext.phase); // indica la fase en la que se realiza el evento, puede ser Started (comienza a presionar), Performed(Ya lo presiono), Canceled (Solto el boton.)
    }
}
