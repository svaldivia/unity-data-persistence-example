using UnityEngine;
using UnityEngine.SceneManagement;

namespace EX1 {

    // Singleton that is responsible for load/save data
    public class DataService : MonoBehaviour {

        private static DataService _instance = null;
        public static DataService Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<DataService>();

                    // If none is found, create a new game object and add this class to it
                    if (_instance == null) {
                        GameObject go = new GameObject(typeof(DataService).ToString());
                        _instance = go.AddComponent<DataService>();
                    }
                }
                return _instance;
            }
        }

        public PlayerPrefsHandler prefs { get; private set; }

        void Awake () {
            if (Instance != this) {
                Destroy(this);
            } else {
                DontDestroyOnLoad(gameObject);

                prefs = new PlayerPrefsHandler();
                prefs.RestorePreferences();

                // Ensure that the player preferences are applied to the new scene
                // Using Lambdas:
                // SceneManager.sceneLoaded += (scene, mode) => prefs.RestorePreferences();
                // Using functions:
                SceneManager.sceneLoaded += OnLevelWasLoaded;
            }
        }

        void OnLevelWasLoaded(Scene scene, LoadSceneMode mode) {
            prefs.RestorePreferences();
        }
    }
}