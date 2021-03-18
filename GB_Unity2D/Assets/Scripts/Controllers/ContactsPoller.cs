using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactsPoller
{
    private const float _collisionThresh = 0.5f;

    private ContactPoint2D[] _contacts = new ContactPoint2D[10];
    private int _contactsCount;
    private readonly Collider2D _collider2D;
    
    public bool IsGrounded { get; private set; }
    public bool HasLeftContacts { get; private set; }
    public bool HasRightContacts { get; private set; }

    public Vector3 GroundVelocty { get; private set; }

    public ContactsPoller(Collider2D collider2D)
    {
        _collider2D = collider2D;
    }
    
    public void UpdateTick()
    {
        IsGrounded = false;
        HasLeftContacts = false;
        HasRightContacts = false;
        _contactsCount = _collider2D.GetContacts(_contacts);
        for (int i = 0; i < _contactsCount; i++)
        {
            var normal = _contacts[i].normal;
            var rigidBody = _contacts[i].rigidbody;

            if (normal.y > _collisionThresh)
            {
                IsGrounded = true;
                GroundVelocty = rigidBody == null ? Vector2.zero : rigidBody.velocity;
            }
            if (normal.x > _collisionThresh && rigidBody == null)
            {
                HasLeftContacts = true;
            }

            if (normal.x < -_collisionThresh && rigidBody == null)
            {
                HasRightContacts = true;
            }
        }
    }
}
