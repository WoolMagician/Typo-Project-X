using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSpriteRotationToGroundSlope : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(Mathf.Abs(player.Core.Movement.GetSlopeAngle()) * 
                                                   player.Core.Movement.FacingDirection * 
                                                   Mathf.Sign(player.Core.Movement.GetSlopeParallel().x), 
                                                   this.transform.rotation.eulerAngles.y, 
                                                   this.transform.rotation.eulerAngles.z);
    }
}