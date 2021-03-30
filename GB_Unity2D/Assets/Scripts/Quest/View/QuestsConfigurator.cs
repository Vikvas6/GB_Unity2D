using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestsConfigurator : MonoBehaviour
{
    #region Fields

    [SerializeField] private QuestObjectView _singleQuestView;
    [SerializeField] private QuestStoryConfig[] _questStoryConfigs;
    [SerializeField] private QuestObjectView[] _questObjectViews;
    
    private Quest _singleQuest;
    private List<IQuestStory> _questStories;

    private readonly Dictionary<QuestType, Func<IQuestModel>> _questFactories =
        new Dictionary<QuestType, Func<IQuestModel>>
        {
            {QuestType.Switch, () => new SwitchQuestModel()},
        };
    
    private readonly Dictionary<QuestStoryType, Func<List<IQuest>, List<IQuestObject>, IQuestStory>> _queryStoryFactories =
        new Dictionary<QuestStoryType, Func<List<IQuest>, List<IQuestObject>, IQuestStory>>
        {
            {QuestStoryType.Common, (questCollection, _) => new QuestStory(questCollection)},
            {QuestStoryType.Resettable, (questCollection, objectCollection) => new ResettableQuestStory(questCollection, objectCollection)}
        };

    private readonly Dictionary<QuestObjectType, Func<GameObject, IQuestObject>> _questObjectFactories =
        new Dictionary<QuestObjectType, Func<GameObject, IQuestObject>>
        {
            {QuestObjectType.Activator, questObject => new QuestObjectActivate(questObject)}
        };
    
    #endregion

    #region Unity Methods

    private void Start()
    {
        //_singleQuest = new Quest(_singleQuestView, new SwitchQuestModel());
        //_singleQuest.Reset();

        _questStories = new List<IQuestStory>();
        foreach (var questStoryConfig in _questStoryConfigs)
        {
            _questStories.Add(CreateQuestStory(questStoryConfig));
        }
    }

    private void OnDestroy()
    {
        //_singleQuest.Dispose();
        foreach (var questStory in _questStories)
        {
            questStory.Dispose();
        }
        _questStories.Clear();
    }

    private IQuestStory CreateQuestStory(QuestStoryConfig config)
    {
        var quests = new List<IQuest>();
        foreach (var questConfig in config.quests)
        {
            var quest = CreateQuest(questConfig);
            if (quest == null) continue;
            quests.Add(quest);
        }
        var objects = new List<IQuestObject>();
        foreach (var objectConfig in config.objects)
        {
            var questObject = CreateObject(objectConfig);
            if (questObject == null) continue;
            objects.Add(questObject);
        }

        return _queryStoryFactories[config.questStoryType].Invoke(quests, objects);
    }

    private IQuestObject CreateObject(QuestObjectConfig config)
    {
        var objectId = config.id;
        var questView = _questObjectViews.FirstOrDefault(value => value.Id == config.id);
        if (questView == null)
        {
            Debug.LogWarning($"QuestConfigurator :: Start : Can't find view of quest object {objectId.ToString()}");
            return null;
        }

        if (_questObjectFactories.TryGetValue(config.QuestObjectType, out var factory))
        {
            return factory.Invoke(questView.gameObject);
        }
        
        Debug.LogWarning($"QuestConfigurator :: Start : Can't create model for quest object {objectId.ToString()}");
        return null;
    }

    private IQuest CreateQuest(QuestConfig config)
    {
        var questId = config.id;
        var questView = _questObjectViews.FirstOrDefault(value => value.Id == config.id);
        if (questView == null)
        {
            Debug.LogWarning($"QuestConfigurator :: Start : Can't find view of quest {questId.ToString()}");
            return null;
        }

        if (_questFactories.TryGetValue(config.questType, out var factory))
        {
            var questModel = factory.Invoke();
            return new Quest(questView, questModel);
        }
        
        Debug.LogWarning($"QuestConfigurator :: Start : Can't create model for quest {questId.ToString()}");
        return null;
    }

    #endregion
    
}
