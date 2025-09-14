using Firebase.Analytics;
using UnityEngine;


public class SpawnMark : MonoBehaviour
{
    [SerializeField] GameObject XMarkPrefab;
    [SerializeField] GameObject OMarkPrefab;
    [SerializeField] ContainerSquareSO containerSquare;

    private BoardManager boardManager;
    private SignAssigner signAssigner;
    private UIManager uIManager;
    private AIPlayer aiPlayer;
    private TurnManager turnManager;
    private AudioManager audioManager;

   /* public Marks AiPlayerMark;
    public Marks HumanPlayerMark;*/

    private void Start()
    {
        boardManager = FindObjectOfType<BoardManager>();  
        uIManager = FindObjectOfType<UIManager>();
        aiPlayer = FindObjectOfType<AIPlayer>();
        turnManager = FindObjectOfType<TurnManager>();
        signAssigner = FindObjectOfType<SignAssigner>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    

    private void Update()
    {
        if (!turnManager.isGameStart)
            return;

        bool isAITurn = GetTurn();

        
        if (isAITurn)
        {
            if (turnManager.isRandFirstMove)
                aiPlayer.MakeFirstMove();
            else
            {
                aiPlayer.BestMove();
               //turnManager.isRandFirstMove = true;
            }
            if (boardManager.CheckForWinner(signAssigner.AiPlayerMark))
            {
                uIManager.DeclareWinner(signAssigner.AiPlayerMark);
                FirebaseAnalytics.LogEvent("GameEnded",new Parameter("Winner","AIPlayer"));
                boardManager.DisableBoard();
            }

            else
            {
                CheckForDrawCondition();
            }
            turnManager.TogglePlayerTurn();
            uIManager.SetCurrentPlayerText(!isAITurn);
        }
    }

    private void OnMouseDown()
    {
        if (!turnManager.isGameStart)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            bool isAITurn = GetTurn();
            
            if (isAITurn)
                return;
            SetMark(OMarkPrefab, signAssigner.HumanPlayerMark);

            if (boardManager.CheckForWinner(signAssigner.HumanPlayerMark))
            {
               
                FirebaseAnalytics.LogEvent("GameEnded", new Parameter("Winner","HumanPlayer"));
                boardManager.DisableBoard();
                return;
            }
            else
                CheckForDrawCondition();

            SetThisObjectInactive();
            turnManager.TogglePlayerTurn();
            uIManager.SetCurrentPlayerText(isAITurn);
            return;
            
        }
    }


    private bool GetTurn()      
    {
        var isPlayerTurn = turnManager.GetPlayerTurn();
        return isPlayerTurn;
    }

    private void SetMark(GameObject markSign, Marks mark)
    {
        Instantiate(markSign, gameObject.transform.position, Quaternion.identity);
        audioManager.PlayTap(gameObject.transform.position);
        containerSquare.storedMark = mark;
    }

    private void SetThisObjectInactive()
    {
        this.gameObject.SetActive(false);
    }

    private void CheckForDrawCondition()
    {
        if (!boardManager.CheckForDraw())
            return;
        FirebaseAnalytics.LogEvent("GameEnded", new Parameter("Winner", "Draw"));
        uIManager.DrawCondition();
        return;
    }
}
