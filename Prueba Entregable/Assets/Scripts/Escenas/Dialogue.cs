/*‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾*\
| Este scipt es esencial para la dinámica del Sistema de dialogos, es completamente  |
| compatible con las localizaciones del juego. Permite al Usuario escuchar y skipear |
| diálogos dichos por un NPC.                                                        |
|                                                                                    |
| Autor: Daniel Díaz Romero                                                          |
\*_________________________________________________________________________________*/


using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems; // Necesario para detectar interacciones con la UI
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public class Dialogue : MonoBehaviour
{
    [Header("Datos del diálogo")]
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private LocalizedString[] dialogueLines;


    [Header("Ajustes del jugador")]
    [SerializeField] private PlayerMovement playerMovementScript; // Asigna el script de movimiento del jugador aquí
    [SerializeField] private Animator playerAnimator; // Asigna el Animator del jugador aquí 
    
    [SerializeField] private GameObject botonMenu; // Asigna el botón de menú aquí
    [SerializeField] private GameObject botonSkip; // Asigna el botón de skip aquí 

    private float typingTime = 0.05f; // Tiempo entre letras

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private string lastLineShown = string.Empty; // Almacena la última línea mostrada
    private bool isTyping = false; // Indica si se está escribiendo el texto
    private LocalizedString.ChangeHandler currentChangeHandler;
    private Coroutine typingCoroutine; // Nueva referencia para controlar el coroutine
    
    private void Start()
    {
        botonSkip.SetActive(false); // Desactivar el botón de skip al inicio
    }

    void Update()
    {
        // Evitar que el diálogo se active si el clic fue sobre un elemento de UI
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (isPlayerInRange && Input.GetButtonDown("Fire1"))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if(dialogueText.text == lastLineShown)
            {
                NextDialogueLine();
            }
            else
            {
                StopCurrentTyping(); // Detener la escritura actual mejor que StopAllCoroutines()
                dialogueText.text = lastLineShown; // Mostrar línea completa
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
        playerMovementScript.canMove = false;


        lineIndex = 0;

        // Desactivar movimiento del jugador y animaciones
        playerMovementScript.enabled = false;
        playerAnimator.SetFloat("Horizontal", 0);
        playerAnimator.SetFloat("Vertical", 0);
        playerAnimator.SetFloat("Speed", 0);

        ShowLocalizedLine();
    }
    
    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            ShowLocalizedLine();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowLocalizedLine()
    {
        StopCurrentTyping();
        dialogueText.text = string.Empty;
        lastLineShown = string.Empty;

        // Desuscribimos el anterior, si existe
        if (currentChangeHandler != null)
            dialogueLines[lineIndex].StringChanged -= currentChangeHandler;

        // Creamos y guardamos el nuevo callback
        currentChangeHandler = (localizedText) =>
        {
            StopCurrentTyping();
            lastLineShown = localizedText;
            typingCoroutine = StartCoroutine(TypeText(localizedText));
        };

        dialogueLines[lineIndex].StringChanged += currentChangeHandler;
        dialogueLines[lineIndex].RefreshString();
    }


    private IEnumerator TypeText(string line)
    {
        isTyping = true; // Indica que se está escribiendo
        dialogueText.text = string.Empty;

        foreach (char letter in line)
        {
            if (!isTyping) yield break; // Si se detiene la escritura, salimos del bucle

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingTime);
        }

        isTyping = false; // La escritura ha terminado
        typingCoroutine = null; // Corrutina ha terminado
    }

    private void StopCurrentTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isTyping = false; // Aseguramos que la escritura se detenga
    }

    private void EndDialogue()
    {
        StopCurrentTyping();
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
        dialogueMark.SetActive(true);
        playerMovementScript.enabled = true;
        botonMenu.SetActive(true);
        botonSkip.SetActive(false);
        playerMovementScript.canMove = true;

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
        EndDialogue();
    }
}
