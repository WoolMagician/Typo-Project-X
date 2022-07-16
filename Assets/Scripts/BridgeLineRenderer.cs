using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeLineRenderer : MonoBehaviour
{
    LineRenderer lineRenderer;

    public GameObject[] planks;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineRenderer.endWidth = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = planks.Length;

        for(int i = 0; i <= planks.Length; i++)
        {
            if (planks[i] != null)
            {
                if(planks[i].GetComponent<MeshRenderer>())
                {
                    planks[i].GetComponent<MeshRenderer>().enabled = false;

                }

                lineRenderer.SetPosition(i, planks[i].transform.position);

            }
        }

    }
}
