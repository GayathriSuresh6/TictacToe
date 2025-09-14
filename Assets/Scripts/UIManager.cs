using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private GameObject GameScreen;
    [SerializeField] private GameObject SelectionScreen;
    private TurnManager turnManager;

    private void Start()
    {
        turnManager = GetComponent<TurnManager>();
        SetCurrentPlayerText(turnManager.GetPlayerTurn());
    }

    public void SetCurrentPlayerText(bool isPlayerTurn)
    {
        if (isPlayerTurn)
            displayText.text = "Player X";
        else
            displayText.text = "Player O";
    }

    public void DeclareWinner(Marks mark)
    {
        winnerText.text = "Player '" + mark + "' Wins";
    }

    public void DrawCondition()
    {
        winnerText.text = "It is a draw!";
    }

    public void StartGame()
    {
        GameScreen.SetActive(true);
        SelectionScreen.SetActive(false);
    }

    public void ResetUI()
    {
        GameScreen.SetActive(false);
        SelectionScreen.SetActive(true);
    }
}
