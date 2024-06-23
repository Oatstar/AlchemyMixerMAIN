using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    [SerializeField] string ingredientName = "Herb";
    [SerializeField] int state = 0; //0 default, 1 crushed, 2 chopped, 3 dried
    [SerializeField] bool inWork = false; //true if in a workstation or pot and being worked on.

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

    public string GetIngredientName()
    {
        return ingredientName;
    }

    void ChangeGraphics()
    {

    }

    void SetName()
    {

    }
}
