using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "StandardCardEvent", menuName = "Scriptable Objects/StandardCardEvent")]

    public class StandardCardEvent : GameEvent<IStandardCard>
    {
    }
}

