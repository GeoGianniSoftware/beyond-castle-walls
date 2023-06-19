using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Furniture : Entity
{
    public Entity user = null;
    public Transform sitPosition;

    public bool canUse(Entity attempting) {
        if (user == null || user == attempting)
            return true;
        return false;
    }

    public void clear() {
        user = null;
    }
}
