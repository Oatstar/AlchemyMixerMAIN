using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    //[SerializeField] string ingredientName = "Mynt"; //Mynt, Saph, Thym, Blis, Neth
    [SerializeField] int herbId= 1; //1,2,3,4,5
    [SerializeField] int state = 0; //0 default, 1 crushed, 2 chopped, 3 dried
    [SerializeField] bool inWork = false; //true if in a workstation or pot and being worked on.

    private void Awake()
    {
    }

    public void SetStartValues()
    {
        herbId = Random.Range(1, 6);
    }

    public void SetWorkState(int stateId)
    {
        state = stateId;
        ChangeGraphics();
        SetName();
    }

    public int GetWorkState()
    {
        return state;
    }

    public int GetHerbId()
    {
        return herbId;
    }


    void ChangeGraphics()
    {

    }

    void SetName()
    {

    }
}
