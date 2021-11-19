using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public enum traps { axe, spikes };
    public traps trapType;

    //Axe
    [SerializeField] float axeSpeed;
    [SerializeField] Transform rotationPoint;
    [SerializeField] Transform rotationTargetLeft;
    [SerializeField] Transform rotationTargetRight;

    [SerializeField] bool isTopRight = false;


    //Spike
    [SerializeField] Transform upPosition;
    [SerializeField] Transform downPosition;

    [SerializeField] float maxTimeToGoUp;
    [SerializeField] float maxTimeToGoDown;

    [SerializeField] float speedUp;
    [SerializeField] float speedDown;

    private bool isUp;


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
             
        if (Quaternion.Angle(rotationPoint.rotation, rotationTargetRight.rotation) <= 0)
        {
            isTopRight = true;
        }

        if (Quaternion.Angle(rotationPoint.rotation, rotationTargetLeft.rotation) <= 0)
        {
            isTopRight = false;
        }

        if (isTopRight)
        {
            GoLeft();
        }

        if (!isTopRight)
        {
            GoRight();
        }

    }



    void SpikeTrapBehaviour()
    {
        if (Vector2.Distance(transform.position, downPosition.position) <= 0.0f)
        {
            isUp = true;

        }

        if (Vector2.Distance(transform.position, upPosition.position) <= 0.0f)
        {
            isUp = false;
        }

        if (isUp)
        {
            GoUp();
        }

        if (!isUp)
        {
            Invoke("GoDown", 1.0f);
        }
    }
    void GoUp()
    {
        transform.position = Vector2.MoveTowards(transform.position, upPosition.position, speedUp * Time.fixedDeltaTime);
    }

    void GoDown()
    {
        transform.position = Vector2.MoveTowards(transform.position, downPosition.position, speedDown * Time.fixedDeltaTime);
    }

    void GoRight()
    {

        float singleStep = axeSpeed * Time.fixedDeltaTime;

        rotationPoint.transform.rotation = Quaternion.RotateTowards(rotationPoint.transform.rotation, rotationTargetRight.rotation, singleStep);

        print("Going right");
    }

    void GoLeft()
    {

        float singleStep = axeSpeed * Time.fixedDeltaTime;

        rotationPoint.transform.rotation = Quaternion.RotateTowards(rotationPoint.transform.rotation, rotationTargetLeft.rotation, singleStep);

        print("Going left");
    }

}