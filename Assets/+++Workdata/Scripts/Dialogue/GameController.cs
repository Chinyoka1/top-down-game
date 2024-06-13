using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private PlayerMovement player;
    private DialogueController dialogueController;
    [SerializeField] private InputReader inputReader;

    public enum GameMode
    {
        PreMenu,
        MainMenu,
        NewGame,
        LoadSaveGame,
        LoadScene,
        DialogueMode,
        DebugMode
    }

    public GameMode gameMode;
    public Button lastSelectable;
    #region Unity Event Functions

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        dialogueController = FindObjectOfType<DialogueController>();
    }

    private void OnEnable()
    {
        DialogueController.DialogueClosed += EndDialogue;
    }

    private void Start()
    {
        if(player)
            EnterPlayMode();
    }

    private void OnDisable()
    {
        DialogueController.DialogueClosed -= EndDialogue;
    }

    #endregion

    #region Modes

    public void EnterPlayMode()
    {
        Time.timeScale = 1;
        // In the editor: Unlock with ESC.
        //Cursor.lockState = CursorLockMode.Locked;
        inputReader.EnablePlayerInput();
    }

    private void EnterDialogueMode()
    {
        Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.Locked;
        inputReader.DisablePlayerInput();
    }

    #endregion

    public void StartDialogue(string dialoguePath)
    {
        EnterDialogueMode();
        dialogueController.StartDialogue(dialoguePath);
    }

    private void EndDialogue()
    {
        EnterPlayMode();
    }
    
    private void EnterInventoryMode()
    {
        Time.timeScale = 0;
        inputReader.DisablePlayerInput();
    }

    public void StartInventoryMode()
    {
        EnterInventoryMode();
    }
    
    public void EndInventoryMode()
    {
        EnterPlayMode();
    }

    public void SetLastSelectable()
    {
        SetSelectable(lastSelectable);
    }
    public void SetSelectable(Button newSelactable)
    {
        Selectable newSelectable;
        lastSelectable = newSelactable;
        newSelectable = newSelactable;

        //newSelactable.Select();
        StartCoroutine(DelayNewSelectable(newSelectable));
    }

    public void ExitMenu()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator DelayNewSelectable(Selectable newSelectable)
    {
        yield return null;
        newSelectable.Select();
    }
}

