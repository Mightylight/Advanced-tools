using UnityEngine;

public class MovingPan : MonoBehaviour
{
    public float _speed = 1f;
    public float _radius = 4f;
    Vector3 _startPoint;
    Vector3 _toPoint;
    
    void Start()
    {
        _startPoint = transform.position;
        _toPoint = _startPoint;
    }

    
    void Update()
    {
        if ((transform.position - _toPoint).sqrMagnitude < .02f)
        {
            Vector2 newPoint = Random.insideUnitCircle * _radius;
            _toPoint = _startPoint + new Vector3(newPoint.x, newPoint.y, 0);
        }
        transform.Translate((_toPoint - transform.position).normalized * Time.deltaTime*_speed);
    }
}
