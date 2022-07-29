using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class LineRendererTransformHelper : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] private Transform[] _transforms;

    // Start is called before the first frame update
    void Awake()
    {
        this._lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this._transforms != null && this._transforms.Length >= 2)
        {
            Vector3[] _points = this._transforms.Select(x => x.position).ToArray();
            this._lineRenderer.SetPositions(_points);
        }
    }
}