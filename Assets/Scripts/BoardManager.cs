using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private List<ContainerSquareSO> board = new List<ContainerSquareSO>();
   

    private void Start()
    {
        //reset board on game start
        ResetBoard();     
    }

    private void ResetBoard()
    {
        board.ForEach(space => space.storedMark = Marks.None);
    }

    public List<ContainerSquareSO> GetOriginalBoard()
    {
        return board;
    }

    public bool CheckForWinner(Marks letter)
    {
        bool isWin =  EvaluateBoardWin(board, letter);
        return isWin;
    }

    public bool EvaluateBoardWin(List<ContainerSquareSO> newBoard, Marks letter)
    {
        bool isWin = (newBoard[0].storedMark == letter && newBoard[1].storedMark == letter && newBoard[2].storedMark == letter) ||
               (newBoard[3].storedMark == letter && newBoard[4].storedMark == letter && newBoard[5].storedMark == letter) ||
               (newBoard[6].storedMark == letter && newBoard[7].storedMark == letter && newBoard[8].storedMark == letter) ||
               (newBoard[0].storedMark == letter && newBoard[3].storedMark == letter && newBoard[6].storedMark == letter) ||
               (newBoard[1].storedMark == letter && newBoard[4].storedMark == letter && newBoard[7].storedMark == letter) ||
               (newBoard[2].storedMark == letter && newBoard[5].storedMark == letter && newBoard[8].storedMark == letter) ||
               (newBoard[0].storedMark == letter && newBoard[4].storedMark == letter && newBoard[8].storedMark == letter) ||
               (newBoard[2].storedMark == letter && newBoard[4].storedMark == letter && newBoard[6].storedMark == letter);
        return isWin;
    }

    public bool CheckForDraw()
    {
        bool isDraw = board.All(item => item.storedMark != Marks.None);
        return isDraw;
    }

    public void DisableBoard()
    {
        var SpawnSpaceList = GameObject.FindGameObjectsWithTag("SpawnSpace");
        foreach (var item in SpawnSpaceList)
        {
            item.SetActive(false);
        }
    }

}
