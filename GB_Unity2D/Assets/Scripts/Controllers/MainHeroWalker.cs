using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroWalker
{
    private CharacterView _view;
    private SpriteAnimatorController _spriteAnimator;

    private float _xAxisInput = 0;
    private bool _doJump = false;

    private Vector3 _leftScale = new Vector3(-1, 1, 1);
    private Vector3 _rightScale = new Vector3(1, 1, 1);
    
    private float _groundLevel = 0.5f;
    private float _yVelocity = 0f;
    
    private const float _jumpStartSpeed = 8f;
    private const float _g = -9.8f;
    private const float _walkSpeed = 3f;
    private const float _animationsSpeed = 10f;
    private const float _movingThresh = 0.1f;
    private const float _flyThresh = 1f;

    public MainHeroWalker(CharacterView view, SpriteAnimatorController spriteAnimator)
    {
        _view = view;
        _spriteAnimator = spriteAnimator;
    }

    public void UpdateTick()
    {
        _doJump = Input.GetAxis("Vertical") > 0;
        _xAxisInput = Input.GetAxis("Horizontal");
        var goSideWay = Mathf.Abs(_xAxisInput) > _movingThresh;
        
        if (IsGrounded())
        {
            if (goSideWay)
            {
                GoSideWay();
            }
            _spriteAnimator.StartAnimation(_view.SpriteRenderer, goSideWay ? Track.Run : Track.Idle, true, _animationsSpeed);

            if (_doJump && _yVelocity == 0)
            {
                _yVelocity = _jumpStartSpeed;
            }
            else if (_yVelocity < 0)
            {
                _yVelocity = 0;
                var position = _view.Transform.position;
                position.Set(position.x, _groundLevel, position.z);
            }
        }
        else
        {
            if (goSideWay)
            {
                GoSideWay();
            }

            if (Mathf.Abs(_yVelocity) > _flyThresh)
            {
                _spriteAnimator.StartAnimation(_view.SpriteRenderer, Track.Jump, true, _animationsSpeed);
            }
            _yVelocity += _g * Time.deltaTime;
            _view.Transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
        }
    }
    
    public bool IsGrounded()
    {
        return _view.Transform.position.y <= _groundLevel + float.Epsilon && _yVelocity <= 0;
    }

    private void GoSideWay()
    {
        _view.Transform.position += Vector3.right * (Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1));
        _view.Transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
    }
}
