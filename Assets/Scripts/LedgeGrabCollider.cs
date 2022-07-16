using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabCollider : MonoBehaviour
{
    public TombiCharacterController charScript;

    // Start is called before the first frame update
    void Start()
    {
        if (charScript == null)
        {
            charScript = FindObjectOfType<TombiCharacterController>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            charScript.OnHandleLedgeGrab(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            charScript.OnHandleLedgeGrabReset();
        }
    }
}
