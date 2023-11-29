using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 1)]
[Serializable]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private LevelTask[] levelTasks;
    [SerializeField] private ModelConfig modelLeftConfig;
    [SerializeField] private ModelConfig modelRightConfig;
    public LevelTask[] LevelTasks
    {
        get => levelTasks;
        set => levelTasks = value;
    }
    public ModelConfig ModelLeftConfig
    {
        get => modelLeftConfig;
        set => modelLeftConfig = value;
    }
    public ModelConfig ModelRightConfig
    {
        get => modelRightConfig;
        set => modelRightConfig = value;
    }
}
