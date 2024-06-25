using System.Collections.Generic;
using UnityEngine;

public class HerbCollectorManager : MonoBehaviour
{
    public GameObject herbCollectorCardPrefab; // Prefab of the HerbCollectorCardObject
    public Transform collectorCardContainer; // Parent container for instantiated cards
    public int numberOfCards = 3; // Number of cards to instantiate initially

    private List<HerbCollectorCard> collectorCards = new List<HerbCollectorCard>();

    void Start()
    {
        // Instantiate initial cards
        for (int i = 0; i < numberOfCards; i++)
        {
            InstantiateCard();
        }
    }

    // Instantiate a new HerbCollectorCard
    void InstantiateCard()
    {
        GameObject newCardObject = Instantiate(herbCollectorCardPrefab, collectorCardContainer);
        HerbCollectorCard newCard = newCardObject.GetComponent<HerbCollectorCard>();
        
        // Set initial values for spots 0 and 1
        newCard.SetStartValues(); // Set random herbs for spots 0 and 1

        // Add card to list of collector cards
        collectorCards.Add(newCard);
    }

    // Called when a card is clicked
    public List<int> ReceiveHerbs(HerbCollectorCard clickedCard)
    {
        List<int> herbsReceived = new List<int>();

        // Add herbs from spots 0 and 1
        herbsReceived.Add(clickedCard.GetHerb(0));
        herbsReceived.Add(clickedCard.GetHerb(1));

        // Add wild card (spot 2)
        int wildCard = GetWildCard(); // Get wild card value
        herbsReceived.Add(wildCard);

        // Create a new card to replace the clicked card
        InstantiateCard();

        // Return list of received herbs
        return herbsReceived;
    }

    // Get wild card value (handled in manager)
    private int GetWildCard()
    {
        return Random.Range(0, 5); // Herb 0 to 4
    }
}
