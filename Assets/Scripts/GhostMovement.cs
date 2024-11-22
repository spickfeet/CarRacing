using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _pointRange; // Расстояние достаточное для перехода к следующей точке.

    private List<Vector3> _points;
    private List<Quaternion> _rotations;
    private int _currentPoint;

    private Rigidbody _rb;

    private void Awake()
    {
        _currentPoint = 0;
        _rb = GetComponent<Rigidbody>();
    }

    public void Inject(List<Vector3> points, List<Quaternion> rotations)
    {
        _points = points;
        _rotations = rotations;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        UpdateCurrentPoint();
        Rotate();
        float step = Time.deltaTime * _speed;
        _rb.MovePosition(Vector3.MoveTowards(transform.position, _points[_currentPoint], step));
    }
        
    private void Rotate()
    {
        Vector3 relativePos = _points[_currentPoint] - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, _rotations[_currentPoint], Time.deltaTime * _rotationSpeed);
    }
    public void UpdateCurrentPoint()
    {
        var heading = _points[_currentPoint] - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        if (heading.sqrMagnitude < _pointRange * _pointRange)
        {
            if (_currentPoint + 1 < _points.Count)
            {
                _currentPoint++;
            }
        }
    }
}
