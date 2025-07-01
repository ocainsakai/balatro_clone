using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class GridObjectLayout2D : MonoBehaviour
{
    #region ENUM
    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public enum VerticalAlignment
    {
        Top,
        Middle,
        Bottom
    }

    public enum Corner
    {
        UpperLeft,
        UpperRight,
        LowerLeft,
        LowerRight
    }

    public enum Axis
    {
        Horizontal,
        Vertical
    }

    public enum Constraint
    {
        Flexible,
        FixedColumnCount,
        FixedRowCount
    }
    #endregion
    #region SETTING
    [Header("Alignment")]
    public HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
    public VerticalAlignment verticalAlignment = VerticalAlignment.Top;


    [Header("Grid Settings")]
    public Corner startCorner = Corner.UpperLeft;
    public Axis startAxis = Axis.Horizontal;
    public Vector2 cellSize = new Vector2(1f, 1f);
    public Vector2 spacing = Vector2.zero;
    public Constraint constraint = Constraint.Flexible;
    public int constraintCount = 2;

    [Header("Target Objects")]
    public List<Transform> targetObjects = new List<Transform>();

    [Header("Calculated positions (readonly)")]
    public List<Vector2> calculatedPositions = new List<Vector2>();
    #endregion
    public async UniTask ApplyWithoutLayout(IEnumerable<MonoBehaviour> targets, int delay = 60)
    {
        delay = Mathf.Clamp(delay, 12, 60);
        List<UniTask> tasks = new List<UniTask>();
        if (targets != null)
        {
            targetObjects = targets.Select(x => x.transform).Where(x => x.position != transform.position).ToList();
            foreach (Transform obj in targetObjects)
            {
                tasks.Add(obj.transform.DOMove(transform.position, 0.1f)
                    .AsyncWaitForCompletion()
                    .AsUniTask());
                await UniTask.Delay(delay);
            }
        }
    }
    public async UniTask ApplyLayout(IEnumerable<MonoBehaviour> targets, int delay = 60)
    {
        delay = Mathf.Clamp(delay, 12, 60);
        List<UniTask> tasks = new List<UniTask>();
        if (targets != null)
        {
            targetObjects = targets.Select(x => x.transform).ToList();
            CalculateLayout();

            for (int i = 0; i < targetObjects.Count && i < calculatedPositions.Count; i++)
            {
                var obj = targetObjects[i];
                Vector2 localPos = calculatedPositions[i];
                tasks.Add(obj.transform.DOMove(new Vector3(localPos.x, localPos.y, obj.localPosition.z), 0.1f)
                    .AsyncWaitForCompletion()
                    .AsUniTask());
                await UniTask.Delay(delay);
            }
            await UniTask.WhenAll(tasks);
        }
    }

    public void CalculateLayout()
    {
        int count = targetObjects.Count;
        if (count == 0)
            return;

        int cellCountX = 1, cellCountY = 1;

        if (constraint == Constraint.FixedColumnCount)
        {
            cellCountX = constraintCount;
            cellCountY = Mathf.CeilToInt(count / (float)cellCountX);
        }
        else if (constraint == Constraint.FixedRowCount)
        {
            cellCountY = constraintCount;
            cellCountX = Mathf.CeilToInt(count / (float)cellCountY);
        }
        else
        {
            cellCountX = Mathf.CeilToInt(Mathf.Sqrt(count));
            cellCountY = Mathf.CeilToInt(count / (float)cellCountX);
        }

        int cellsPerMainAxis = (startAxis == Axis.Horizontal) ? cellCountX : cellCountY;

        int cornerX = (int)startCorner % 2; // 0: Left, 1: Right
        int cornerY = (int)startCorner / 2; // 0: Upper, 1: Lower

        calculatedPositions.Clear();
        float totalWidth = cellCountX * cellSize.x + (cellCountX - 1) * spacing.x;
        float totalHeight = cellCountY * cellSize.y + (cellCountY - 1) * spacing.y;

        float offsetX = 0f;
        switch (horizontalAlignment)
        {
            case HorizontalAlignment.Left:
                offsetX = 0f;
                break;
            case HorizontalAlignment.Center:
                offsetX = -totalWidth / 2f;
                break;
            case HorizontalAlignment.Right:
                offsetX = -totalWidth;
                break;
        }

        float offsetY = 0f;
        switch (verticalAlignment)
        {
            case VerticalAlignment.Top:
                offsetY = 0f;
                break;
            case VerticalAlignment.Middle:
                offsetY = totalHeight / 2f;
                break;
            case VerticalAlignment.Bottom:
                offsetY = totalHeight;
                break;
        }


        for (int i = 0; i < count; i++)
        {
            int posX, posY;

            if (startAxis == Axis.Horizontal)
            {
                posX = i % cellsPerMainAxis;
                posY = i / cellsPerMainAxis;
            }
            else
            {
                posX = i / cellsPerMainAxis;
                posY = i % cellsPerMainAxis;
            }

            if (cornerX == 1)
                posX = cellCountX - 1 - posX;
            if (cornerY == 1)
                posY = cellCountY - 1 - posY;

            float x = posX * (cellSize.x + spacing.x);
            float y = -posY * (cellSize.y + spacing.y);
            calculatedPositions.Add(new Vector2(transform.position.x +x + offsetX,transform.position.y+ y + offsetY));

            //calculatedPositions.Add(new Vector2(x, y));
        }
    }
}
