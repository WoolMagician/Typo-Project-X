using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UIEventTile : MonoBehaviour
{
    public TMP_FontAsset font = default;

    private float rotationSign = 1f;
    private float actualZRotation = 0f;

    [SerializeField] private Quaternion _originalRotation;
    [SerializeField] private Vector3 _originalPosition;
    [SerializeField] [ShowOnly] private bool isWiggling = false;

    [SerializeField] private TextMeshPro _textGUI = default;

    private Rigidbody rb;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this._originalRotation = this.transform.rotation;
        this._originalPosition = this.transform.position;
        this.SetWiggling(isWiggling);
    }

    void Update()
    {
        if (isWiggling)
        {
            actualZRotation = this.transform.eulerAngles.z;
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1, Random.Range(0.8f, 1f), 1), Time.deltaTime * 5f);

            if (rotationSign == 1f)
            {
                this.transform.Rotate(-this.transform.forward, Time.deltaTime * 50f * rotationSign);

                if (actualZRotation >= 10f && actualZRotation <= 40f)
                {
                    rotationSign = -1f;
                }
            }
            else if (rotationSign == -1f)
            {
                this.transform.Rotate(-this.transform.forward, Time.deltaTime * 50f * rotationSign);

                if (actualZRotation <= (355f) && actualZRotation > (310f))
                {
                    rotationSign = 1f;
                }
            }
        }
    }

    public void SetLetter(char letter)
    {
        _textGUI.text = letter.ToString();
    }

    public void SetWiggling(bool wiggle)
    {
        if ((wiggle && isWiggling) || (!wiggle && !isWiggling)) return;

        if(wiggle)
        {
            this.transform.Rotate(this.transform.forward, Random.Range(-15, 15));
            this.transform.position -= new Vector3(0f, Random.Range(-0.15f, 0.15f), 0f);
        }
        else
        {
            this.transform.rotation = _originalRotation;
            this.transform.position = _originalPosition;
        }
        isWiggling = wiggle;
    }

    public void CanFall(bool canFall)
    {        
        rb.isKinematic = !canFall;

        if (canFall)
        {
            this.SetWiggling(false);
            rb.AddTorque(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), ForceMode.Impulse);
        }
    }
}