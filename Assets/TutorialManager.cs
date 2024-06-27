using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialPanels;
    int currentPanel = -1;
    public GameObject tutorialContainer;

    private void Start()
    {
        //If playerpref is not found, set it to 0 and trigger tutorial
        if(!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt("FirstTime", 0);
            Invoke("OpenTutorial", 1f);
        }
    }

    public void OpenTutorial()
    {
        tutorialContainer.SetActive(true);
        Time.timeScale = 0f;
        currentPanel = -1;
        TutorialClicked();
        tutorialContainer.SetActive(true);
    }

    public void TutorialClicked()
    {
        currentPanel++;
        if(currentPanel >= tutorialPanels.Length)
        {
            TutorialEnded();
        }
        else
        {
            foreach (GameObject panel in tutorialPanels)
            {
                panel.SetActive(false);
            }

            tutorialPanels[currentPanel].SetActive(true);
        }
        

    }

    void TutorialEnded()
    {
        foreach (GameObject panel in tutorialPanels)
            panel.SetActive(false);
        tutorialContainer.SetActive(false);

        Time.timeScale = 1f;
    }
}
