using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ModelManager modelManager;
    [SerializeField] private LevelConfig[] levelConfigs;
    public event Action<int> LevelChanged;
    public LevelConfig[] LevelConfigs => levelConfigs;
    public int LevelNumber { get; private set; } = 0;
    public int CurrentTaskIndex { get; private set; } = 0;

    private void Start()
    {
        modelManager.ModelRotated += OnModelRotated;
        StartLevel(LevelNumber);
    }

    private void StartLevel(int levelNumber)
    {
        modelManager.CreateAndPlaceModel(levelConfigs[levelNumber].ModelLeftConfig, levelConfigs[levelNumber].ModelRightConfig);
    }

    private void OnModelRotated()
    {
        if (LevelNumber <= levelConfigs.Length
            && levelConfigs[LevelNumber].LevelTasks[CurrentTaskIndex].topType == modelManager.GetActiveModelIntersectionTypes()[0]
            && levelConfigs[LevelNumber].LevelTasks[CurrentTaskIndex].bottomType == modelManager.GetActiveModelIntersectionTypes()[1])
        {
            CurrentTaskIndex++;
            LevelChanged?.Invoke(LevelNumber);
        }
    }

    public void StartNewLevel()
    {
        if (CurrentTaskIndex >= levelConfigs[LevelNumber].LevelTasks.Length)
        {
            modelManager.ClearAllModels();
            LevelNumber++;
            StartLevel(LevelNumber);
            CurrentTaskIndex = 0;
        }
    }
}
