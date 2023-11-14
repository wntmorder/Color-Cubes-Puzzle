using UnityEngine;

[CreateAssetMenu(fileName = "ModelFactory", menuName = "Factories/ModelFactory", order = 1)]
public class ModelFactory:ScriptableObject
{
    [SerializeField] private Model modelPrefab;
    public Model CreateModel(int numberOfObjects, Transform parentTransform, ModelSides modelSide, int intersectionDistance)
    {
        Model model = Instantiate(modelPrefab, parentTransform);
        model.Initialize(modelSide, numberOfObjects, intersectionDistance);
        return model;
    }
}
