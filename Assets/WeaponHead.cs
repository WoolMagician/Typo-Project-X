using UnityEngine;
using UnityEngine.Events;

public class WeaponHead : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _rotate = false;

    [SerializeField] private SpriteRenderer _renderer;

    public UnityAction<Collider> OnTriggerEnterCallback;
    public UnityAction<Collider> OnTriggerExitCallback;
    public UnityAction<Collision> OnCollisionEnterCallback;
    public UnityAction<Collision> OnCollisionExitCallback;

    public Rigidbody Rigidbody { get { return _rb; } }

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(_rotate)
        {
            //Basically only used to simulate boomerang rotation,
            //  can keep fixed value of 750 for the whole game as it WORKS
            this.transform.Rotate(this.transform.forward, 750f * Time.deltaTime);
        }
    }

    public void SetSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    public void Rotate(bool flag)
    {
        _rotate = flag;
    }

    private void OnTriggerEnter(Collider other)
    {
        this.OnTriggerEnterCallback(other);
    }

    private void OnTriggerExit(Collider other)
    {
        this.OnTriggerExitCallback(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.OnCollisionEnterCallback(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        this.OnCollisionExitCallback(collision);
    }
}