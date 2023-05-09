using Unity.VisualScripting;
using UnityEngine;

public class PlaceStructure : MonoBehaviour
{
    [SerializeField]
    GameObject stoneAgeHut;
    bool buildMode = false;
    GameObject house;

    void ToggleBuildMode()
    {
        buildMode = !buildMode;
        if (!buildMode)
        {
            GameObject.Destroy(house);
            return;
        }
        house = Instantiate(stoneAgeHut);
        SpriteRenderer houseRenderer = house.GetComponent<SpriteRenderer>();
        Color color = houseRenderer.color;
        color.a = 0.75f;
        houseRenderer.color = color;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Toggle Build Mode"))
        {
            ToggleBuildMode();
        }
        if (buildMode)
        {
            BuildModeUpdate();
        }
    }
    void BuildModeUpdate()
    {
        house.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    }
    void Place()
    {

    }
}
