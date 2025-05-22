
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;


public class CardGridView : BaseAnim
{
    [Header("Grid Settings")]
    //[SerializeField] public bool useGrid = true;
    [SerializeField] public float spacingX = 0; 
    [SerializeField] public float spacingY = 0; 
    [SerializeField] public int maxColumns = 0;
    [SerializeField] public Vector2 padding = Vector2.zero;
    [SerializeField] public bool centerPivot = false;
    [Header("Alignment")]
    [SerializeField] public HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
    [SerializeField] public VerticalAlignment verticalAlignment = VerticalAlignment.Top;
    [Header("Size Adjustment")]
    [SerializeField] private bool useBoundsForSize = true;
    [SerializeField] private Vector2 defaultChildSize = new Vector2(1f, 1f);
    public enum HorizontalAlignment { Left, Center, Right }
    public enum VerticalAlignment { Top, Middle, Bottom }

   
    public async UniTask SequentialReposition(int delayMs)
    {
        foreach (var (child, targetPos) in GetRepositionTargets())
        {
            child.DOLocalMove(targetPos, animationDuration)
                           .SetEase(animationEase);
            child.GetComponent<Card>()?.Flip(false);
            await UniTask.Delay(delayMs);
        }
    }

    public async UniTask SequentialReposition()
    {
        foreach (var (child, targetPos) in GetRepositionTargets())
        {
            if (!DOTween.IsTweening(child))
            {
                await child.DOLocalMove(targetPos, animationDuration)
                           .SetEase(animationEase)
                           .AsyncWaitForCompletion();
            }
        }
    }
    public UniTask ParallelReposition()
    {
        List<UniTask> tasks = new List<UniTask>();

        foreach (var (child, targetPos) in GetRepositionTargets())
        {
            if (!DOTween.IsTweening(child))
            {
                tasks.Add(
                    child.DOLocalMove(targetPos, animationDuration)
                         .SetEase(animationEase)
                         .AsyncWaitForCompletion()
                         .AsUniTask()
                );
            }
        }

        return UniTask.WhenAll(tasks);
    }

    #region CAlCULATE
    private IEnumerable<(Transform child, Vector3 targetPos)> GetRepositionTargets()
    {
        int count = transform.childCount;
        int columns = maxColumns > 0 ? Mathf.Min(maxColumns, count) : count;
        int rows = maxColumns > 0 ? Mathf.CeilToInt((float)count / columns) : 1;

        Vector2 childSize = useBoundsForSize ? CalculateChildSizeFromBounds() : defaultChildSize;

        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 localPos = CalculatePosition(columns, rows, childSize, i);
            yield return (child, localPos);
        }
    }

    private Vector3 CalculatePosition(int columns, int rows, Vector2 childSize, int i)
    {
        int row = i / columns;
        int col = i % columns;

        float xPos = col * (childSize.x + spacingX) + padding.x;
        float yPos = -row * (childSize.y + spacingY) - padding.y;

        if (horizontalAlignment == HorizontalAlignment.Center)
            xPos += (columns - 1) * (childSize.x + spacingX) * -0.5f;
        else if (horizontalAlignment == HorizontalAlignment.Right)
            xPos += (columns - 1) * (childSize.x + spacingX) * -1f;

        if (verticalAlignment == VerticalAlignment.Middle)
            yPos += (rows - 1) * (childSize.y + spacingY) * 0.5f;
        else if (verticalAlignment == VerticalAlignment.Bottom)
            yPos += (rows - 1) * (childSize.y + spacingY);

        Vector3 localPos = new Vector3(xPos, yPos, 0f);
        if (centerPivot)
        {
            localPos.x -= (columns - 1) * (childSize.x + spacingX) * 0.5f;
            localPos.y += (rows - 1) * (childSize.y + spacingY) * 0.5f;
        }

        return localPos;
    }
    private Vector2 CalculateChildSizeFromBounds()
    {
        Vector2 size = defaultChildSize;
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                Bounds bounds = renderer.bounds;
                size.x = Mathf.Max(size.x, bounds.size.x);
                size.y = Mathf.Max(size.y, bounds.size.y);
            }
            else
            {
                Collider2D collider2D = child.GetComponent<Collider2D>();
                if (collider2D != null)
                {
                    Bounds bounds = collider2D.bounds;
                    size.x = Mathf.Max(size.x, bounds.size.x);
                    size.y = Mathf.Max(size.y, bounds.size.y);
                }
            }
        }

        return size;
    }
    #endregion
}
