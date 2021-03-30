using UnityEngine;


[CreateAssetMenu(menuName = "Quest/Story", fileName = "QuestStoryConfig", order = 0)]
public class QuestStoryConfig : ScriptableObject
{
    public QuestConfig[] quests;
    public QuestStoryType questStoryType;
    public QuestObjectConfig[] objects;
}

public enum QuestStoryType
{
    Common,
    Resettable
}
