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


}