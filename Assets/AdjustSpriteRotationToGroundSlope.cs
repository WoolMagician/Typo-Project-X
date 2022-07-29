using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSpriteRotationToGroundSlope : MonoBehaviour
{
    [SerializeField] private Player player;

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(Mathf.Abs(player.Core.Movement.GetSlopeAngle()) * 
                                                   player.Core.Movement.FacingDirection * 
                                                   Mathf.Sign(player.Core.Movement.GetSlopeParallel().x), 
                                                   this.transform.rotation.eulerAngles.y, 
                                                   this.transform.rotation.eulerAngles.z);
    }
}