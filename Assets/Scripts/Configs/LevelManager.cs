using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ModelManager modelManager;
    [SerializeField] private LevelConfig[] levelConfigs;
    private void Start()
    {
        StartLevel(0);
    }
    private void StartLevel(int levelNumber)
    {
        modelManager.CreateAndPlaceModel(levelConfigs[levelNumber].ModelLeftConfig, levelConfigs[levelNumber].ModelRightConfig);
    }
}
