using Diamonds;
using UnityEngine;
using System.Collections.Generic;

public class ModelManager : MonoBehaviour
{
    [SerializeField] private ModelFactory modelFactory;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int numberOfObjects = 16;
    public Type type = Type.Rose;

    private Model modelLeft;
    private Model modelRight;

    private void Start()
    {
        CreateAndPlaceModel();
    }
    private void CreateAndPlaceModel()
    {
        modelLeft = modelFactory.CreateModel(numberOfObjects, parentTransform, ModelSides.Left);
        modelRight = modelFactory.CreateModel(numberOfObjects, parentTransform, ModelSides.Right);
    }
    public void Rotate(Vector3 startPosition, Vector3 offset)
    {
        float screenHalfWidth = Screen.width * 0.5f;
        if (startPosition.x < screenHalfWidth)
        {
            if (offset.y < 0)
            {
                modelLeft.RotateLeft();
            }
            else if (offset.y > 0)
            {
                modelLeft.RotateRight();
            }
        }
        else
        {
            if (offset.y < 0)
            {
                modelRight.RotateLeft();
            }
            else if (offset.y > 0)
            {
                modelRight.RotateRight();
            }
        }
    }
    public void CheckIntersection()
    {
        List<Vector3> leftModelPositions = modelLeft.GetDiamondPositions();
        List<Vector3> rightModelPositions = modelRight.GetDiamondPositions();
        for (int i = 0; i < leftModelPositions.Count; i++)
        {
            Vector3 leftPosition = leftModelPositions[i];
            for (int j = 0; j < rightModelPositions.Count; j++)
            {
                Vector3 rightPosition = rightModelPositions[j];
                if (Vector3.Distance(leftPosition, rightPosition) < 0.1f)
                {
                    //Debug.Log("Color at the intersection point: " + modelLeft.GetDiamondColor(modelLeft.GetIndexByPosition(leftPosition)));
                    Type modelLeftIntersectionType = modelLeft.GetDiamondColor(modelLeft.GetIndexByPosition(leftPosition));
                    Type modelRightIntersectionType = modelRight.GetDiamondColor(modelRight.GetIndexByPosition(leftPosition));
                    if(modelLeftIntersectionType == type && modelRightIntersectionType == type)
                    {
                        Debug.Log("Task completed");
                    }
                }
            }
        }
    }
}
