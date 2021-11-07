using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField] bool playerTurn = true;

    public bool PlayerTurn { get => playerTurn; set => playerTurn = value; }
}
