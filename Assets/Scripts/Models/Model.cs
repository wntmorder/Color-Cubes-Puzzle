using UnityEngine;
using System.Collections.Generic;

public class Model : MonoBehaviour
{
    [SerializeField] private ModelObjectsConfig ModelObjectsConfig;
    [SerializeField] private ModelObject objectPrefab;
    [SerializeField] private float objectSpacing = 0.75f;
    private int numberOfObjects;
    private readonly List<ModelObject> modelObjects = new();

    private int sideLength;
    private int offset = 0;
    private float radius;
    private SideData[] sides;

    private int topIntersectionIndex;
    private int bottomIntersectionIndex;
    private bool isActiveModel = true;
    public bool IsActiveModel
    {
        get => isActiveModel;
        set
        {
            isActiveModel = value;
            GetTopIntersectionModelObject().gameObject.SetActive(value);
            GetBottomIntersectionModelObject().gameObject.SetActive(value);
        }
    }
    public void Initialize(ModelConfig modelConfig, ModelSides modelSide, int intersectionDistance)
    {
        numberOfObjects = modelConfig.Configs.Length;
        transform.Rotate(transform.up * 45f);
        sideLength = Mathf.FloorToInt(numberOfObjects * 0.25f);
        radius = (sideLength * 0.5f) * objectSpacing;
        float modelOffsetValue = 0f;
        switch (modelSide)
        {
            case ModelSides.Left:
                {
                    topIntersectionIndex = (numberOfObjects - sideLength) + intersectionDistance;
                    bottomIntersectionIndex = sideLength - intersectionDistance;
                    modelOffsetValue = (-intersectionDistance * objectSpacing) * 0.5f;
                    break;
                }
            case ModelSides.Right:
                {
                    topIntersectionIndex = numberOfObjects - sideLength - intersectionDistance;
                    bottomIntersectionIndex = sideLength + intersectionDistance;
                    modelOffsetValue = (intersectionDistance * objectSpacing) * 0.5f;
                    break;
                }
        }
        Vector3 modelOffset = new(modelOffsetValue, 0f, modelOffsetValue);

        sides = new SideData[] {
            new SideData {
                StartPosition = new Vector3(radius, 0f, radius) + modelOffset,
                Direction = new Vector3(0f, 0f, -objectSpacing),
            },
            new SideData {
                StartPosition = new Vector3(radius, 0f, -radius) + modelOffset,
                Direction = new Vector3(-objectSpacing, 0f, 0f),
            },
            new SideData {
                StartPosition = new Vector3(-radius, 0f, -radius) + modelOffset,
                Direction = new Vector3(0f, 0f, objectSpacing),
            },
            new SideData {
                StartPosition = new Vector3(-radius, 0f, radius) + modelOffset,
                Direction = new Vector3(objectSpacing, 0f, 0f),
            },
        };

        InstantiateModelObjects(modelConfig);
    }
    private void InstantiateModelObjects(ModelConfig modelConfig)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            ModelObject ModelObject = Instantiate(objectPrefab, transform);
            ModelObject.ModelObjectConfig = ModelObjectsConfig.GetModelObjectConfig(modelConfig.Configs[i]);
            ModelObject.MoveIn(GetPositionByIndex(i), Time.time);
            modelObjects.Add(ModelObject);
        }
    }
    public void RotateLeft(float duration)
    {
        SetOffset(offset - 1);
        UpdateState(duration);
    }
    public void RotateRight(float duration)
    {
        SetOffset(offset + 1);
        UpdateState(duration);
    }
    private void SetOffset(int value)
    {
        offset = (value % numberOfObjects + numberOfObjects) % numberOfObjects;
        while (offset < 0)
        {
            offset += numberOfObjects;
        }
    }
    private void UpdateState(float duration)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            int index = CalculateIndexWithOffset(i);
            modelObjects[i].MoveIn(GetPositionByIndex(index), duration);
        }
    }
    private int CalculateIndexWithOffset(int index)
    {
        return (index + offset) % numberOfObjects;
    }
    private int CalculateIntersectIndexWithOffset(int index)
    {
        int indexWO = (index - offset);
        while (indexWO < 0)
        {
            indexWO += numberOfObjects;
        }
        return indexWO % numberOfObjects;
    }
    private Vector3 GetPositionByIndex(int index)
    {
        int side = Mathf.FloorToInt(index / sideLength);
        SideData sideData = sides[side];
        Vector3 position = sideData.StartPosition + sideData.Direction * (CalculateIndexInSide(index, side));
        return position;
    }
    private int CalculateIndexInSide(int index, int side)
    {
        return index - (side * sideLength);
    }
    public ModelObject GetTopIntersectionModelObject()
    {
        return modelObjects[CalculateIntersectIndexWithOffset(topIntersectionIndex)];
    }
    public ModelObject GetBottomIntersectionModelObject()
    {
        return modelObjects[CalculateIntersectIndexWithOffset(bottomIntersectionIndex)];
    }
    public ModelObject[] GetIntersectionModelObjects()
    {
        return new ModelObject[2] { GetTopIntersectionModelObject(), GetBottomIntersectionModelObject() };
    }
}
