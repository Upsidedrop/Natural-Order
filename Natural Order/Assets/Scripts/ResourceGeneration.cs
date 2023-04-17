using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceGeneration : MonoBehaviour
{
    [SerializeField]
    GameObject[] resources;
    [SerializeField]
    int[] chances1In;
    MapGenerator MapGenerator;

    private void Awake()
    {
        MapGenerator = GetComponent<MapGenerator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
print(RandomGameobject().name);
        }
        
    }
    void PlaceResources(int index)
    {

    }
    GameObject RandomGameobject()
    {
        int totalWeight = 0;
        GameObject selectedResource = null;
        int randomNumber;
        int cumulativeWeight = 0;

        foreach (int value in chances1In)
        {
            totalWeight += value;
        }
        randomNumber = Random.Range(0, totalWeight);
        for (int i = 0; i < chances1In.Length; i++)
        {
            cumulativeWeight += chances1In[i];
            if (randomNumber < cumulativeWeight)
            {
                selectedResource = resources[i];
                break;
            }
        }
        return selectedResource;
    }
}
