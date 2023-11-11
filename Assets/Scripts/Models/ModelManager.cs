using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    [SerializeField] private ModelFactory modelFactory;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int numberOfObjects = 16;

    private Model modelLeft;
    private Model modelRight;

    private void Start()
    {
        CreateAndPlaceModel();
        CheckIntersection();
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
    private void CheckIntersection()
    {
        List<Vector3> leftModelPositions = modelLeft.GetDiamondPositions();
        List<Vector3> rightModelPositions = modelRight.GetDiamondPositions();
        foreach (Vector3 leftPosition in leftModelPositions)
        {
            foreach (Vector3 rightPosition in rightModelPositions)
            {
                if (Vector3.Distance(leftPosition, rightPosition) < 0.1f)
                {
                    Debug.Log("Intersection: " + leftPosition);
                }
            }
        }
    }
}
