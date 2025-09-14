using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private AIPlayer aiPlayer;
    private UIManager uiManager;
    private AudioManager audioManager;

    private bool isAITurn;
    public bool isRandFirstMove;
    public bool isGameStart;
    

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        aiPlayer = FindObjectOfType<AIPlayer>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public bool GetPlayerTurn() => isAITurn;

    public void SetTurnAsAI()
    {
        Debug.Log("SetAITurn");
        audioManager.PlayButtonTap(gameObject.transform.position);
        isAITurn = true;
        isRandFirstMove = true;
        uiManager.StartGame();
        isGameStart = true;
    }

    public void SetTurnAsPlayer()
    {
        Debug.Log("SetPlayerTurn");
        audioManager.PlayButtonTap(gameObject.transform.position);
        isAITurn = false;
        uiManager.StartGame();
        isGameStart = true;
    }

    public void TogglePlayerTurn()
    {
        isAITurn = !isAITurn;
    }
}
