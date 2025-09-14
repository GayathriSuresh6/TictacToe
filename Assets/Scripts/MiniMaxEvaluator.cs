using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMaxEvaluator : MonoBehaviour
{
    private BoardManager boardManager;
    private SignAssigner signAssigner;

    private void Start()
    {
        boardManager = FindObjectOfType<BoardManager>();
        signAssigner = FindObjectOfType<SignAssigner>();
    }

    public int MiniMax(List<ContainerSquareSO> newBoard,int depth, bool isMaximizing)
    {
        
        //return score value on terminal conditions
        if (boardManager.EvaluateBoardWin(newBoard, signAssigner.AiPlayerMark))
            return 10;
        else if (boardManager.EvaluateBoardWin(newBoard, signAssigner.HumanPlayerMark))
            return -10;
        else if (boardManager.CheckForDraw())
            return 0;
       
        if(isMaximizing)
        {
            int bestScore = -int.MaxValue;
            var availableSpaces = GetPlayableSpaces(newBoard);
            foreach (var space in availableSpaces)
            {
                space.storedMark = signAssigner.AiPlayerMark;
                var score = MiniMax(newBoard, depth+1, false);
                bestScore = Mathf.Max(score, bestScore);
                //set SO back to initial val
                space.storedMark = Marks.None;
            }
            return bestScore;
        }

        else
        {
            int worstScore = int.MaxValue;
            var availableSpaces = GetPlayableSpaces(newBoard);
            foreach (var space in availableSpaces)
            {
                space.storedMark = signAssigner.HumanPlayerMark;
                var score = MiniMax(newBoard, depth + 1, true);
                worstScore = Mathf.Min(score, worstScore);
                //set SO back to initial val
                space.storedMark = Marks.None;
            }
            return worstScore;
        }

    }

    public IEnumerable<ContainerSquareSO> GetPlayableSpaces(List<ContainerSquareSO> board)
    {
        return board.Where(s => s.storedMark == Marks.None);
    }

}
