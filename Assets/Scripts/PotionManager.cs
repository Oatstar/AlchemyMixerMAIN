using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    int seed = 123456;


    #region
    [SerializeField] List<string> potionNameList = new List<string>()
    {
        "Healing", "Strength", "Agility", "Wisdom", "Invisibility", "Fire Resistance", "Water Breathing", "Speed", "Night Vision", "Fortitude", "Mana",
        "Luck", "Regeneration", "Poison", "Courage","Clarity","Silence","Stone Skin","Stamina","Transformation","Teleportation","Energy",
        "Antidote","Reflection","Charm","Intellect","Vitality","Protection","Berserk","Shielding","Focus","Awakening","Calmness","Featherfall",
        "Illusion","Stealth","Fortification","Growth","Shrinking","Restoration","Invulnerability","Purity","Detect Magic","Elemental Resistance","Magic Absorption",
        "Swiftfoot","Etherealness","Amplification","Rejuvenation","Phantom","Quickness","Insight","Harmony","Rebirth","Celerity","Alacrity","Tenacity","Focus",
        "Magnetism","Evasion","Transformation","Vision","Climbing","Courage","Mimicry","Sanctuary","Endurance","Foresight","Luminescence","Mending","Shield",
        "Steadfastness","Haste","Resistance","Escape","Empathy","Balance","Clarity","Precision","Serenity","Fortification","Transmutation","Adaptation",
        "Deflection","Persistence","Immunity","Invigoration","Reinforcement","Secrecy","Tranquility","Vigilance","Warmth","Wisdom","Enlightenment"
    };
    [SerializeField] List<string> tempNameList = new List<string>();
    HashSet<string> usedCombinations = new HashSet<string>(); // Track used combinations


    #endregion
    [SerializeField] List<RequestedPotion> allRequestablePotions = new List<RequestedPotion>() { };
    [SerializeField] List<RequestedPotion> requestedPotions = new List<RequestedPotion>() { };

    void Start()
    {
        tempNameList.AddRange(potionNameList);
        //UnityEngine.Random.InitState(seed);
        CreateAllPotions();
    }

    void CreateAllPotions()
    {
        for (int i = 0; i < 10; i++)
            CreatePotion(2);
        for (int i = 0; i < 25; i++)
            CreatePotion(3);
        for (int i = 0; i < 35; i++)
            CreatePotion(4);

        //Only 1 level 5 potion
        CreatePotion(5);
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
            newPotion.herbs.Add(newHerb);
        }

        if(potionLevel == 5)
        {
            newPotion.potionName = "Potion of Flight";
        }

        allRequestablePotions.Add(newPotion);
    }

    public RequestedPotion GetNewRecipeRequest()
    {
        RequestedPotion tempPotion = allRequestablePotions[0];
        allRequestablePotions.RemoveAt(0);
        return tempPotion;
    }

    public string CompareIngredientsAndGetPotionName(RequestedPotion newPotion)
    {
        foreach (RequestedPotion potion in allRequestablePotions)
        {
            if (potion.herbs.Count == newPotion.herbs.Count)
            {
                bool match = true;
                List<Herb> tempHerbs = new List<Herb>(potion.herbs);
    
                foreach (Herb herb in newPotion.herbs)
                {
                    Herb matchHerb = tempHerbs.Find(h => h.herbId == herb.herbId && h.workState == herb.workState);
                    if (matchHerb != null)
                    {
                        tempHerbs.Remove(matchHerb);
                    }
                    else
                    {
                        match = false;
                        break;
                    }
                }
    
                if (match && tempHerbs.Count == 0)
                {
                    return potion.potionName;
                }
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
