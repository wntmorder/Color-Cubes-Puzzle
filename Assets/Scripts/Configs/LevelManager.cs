using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ModelManager modelManager;
    [SerializeField] private LevelConfig[] levelConfigs;
    private int levelNumber = 0;
    public event Action<int> LevelChanged;
    public LevelConfig[] LevelConfigs => levelConfigs;
    public int CurrentTaskIndex { get; private set; } = 0;
    private void Start()
    {
        modelManager.ModelRotated += OnModelRotated;
        StartLevel(levelNumber);
    }
    private void StartLevel(int levelNumber)
    {
        LevelChanged?.Invoke(levelNumber);
        modelManager.CreateAndPlaceModel(levelConfigs[levelNumber].ModelLeftConfig, levelConfigs[levelNumber].ModelRightConfig);
    }
    private void OnModelRotated()
    {
        if (levelConfigs[levelNumber].LevelTasks[CurrentTaskIndex].topType == modelManager.GetActiveModelIntersectionTypes()[0]
        && levelConfigs[levelNumber].LevelTasks[CurrentTaskIndex].bottomType == modelManager.GetActiveModelIntersectionTypes()[1])
        {
            CurrentTaskIndex++;
            LevelChanged?.Invoke(levelNumber);

            if (CurrentTaskIndex == levelConfigs[levelNumber].LevelTasks.Length)
            {
                levelNumber++;
                StartLevel(levelNumber);
            }
        }
    }
}
