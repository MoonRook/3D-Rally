using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDependenciesConteiner : Dependency
{
    [SerializeField] private Pauser pauser;
    
    private static GlobalDependenciesConteiner instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void BindAll(MonoBehaviour monoBehaviourScene)
    {
        Bind<Pauser>(pauser, monoBehaviourScene);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectToBind();
    }
}
