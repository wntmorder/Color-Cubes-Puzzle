using UnityEngine;
public class ModelManager : MonoBehaviour
{
    [SerializeField] private ModelFactory modelFactory;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int numberOfObjects = 16;
    [SerializeField] private int intersectionDistance = 2;
    [SerializeField] private float rotateDuration;
    private Model modelLeft;
    private Model modelRight;
    private Model activeModel;
    private void Start()
    {
        CreateAndPlaceModel();
    }
    private void CreateAndPlaceModel()
    {
        modelLeft = modelFactory.CreateModel(numberOfObjects, parentTransform, ModelSides.Left, intersectionDistance, rotateDuration);
        modelRight = modelFactory.CreateModel(numberOfObjects, parentTransform, ModelSides.Right, intersectionDistance, rotateDuration);
    }
    public void Rotate(Vector3 startPosition, Vector3 offset)
    {
        float screenHalfWidth = Screen.width * 0.5f;
        activeModel = (startPosition.x < screenHalfWidth) ? modelLeft : modelRight;
        if (offset.y < 0)
        {
            activeModel.RotateLeft();
        }
        else if (offset.y > 0)
        {
            activeModel.RotateRight();
        }
    }
    public void CheckIntersection()
    {
        //Debug.Log($"Left: {modelLeft.GetIntersectionPointsByIndex(1)}\n\tRight: {modelRight.GetIntersectionPointsByIndex(1)}");
    }
}
