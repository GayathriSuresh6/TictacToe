
using UnityEngine;

[CreateAssetMenu(fileName = "ContainerSquareSO", menuName = "ScriptableObjects/ContainerSquare")]
public class ContainerSquareSO : ScriptableObject
{
    public int squareNumber;
    public Marks storedMark;
}

public enum Marks
{
    None,
    x,
    o
}
