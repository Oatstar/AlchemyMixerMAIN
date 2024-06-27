using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMover : MonoBehaviour
{
    RectTransform rectTrans;
    Vector3 startingPos;
    public float speed = 5f; // Speed of movement
    private float duration = 20f; // Duration in seconds
    private float elapsedTime = 0f; // Time elapsed

    private void Awake()
    {
        rectTrans = this.GetComponent<RectTransform>();
        startingPos = rectTrans.anchoredPosition;
    }

    private void Start()
    {
        // Initialization if needed
    }

    private void Update()
    {
        if (elapsedTime < duration)
        {
            float moveAmount = speed * Time.unscaledDeltaTime;
            rectTrans.anchoredPosition += new Vector2(0, moveAmount);
            elapsedTime += Time.unscaledDeltaTime;
        }
    }
}
