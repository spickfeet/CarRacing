using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    private List<Vector3> _points;
    private List<Quaternion> _rotations;

    private Transform _player;

    [SerializeField] private float _timeNextSave; // Время через которое произойдет сохранение позиции
    private float _timerNextSave; // Таймер до следующего сохранения

    public List<Vector3> Points
    {
        get { return _points; }
    }

    public List<Quaternion> Rotations
    {
        get { return _rotations; }
    }

    public void Inject(Transform player)
    {
        _player = player;
    }

    private void Awake()
    {
        _points = new();
        _rotations = new();
        _timerNextSave = _timeNextSave;
    }


    private void Update()
    {
        _timerNextSave -= Time.deltaTime;
        if (_timerNextSave < 0)
        {
            _timerNextSave = _timeNextSave;
            _points.Add(_player.position);
            _rotations.Add(_player.rotation);
        }
    }
}
