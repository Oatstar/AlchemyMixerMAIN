using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    int seed = 123456;


    #region
    [SerializeField] List<string> potionNameList = new List<string>()
    {
        "Healing", "Strength", "Agility", "Wisdom", "Invisibility", "Fire Resistance", "Breathing", "Speed", "Night Vision", "Fortitude", "Mana",
        "Luck", "Regeneration", "Poison", "Courage","Clarity","Silence","Stone Skin","Stamina","Transformation","Teleportation","Energy",
        "Antidote","Reflection","Charm","Intellect","Vitality","Protection","Berserk","Shielding","Focus","Awakening","Calmness","Featherfall",
        "Illusion","Stealth","Fortification","Growth","Shrinking","Restoration","Invulnerability","Purity","Detect Magic","Magic Absorption",
        "Swiftfoot","Etherealness","Amplification","Rejuvenation","Phantom","Quickness","Insight","Harmony","Rebirth","Celerity","Alacrity","Tenacity","Focus",
        "Magnetism","Evasion","Transformation","Vision","Climbing","Courage","Mimicry","Sanctuary","Endurance","Foresight","Luminescence","Mending","Shield",
        "Steadfastness","Haste","Resistance","Escape","Empathy","Balance","Clarity","Precision","Serenity","Fortification","Transmutation","Adaptation",
        "Deflection","Persistence","Immunity","Invigoration","Reinforcement","Secrecy","Tranquility","Vigilance","Warmth","Wisdom","Enlightenment"
    };
    #endregion

    [SerializeField] List<string> tempNameList = new List<string>();
    HashSet<string> usedCombinations = new HashSet<string>(); // Track used combinations

    public RequestedPotion potionOfFlight;
    [SerializeField] List<RequestedPotion> allRequestablePotions = new List<RequestedPotion>() { };
    [SerializeField] List<RequestedPotion> staticPotionList = new List<RequestedPotion>() { };
    [SerializeField] List<RequestedPotion> requestedPotions = new List<RequestedPotion>() { };

    public static PotionManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tempNameList.AddRange(potionNameList);
        //UnityEngine.Random.InitState(seed);
        CreateAllPotions();
    }

    void CreateAllPotions()
    {
        CreatePotion(2);
        CreatePotion(3);
        CreatePotion(4);
        CreatePotion(3);
        for (int i = 0; i < 5; i++)
            CreatePotion(2);
        for (int i = 0; i < 15; i++)
        {
            int randomChance = UnityEngine.Random.Range(0, 2);
            if(randomChance == 1)
                CreatePotion(3);
            else
                CreatePotion(4);

        }
        for (int i = 0; i < 35; i++)
            CreatePotion(4);

        //Only 1 level 5 potion
        CreatePotionOfFlight();

        staticPotionList.AddRange(allRequestablePotions);
    }

    void CreatePotion(int potionLevel)
    {
         RequestedPotion newPotion = new RequestedPotion();
        newPotion.potionName = "Potion of " +tempNameList[UnityEngine.Random.Range(0, tempNameList.Count-1)];
        tempNameList.Remove(newPotion.potionName); //Remove the used potion name

        for (int i = 0; i < potionLevel; i++)
        {
            int herb;
            int herbState;
            string combination;

            int loopBreaker = 0;
            do
            {
                herb = UnityEngine.Random.Range(0, 5);
                herbState = UnityEngine.Random.Range(0, 4);

                combination = herb.ToString() + "," + herbState.ToString();

                loopBreaker++;

                if (loopBreaker > 1000)
                    break;
            } while (usedCombinations.Contains(combination));

            usedCombinations.Add(combination);

            Herb newHerb = new Herb();
            newHerb.herbId = herb;
            newHerb.herbState = herbState;

            newPotion.herbs.Add(newHerb);
        }
        newPotion.potionLevel = potionLevel;
        newPotion.boilLevel = SetBoilLevel(newPotion.potionLevel);

        allRequestablePotions.Add(newPotion);
    }

    int SetBoilLevel(int potLevel)
    {
        if(potLevel == 2)
        {
            return UnityEngine.Random.Range(0, 2); //0,1
        }
        else if (potLevel == 3)
        {
            return UnityEngine.Random.Range(0, 3); //0,1,2
        }
        else if (potLevel == 4)
        {
            return UnityEngine.Random.Range(1, 4); //0,1,2,3
        }
        else
        {
            return 0;   
        }
    }

    void CreatePotionOfFlight()
    {
        RequestedPotion newPotion = new RequestedPotion();
        newPotion.potionName = "Potion of Flight";
        tempNameList.Remove(newPotion.potionName); //Remove the used potion name

        Herb newHerb1 = new Herb();
        newHerb1.herbId = 5;
        newHerb1.herbState = 0;
        newPotion.herbs.Add(newHerb1);

        Herb newHerb2 = new Herb();
        newHerb2.herbId = 5;
        newHerb2.herbState = 1;
        newPotion.herbs.Add(newHerb2);

        Herb newHerb3 = new Herb();
        newHerb3.herbId = 5;
        newHerb3.herbState = 2;
        newPotion.herbs.Add(newHerb3);

        Herb newHerb4 = new Herb();
        newHerb4.herbId = 5;
        newHerb4.herbState = 3;
        newPotion.herbs.Add(newHerb4);

        Herb newHerb5 = new Herb();
        newHerb5.herbId = 1;
        newHerb5.herbState = 3;
        newPotion.herbs.Add(newHerb5);

        newPotion.boilLevel = 3;

        potionOfFlight = newPotion;
        staticPotionList.Add(potionOfFlight);
    }

    public RequestedPotion GetNewRecipeRequest()
    {
        RequestedPotion tempPotion = allRequestablePotions[0];
        allRequestablePotions.RemoveAt(0);
        return tempPotion;
    }

    public string CompareIngredientsAndGetPotionName(RequestedPotion newPotion)
    {
        bool herbsMatch = false;
        RequestedPotion currentMatchPotion = new RequestedPotion();

        foreach (RequestedPotion potion in staticPotionList)
        {
            if (potion.herbs.Count == newPotion.herbs.Count)
            {
                List<Herb> tempHerbsNewPotion = new List<Herb>(newPotion.herbs);
                List<Herb> tempHerbsComparePotion = new List<Herb>(potion.herbs);

                tempHerbsNewPotion.Sort((h1, h2) =>
                {
                    int idComparison = h1.herbId.CompareTo(h2.herbId);
                    if (idComparison == 0)
                    {
                        return h1.herbState.CompareTo(h2.herbState);
                    }
                    return idComparison;
                });

                tempHerbsComparePotion.Sort((h1, h2) =>
                {
                    int idComparison = h1.herbId.CompareTo(h2.herbId);
                    if (idComparison == 0)
                    {
                        return h1.herbState.CompareTo(h2.herbState);
                    }
                    return idComparison;
                });

                // Compare sorted lists
                bool match = true;
                for (int i = 0; i < tempHerbsComparePotion.Count; i++)
                {
                    if (tempHerbsComparePotion[i].herbId != tempHerbsNewPotion[i].herbId ||
                        tempHerbsComparePotion[i].herbState != tempHerbsNewPotion[i].herbState)
                    {
                        match = false;
                        break;
                    }
                }

                // If all herbs matched
                if (match)
                {
                    currentMatchPotion = potion;
                    herbsMatch = match;
                    break;
                }
            }
        }

        if(currentMatchPotion != null)
        {
            if (herbsMatch && newPotion.boilLevel == currentMatchPotion.boilLevel)
            {
                return currentMatchPotion.potionName;
            }
        }
        
        
        return "Unknown Potion";
    }

}



[System.Serializable]
public class RequestedPotion
{
    public string potionName = "";
    public List<Herb> herbs = new List<Herb> { };
    public int boilLevel; //0, 1, 2, 3
    public int potionLevel; //(2,3,4,5);
}
