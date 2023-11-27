using UnityEngine;

public class ModelManager : MonoBehaviour
{
    [SerializeField] private ModelFactory modelFactory;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int intersectionDistance;
    [SerializeField] private ModelConfig modelLeftConfig;
    [SerializeField] private ModelConfig modelRightConfig;
    private Model modelLeft;
    private Model modelRight;
    private Model activeModel;
    private Model notActiveModel;
    private float screenHalfWidth = 0.5f;
    private void Start()
    {
        CreateAndPlaceModel();
    }
    private void CreateAndPlaceModel()
    {
        modelLeft = modelFactory.CreateModel(modelLeftConfig, parentTransform, ModelSides.Left, intersectionDistance);
        modelRight = modelFactory.CreateModel(modelRightConfig, parentTransform, ModelSides.Right, intersectionDistance);
    }
    public void Rotate(Vector3 startPosition, Vector3 offset, float duration)
    {
        if (startPosition.x < screenHalfWidth)
        {
            modelLeft.IsActiveModel = true;
            modelRight.IsActiveModel = false;
            activeModel = modelLeft;
            notActiveModel = modelRight;
            if (offset.y < 0)
            {
                activeModel.RotateLeft(duration);
            }
            else if (offset.y > 0)
            {
                activeModel.RotateRight(duration);
            }
        }
        else
        {
            modelLeft.IsActiveModel = false;
            modelRight.IsActiveModel = true;
            activeModel = modelRight;
            notActiveModel = modelLeft;
            if (offset.y < 0)
            {
                activeModel.RotateRight(duration);
            }
            else if (offset.y > 0)
            {
                activeModel.RotateLeft(duration);
            }
        }

        notActiveModel.GetTopIntersectionDiamond().DiamondConfig = activeModel.GetTopIntersectionDiamond().DiamondConfig;
        notActiveModel.GetBottomIntersectionDiamond().DiamondConfig = activeModel.GetBottomIntersectionDiamond().DiamondConfig;
    }
}
