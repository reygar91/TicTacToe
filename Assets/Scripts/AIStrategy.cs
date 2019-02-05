using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStrategy : ScriptableObject
{

    public abstract bool MakeMove(GameController controller);

}
