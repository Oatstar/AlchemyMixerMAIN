using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashcanScript : MonoBehaviour
{
    public static TrashcanScript instance;

    private void Awake()
    {
        instance = this;
    }

    public void TrashItem(GameObject item)
    {
        Destroy(item);
        InfoTextPopupManager.instance.SpawnInfoTextPopup("Trashed item");
    }
}
