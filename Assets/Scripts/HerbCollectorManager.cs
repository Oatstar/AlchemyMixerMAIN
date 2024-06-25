using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerbCollectorManager : MonoBehaviour
{
    public GameObject herbCollectorCardPrefab; // Prefab of the HerbCollectorCardObject
    public Transform collectorCardContainer; // Parent container for instantiated cards
    public int numberOfCards = 3; // Number of cards to instantiate initially

    private List<HerbCollectorCard> collectorCards = new List<HerbCollectorCard>();

    public static HerbCollectorManager instance;
    [SerializeField] Slider slider;

    [SerializeField] float cardTimer = 0f;
    float cardSpawnInterval = 20f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Instantiate initial cards
        for (int i = 0; i < numberOfCards; i++)
        {
            InstantiateCard();
        }
    }

    private void Update()
    {
        cardTimer += Time.deltaTime;
        slider.value = cardTimer;

        if(cardTimer >= cardSpawnInterval)
        {
            cardTimer = 0;

            if (collectorCardContainer.childCount < 3)
            {
                InstantiateCard();
            }
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
    public void HerbCollectorCardClicked(HerbCollectorCard herbCardContr)
    {
        if (HerbManager.instance.CountFreeInventorySlots() < 3)
            Debug.Log("Inventory full");
        else
        {
            ReceiveHerbs(herbCardContr);
        }
    }

    public void ReceiveHerbs(HerbCollectorCard clickedCard)
    {
        List<int> herbsReceived = new List<int>();

        // Add herbs from spots 0 and 1
        herbsReceived.Add(clickedCard.GetHerb(0));
        herbsReceived.Add(clickedCard.GetHerb(1));

        // Add wild card (spot 2)
        int wildCard = Random.Range(0, 5); // Get wild card value
        herbsReceived.Add(wildCard);

        HerbManager.instance.SpawnMultipleHerbs(herbsReceived);
        // Create a new card to replace the clicked card

    }


}
