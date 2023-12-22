using UnityEngine;

public static class NestedComponentUtilities
{


    /// <summary>
    /// Find T on supplied transform or any parent. Unlike GetComponentInParent, GameObjects do not need to be active to be found.
    /// </summary>
    public static T GetParentComponent<T>(this Transform t)
        where T : Component
    {
        T found = t.GetComponent<T>();

        if (found)
            return found;

        var par = t.parent;
        while (par)
        {
            found = par.GetComponent<T>();
            if (found)
                return found;
            par = par.parent;
        }
        return null;
    }
}
