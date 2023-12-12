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
    private enum SwipeState
    {
        NotSwiping,
        Swiping
    }
    private SwipeState swipeState = SwipeState.NotSwiping;
    public void CreateAndPlaceModel(ModelConfig modelLeftConfig, ModelConfig modelRightConfig)
    {
        modelLeft = modelFactory.CreateModel(modelLeftConfig, parentTransform, ModelSides.Left, intersectionDistance);
        modelRight = modelFactory.CreateModel(modelRightConfig, parentTransform, ModelSides.Right, intersectionDistance);
        SetActiveModel(modelLeft);
        UpdateIntersectionModelObjects();
    }
    public void Rotate(Vector3 startPosition, Vector3 offset, float duration)
    {
        SetActiveModel((startPosition.x < screenHalfWidth) ? modelLeft : modelRight);

        RotateActiveModel(offset.y, duration);

        UpdateIntersectionModelObjects();
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
        if (swipeState == SwipeState.Swiping)
        {
            ModelRotated?.Invoke();
        }
    }
    private void UpdateIntersectionModelObjects()
    {
        notActiveModel.GetTopIntersectionModelObject().ModelObjectConfig = activeModel.GetTopIntersectionModelObject().ModelObjectConfig;
        notActiveModel.GetBottomIntersectionModelObject().ModelObjectConfig = activeModel.GetBottomIntersectionModelObject().ModelObjectConfig;
    }
    public ModelObjectType[] GetActiveModelIntersectionTypes()
    {
        return new ModelObjectType[]
        {
            activeModel.GetTopIntersectionModelObject().ModelObjectConfig.type,
            activeModel.GetBottomIntersectionModelObject().ModelObjectConfig.type
        };
    }
    public void UpdateSwipeState(bool isSwiping)
    {
        swipeState = isSwiping ? SwipeState.Swiping : SwipeState.NotSwiping;
    }
    public void ClearAllModels()
    {
        activeModel.ModelObjects.Clear();
        notActiveModel.ModelObjects.Clear();

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Destroy(parentTransform.GetChild(i).gameObject);
        }
    }
}
