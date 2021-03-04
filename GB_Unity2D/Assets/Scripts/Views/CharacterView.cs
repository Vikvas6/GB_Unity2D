using System;
using UnityEngine;


public class CharacterView : MonoBehaviour
{
    #region Fields

    public Transform Transform;
    public SpriteRenderer SpriteRenderer;
    public Rigidbody2D Rigidbody2D;
    public Collider2D Collider2D;
    public TrailRenderer Trail;
    
    public Action<CharacterView> OnLevelObjectContact { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var levelObject = other.gameObject.GetComponent<CharacterView>();
        OnLevelObjectContact?.Invoke(levelObject);
    }

    #endregion
}
