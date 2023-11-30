using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ModelManager modelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LevelConfig[] levelConfigs;
    private void Start()
    {
        StartLevel(0);
    }
    private void StartLevel(int levelNumber)
    {
        uiManager.DisplayTasks(levelConfigs[levelNumber].LevelTasks[0]);
        modelManager.CreateAndPlaceModel(levelConfigs[levelNumber].ModelLeftConfig, levelConfigs[levelNumber].ModelRightConfig);
    }
}
