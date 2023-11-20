using Diamonds;
using UnityEngine;
using System.Collections.Generic;
public class Model : MonoBehaviour
{
    [SerializeField] private DiamondsConfig diamondsConfig;
    [SerializeField] private Diamond objectPrefab;
    [SerializeField] private float objectSpacing = 0.75f;
    private int numberOfObjects;
    private List<Diamond> Diamonds = new();

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
        }
    }
    public void Initialize(ModelSides modelSide, int numberOfObjects, int intersectionDistance)
    {
        this.numberOfObjects = numberOfObjects;
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

        for (int i = 0; i < numberOfObjects; i++)
        {
            Diamond diamond = Instantiate(objectPrefab, transform);
            diamond.SetConfig(diamondsConfig.GetDiamondConfig(i % sideLength == 1 ? 1 : 0));
            diamond.MoveIn(GetPositionByIndex(i), Time.time);
            Diamonds.Add(diamond);
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
    }
    private void UpdateState(float duration)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            int index = CalculateIndexWithOffset(i);
            Diamonds[i].MoveIn(GetPositionByIndex(index), duration);
        }
    }
    private int CalculateIndexWithOffset(int index)
    {
        return (index + offset) % numberOfObjects;
    }
    public Vector3 GetPositionByIndex(int index)
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
    public IntersectionPoints GetIntersectionPointsByIndex(int index)
    {
        index = CalculateIndexWithOffset(index);
        return (index == topIntersectionIndex) ? IntersectionPoints.Top : (index == bottomIntersectionIndex) ? IntersectionPoints.Bottom : IntersectionPoints.None;
    }
    public IEnumerable<int> GetIntersectionPoints()
    {
        return new int[] { topIntersectionIndex, bottomIntersectionIndex };
    }
    public Diamonds.Type GetDiamondColor(int index)
    {
        return diamondsConfig.GetDiamondConfig(CalculateIndexWithOffset(index)).type;
    }
}
