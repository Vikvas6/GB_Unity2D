using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBullet
{
    private float _radius = 0.3f;
    private Vector3 _velocity;

    private float _groundLevel = 0;
    private float _g = -10;

    private CharacterView _view;

    public PhysicalBullet(CharacterView view)
    {
        _view = view;
        SetVisible(false);
    }

    public void Throw(Vector3 position, Vector3 force)
    {
        SetVisible(false);
        _view.Transform.position = position;
        AddForce(force);
        SetVisible(true);
    }

    private void AddForce(Vector3 force)
    {
        _view.Rigidbody2D.velocity = Vector2.zero;
        _view.Rigidbody2D.angularVelocity = 0;
        _view.Rigidbody2D.AddForce(force, ForceMode2D.Impulse);
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
