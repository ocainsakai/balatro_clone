
using UnityEngine;

[RequireComponent(typeof(GridLayoutGO))]
public abstract class GridView : MonoBehaviour
{
    protected GridLayoutGO _layout => GetComponent<GridLayoutGO>();
}