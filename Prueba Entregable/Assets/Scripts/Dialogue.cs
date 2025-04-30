using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems; // Necesario para detectar interacciones con la UI

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
    
    [SerializeField] private GameObject botonMenu; // Asigna el botón de menú aquí
    [SerializeField] private GameObject botonSkip; // Asigna el botón de skip aquí
    
    private void Start()
    {
        botonSkip.SetActive(false); // Desactivar el botón de skip al inicio
    }

    void Update()
    {
        // Evitar que el diálogo se active si el clic fue sobre un elemento de UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

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

        lineIndex = 0;

        // Desactivar movimiento del jugador y animaciones
        playerMovementScript.enabled = false;
        playerAnimator.SetFloat("Horizontal", 0);
        playerAnimator.SetFloat("Vertical", 0);
        playerAnimator.SetFloat("Speed", 0);

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
            playerMovementScript.enabled = true;
            botonMenu.SetActive(true);
            botonSkip.SetActive(false);
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char letter in dialogueLines[lineIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingTime);
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
        botonSkip.SetActive(false);
    }
}
