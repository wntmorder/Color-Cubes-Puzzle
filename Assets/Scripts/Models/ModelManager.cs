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
        Debug.Log($"Left: {modelLeft.GetIntersectionPointsByIndex(1)}\n\tRight: {modelRight.GetIntersectionPointsByIndex(1)}");
        //Type modelLeftIntersectionType = modelLeft.GetDiamondColor();
        //Type modelRightIntersectionType = modelRight.GetDiamondColor();
        //if(modelLeftIntersectionType == type && modelRightIntersectionType == type)
        //{
        //    Debug.Log("Task completed");
        //}
    }
}
