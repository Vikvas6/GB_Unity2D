using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemiesConfigurator : MonoBehaviour
{

    [Header("Stalker AI")]
    [SerializeField] private AIConfig _stalkerAIConfig;
    [SerializeField] private CharacterView _stalkerAIView;
    [SerializeField] private Seeker _stalkerAISeeker;
    [SerializeField] private Transform _stalkerAITarget;
    
    [Header("Protector AI")]
    [SerializeField] private CharacterView _protectorAIView;
    [SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
    [SerializeField] private AIPatrolPath _protectorAIPatrolPath;
    [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;
    [SerializeField] private Transform[] _protectorWaypoints;

    #region Fields

    private StalkerAI _stalkerAI;
    private ProtectorAI _protectorAI;
    private ProtectedZone _protectedZone;

    #endregion

  
    #region Unity methods

    private void Start()
    {
        if (_stalkerAIView != null)
        {
            _stalkerAI = new StalkerAI(_stalkerAIView, new StalkerAIModel(_stalkerAIConfig), _stalkerAISeeker,
                _stalkerAITarget);
            InvokeRepeating(nameof(RecalculateAIPath), 0.0f, 1.0f);
        }

        if (_protectorAIView != null)
        {
            _protectorAI = new ProtectorAI(_protectorAIView, new PatrolAIModel(_protectorWaypoints),
                _protectorAIDestinationSetter, _protectorAIPatrolPath);
            _protectorAI.Init();
            _protectedZone = new ProtectedZone(_protectedZoneTrigger, new List<IProtector> {_protectorAI});
            _protectedZone.Init();
        }
    }

    private void FixedUpdate()
    {
        if (_stalkerAI != null) _stalkerAI.FixedUpdate();
    }
    
    private void OnDestroy()
    {
        _protectorAI.Deinit();
        _protectedZone.Deinit();
    }

    #endregion

    #region Methods

    private void RecalculateAIPath()
    {
        _stalkerAI.RecalculatePath();
    }
      
    #endregion
}

