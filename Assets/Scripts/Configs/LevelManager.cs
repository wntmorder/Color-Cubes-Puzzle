using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ModelManager modelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LevelConfig[] levelConfigs;
    private int levelNumber = 0;
    private int currentTaskIndex = 0;
    private void Start()
    {
        modelManager.ModelRotated += OnModelRotated;
        StartLevel(levelNumber);
    }
    private void StartLevel(int levelNumber)
    {
        uiManager.DisplayTask(levelConfigs[levelNumber].LevelTasks[0]);
        modelManager.CreateAndPlaceModel(levelConfigs[levelNumber].ModelLeftConfig, levelConfigs[levelNumber].ModelRightConfig);
    }
    private void OnModelRotated()
    {
        Diamonds.Type[] activeModelIntersectionTypes = modelManager.GetActiveModelIntersectionTypes();
        LevelTask currentTask = levelConfigs[levelNumber].LevelTasks[currentTaskIndex];
        if (currentTask.topType == activeModelIntersectionTypes[0] && currentTask.bottomType == activeModelIntersectionTypes[1])
        {
            for (int i = 0; i < uiManager.ParentTransform.transform.childCount; i++)
            {
                Destroy(uiManager.ParentTransform.transform.GetChild(i).gameObject);
            }

            currentTaskIndex++;

            if(currentTaskIndex != levelConfigs[levelNumber].LevelTasks.Length)
            {
                uiManager.DisplayTask(levelConfigs[levelNumber].LevelTasks[currentTaskIndex]);
            }
            else
            {
                uiManager.LevelComplete();
            }
        }
    }
}
