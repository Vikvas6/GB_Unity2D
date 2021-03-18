using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsEmitter
{    
    private const float _delay = 3;
    private const float _startSpeed = 1;

    private List<PhysicalBullet> _bullets = new List<PhysicalBullet>();
    private Transform _transform;

    private int _currentIndex;
    private float _timeTillNextBullet;

    public BulletsEmitter(List<CharacterView> bulletViews, Transform transform)
    {
        _transform = transform;
        foreach (var bulletView in bulletViews)
        {
            _bullets.Add(new PhysicalBullet(bulletView));
        }
    }

    public void UpdateTick()
    {
        if(_timeTillNextBullet > 0)
        {
            _timeTillNextBullet -= Time.deltaTime;
        }
        else
        {
            _timeTillNextBullet = _delay;
            _bullets[_currentIndex].Throw(_transform.position, -_transform.right * _startSpeed);
            _currentIndex++;
            if (_currentIndex >= _bullets.Count) _currentIndex = 0;
        }
    }
}
