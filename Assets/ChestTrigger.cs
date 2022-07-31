using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public ChestSO chestSO;
    public bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger = chestSO.GetOpened();
    }

    // Update is called once per frame
    void Update()
    {        
        chestSO.SetOpened(trigger);
    }
}
