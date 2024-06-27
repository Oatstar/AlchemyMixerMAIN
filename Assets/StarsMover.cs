using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsMover : MonoBehaviour
{
    RectTransform rectTrans;
    Vector3 startingPos;
    Vector3 targetPos;
    public float moveAmount = 2f; // Amount to move left and right
    public float interval = 0.7f; // Interval in seconds


    private void Awake()
    {
        rectTrans = this.GetComponent<RectTransform>();

        startingPos = rectTrans.anchoredPosition;
        targetPos = startingPos + new Vector3(moveAmount, 0, 0);

    }

    private void Start()
    {
        StartCoroutine(MoveBackAndForth());
    }


    IEnumerator MoveBackAndForth()
    {
        while (true)
        {
            rectTrans.anchoredPosition = targetPos;
            yield return new WaitForSeconds(interval);

            // Move back to starting position
            rectTrans.anchoredPosition = startingPos;
            yield return new WaitForSeconds(interval);
        }
    }
}
