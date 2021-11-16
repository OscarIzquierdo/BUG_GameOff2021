using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellTip : MonoBehaviour
{
    [SerializeField] GameObject tooltip;

    private void Start()
    {
        tooltip.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tooltip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tooltip.SetActive(false);
        }
    }
}
