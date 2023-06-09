using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOfObjects : MonoBehaviour
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Transform objectParent = null;

    [Space, Header("Tick this to generate the grid. It won't give you any tasty feedback. It'll just build the grid")]
    [SerializeField] private bool buildGrid = false;

    [Header("You didn't like the grid? You set an objectParent? DESTROY ALL CHILDREN")]
    [SerializeField] private bool destroyGrid = false;

    [Space, Header("Grid Stats")]
    [SerializeField] private int x_Length = 0;//Normally wouldn't put an underscore but I feel like the x and y are important.
    [SerializeField] private int y_Length = 0;

    [SerializeField] private float distBetweenObjects = 1f;
    void OnValidate()
    {
        if (prefab == null) 
        {
            Debug.LogWarning("There's no prefab object on " + name + " so the grid won't be built", this);
        }

        if (buildGrid) 
        {
            buildGrid = false;

            Vector3 buildPos;
            float basePosition = 0f;//Where, in elevation, the objects start

            float start_X = transform.position.x;
            float start_Z = transform.position.z;

            for (int x = 0; x < x_Length; x++)
            {
                for (int y = 0; y < y_Length; y++)
                {
                    buildPos = new Vector3(start_X + (x * distBetweenObjects), basePosition,start_Z + (y * distBetweenObjects));

                    var o = Instantiate(prefab);

                    o.name = $"{prefab.name}_{x}_{y}";

                    o.transform.position = buildPos;

                    if (objectParent)
                        o.transform.parent = objectParent;
                }
            }
        }

        if (destroyGrid) 
        {
            destroyGrid = false;

            if (objectParent == null)
                return;

            int length = objectParent.childCount;

            for (int i = 0; i < length; i++)
            {
                DestroyImmediate(objectParent.GetChild(i));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
