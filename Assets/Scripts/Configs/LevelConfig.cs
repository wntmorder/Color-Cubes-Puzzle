using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private LevelTask[] levelTasks;
}
