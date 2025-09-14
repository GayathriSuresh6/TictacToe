using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [SerializeField] private GameObject XMarkSign;
    [SerializeField] private List<GameObject> spaces = new();

    private BoardManager boardManager;
    private MiniMaxEvaluator miniMaxEvaluator;
    private TurnManager turnManager;
    private SignAssigner signAssigner;

    private void Start()
    {
        boardManager = FindObjectOfType<BoardManager>();
        miniMaxEvaluator = FindObjectOfType<MiniMaxEvaluator>();
        turnManager = FindObjectOfType<TurnManager>();
        signAssigner = FindObjectOfType<SignAssigner>();
    }


    public void MakeFirstMove()
    {
        var newBoard = boardManager.GetOriginalBoard();

        // get all available spaces for newBoard
        var availableSpaces = miniMaxEvaluator.GetPlayableSpaces(newBoard);
        List<int> squares = new List<int>();
        foreach (var space in availableSpaces) { squares.Add(space.squareNumber); }
        var move = Random.Range(0,squares.Count);
        MakeMyMove(squares[move]);
        turnManager.isRandFirstMove = false;
    }

    public void BestMove()
    {

        var bestScore = -int.MaxValue;
        int move = -1;
        var newBoard = boardManager.GetOriginalBoard();

        // get all available spaces for newBoard
        var availableSpaces = miniMaxEvaluator.GetPlayableSpaces(newBoard);

        foreach (var space in availableSpaces)
        {
            space.storedMark = signAssigner.AiPlayerMark;
            var score = miniMaxEvaluator.MiniMax(newBoard,0,false);
            if (score > bestScore)
            {
                bestScore = score;
                move = space.squareNumber;
            }

            //set SO back to initial val
            space.storedMark = Marks.None;
        }
        //make the move with the best score
        MakeMyMove(move);
    }

    private void MakeMyMove(int move)
    {
        var currentBoard = boardManager.GetOriginalBoard();
        if(move < 0)
            Debug.Log("Move cannot be negative");
        else
        {
            currentBoard[move].storedMark = signAssigner.AiPlayerMark;
            Instantiate(XMarkSign, spaces[move].transform.position, Quaternion.identity);
            spaces[move].SetActive(false);
        }   
    }
}
