using System.Collections.Generic;
using UnityEngine;

public class PotionGraphicsManager : MonoBehaviour
{
    [SerializeField] List<Sprite> bottomParts;
    [SerializeField] List<Sprite> middleParts;
    [SerializeField] List<Sprite> topParts;
    [SerializeField] List<Sprite> corks;
    [SerializeField] List<Color> bottleColors;

    Dictionary<string, int[]> potionGraphicsMapping = new Dictionary<string, int[]>();

    public int[] GetGraphicsForPotion(string potionIdentifier)
    {
        if (!potionGraphicsMapping.ContainsKey(potionIdentifier))
        {
            potionGraphicsMapping[potionIdentifier] = GenerateGraphicsIndices(potionIdentifier);
        }

        return potionGraphicsMapping[potionIdentifier];
    }

    private int[] GenerateGraphicsIndices(string identifier)
    {
        int hash = identifier.GetHashCode();
        int bottomIndex = Mathf.Abs(hash) % bottomParts.Count;
        int middleIndex = Mathf.Abs(hash / 10) % middleParts.Count;
        int topIndex = Mathf.Abs(hash / 100) % topParts.Count;
        int corkIndex = Mathf.Abs(hash / 1000) % corks.Count;
        int colorIndex = Mathf.Abs(hash / 10000) % bottleColors.Count;

        return new int[] { bottomIndex, middleIndex, topIndex, corkIndex, colorIndex };
    }

    public void ApplyGraphicsToPotion(GameObject potionGameObject, string potionIdentifier)
    {
        int[] indices = GetGraphicsForPotion(potionIdentifier);

        SpriteRenderer bottomRenderer = potionGameObject.transform.Find("Bottom").GetComponent<SpriteRenderer>();
        SpriteRenderer middleRenderer = potionGameObject.transform.Find("Middle").GetComponent<SpriteRenderer>();
        SpriteRenderer topRenderer = potionGameObject.transform.Find("Top").GetComponent<SpriteRenderer>();
        SpriteRenderer corkRenderer = potionGameObject.transform.Find("Cork").GetComponent<SpriteRenderer>();

        bottomRenderer.sprite = bottomParts[indices[0]];
        middleRenderer.sprite = middleParts[indices[1]];
        topRenderer.sprite = topParts[indices[2]];
        corkRenderer.sprite = corks[indices[3]];

        Color bottleColor = bottleColors[indices[4]];
        bottomRenderer.color = bottleColor;
        middleRenderer.color = bottleColor;
        topRenderer.color = bottleColor;
        corkRenderer.color = bottleColor;
    }
}
