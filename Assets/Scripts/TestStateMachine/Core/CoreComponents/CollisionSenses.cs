using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{

    #region Check Transforms

    public Transform GroundCheck
    {
        get => groundCheck;
        private set => groundCheck = value;
    }

    public Transform WallCheck
    {
        get => wallCheck;
        private set => wallCheck = value;
    }

    public Transform LedgeCheckHorizontal
    {
        get => ledgeCheckHorizontal;
        private set => ledgeCheckHorizontal = value;
    }

    public Transform LedgeCheckVertical
    {
        get => ledgeCheckVertical;
        private set => ledgeCheckVertical = value;
    }

    public Transform CeilingCheck
    {
        get => ceilingCheck;
        private set => ceilingCheck = value;
    }

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }
    public LayerMask WhatIsSwing { get => whatIsSwing; set => whatIsSwing = value; }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform wallCheck2;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
    [SerializeField] private Transform ceilingCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;

    [Header("Ground check settings")]
    private const float checkRaycastGroundedOffset = 0.75f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsSwing;
    [SerializeField] private float maxGroundedRadius = 0.1925f; //0.13
    [SerializeField] private Vector3 maxGroundedHeight; // 0, 0.95f, 0
    [SerializeField] private Vector3 maxGroundedDistanceDown = Vector3.down * 0.2f; // 0, -0.005, 0

    public Vector3 groundNormal = Vector3.zero;
    public Vector3 groundHit = Vector3.zero;

    [SerializeField] private Transform playerTransform;

    #endregion


    public bool Ceiling
    {
        get => Physics.OverlapSphere(CeilingCheck.position, groundCheckRadius, whatIsGround).Length > 0;
    }

    public bool WallFront
    {
        get => Physics.Raycast(WallCheck.position, Vector3.right * core.Movement.FacingDirection, out RaycastHit hit, wallCheckDistance, whatIsGround) && hit.collider.gameObject.tag != "SwingObject";
    }

    public bool LedgeHorizontal
    {
        get => Physics.Raycast(LedgeCheckHorizontal.position, Vector3.right * core.Movement.FacingDirection, out RaycastHit hit, wallCheckDistance, whatIsGround) && hit.collider.gameObject.tag != "SwingObject";
    }

    public bool Swing
    {
        get => Physics.Raycast(LedgeCheckHorizontal.position, Vector3.right * core.Movement.FacingDirection, out RaycastHit hit, wallCheckDistance, whatIsSwing) && hit.collider.gameObject.tag == "SwingObject";
    }

    public bool LedgeVertical
    {
        get => Physics.Raycast(LedgeCheckVertical.position, Vector3.down, wallCheckDistance, whatIsGround);
    }

    public bool WallBack
    {
        get => Physics.Raycast(WallCheck.position, Vector3.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    public bool IsGrounded()
    {
        //bool isGroundedBySphereOverlap = Physics.OverlapSphere(GroundCheck.position, groundCheckRadius, whatIsGround).Length > 0;

        //if (isGroundedBySphereOverlap)
        //{
            bool result = this.IsGrounded(out RaycastHit hit);
            groundNormal = result ? hit.normal : Vector3.up;
            return result;
        //}
        //return false;
    }

    private bool IsGrounded(out RaycastHit hit)
    {
        bool thresholdDistanceGrounded = false;
        Vector3 offset = new Vector3(0, .45f, 0);
        Vector3 offsetPosition = playerTransform.position + offset;
        Ray downcastFromOffsetPositionRay = new Ray(offsetPosition, Vector3.down);

        if (Physics.Raycast(downcastFromOffsetPositionRay, out hit, 0.55f, whatIsGround))
        {
            //If distance is less than threshold we are grounded
            thresholdDistanceGrounded = hit.distance < maxGroundedDistanceDown.y;
        }
        return thresholdDistanceGrounded ||
               this.IsGroundedRaycast(whatIsGround, out hit);
    }

    bool IsGroundedRaycast(LayerMask layerMask, out RaycastHit hit)
    {
        Vector3 pos = playerTransform.position;
        float fullRadius = maxGroundedRadius;
        float halfRadius = (maxGroundedRadius / 2);
        return (Physics.Linecast(pos + maxGroundedHeight, pos + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos - playerTransform.forward * halfRadius + maxGroundedHeight, pos - playerTransform.forward * halfRadius + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos + playerTransform.forward * halfRadius + maxGroundedHeight, pos + playerTransform.forward * halfRadius + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos - playerTransform.right * halfRadius + maxGroundedHeight, pos - playerTransform.right * halfRadius + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos + playerTransform.right * halfRadius + maxGroundedHeight, pos + playerTransform.right * halfRadius + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos - playerTransform.forward * halfRadius - playerTransform.right * halfRadius + maxGroundedHeight, pos - playerTransform.forward * halfRadius - playerTransform.right * halfRadius + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos + playerTransform.forward * halfRadius + playerTransform.right * halfRadius + maxGroundedHeight, pos + playerTransform.forward * halfRadius + playerTransform.right * halfRadius + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos - playerTransform.forward * halfRadius + playerTransform.right * halfRadius + maxGroundedHeight, pos - playerTransform.forward * halfRadius + playerTransform.right * halfRadius + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos + playerTransform.forward * halfRadius - playerTransform.right * halfRadius + maxGroundedHeight, pos + playerTransform.forward * halfRadius - playerTransform.right * halfRadius + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos - playerTransform.forward * fullRadius + maxGroundedHeight, pos - playerTransform.forward * (maxGroundedRadius) + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos + playerTransform.forward * fullRadius + maxGroundedHeight, pos + playerTransform.forward * (maxGroundedRadius) + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos + playerTransform.right * fullRadius + maxGroundedHeight, pos + playerTransform.right * (maxGroundedRadius) + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos - playerTransform.right * fullRadius + maxGroundedHeight, pos - playerTransform.right * (maxGroundedRadius) + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos - playerTransform.forward * (fullRadius * checkRaycastGroundedOffset) - playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedHeight, pos - playerTransform.forward * (maxGroundedRadius * checkRaycastGroundedOffset) - playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos + playerTransform.forward * (fullRadius * checkRaycastGroundedOffset) + playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedHeight, pos + playerTransform.forward * (maxGroundedRadius * checkRaycastGroundedOffset) + playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos - playerTransform.forward * (fullRadius * checkRaycastGroundedOffset) + playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedHeight, pos - playerTransform.forward * (maxGroundedRadius * checkRaycastGroundedOffset) + playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedDistanceDown, out hit, layerMask)
         || Physics.Linecast(pos + playerTransform.forward * (fullRadius * checkRaycastGroundedOffset) - playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedHeight, pos + playerTransform.forward * (maxGroundedRadius * checkRaycastGroundedOffset) - playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedDistanceDown, out hit, layerMask));
             
    }

    private void OnDrawGizmos()
    {
        Color oldGizmoColor = Gizmos.color;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(CeilingCheck.position, groundCheckRadius);
        Gizmos.color = oldGizmoColor;

        Vector3 pos = playerTransform.position;
        float fullRadius = maxGroundedRadius;
        float halfRadius = (maxGroundedRadius / 2);
        Debug.DrawLine(pos + maxGroundedHeight, pos + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - playerTransform.forward * halfRadius + maxGroundedHeight, pos - playerTransform.forward * halfRadius + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + playerTransform.forward * halfRadius + maxGroundedHeight, pos + playerTransform.forward * halfRadius + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - playerTransform.right * halfRadius + maxGroundedHeight, pos - playerTransform.right * halfRadius + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + playerTransform.right * halfRadius + maxGroundedHeight, pos + playerTransform.right * halfRadius + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.forward * (maxGroundedRadius / 2) - transform.right * (maxGroundedRadius / 2) + maxGroundedHeight, pos - transform.forward * (maxGroundedRadius / 2) - transform.right * (maxGroundedRadius / 2) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.forward * (maxGroundedRadius / 2) + transform.right * (maxGroundedRadius / 2) + maxGroundedHeight, pos + transform.forward * (maxGroundedRadius / 2) + transform.right * (maxGroundedRadius / 2) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.forward * (maxGroundedRadius / 2) + transform.right * (maxGroundedRadius / 2) + maxGroundedHeight, pos - transform.forward * (maxGroundedRadius / 2) + transform.right * (maxGroundedRadius / 2) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.forward * (maxGroundedRadius / 2) - transform.right * (maxGroundedRadius / 2) + maxGroundedHeight, pos + transform.forward * (maxGroundedRadius / 2) - transform.right * (maxGroundedRadius / 2) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.forward * (maxGroundedRadius) + maxGroundedHeight, pos - transform.forward * (maxGroundedRadius) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.forward * (maxGroundedRadius) + maxGroundedHeight, pos + transform.forward * (maxGroundedRadius) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.right * (fullRadius) + maxGroundedHeight, pos - transform.right * (maxGroundedRadius) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.right * (fullRadius) + maxGroundedHeight, pos + transform.right * (maxGroundedRadius) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - playerTransform.forward * (fullRadius * checkRaycastGroundedOffset) - playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedHeight, pos - playerTransform.forward * (maxGroundedRadius * checkRaycastGroundedOffset) - playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + playerTransform.forward * (fullRadius * checkRaycastGroundedOffset) + playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedHeight, pos + playerTransform.forward * (maxGroundedRadius * checkRaycastGroundedOffset) + playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - playerTransform.forward * (fullRadius * checkRaycastGroundedOffset) + playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedHeight, pos - playerTransform.forward * (maxGroundedRadius * checkRaycastGroundedOffset) + playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + playerTransform.forward * (fullRadius * checkRaycastGroundedOffset) - playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedHeight, pos + playerTransform.forward * (maxGroundedRadius * checkRaycastGroundedOffset) - playerTransform.right * (maxGroundedRadius * checkRaycastGroundedOffset) + maxGroundedDistanceDown, Color.yellow);

    }
}