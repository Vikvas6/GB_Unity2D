using UnityEngine;


public class SwitchQuestModel : IQuestModel
{
    private const string TargetTag = "Player";
  
    #region Methods

    public bool TryComplete(GameObject activator)
    {
        return activator.CompareTag(TargetTag);
    }
  
    #endregion
}
