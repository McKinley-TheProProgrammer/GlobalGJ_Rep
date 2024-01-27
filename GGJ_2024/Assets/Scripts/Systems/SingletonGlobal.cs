using System;
using UnityEngine;

public abstract class SingletonGlobal<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_Instance;

    private static object m_Lock = new object();

    //Checa se o Jogo/App esta sendo fechado
    private static bool m_ShuttingDown = false, destroyingDuplicate = false;
    
    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                //Debug.LogWarning("[SingletonSingleScene] Instance '" + typeof(T) +
                //    "' already destroyed. Returning null.");
                return null;
            }
            
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (T)FindObjectOfType(typeof(T));
                    // Checa se foi encontrado uma instância do tipo T
                    if (m_Instance == null)
                    {
                        if (!m_ShuttingDown)
                        {
                            Debug.Log($"Instância do {typeof(T)} Singleton não encontrado");
                            return null;
                        }
                    }
                    
                    DontDestroyOnLoad(m_Instance.gameObject);
                }

                return m_Instance;
            }
        }
    }

    protected virtual void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }

    protected virtual void OnDestroy()
    {
        m_ShuttingDown = true;
    }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"Mais de uma instância encontrada do Sistema {typeof(T)} no objeto {m_Instance.gameObject}");
            Application.Quit();
            return;
        }

    }
}
