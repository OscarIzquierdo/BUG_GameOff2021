using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public enum traps { axe, spikes };
    public traps trapType;


    [SerializeField] float timeToChangeSide;
    private float maxTimeToChangeSide = 1.5f;

    private bool isTopRight = false;
    private bool isTopLeft = true;



    [SerializeField] float timeToGoUp;
    private float maxTimeToGoUp = 1.5f;
    [SerializeField] float timeToGoDown;
    private float maxTimeToGoDown = 0.5f;

    private bool isUp;
    private bool isDown;

    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (trapType == traps.axe)
        {
            AxeTrapBehaviour();
        }
        else if (trapType == traps.spikes)
        {
            SpikeTrapBehaviour();
        }
    }

    void AxeTrapBehaviour()
    {
        timeToChangeSide += Time.fixedDeltaTime;

        if (timeToChangeSide >= maxTimeToChangeSide && isTopLeft)
        {
            isTopLeft = false;
            isTopRight = true;
        }

        else if (timeToChangeSide >= maxTimeToChangeSide && isTopRight)
        {
            isTopLeft = true;
            isTopRight = false;
        }
    }

    void SpikeTrapBehaviour()
    {

    }
}
