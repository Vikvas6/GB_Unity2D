using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroPhysicsWalker
{
    private CharacterView _view;
    private SpriteAnimatorController _spriteAnimator;
    private ContactsPoller _contactsPoller;

    private float _xAxisInput = 0;
    private bool _doJump = false;

    private Vector3 _leftScale = new Vector3(-1, 1, 1);
    private Vector3 _rightScale = new Vector3(1, 1, 1);
    
    private float _groundLevel = 0.5f;
    
    private const float _jumpForce = 350f;
    private const float _jumpThresh = 0.1f;
    private const float _walkSpeed = 150f;
    private const float _animationsSpeed = 10f;
    private const float _movingThresh = 0.1f;
    private const float _flyThresh = 1f;

    public MainHeroPhysicsWalker(CharacterView view, SpriteAnimatorController spriteAnimator)
    {
        _view = view;
        _spriteAnimator = spriteAnimator;
        _contactsPoller = new ContactsPoller(_view.Collider2D);
    }

    public void UpdateTick()
    {
        _contactsPoller.UpdateTick();
        
        _doJump = Input.GetAxis("Vertical") > 0;
        _xAxisInput = Input.GetAxis("Horizontal");
        var goSideWay = Mathf.Abs(_xAxisInput) > _movingThresh;

        if (goSideWay)
        {
            _view.SpriteRenderer.flipX = _xAxisInput < 0;
        }

        var newVelocity = 0f;
        if (goSideWay &&
            (_xAxisInput > 0 || !_contactsPoller.HasLeftContacts) &&
            (_xAxisInput < 0 || !_contactsPoller.HasRightContacts))
        {
            newVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
        }
        //_view.Rigidbody2D.velocity.Set(newVelocity, _view.Rigidbody2D.velocity.y); //not working
        var currentVelocity = _view.Rigidbody2D.velocity;
        currentVelocity.x = newVelocity;
        _view.Rigidbody2D.velocity = currentVelocity;
        
        if (_contactsPoller.IsGrounded)
        {
            if (_doJump && Mathf.Abs(_view.Rigidbody2D.velocity.y) <= _jumpThresh)
            {
                _view.Rigidbody2D.AddForce(Vector2.up * _jumpForce);
            }
            _spriteAnimator.StartAnimation(_view.SpriteRenderer, goSideWay ? Track.Run : Track.Idle, true, _animationsSpeed);
        }
        else if (Mathf.Abs(_view.Rigidbody2D.velocity.y) > _flyThresh)
        {
            _spriteAnimator.StartAnimation(_view.SpriteRenderer, Track.Jump, true, _animationsSpeed);
        }
    }
}
