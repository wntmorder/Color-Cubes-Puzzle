using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ModelManager modelManager;
    [SerializeField] private ModelConfig modelLeftConfig;
    [SerializeField] private ModelConfig modelRightConfig;
    [SerializeField] private LevelConfig levelConfig;   //Arrray of LevelConfigs
    private void Start()
    {
        modelManager.CreateAndPlaceModel(modelLeftConfig, modelRightConfig);
    }
}
