/*‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾*\
| Script sencillo que permite el movimiento del NPC por el mapa al recibir los       |
| inputs de movimiento del jugador, según los valores prestablecidos de velocidad.   |
|                                                                                    |
| Autor: Daniel Díaz Romero                                                          |
\*_________________________________________________________________________________*/


using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;
    public bool canMove = true;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove) return; // Check if player can move

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized; // Normalize to prevent faster diagonal movement

        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude); // Use sqrMagnitude for performance
    }
    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
