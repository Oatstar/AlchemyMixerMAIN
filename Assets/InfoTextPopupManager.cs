using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTextPopupManager : MonoBehaviour
{
    public static InfoTextPopupManager instance;
    [SerializeField] GameObject infoFloaterObject;
    [SerializeField] GameObject infoFloaterContainer;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnInfoTextPopup(string inputText)
    {
        GameObject popupFloater = Instantiate(infoFloaterObject, transform.position, Quaternion.identity);
        popupFloater.transform.SetParent(infoFloaterContainer.transform);
        popupFloater.transform.localScale = new Vector3(1, 1,1);
        popupFloater.GetComponent<InfoFloaterScript>().ShowInfoText(inputText);

    }
}
