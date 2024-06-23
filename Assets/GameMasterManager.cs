using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterManager : MonoBehaviour
{
    float gameTime = 0f;
    float potionOrderTimer = 0f;
    float potionOrderInterval = 30f;

    [SerializeField] PotionManager potionMan;
    [SerializeField] RequestManager requestMan;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
            requestMan.RequestPotion();


        gameTime += Time.deltaTime;
        potionOrderTimer += Time.deltaTime;

        if (potionOrderTimer >= potionOrderInterval)
        {
            potionOrderTimer = 0;
            requestMan.RequestPotion();
        }
    }
}
