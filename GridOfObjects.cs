using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOfObjects : MonoBehaviour
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Transform objectParent = null;

    [Space, Header("Tick this to generate the grid. It won't give you any tasty feedback. It'll just build the grid")]
    [SerializeField] private bool buildGrid = false;

    [Space, Header("Grid Stats")]
    [SerializeField] private int x_Length = 0;//Normally wouldn't put an underscore but I feel like the x and y are important.
    [SerializeField] private int y_Length = 0;
    
    [SerializeField] private bool useObjectsSize = true; //the grid will adapt to the size of the object. 
    [SerializeField] private float fallback_distBetweenObjects = 1f; //Ideally use the object's size
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

            float x_Dist = fallback_distBetweenObjects;
            float z_Dist = fallback_distBetweenObjects ;
           
            //Adapt the size of the grid to the size of the object.
            if (useObjectsSize && prefab.TryGetComponent(out MeshFilter mf))
            {
                x_Dist = prefab.transform.localScale.x * mf.sharedMesh.bounds.size.x;
                z_Dist = prefab.transform.localScale.z * mf.sharedMesh.bounds.size.z;
            }

            for (int x = 0; x < x_Length; x++)
            {
                for (int y = 0; y < y_Length; y++)
                {
                    buildPos = new Vector3(start_X + (x * x_Dist), basePosition, start_Z + (y * z_Dist));

                    var o = Instantiate(prefab);

                    o.name = $"{prefab.name}_{x}_{y}";

                    o.transform.position = buildPos;

                    if (objectParent)
                        o.transform.parent = objectParent;
                }
            }
        }
    }

}
