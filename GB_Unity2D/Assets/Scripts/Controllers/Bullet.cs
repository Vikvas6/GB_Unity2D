using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    private float _radius = 0.3f;
    private Vector3 _velocity;

    private float _groundLevel = 0;
    private float _g = -10;

    private CharacterView _view;

    public Bullet(CharacterView view)
    {
        _view = view;
        SetVisible(false);
    }

    public void UpdateTick()
    {
        if (IsGrounded())
        {
            _velocity.y = -_velocity.y;
            SetVelocity(_velocity);
            var position = _view.Transform.position;
            position.Set(position.x, _groundLevel+_radius, position.z);
        }
        else
        {
            SetVelocity(_velocity + Vector3.up * _g * Time.deltaTime);
            _view.Transform.position += _velocity * Time.deltaTime;
        }
    }

    public void Throw(Vector3 position, Vector3 velocity)
    {
        _view.Transform.position = position;
        SetVelocity(velocity);
        SetVisible(true);
    }

    private void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
        var angle = Vector3.Angle(Vector3.left, _velocity);
        var axis = Vector3.Cross(Vector3.left, _velocity);
        _view.Transform.rotation = Quaternion.AngleAxis(angle, axis);

    }

    private bool IsGrounded()
    {
        return _view.Transform.position.y <= _groundLevel + _radius + float.Epsilon && _velocity.y <= 0;
    }

    private void SetVisible(bool visible)
    {
        if (_view.Trail)
        {
            _view.Trail.enabled = visible;
        }
        if (_view.Trail)
        {
            _view.Trail.Clear();
        }

        _view.SpriteRenderer.enabled = visible;
    }

}
