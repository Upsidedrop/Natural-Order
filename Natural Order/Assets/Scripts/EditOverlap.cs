using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditOverlap : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.y);
    }

}
