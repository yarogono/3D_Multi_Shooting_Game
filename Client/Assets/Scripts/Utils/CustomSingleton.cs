using UnityEngine;

public class CustomSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed
    private static bool m_ShuttingDown = false;
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                return null;
            }

            if (m_Instance == null)
            {
                m_Instance = (T)FindObjectOfType(typeof(T));

                if (m_Instance == null)
                {
                    GameObject singletonObject = new GameObject { name = "@" + typeof(T).ToString() };
                    m_Instance = singletonObject.AddComponent<T>();

                    DontDestroyOnLoad(singletonObject);
                }
            }

            return m_Instance;
        }
    }

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}
