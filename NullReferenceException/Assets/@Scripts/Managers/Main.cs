using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {


    #region Singleton

    private static Main _instance;
    private static bool _initialized;

    public static Main Instance {
        get {
            if (!_initialized) {
                _initialized = true;

                GameObject obj = GameObject.Find("@Main");
                if (obj == null) {
                    obj = new() { name = "@Main" };
                    obj.AddComponent<Main>();
                    DontDestroyOnLoad(obj);
                    _instance = obj.GetComponent<Main>();
                }
            }
            return _instance;
        }
    }

    #endregion

    private PoolManager _pool = new();
    private ResourceManager _resource = new();
    private DataManager _data = new();
    private UIManager _ui = new();
    private SceneManagerEx _scene = new();
    private ObjectManager _object = new();
    private GameManager _game = new();

    public static PoolManager Pool => Instance?._pool;
    public static ResourceManager Resource => Instance?._resource;
    public static DataManager Data => Instance?._data;
    public static UIManager UI => Instance?._ui;
    public static SceneManagerEx Scene => Instance?._scene;
    public static ObjectManager Object => Instance?._object;
    public static GameManager Game => Instance?._game;

    public SkillManager Skill;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject skillSystemPrefab = Resources.Load<GameObject>("Prefabs/Skill/SkillSystem");

        if (skillSystemPrefab != null)
        {
            GameObject skillSystemInstance = Instantiate(skillSystemPrefab);
        }
        else
        {
            Debug.LogError("경로를 확인필요.");
        }
    }
}
