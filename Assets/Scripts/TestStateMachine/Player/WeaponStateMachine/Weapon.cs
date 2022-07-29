using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public const string WEAPON_JOINT_GO_NAME = "WeaponJoint";
    public const string SPRITE_MATERIAL_TEXTURE_NAME = "_MainTex";

    private SpringJoint _weaponJoint = default;

    [SerializeField] private float _headbackCatchThreshold = 0.1f;
    [SerializeField] private Vector3 _headbackOffset = default;
    [SerializeField] private Player _player = default;
    [SerializeField] private WeaponSO _weaponSO = default;
    [SerializeField] private WeaponHead _weaponHead = default;
    [SerializeField] private LineRenderer _weaponRopeRenderer = default;
    [SerializeField] private Transform _spriteHandTransform = default;

    public Player Player { get { return _player; } }
    public WeaponSO WeaponSO { get { return _weaponSO; } }
    public WeaponHead WeaponHead { get { return _weaponHead; } }
    public SpringJoint WeaponJoint { get { return _weaponJoint; } }
    public LineRenderer WeaponRopeRenderer { get { return _weaponRopeRenderer; } }
    public Vector3 WeaponHeadbackOffsetFromJoint { get { return _headbackOffset; } }
    public Transform SpriteHandTransform { get { return _spriteHandTransform; } }
    public float HeadbackCatchThreshold { get { return _headbackCatchThreshold; } }

    public WeaponStateMachine StateMachine { get; private set; }
    public WeaponIdleState IdleState { get; private set; }
    public WeaponChargeState ChargeState { get; private set; }
    public WeaponThrowState ThrowState { get; private set; }
    public WeaponHeadbackState HeadbackState { get; private set; }

    public UnityAction OnThrowEnd;

    private void Start()
    {
        //Create weapon join on player
        this.CreateWeaponJoint();

        this.StateMachine = new WeaponStateMachine();
        this.IdleState = new WeaponIdleState(this);
        this.ChargeState = new WeaponChargeState(this);
        this.ThrowState = new WeaponThrowState(this);
        this.HeadbackState = new WeaponHeadbackState(this);

        //Initialize state machine
        this.StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        //If Scriptable object is not set, avoid weapon behaviour
        if (_weaponSO == null) return;

        //Update line renderer based on rope sprite presence
        if (_weaponSO.RopeSprite == null)
        {
            _weaponRopeRenderer.enabled = false;
        }
        else
        {
            _weaponRopeRenderer.sharedMaterial.SetTexture(SPRITE_MATERIAL_TEXTURE_NAME,
                                                          Texture2DFromSprite(_weaponSO.RopeSprite));
        }

        //Update state machine
        this.StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        this.StateMachine.CurrentState.PhysicsUpdate();
    }

    private void CreateWeaponJoint()
    {
        SpringJoint joint;
        Rigidbody rb;

        GameObject weaponJointObj = new GameObject(WEAPON_JOINT_GO_NAME);
        weaponJointObj.SetActive(false);
        weaponJointObj.transform.parent = _player.transform;
        weaponJointObj.transform.localPosition = Vector3.zero;
        weaponJointObj.transform.rotation = Quaternion.identity;

        rb = weaponJointObj.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        joint = weaponJointObj.AddComponent<SpringJoint>();
        joint.damper = 0;
        joint.spring = 50f;
        joint.minDistance = 0f;
        joint.maxDistance = 0f;
        joint.tolerance = 0f;
        joint.enablePreprocessing = false;
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector3.zero;
        joint.connectedBody = _weaponHead.Rigidbody;

        //Set instance joint
        _weaponJoint = joint;

        //Activate joint
        weaponJointObj.SetActive(true);
    }

    private void OnEnable()
    {
        _weaponHead.OnCollisionEnterCallback += HandleHeadCollisionEnter;
        _weaponHead.OnCollisionExitCallback += HandleHeadCollisionExit;
    }

    private void OnDisable()
    {
        _weaponHead.OnCollisionEnterCallback += HandleHeadCollisionEnter;
        _weaponHead.OnCollisionExitCallback += HandleHeadCollisionExit;
    }

    #region HEAD COLLISION

    private void HandleHeadCollisionEnter(Collision collision)
    {
        //Handle head collision
        //Play particles
    }

    private void HandleHeadCollisionExit(Collision collision)
    {
        //Handle head collision
    }

    #endregion

    private void OnDrawGizmos()
    {
        if(_weaponJoint != null)
        {
            Gizmos.DrawWireSphere(_weaponJoint.transform.position - _headbackOffset, 0.01f);
        }
    }

    public static Texture2D Texture2DFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
}