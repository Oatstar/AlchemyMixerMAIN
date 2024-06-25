using UnityEngine;

public class HerbCollectorCard : MonoBehaviour
{
    private int[] herbs = new int[3]; // Array to store the herbs (0, 1, 2, 3, 4)

    // Method to set start values for spots 0 and 1
    public void SetStartValues()
    {
        // Assign random herbs to spots 0 and 1
        herbs[0] = Random.Range(0, 5); // Herb 0 to 4
        herbs[1] = Random.Range(0, 5); // Herb 0 to 4
    }

    // Method to set herb graphics for spots 0 and 1
    public void SetHerbGraphics(int herb1, int herb2)
    {
        // Set graphics for spot 0 (herb1) and spot 1 (herb2)
        // Example: Replace with your logic to set sprite graphics
        Debug.Log("Setting graphics for spot 0 with herb: " + herb1);
        Debug.Log("Setting graphics for spot 1 with herb: " + herb2);
    }

    // Method to get herb at specified spot index
    public int GetHerb(int spotIndex)
    {
        // Replace with logic to return actual herb values
        // For example, return this.herbs[spotIndex];
        return herbs[spotIndex]; // Example placeholder
    }

    // Additional methods as needed
}
