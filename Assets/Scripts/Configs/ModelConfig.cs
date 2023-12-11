using UnityEngine;

[CreateAssetMenu(fileName = "ModelConfig", menuName = "Configs/ModelConfig", order = 1)]
public class ModelConfig : ScriptableObject
{
    [SerializeField] private ModelObjectType[] configs;
    public ModelObjectType[] Configs
    {
        get => configs;
        private set => configs = value;
    }
}
