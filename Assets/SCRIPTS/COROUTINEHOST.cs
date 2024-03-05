using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COROUTINEHOST : MonoBehaviour
{
    public static COROUTINEHOST Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (COROUTINEHOST)FindAnyObjectByType(typeof(COROUTINEHOST));

                if (m_Instance == null)
                {
                    GameObject go = new GameObject();
                    m_Instance = go.AddComponent<COROUTINEHOST>();
                }

                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }

    private static COROUTINEHOST m_Instance = null;
}