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

    Transform actualPosition;
    [SerializeField] Transform upPosition;
    [SerializeField] Transform downPosition;

    [SerializeField] float maxTimeToGoUp;
    [SerializeField] float maxTimeToGoDown;
    private float currentTimeMoving = 0.0f;
    private float percentageUp;
    private float percentageDown;

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
            timeToChangeSide = 0.0f;
            //trasform.rotation = Quaternion.Lerp();
        }

        else if (timeToChangeSide >= maxTimeToChangeSide && isTopRight)
        {
            isTopLeft = true;
            isTopRight = false;
            timeToChangeSide = 0.0f;
        }
    }

    void SpikeTrapBehaviour()
    {
        actualPosition = gameObject.transform;
        currentTimeMoving += Time.fixedDeltaTime;
        percentageUp = currentTimeMoving / maxTimeToGoUp;
        percentageDown = currentTimeMoving / maxTimeToGoDown;


        if(actualPosition.position.y <= downPosition.position.y) //Vector2.Distance(punto actual, punto final) <= float 
        {
            currentTimeMoving = 0.0f;
            isDown = true;
            isUp = false;
            print("going up");
        }

        if(actualPosition.position.y >= upPosition.position.y) //Vector2.Distance(punto actual, punto final) <= float
        {
            currentTimeMoving = 0.0f;
            isUp = true;
            isDown = false;
            print("going down");
        }

        if(isDown && percentageUp <= 1.0f)
        {           
            goUp();
        }

        if(isUp && percentageDown <= 1.0f)
        {
            //goDown();
            Invoke("goDown", 0.5f);
        }       
    }
    void goUp()
    {
        transform.position = Vector2.Lerp(downPosition.position, upPosition.position, percentageUp);
    }

    void goDown()
    {
        transform.position = Vector2.Lerp(upPosition.position, downPosition.position, percentageDown);
    }
}