using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] Transform posE;
    [SerializeField] Transform posP;

    public Transform PosE { get => posE; set => posE = value; }
    public Transform PosP { get => posP; set => posP = value; }
}
