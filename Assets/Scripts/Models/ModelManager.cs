using UnityEngine;
public class ModelManager : MonoBehaviour
{
    [SerializeField] private ModelFactory modelFactory;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int numberOfObjects = 16;
    [SerializeField] private int intersectionDistance;
    private Model modelLeft;
    private Model modelRight;
    private Model activeModel;
    private float screenHalfWidth = 0.5f;
    private void Start()
    {
        CreateAndPlaceModel();
    }
    private void CreateAndPlaceModel()
    {
        modelLeft = modelFactory.CreateModel(numberOfObjects, parentTransform, ModelSides.Left, intersectionDistance);
        modelRight = modelFactory.CreateModel(numberOfObjects, parentTransform, ModelSides.Right, intersectionDistance);
    }
    public void Rotate(Vector3 startPosition, Vector3 offset, float duration)
    {
        if (startPosition.x < screenHalfWidth)
        {
            activeModel = modelLeft;
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
            activeModel = modelRight;
            if (offset.y < 0)
            {
                activeModel.RotateRight(duration);
            }
            else if (offset.y > 0)
            {
                activeModel.RotateLeft(duration);
            }
        }
    }
    public void CheckIntersection()
    {
       
    }
}
