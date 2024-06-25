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
        newCard.SetStartValues(); // Set initial herbs

        // Add to list of collector cards
        collectorCards.Add(newCard);
    }

    // Called when a card is clicked (assuming some kind of click event handler)
    public List<int> ReceiveHerbs(HerbCollectorCard clickedCard)
    {
        List<int> herbsReceived = new List<int>();

        // Add herbs from spots 1 and 2
        herbsReceived.Add(clickedCard.GetHerb(0));
        herbsReceived.Add(clickedCard.GetHerb(1));

        // Add wild card (spot 3), reveal and add to herbs received
        int wildCard = clickedCard.GetWildCard();
        herbsReceived.Add(wildCard);

        // Create a new card to replace the clicked card
        InstantiateCard();

        // Return list of received herbs
        return herbsReceived;
    }
}
