using UnityEngine;
using UnityEngine.UI;

public class HerbCollectorCard : MonoBehaviour
{
    private int[] herbs = new int[3]; // Array to store the herbs (0, 1, 2, 3, 4)

    [SerializeField] Image[] herbImageObjects = new Image[3];
    [SerializeField] Sprite questionMarkSprite;

    // Method to set start values for spots 0 and 1
    public void SetStartValues()
    {
        // Assign random herbs to spots 0 and 1
        herbs[0] = Random.Range(0, 5); // Herb 0 to 4
        herbs[1] = Random.Range(0, 5); // Herb 0 to 4
        SetHerbGraphics();
    }

    // Method to set herb graphics for spots 0 and 1
    public void SetHerbGraphics()
    {
        herbImageObjects[0].sprite = HerbManager.instance.GetHerbImage(herbs[0], 0);
        herbImageObjects[1].sprite = HerbManager.instance.GetHerbImage(herbs[1], 0);
        herbImageObjects[2].sprite = questionMarkSprite;
    }

    public int GetHerb(int spotIndex)
    {

        return herbs[spotIndex];
    }

    public void CardClicked()
    {
        HerbCollectorManager.instance.HerbCollectorCardClicked(this);
    }

    // Additional methods as needed
}
