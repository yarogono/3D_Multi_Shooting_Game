using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public static class NestedComponentUtilities
{

    // Recycled collections
    private static Queue<Transform> nodesQueue = new Queue<Transform>();
    public static Dictionary<System.Type, ICollection> searchLists = new Dictionary<System.Type, ICollection>();

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


    /// <summary>
    /// Same as GetComponentsInChildren, but will not recurse into children with component of the NestedT type. This allows nesting of PhotonViews/NetObjects to be respected.
    /// </summary>
    /// <typeparam name="T">Cast found components to this type. Typically Component, but any other class/interface will work as long as they are assignable from SearchT.</typeparam>
    /// <typeparam name="SearchT">Find components of this class or interface type.</typeparam>
    /// <typeparam name="DontRecurseOnT"></typeparam>
    /// <param name="t"></param>
    /// <param name="includeInactive"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static void GetNestedComponentsInChildren<T, SearchT, NestedT>(this Transform t, bool includeInactive, List<T> list)
        where T : class
        where SearchT : class
    {
        list.Clear();

        // If this is inactive, nothing will be found. Give up now if we are restricted to active.
        if (!includeInactive && !t.gameObject.activeSelf)
            return;

        System.Type searchType = typeof(SearchT);

        // Temp lists are also recycled. Get/Create a reusable List of this type.
        List<SearchT> searchList;
        if (!searchLists.ContainsKey(searchType))
            searchLists.Add(searchType, searchList = new List<SearchT>());
        else
            searchList = searchLists[searchType] as List<SearchT>;

        // Recurse child nodes one layer at a time. Using a Queue allows this to happen without a lot of work.
        nodesQueue.Clear();
        nodesQueue.Enqueue(t);

        while (nodesQueue.Count > 0)
        {
            var node = nodesQueue.Dequeue();

            // Add found components on this gameobject node
            searchList.Clear();
            node.GetComponents(searchList);
            foreach (var comp in searchList)
            {
                var casted = comp as T;
                if (!ReferenceEquals(casted, null))
                    list.Add(casted);
            }

            // Add children to the queue for next layer processing.
            for (int i = 0, cnt = node.childCount; i < cnt; ++i)
            {
                var child = node.GetChild(i);

                // Ignore inactive nodes (optional)
                if (!includeInactive && !child.gameObject.activeSelf)
                    continue;

                // ignore nested DontRecurseOnT
                if (!ReferenceEquals(child.GetComponent<NestedT>(), null))
                    continue;

                nodesQueue.Enqueue(child);
            }
        }

    }
}
