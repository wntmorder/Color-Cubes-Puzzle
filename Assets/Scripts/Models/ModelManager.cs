using System;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    [SerializeField] private ModelFactory modelFactory;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int intersectionDistance;
    private Model modelLeft;
    private Model modelRight;
    private Model activeModel;
    private Model notActiveModel;
    public event Action ModelRotated;
    private readonly float screenHalfWidth = 0.5f;
    public void CreateAndPlaceModel(ModelConfig modelLeftConfig, ModelConfig modelRightConfig)
    {
        modelLeft = modelFactory.CreateModel(modelLeftConfig, parentTransform, ModelSides.Left, intersectionDistance);
        modelRight = modelFactory.CreateModel(modelRightConfig, parentTransform, ModelSides.Right, intersectionDistance);
        SetActiveModel(modelLeft);
        UpdateIntersectionDiamonds();
    }
    public void Rotate(Vector3 startPosition, Vector3 offset, float duration)
    {
        SetActiveModel((startPosition.x < screenHalfWidth) ? modelLeft : modelRight);

        RotateActiveModel(offset.y, duration);

        UpdateIntersectionDiamonds();
    }
    private void SetActiveModel(Model model)
    {
        activeModel = model;
        notActiveModel = (model == modelLeft) ? modelRight : modelLeft;
        activeModel.IsActiveModel = true;
        notActiveModel.IsActiveModel = false;
    }
    private void RotateActiveModel(float offsetY, float duration)
    {
        if (offsetY < 0)
        {
            activeModel.RotateLeft(duration);
        }
        else if (offsetY > 0)
        {
            activeModel.RotateRight(duration);
        }

        ModelRotated?.Invoke();
    }
    private void UpdateIntersectionDiamonds()
    {
        notActiveModel.GetTopIntersectionDiamond().DiamondConfig = activeModel.GetTopIntersectionDiamond().DiamondConfig;
        notActiveModel.GetBottomIntersectionDiamond().DiamondConfig = activeModel.GetBottomIntersectionDiamond().DiamondConfig;
    }
    public Diamonds.Type[] GetActiveModelIntersectionTypes()
    {
        return new Diamonds.Type[]
        {
            activeModel.GetTopIntersectionDiamond().DiamondConfig.type,
            activeModel.GetBottomIntersectionDiamond().DiamondConfig.type
        };
    }
}
