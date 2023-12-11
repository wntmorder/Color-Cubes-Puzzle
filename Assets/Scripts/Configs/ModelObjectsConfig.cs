using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelObjectsConfig", menuName = "Configs/ModelObjectsConfig", order = 1)]
public class ModelObjectsConfig : ScriptableObject
{
    [SerializeField] private ModelObjectConfig[] configs;
    private Dictionary<ModelObjectType, ModelObjectConfig> configsByType;
    private void OnValidate()
    {
        configsByType = new();
        foreach (ModelObjectConfig config in configs)
        {
            configsByType[config.type] = config;
        }
    }
    public ModelObjectConfig GetModelObjectConfig(ModelObjectType type)
    {
        return configsByType.ContainsKey(type) ? configsByType[type] : configsByType[ModelObjectType.Yellow];
    }
}
