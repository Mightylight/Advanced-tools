using UnityEngine;

public class MovingPan : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _radius = 4f;
    private Vector3 _startPoint;
    private Vector3 _toPoint;

    public void Start()
    {
        _startPoint = transform.position;
        _toPoint = _startPoint;
    }


    private void Update()
    {
        if ((transform.position - _toPoint).sqrMagnitude < .02f)
        {
            Vector2 newPoint = Random.insideUnitCircle * _radius;
            _toPoint = _startPoint + new Vector3(newPoint.x, newPoint.y, 0);
        }
        transform.Translate((_toPoint - transform.position).normalized * Time.deltaTime*_speed);
    }
}
