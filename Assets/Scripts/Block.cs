using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float minHeight;
    public float maxHeight;
    public GameObject root;
    public GameObject BottomBlock;
    // Start is called before the first frame update
    void Start()
    {
        ChangeHeight();
    }

    void ChangeHeight()
    {
        float height = Random.Range(minHeight, maxHeight);
        root.transform.localPosition = new Vector3(0f, height, 0f);
        float height2 = Random.Range(-3f, -1f);
        BottomBlock.transform.localPosition = new Vector3(0f, height2, 0f);
    }

    void OnScrollEnd()
    {
        ChangeHeight();
    }

}
