using Diamonds;
using System.Collections.Generic;
using UnityEngine;
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
    public void Initialize(ModelSides side, int numberOfObjects)
    {
        this.numberOfObjects = numberOfObjects;
        transform.Rotate(Vector3.up * 45f);
        sideLength = Mathf.FloorToInt(numberOfObjects * 0.25f);
        radius = sideLength * objectSpacing * 0.5f;

        float modelOffsetValue = (side == ModelSides.Left) ? -radius * 0.5f : (side == ModelSides.Right) ? radius * 0.5f : 0f;
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
            diamond.SetConfig(diamondsConfig.GetDiamondConfig(i % numberOfObjects));
            diamond.MoveIn(GetPositionByIndex(i));
            Diamonds.Add(diamond);
        }
    }
    public void RotateLeft()
    {
        SetOffset(offset - 1);
        UpdateState();
    }
    public void RotateRight()
    {
        SetOffset(offset + 1);
        UpdateState();
    }
    private void SetOffset(int value)
    {
        offset = (value % numberOfObjects + numberOfObjects) % numberOfObjects;
    }
    private void UpdateState()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            int index = CalculateIndexWithOffset(i);
            Diamonds[i].MoveIn(GetPositionByIndex(index));
        }
    }
    private int CalculateIndexWithOffset(int index)
    {
        return (index + offset) % numberOfObjects;
    }
    public Vector3 GetPositionByIndex(int index)
    {
        int side = Mathf.FloorToInt(index / sideLength);
        int numberInSide = index - (side * sideLength);
        SideData sideData = sides[side];
        Vector3 position = sideData.StartPosition + sideData.Direction * numberInSide;
        return position;
    }
    public int GetIndexByPosition(Vector3 position)
    {
        int side = 0;
        float minDistance = float.MaxValue;
        for (int i = 0; i < sides.Length; i++)
        {
            float distance = Vector3.Dot(position - sides[i].StartPosition, sides[i].Direction);
            if (distance < minDistance)
            {
                minDistance = distance;
                side = i;
            }
        }
        int numberInSide = Mathf.FloorToInt(Vector3.Dot(position - sides[side].StartPosition, sides[side].Direction));
        int index = side * sideLength + numberInSide;
        return index;
    }

    public List<Vector3> GetDiamondPositions()
    {
        List<Vector3> positions = new();

        foreach (Diamond diamond in Diamonds)
        {
            positions.Add(diamond.transform.localPosition);
        }
        return positions;
    }
    public Type GetDiamondColor(int index)
    {
        return diamondsConfig.GetDiamondConfig(CalculateIndexWithOffset(index)).type;
    }
}
