using UnityEngine;
using Random = UnityEngine.Random;

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

    private Vector2 GetRandomInsideUnitCircle()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float radius = Random.Range(0f, 1f);
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);
        return new Vector2(x, y);
    }


    private void Update()
    {
        if ((_toPoint - transform.position).magnitude < .1f)
        {
            Vector2 newPoint = GetRandomInsideUnitCircle();
            _toPoint = _startPoint + new Vector3(newPoint.x, newPoint.y, 0)  * _radius;
        }
        Vector3 direction = Vector3.Normalize(_toPoint - transform.position);
        
        transform.position += direction * Time.deltaTime * _speed;
    }
}
