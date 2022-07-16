using System.Collections;
using UnityEngine;

public class TombiCharacterController : SerializableMonoBehaviour
{
    public static TombiCharacterController singletonInstance;

    public override string FileExtension { get => ".smtc"; protected set => base.FileExtension = value; }
    public override string FileExtensionName { get => "SMTC"; protected set => base.FileExtensionName = value; }

    #region CUSTOM CLASSES
    [System.Serializable]
    public class LGWCController
    {
        [ShowOnly]
        public bool canLedgeGrab = true;

        [ShowOnly]
        public bool isLedgeGrabbing = false;

        public Vector3 ledgeGrabOffset = Vector3.zero;

        //[EditorRename("Enable Contact Wall Snap")]
        //public bool contactWallSnap = true;
        //public float rayDistanceFromPlayerPosition = 0.3f;
        //public float snapLerpFactor = 4f;

        //public float ledgeClimbProgress = 0f;
        //public float ledgeYPosAdjustment = 0f;
        //public float pushBackMultiplierOnJumpOff = 1f;
        //public float pushUpMultiplierOnJumpOff = 1f;
        //public float distanceFromWall = 0.04f;
        //public float distanceFromWallWhenLedgeSnapped = 0.04f;
        //public float distanceFromWallWhenLedgeSnappedSlope = 0.3f;

        //public float distanceFromLedgeZ = 0.04f;
        //public float distanceFromLedgeY = 0.04f;
        //public float climbUpSpeed = 1.2f;
        //public float climbFallSpeed = 2f;
        public LayerMask climbableSurfaces;


        [HideInInspector]
        public Vector3[] climbDetectionRays = new Vector3[5];

        [HideInInspector]
        public Vector3[] ledgeDetectionRays = new Vector3[2];

        [HideInInspector]
        public Vector3[] ledgeDetectionRaysClimbOffset = new Vector3[2];

        [HideInInspector]
        public Vector3[] climbDetectionRaysHits = new Vector3[5];

        //[Header("Wall Snap Settings")]
        //public float wallSnapRayDistance = 0.2f;
        //public float hasWallSnappedThresholdDistance = 0.1f;
        //public float hasWallSnappedResetThresholdDistance = 0.15f;

        //[Space(5)]
        //public float climbDetectionRayUpOffset = 0.3f;
        //public float climbDetectionRayDownOffset = 0.2f;
        //public float climbDetectionRayRightOffset = 0.2f;
        //public float climbDetectionRayLeftOffset = 0.2f;
        //public float climbDetectionRayOverheadOffset = 0.1f;

        //[Header("Ledge Snap Settings")]
        //public float ledgeSnapRayDistance = 0.2f;

        //[EditorRename("Has Ledge Snapped Threshold Distance")]
        //public float hasLedgeSnappedThresholdDistance = 0.1f;

        //[EditorRename("Has Ledge Snapped Threshold Distance (Reset)")]
        //public float hasLedgeSnappedResetThresholdDistance = 26.2f;

        //[Space(5)]
        //public float ledgeSnapFirstRayOffsetRight = 0.2f;
        //public float ledgeSnapFirstRayOffsetUp = 0.85f;
        //public float ledgeSnapFirstRayOffsetForward = 0.2f;
        //public float ledgeSnapSecondRayOffsetRight = 0.2f;
        //public float ledgeSnapSecondRayOffsetUp = 0.85f;
        //public float ledgeSnapSecondRayOffsetForward = 0.2f;
        //public float ledgeDetectionRayClimbRayUpOffset = 0.2f;
        //[Header("Wall Climb Settings")]


        //[Header("Ledge Climb Settings")]
        //public float canLedgeClimbDistance = 0.4f;
        //public float ledgeClimbSlerpFactor = 20f;

        public LGWCController()
        {
            //ledgeClimbSpline = new BezierSpline();
        }

        public void ResetFlags()
        {

        }
    }

    [System.Serializable]
    public class PlayerStateController
    {
        [ShowOnly] public bool isMoving = false;
        [ShowOnly] public bool isSliding = false;
        [ShowOnly] public bool isJumping = false;
        [ShowOnly] public bool isGrounded;
        [ShowOnly] public bool isFalling;
        [ShowOnly] public bool isStrafing = false;
        [ShowOnly] public bool isRolling = false;
        [ShowOnly] public bool isDead = false;
        [ShowOnly] public bool isBlocking = false;
        [ShowOnly] public bool isKnockback;
        [ShowOnly] public bool canAction = true;
        [ShowOnly] public bool canMove = true;
        [ShowOnly] public bool canJump;
        [ShowOnly] public bool onBridge = false;
        [ShowOnly] public bool onPumpRock = false;
        [ShowOnly] public bool onSlope = false;
        [ShowOnly] public bool onSteepSlope = false;
        [ShowOnly] public bool canApplyGravity = false;
    }

    [System.Serializable]
    public class MovementSettings
    {
        [Header("General Settings")]
        public bool lockOnZAxis = true;
        public bool moveOnly = false;

        [Header("Particles")]
        public GameObject runParticles;

        [Header("Speed Settings")]
        public float jumpSpeed = 5;
        //public float wallJumpSpeed = 5f;
        //public float swingJumpSpeed = 5f;
        public float runSpeed = 3f;
        public float animalDashSpeed = 5f;
        //public float inAirSpeed = 0.6f;
        public float walkSpeed = 2f;
        public float rotationSpeed = 40f;

        [Curve("Air Acceleration Curve (speed over time)", 0, 0, 5f, 1f, true, CurveAttribute.CurveColor.Cyan)]
        public AnimationCurve airAccelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(.5f, 1) });

        [Curve("Air Momentum Curve (speed over time)", 0, 0, 5f, 1f, true, CurveAttribute.CurveColor.Cyan)]
        public AnimationCurve airMomentumCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(.25f, 0) });

        [Curve("Grounded Acceleration Curve (speed over time)", 0, 0, 5f, 1f, true, CurveAttribute.CurveColor.Cyan)]
        public AnimationCurve groundedAccelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(.5f, 1) });

        [Curve("Grounded Deceleration Curve (speed over time)", 0, 0, 5f, 1f, true, CurveAttribute.CurveColor.Cyan)]
        public AnimationCurve groundedDecelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(.25f, 0) });
    }

    [System.Serializable]
    public class PhysicsSettings
    {
        public LayerMask collidingMask;

        [Header("Gravity Settings")]
        public float groundGravity = 35; //-20
        //public float bridgeGravity = -9.8f; // -20

        [Header("Ground Check Settings")]
        public float maxGroundedRadius = 0.1925f; //0.13
        public Vector3 maxGroundedHeight; // 0, 0.95f, 0
        public Vector3 maxGroundedDistanceDown = Vector3.down * 0.2f; // 0, -0.005, 0
    }

    [System.Serializable]
    public class WeaponController
    {
        public GameObject equippedWeapon;
    }

    [System.Serializable]
    public class BaseGrabController
    {
        [Header("Grab Flags")]
        [ShowOnly] public bool canGrab = false;
        [ShowOnly] public bool isGrabbing = false;
        [ShowOnly] public bool hasGrabbed = false;

        public GameObject grabbedObj = null;

        public float onGrabColliderHeightOffset = 0f;
        public Vector3 onGrabColliderOffset = Vector3.zero;

        [Header("Grab Tags List")]
        public System.Collections.Generic.List<string> grabbableTags;

    }

    [System.Serializable]
    public class ObjectGrabController : BaseGrabController
    {
        [Header("Throw Flags")]
        [ShowOnly] public bool waitForThrow = false;
        [ShowOnly] public bool isThrowing = false;

        public void PlayObjectGrabSound(GrabObject.eType type)
        {
            //FindObjectOfType<AudioManager>().Play("Land_Grab");

            switch (type)
            {
                case GrabObject.eType.Chest:
                    //FindObjectOfType<AudioManager>().Play("Chest_Grab");
                    break;

                case GrabObject.eType.Pig:
                    //FindObjectOfType<AudioManager>().Play("Pig_Grab");
                    break;

                case GrabObject.eType.Bird:
                    break;

                default:
                    break;
            }
        }
    }

    [System.Serializable]
    public class EnvironmentGrabController : BaseGrabController
    {

    }
    #endregion

    #region FIELDS AND PROPERTIES

    /* COMPONENTS */
    //public AudioManager AudioManager { get; private set; }
    //public Animator Animator { get; private set; }
    //public Rigidbody Rigidbody { get; private set; }
    public CharacterController _controller { get; private set; }

    public InputReader _inputReader;

    [Header("General Variables")]

    /* IDLE VARIABLES */
    private const float idleTime = 5f;
    private float currentIdleTime = 0f;

    private float inputDashVertical;
    private float inputDashHorizontal;
    private float inputHorizontal = 0;
    private float inputVertical = 0;
    private Vector3 finalMotionVector;

    [Header("Input Buffering")]

    /* JUMP INPUT BUFFERING */
    [SerializeField]
    private int _jumpBufferMaxFrames = 10;
    public int JumpBufferMaxFrames { get { return _jumpBufferMaxFrames; } private set { _jumpBufferMaxFrames = value; } }
    public int JumpBufferElapsedFrames { get; private set; } = 100;

    /* COYOTE TIME */
    [SerializeField]
    private int _coyoteTimeMaxFrames = 10;
    public int CoyoteTimeMaxFrames { get { return _coyoteTimeMaxFrames; } private set { _coyoteTimeMaxFrames = value; } }
    public int CoyoteTimeElapsedFrames { get; private set; } = 0;

    public Vector3 InputVector { get; private set; }
    public Vector3 LastInputVector { get; private set; }
    public Vector3 LastCompleteInputVector { get; private set; }
    public Vector3 CompleteInputVector { get; private set; }
    public float LastInputHorizontal { get; private set; }

    [SerializeField]
    private Camera _sceneCamera;
    public Camera SceneCamera { get { return _sceneCamera; } private set { _sceneCamera = value; } }

    public GameObject stomachRigJoint;

    private Vector3 targetDashDirection;
    private Typo.Utilities.Cameras.BasicCameraController camScript;

    private Vector3 groundNormal = Vector3.zero;
    private Vector3 groundHit = Vector3.zero;
    private Vector2 ReadInputVector;

    private const float checkRaycastGroundedOffset = 0.75f;

    // Used for continuing momentum while in air
    private const float maxVelocity = 5f;
    private const float minVelocity = -5f;    

    public bool Initialized { get; private set; } = false;

    [Header("Settings and Controllers")]
    /* TOMBA SETTINGS */
    public MovementSettings movementSettings = new MovementSettings();
    public PhysicsSettings physicsSettings = new PhysicsSettings();

    /* TOMBA CONTROLLERS */
    public LGWCController parkourController = new LGWCController();
    public PlayerStateController stateController = new PlayerStateController();
    //public WeaponController weaponController = new WeaponController();
    //public ObjectGrabController objGrabController = new ObjectGrabController();
    //public EnvironmentGrabController envGrabController = new EnvironmentGrabController();

    #endregion

    float elapsedPressTime = 0;
    float elapsedReleaseTime = 0;
    float maxInputHorizontal = 0;
    bool firstRelease = false;
    bool storedMaxVel = false;
    float originalStepOffset;
    int notGroundedFilterFrames = 0;
    int currentNotGroundedFrames = 0;

    //Adds listeners for events being triggered in the InputReader script
    private void OnEnable()
    {
        //_inputReader.JumpEvent += OnJumpInitiated;
        //_inputReader.JumpCanceledEvent += OnJumpCanceled;
        _inputReader.MoveEvent += OnMove;
        //_inputReader.StartedRunning += OnStartedRunning;
        //_inputReader.StoppedRunning += OnStoppedRunning;
        //_inputReader.AttackEvent += OnStartedAttack;
        //...
    }

    //Removes all listeners to the events coming from the InputReader script
    private void OnDisable()
    {
        //_inputReader.JumpEvent -= OnJumpInitiated;
        //_inputReader.JumpCanceledEvent -= OnJumpCanceled;
        _inputReader.MoveEvent -= OnMove;
        //_inputReader.StartedRunning -= OnStartedRunning;
        //_inputReader.StoppedRunning -= OnStoppedRunning;
        //_inputReader.AttackEvent -= OnStartedAttack;
        //...
    }

    private bool Initialize()
    {
        //// Get the animator component
        //Animator = GetComponentInChildren<Animator>();

        //// Get the audiomanager component
        //AudioManager = FindObjectOfType<AudioManager>();

        //// Get the rigidbody component
        //Rigidbody = GetComponent<Rigidbody>();

        // Get the camera controller component
        camScript = FindObjectOfType<Typo.Utilities.Cameras.BasicCameraController>();

        _controller = GetComponent<CharacterController>();

        ///* 
        // * Check if audiomanager and camera script were found
        // * Animator and RigidBody are required components. 
        // */
        //if (AudioManager == null)
        //{
        //    Debug.LogWarning("'AudioManager' script was not found by 'TombiCharacterController'.");
        //    return false;
        //}

        //if (AudioManager == null)
        //{
        //    Debug.LogError("'BasicCameraController' script was not found by 'TombiCharacterController'.");
        //    return false;
        //}

        //// Check if animator has controller and avatar
        //if (Animator.avatar == null)
        //{
        //    Debug.LogError("'Animator' component of 'TombiCharacterController' has no 'Avatar' attached to it.");
        //    return false;
        //}

        //if (Animator.runtimeAnimatorController == null)
        //{
        //    Debug.LogError("'Animator' component of 'TombiCharacterController' has no 'Controller' attached to it.");
        //    return false;
        //}

        //Initialize climb and ledge detection rays
        parkourController.climbDetectionRays = new Vector3[5];
        parkourController.ledgeDetectionRays = new Vector3[2];

        //Force last input to RIGHT
        LastInputHorizontal = 1;

        if (stomachRigJoint != null && movementSettings.runParticles != null)
        {
            movementSettings.runParticles.transform.parent = stomachRigJoint.transform;
        }

        return true;
    }


    void Awake()
    {
        //Singleton handling
        if (singletonInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singletonInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Initialized = Initialize();

        originalStepOffset = _controller.stepOffset;

        if (!Initialized)
        {
            Debug.LogError("TombiCharacterController script wasn't initialized properly, the script will not execute any function.");
        }
        else
        {
            Debug.Log("TombiCharacterController script has been initialized.");
        }
    }

    void Update()
    {
        //Make sure that the script has been initialized properly.
        if (this.Initialized)
        {
            // Fix character controller stepOffset bug
            _controller.stepOffset = this.stateController.isGrounded ? originalStepOffset : 0;

            this.stateController.canApplyGravity = !this.parkourController.isLedgeGrabbing;

            //Update movement vectors
            this.UpdateMovementVectors();

            //Update movement of the character with pre-calculated movement vectors
            this.UpdateMovement();

            //Check if we are grounded or not
            this.UpdateGroundedState();

            //Handle press of jump button
            this.HandleJump();
        }
    }

    public void OnHandleLedgeGrab(GameObject hitLedgeObject)
    {
        if (this.parkourController.canLedgeGrab)
        {
            this.stateController.isSliding = false;
            this.stateController.onSteepSlope = false;
            this.parkourController.isLedgeGrabbing = true;
            this.parkourController.canLedgeGrab = false;
        }

        if (this.parkourController.isLedgeGrabbing)
        {
            //Reposition player to make it attached to ledge
            this.transform.position = hitLedgeObject.transform.position - (this.transform.forward * 0.17f) - this.parkourController.ledgeGrabOffset;
        }
    }

    public void OnHandleLedgeGrabReset()
    {
        this.parkourController.isLedgeGrabbing = false;
        this.parkourController.canLedgeGrab = true;
    }

    //Rotate character towards direction
    public void RotateTowardsMovementDir(Vector3 dirVector, bool instantRotation)
    {
        if (dirVector == Vector3.zero) return;
        this.transform.rotation = instantRotation ? Quaternion.LookRotation(dirVector) : Quaternion.Slerp(this.transform.rotation,
                                                                                                          Quaternion.LookRotation(dirVector),
                                                                                                          Time.deltaTime * movementSettings.rotationSpeed);
        
    }

    private void OnMove(Vector2 movement)
    {
        this.ReadInputVector = movement;
    }

    void UpdateMovementVectors()
    {
        float horizontalMovement = this.ReadInputVector.x;
        float verticalMovement = this.ReadInputVector.x;

        //Check if horizontal axis is not zero
        if (horizontalMovement != 0)
        {
            elapsedPressTime += Time.deltaTime;
            elapsedReleaseTime = 0;
            storedMaxVel = false;
            firstRelease = false;
        }

        // If we switched input vector or we just stopped
        if ((horizontalMovement != LastInputHorizontal) || (horizontalMovement == 0))
        {
            elapsedReleaseTime += Time.deltaTime;
            elapsedPressTime = 0;

            if (!storedMaxVel)
            {
                firstRelease = true;
            }
        }

        if (firstRelease && !storedMaxVel)
        {
            Keyframe[] keyframes = movementSettings.groundedDecelerationCurve.keys;

            keyframes[0].time = 0;
            keyframes[0].value = Mathf.Abs(inputHorizontal);

            movementSettings.groundedDecelerationCurve.keys = keyframes;

            keyframes = movementSettings.airMomentumCurve.keys;
            keyframes[0].time = 0;
            keyframes[0].value = Mathf.Abs(inputHorizontal);

            movementSettings.airMomentumCurve.keys = keyframes;

            storedMaxVel = true;
            firstRelease = false;
        }

        if (horizontalMovement > 0)
        {
            if (stateController.canMove)
            {
                inputHorizontal = horizontalMovement;
                LastInputHorizontal = horizontalMovement;
            }
        }

        if (horizontalMovement < 0)
        {
            if (stateController.canMove)
            {
                inputHorizontal = horizontalMovement;
                LastInputHorizontal = horizontalMovement;
            }
        }

        if (!stateController.isGrounded)
        {
            if (horizontalMovement == 0)
            {
                inputHorizontal = movementSettings.airMomentumCurve.Evaluate(elapsedReleaseTime) * LastInputHorizontal;

            }
            else
            {
                inputHorizontal = movementSettings.airAccelerationCurve.Evaluate(elapsedPressTime) * horizontalMovement;

            }

        }
        else if (horizontalMovement != 0)
        {
            inputHorizontal = movementSettings.groundedAccelerationCurve.Evaluate(elapsedPressTime) * horizontalMovement;

        }
        else if (horizontalMovement == 0)
        {
            inputHorizontal = movementSettings.groundedDecelerationCurve.Evaluate(elapsedReleaseTime) * LastInputHorizontal;
        }


        //Check if we can acquire vertical inputs
        if (!movementSettings.lockOnZAxis)
        {
            inputVertical = verticalMovement * 1f;
        }

        //converts control input vectors into camera facing vectors
        Transform cameraTransform = SceneCamera.transform;

        //Forward vector relative to the camera along the x-z plane   
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        //Right vector relative to the camera always orthogonal to the forward vector
        //Vector3 right = new Vector3(forward.z, 0, -forward.x);
        Vector3 right;

        if (movementSettings.lockOnZAxis)
        {
            right = Vector3.right;
        }
        else
        {
            right = new Vector3(forward.z, 0, -forward.x);
        }

        CompleteInputVector = inputHorizontal * right + inputVertical * forward;

        if (!movementSettings.lockOnZAxis)
        {
            InputVector = CompleteInputVector;
        }
        else
        {
            InputVector = inputHorizontal * right;
        }

        if (InputVector != Vector3.zero)
        {
            LastInputVector = InputVector;
        }

        if (CompleteInputVector != Vector3.zero)
        {
            LastCompleteInputVector = CompleteInputVector;
        }
    }

    /// <summary>
    /// Routine used to update grounded state by mixing character controller ground state
    /// with downwards-circle raycasting
    /// </summary>
    void UpdateGroundedState()
    {
        //Store raycast result
        bool raycastGrounded = this.IsGrounded(this.physicsSettings.maxGroundedDistanceDown.y, out RaycastHit hit);

        //Store unfiltered grounded flag
        bool unfilteredGrounded = (this._controller.isGrounded || raycastGrounded) &&                                    
                                   !this.stateController.isJumping && 
                                   !this.parkourController.isLedgeGrabbing;

        //Update grounding informations
        groundHit = raycastGrounded ? hit.point : this.transform.position;
        groundNormal = raycastGrounded ? hit.normal : Vector3.up;

        //If we are grounded reset grounding filter
        if (unfilteredGrounded)
        {
            //This check should avoid to snap on flat surfaces after jumping
            if (!this.IsOnSlope())
            {
                if(this._controller.velocity.y <= 0)
                {
                    this.stateController.isGrounded = true;
                    this.currentNotGroundedFrames = 0;
                }
            }
            else
            {
                this.stateController.isGrounded = true;
                this.currentNotGroundedFrames = 0;
            }
        }
        else
        {
            //Reset grounded only after filter frames are elapsed
            if (this.currentNotGroundedFrames >= notGroundedFilterFrames)
            {
                this.stateController.isGrounded = false;
            }
            else
            {
                this.currentNotGroundedFrames++;    
            }
        }
    }

    void UpdateMovement()
    {
        // Update movement vectors based on camera and input
        if(!this.stateController.isSliding) this.UpdateMovementVectors();

        // Cache input vector
        Vector3 motionVector = this.InputVector.magnitude > 0.01f ? this.InputVector : Vector3.zero;

        // Calculate motion vector based on if we are grounded or not
        if (this.stateController.isGrounded && 
            this.stateController.isMoving) {

            // Adjust velocity to slope
            finalMotionVector = this.AdjustMotionVectorToGroundSlope(motionVector * movementSettings.runSpeed);
        }
        else
        {
            // If head collides interrupt jump
            if (IsHeadCollidingRaycast(this.physicsSettings.collidingMask)) this.stateController.isJumping = false;

            if (this.stateController.isJumping)
            {
                finalMotionVector = (motionVector.normalized / 2f) * this.movementSettings.jumpSpeed;
                finalMotionVector.y = this.movementSettings.jumpSpeed;                
            }
            else
            {
                //If we are falling use momentum
                finalMotionVector = (motionVector / 4.5f) + new Vector3(finalMotionVector.x * 0.95f,
                                                                        _controller.velocity.y,
                                                                        finalMotionVector.z * 0.95f);
            }

            //Clamp vertical speed
            finalMotionVector.y = Mathf.Clamp(finalMotionVector.y, -10, 10);
        }

        // If we have grabbed a ledge, reset motion vector to zero
        if (this.parkourController.isLedgeGrabbing){ finalMotionVector = Vector3.zero; }

        // Apply gravity
        if (this.stateController.canApplyGravity)
        {
            if (Input.GetButton("PS4_X") && _controller.velocity.y >= 0)
            {
                finalMotionVector.y -= (physicsSettings.groundGravity / 2f) * Time.deltaTime;
            }
            else
            {
                finalMotionVector.y -= physicsSettings.groundGravity * Time.deltaTime;
            }
        }

        // Rotate towards last input vector
        this.stateController.isMoving = finalMotionVector != Vector3.zero;
        if (!this.parkourController.isLedgeGrabbing) this.RotateTowardsMovementDir(LastInputVector, true);

        // Apply motion vector
        this._controller.Move(finalMotionVector * Time.deltaTime);
    }

    private Vector3 AdjustMotionVectorToGroundSlope(Vector3 motionVector)
    {
        float currentSlope = this.GetSlopeAngle();

        var slopeRotation = Quaternion.FromToRotation(this.transform.up, groundNormal);
        var adjustedVelocity = slopeRotation * motionVector;

        Vector3 groundParallel = Vector3.Cross(this.transform.up, groundNormal);
        Vector3 slopeParallel = Vector3.Cross(groundParallel, groundNormal);

        this.stateController.onSteepSlope = currentSlope > 55f;
        this.stateController.isSliding = this.stateController.onSteepSlope && this._controller.velocity.y < 0.5f;            

        if(this.stateController.onSteepSlope) return finalMotionVector + slopeParallel.normalized / 3f;
        if (adjustedVelocity.y < 0) { return adjustedVelocity; }

        return motionVector;

    }

    float GetSlopeAngle()
    {
        return Mathf.Round(Vector3.Angle(groundNormal, this.transform.up));
    }

    bool IsOnSlope()
    {
        return this.GetSlopeAngle() > 0;
    }

    bool IsGrounded(float threshold, out RaycastHit hit)
    {
        bool thresholdDistanceGrounded = false;
        Vector3 offset = new Vector3(0, .45f, 0);
        Vector3 offsetPosition = this.transform.position + offset;
        Ray downcastFromOffsetPositionRay = new Ray(offsetPosition, Vector3.down);

        if (Physics.Raycast(downcastFromOffsetPositionRay, out hit, 100f, physicsSettings.collidingMask))
        {
            //If distance is less than threshold we are grounded
            thresholdDistanceGrounded = hit.distance < threshold;
        }
        return thresholdDistanceGrounded ||
               this.IsGroundedRaycast(physicsSettings.collidingMask, out hit);
    }

    bool IsGroundedRaycast(LayerMask layerMask, out RaycastHit hit)
    {
        Vector3 pos = this.transform.position;
        float fullRadius = physicsSettings.maxGroundedRadius;
        float halfRadius = (physicsSettings.maxGroundedRadius / 2);

        return (Physics.Linecast(pos + physicsSettings.maxGroundedHeight, pos + physicsSettings.maxGroundedDistanceDown, out hit, layerMask)
             || Physics.Linecast(pos - transform.forward * halfRadius + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, out hit,layerMask)
             || Physics.Linecast(pos + transform.forward * halfRadius + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, out hit, layerMask)
             || Physics.Linecast(pos - transform.right * halfRadius + physicsSettings.maxGroundedHeight, pos - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, out hit,layerMask)
             || Physics.Linecast(pos + transform.right * halfRadius + physicsSettings.maxGroundedHeight, pos + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, out hit, layerMask)
             || Physics.Linecast(pos - transform.forward * halfRadius - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius / 2) - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, out hit,layerMask)
             || Physics.Linecast(pos + transform.forward * halfRadius + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius / 2) + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, out hit,layerMask)
             || Physics.Linecast(pos - transform.forward * halfRadius + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius / 2) + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, out hit,layerMask)
             || Physics.Linecast(pos + transform.forward * halfRadius - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius / 2) - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, out hit, layerMask)
             || Physics.Linecast(pos - transform.forward * fullRadius + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedDistanceDown, out hit,layerMask)
             || Physics.Linecast(pos + transform.forward * fullRadius + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedDistanceDown, out hit, layerMask)
             || Physics.Linecast(pos - transform.right * fullRadius + physicsSettings.maxGroundedHeight, pos - transform.right * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedDistanceDown, out hit,layerMask)
             || Physics.Linecast(pos + transform.right * fullRadius + physicsSettings.maxGroundedHeight, pos + transform.right * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedDistanceDown, out hit, layerMask)
             || Physics.Linecast(pos - transform.forward * (fullRadius * checkRaycastGroundedOffset) - transform.right * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) - transform.right * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) + physicsSettings.maxGroundedDistanceDown, out hit,layerMask)
             || Physics.Linecast(pos + transform.forward * (fullRadius * checkRaycastGroundedOffset) + transform.right * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) + transform.right * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) + physicsSettings.maxGroundedDistanceDown, out hit,layerMask)
             || Physics.Linecast(pos - transform.forward * (fullRadius * checkRaycastGroundedOffset) + transform.right * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) + transform.right * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) + physicsSettings.maxGroundedDistanceDown, out hit,layerMask)
             || Physics.Linecast(pos + transform.forward * (fullRadius * checkRaycastGroundedOffset) - transform.right * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) - transform.right * (physicsSettings.maxGroundedRadius * checkRaycastGroundedOffset) + physicsSettings.maxGroundedDistanceDown, out hit, layerMask));
    }

    bool IsHeadCollidingRaycast(LayerMask layerMask)
    {
        return Physics.OverlapSphereNonAlloc(this.transform.position + (Vector3.up * (_controller.height - _controller.radius + 0.01f)), 
                                             _controller.radius, new Collider[1], layerMask) > 0;
    }

    #region Jumping    

    void HandleJump()
    {
        // Reset jump input buffering
        if (Input.GetButtonDown("PS4_X")) this.JumpBufferElapsedFrames = 0;

        // Check if buffer is at max frames
        if (JumpBufferElapsedFrames < _jumpBufferMaxFrames)
        {
            // Increase buffer timeout
            this.JumpBufferElapsedFrames++;

            // Avoid jumpings on steep slopes
            // Maybe to handle in a different way
            if (this.stateController.onSteepSlope) return;

            if (this.stateController.isGrounded && !this.stateController.isJumping)
            {
                StartCoroutine(PerformJump());
            }
            else if(this.parkourController.isLedgeGrabbing && !this.stateController.isJumping)
            {
                StartCoroutine(PerformJump());
            }
        }
    }

    public float jumpTime;

    public IEnumerator PerformJump()
    {
        this.parkourController.isLedgeGrabbing = false;
        this.stateController.isJumping = true;
        yield return new WaitForSeconds(0.2f);
        this.stateController.isJumping = false;
    }

    #endregion


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        //RaycastHit hit1;
        //Vector3 offset = new Vector3(0, 5f, 0);
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(hitLedgeClimb.point, 0.2f);

        Vector3 pos = this.transform.position;

        ////Gizmos.DrawWireSphere(result, .1f);


        Debug.DrawLine(pos + physicsSettings.maxGroundedHeight, pos + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.forward * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.forward * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.forward * (physicsSettings.maxGroundedRadius / 2) - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius / 2) - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.forward * (physicsSettings.maxGroundedRadius / 2) + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius / 2) + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.forward * (physicsSettings.maxGroundedRadius / 2) + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius / 2) + transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.forward * (physicsSettings.maxGroundedRadius / 2) - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius / 2) - transform.right * (physicsSettings.maxGroundedRadius / 2) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.forward * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.forward * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.right * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedHeight, pos - transform.right * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.right * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedHeight, pos + transform.right * (physicsSettings.maxGroundedRadius) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.forward * (physicsSettings.maxGroundedRadius * 0.75f) - transform.right * (physicsSettings.maxGroundedRadius * 0.75f) + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius * 0.75f) - transform.right * (physicsSettings.maxGroundedRadius * 0.75f) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.forward * (physicsSettings.maxGroundedRadius * 0.75f) + transform.right * (physicsSettings.maxGroundedRadius * 0.75f) + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius * 0.75f) + transform.right * (physicsSettings.maxGroundedRadius * 0.75f) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos - transform.forward * (physicsSettings.maxGroundedRadius * 0.75f) + transform.right * (physicsSettings.maxGroundedRadius * 0.75f) + physicsSettings.maxGroundedHeight, pos - transform.forward * (physicsSettings.maxGroundedRadius * 0.75f) + transform.right * (physicsSettings.maxGroundedRadius * 0.75f) + physicsSettings.maxGroundedDistanceDown, Color.yellow);
        Debug.DrawLine(pos + transform.forward * (physicsSettings.maxGroundedRadius * 0.75f) - transform.right * (physicsSettings.maxGroundedRadius * 0.75f) + physicsSettings.maxGroundedHeight, pos + transform.forward * (physicsSettings.maxGroundedRadius * 0.75f) - transform.right * (physicsSettings.maxGroundedRadius * 0.75f) + physicsSettings.maxGroundedDistanceDown, Color.yellow);


        //if (Physics.SphereCast((transform.position + offset), 0.2f, -Vector3.up, out hit1, 10f))
        //{
        //    groundNormal = hit1.normal;
        //    Gizmos.color = Color.green;

        //    Gizmos.DrawRay(hit1.point, hit1.normal);

        //}

        //if (Physics.Raycast((transform.position + offset), -Vector3.up, out hit1, 10f))
        //{
        //    groundNormal = hit1.normal;
        //    Gizmos.color = Color.red;

        //    Gizmos.DrawRay(hit1.point, hit1.normal);

        //}


        ////if (LedgeClimbPosition != Vector3.zero)
        ////{
        ////    if (climbSnapController.canLedgeClimb)
        ////    {
        ////        Gizmos.color = Color.green;

        ////    }
        ////    else
        ////    {
        ////        Gizmos.color = Color.white;

        ////    }
        ////    Gizmos.DrawWireSphere(LedgeClimbPosition, 0.15f);
        ////    Gizmos.DrawRay(LedgeClimbPosition, ledgeclimbhitNormal);
        ////}

        //if (ClimbPosition != Vector3.zero)
        //{
        //    Gizmos.DrawWireSphere(ClimbPosition, 0.15f);
        //}

        //if (Physics.Raycast(climbSnapController.climbDetectionRays[4] - transform.forward, transform.forward, 2f, climbSnapController.climbableSurfaces))
        //{
        //    Gizmos.color = Color.green;
        //}
        //else
        //{
        //    Gizmos.color = Color.blue;
        //}

        //Gizmos.DrawRay(climbSnapController.climbDetectionRays[4] - transform.forward, transform.forward);

        ////if (climbSnapController.canLedgeClimb)
        ////{
        ////    Gizmos.color = Color.yellow;
        ////}
        ////else
        ////{
        ////    Gizmos.color = Color.blue;
        ////}

        //Gizmos.DrawRay(climbSnapController.climbDetectionRays[0] + (transform.up * 0.2f), transform.forward);

        //Gizmos.color = Color.blue;

        //RaycastHit hit;

        //if (Physics.Linecast(climbSnapController.climbDetectionRays[0] + (transform.forward * 0.2f) + (transform.up * 0.4f), climbSnapController.climbDetectionRays[0] + (transform.forward * 0.2f), out hit, climbSnapController.climbableSurfaces))
        //{

        //    UnityEditor.Handles.Label(climbSnapController.climbDetectionRays[0] + (transform.forward * 0.2f) + (transform.up * 0.5f), hit.distance.ToString());

        //    if (hit.distance < 5f && hit.distance < 4.5f)
        //    {
        //        Gizmos.color = Color.red;
        //    }
        //}
        //Gizmos.DrawLine(climbSnapController.climbDetectionRays[0] + (transform.forward * 0.2f) + (transform.up * 0.4f), climbSnapController.climbDetectionRays[0] + (transform.forward * 0.2f));

        ////DrawGizmosDetectionRays();

        //Gizmos.color = Color.cyan;
        //Gizmos.DrawRay(climbSnapController.climbDetectionRaysHits[1], climbSnapController.climbDetectionRaysHits[4] - climbSnapController.climbDetectionRaysHits[1]);
        //Gizmos.DrawRay(climbSnapController.climbDetectionRaysHits[1], this.transform.up);

        //Gizmos.color = Color.blue;

        //for (int i = 0; i < climbSnapController.climbDetectionRaysHits.Length - 1; i++)
        //{
        //    if (i > 0)
        //    {
        //        Gizmos.DrawLine(climbSnapController.climbDetectionRaysHits[i - 1], climbSnapController.climbDetectionRaysHits[i]);
        //    }
        //    Gizmos.DrawWireSphere(climbSnapController.climbDetectionRaysHits[i], 0.02f);
        //}
    }

    //private void DrawGizmosDetectionRays()
    //{
    //    foreach (Vector3 r in climbSnapController.climbDetectionRays)
    //    {
    //        if (Physics.Raycast(r, transform.forward, 0.35f, climbSnapController.climbableSurfaces))
    //        {
    //            Gizmos.color = Color.red;
    //            Gizmos.DrawRay(r, transform.forward);
    //        }
    //        else
    //        {
    //            Gizmos.color = Color.blue;
    //            Gizmos.DrawRay(r, transform.forward);
    //        }
    //    }

    //    foreach (Vector3 r in climbSnapController.ledgeDetectionRays)
    //    {
    //        RaycastHit hitInfo;

    //        if (Physics.Raycast(r, -transform.up, out hitInfo, 1f, climbSnapController.climbableSurfaces))
    //        {
    //            Gizmos.color = Color.red;
    //            Gizmos.DrawRay(r, -transform.up);
    //            UnityEditor.Handles.Label(r, hitInfo.distance.ToString());

    //        }
    //        else
    //        {
    //            Gizmos.color = Color.blue;
    //            Gizmos.DrawRay(r, -transform.up);
    //            UnityEditor.Handles.Label(climbSnapController.climbDetectionRays[0] + (transform.forward * 0.2f) + (transform.up * 0.5f), hitInfo.distance.ToString());
    //            UnityEditor.Handles.Label(r, hitInfo.distance.ToString());
    //        }
    //    }
    //}

#endif

    public override void Save(string path)
    {
        throw new System.NotImplementedException();
    }

    public override void Load()
    {
        throw new System.NotImplementedException();
    }
}
