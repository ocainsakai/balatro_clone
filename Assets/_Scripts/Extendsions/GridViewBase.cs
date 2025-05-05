using UnityEngine;

[RequireComponent (typeof(GridLayoutGO))]
public abstract class GridViewBase : MonoBehaviour
{
    protected GridLayoutGO _layout => GetComponent<GridLayoutGO>();
}