using UnityEngine;

public interface IBlind 
{
    string Name { get; }
    Sprite Sprite { get; }
    int minimunAnte {  get; }
    float scoreMultiple { get; }
    int reward {  get; }
}
