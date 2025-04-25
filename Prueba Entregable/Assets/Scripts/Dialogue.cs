using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

/*
    * Autor: Daniel Díaz
*/

public class Dialogue : MonoBehaviour
{
    [Header("Datos del diálogo")]
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    [Header("Ajustes del jugador")]
    [SerializeField] private PlayerMovement playerMovementScript; // Asigna el script de movimiento del jugador aquí
    [SerializeField] private Animator playerAnimator; // Asigna el Animator del jugador aquí  

    private float typingTime = 0.05f; // Tiempo entre letras

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    
    [SerializeField] private GameObject botonMenu; // Asigna el botón de menú aquí// Asigna el objeto del jugador aquí
    [SerializeField] private GameObject botonSkip; // Asigna el objeto del jugador aquí
    
    private void Start()
    {
        botonSkip.SetActive(false); // Desactivar el botón de skip al inicio
    }

    void Update()
    {
        /* if (didDialogueStart && Input.GetButtonDown("Jump")) // Skipea el diálogo con Space
            {
                SkipAllDialogue();
            } */
        
        if (isPlayerInRange && Input.GetButtonDown("Fire1"))
        {

            if (!didDialogueStart)
            {
                StartDialogue();

            }
            else if(dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines(); // Detener la escritura actual
                dialogueText.text = dialogueLines[lineIndex]; // Mostrar línea completa
            }    
        }
    }

    private void StartDialogue()
    {
        botonMenu.SetActive(false);
        botonSkip.SetActive(true);
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);

        //System.Array.Reverse(dialogueLines); // Invertir el orden de las líneas de diálogo para hacer testing

        lineIndex = 0;

        //Desactivar movimiento del jugador y animaciones
        playerMovementScript.enabled = false; // Desactivar el movimiento del jugador
        playerAnimator.SetFloat("Horizontal", 0); // Desactivar la animación del jugador (ajusta el parámetro según tu Animator)
        playerAnimator.SetFloat("Vertical", 0); // Desactivar la animación del jugador (ajusta el parámetro según tu Animator)
        playerAnimator.SetFloat("Speed", 0); // Desactivar la animación del jugador (ajusta el parámetro según tu Animator)
        StartCoroutine(ShowLine());
    }
    
    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            playerMovementScript.enabled = true; // Reactivar el movimiento del jugador
            botonMenu.SetActive(true);
            botonSkip.SetActive(false); // Desactivar el botón de skip al finalizar el diálogo
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char letter in dialogueLines[lineIndex])
        {
            dialogueText.text += letter; // aqui voy a poner el skip de texto
            yield return new WaitForSeconds(typingTime); // Adjust the typing speed here
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }

    public void SkipAllDialogue()
    {
        StopAllCoroutines(); 
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
        dialogueMark.SetActive(true);
        playerMovementScript.enabled = true; 
        botonMenu.SetActive(true);
        botonSkip.SetActive(false); // Desactivar el botón de skip al finalizar el diálogo
    }
}