using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

// ----------------------------------------------------------------------------------------------------
// This Data service builds on top of the EX1, adding JSON data to be saved in addition to PlayerPrefs
// ----------------------------------------------------------------------------------------------------

namespace EX2 {

    // Repsonsible for loading/savign data
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
            if (Instance != this || FindObjectOfType<EX1.DataService>()) {
                Destroy(this);
            } else {
                DontDestroyOnLoad(gameObject);

                prefs = new PlayerPrefsHandler();
                prefs.RestorePreferences();

                SceneManager.sceneLoaded += OnLevelWasLoaded;
            }
        }

        void OnLevelWasLoaded(Scene scene, LoadSceneMode mode) {
            Debug.Log("Scene Loaded");
            OnLevelWasLoaded();
        }

        void OnLevelWasLoaded() {
            prefs.RestorePreferences();

            if(SaveData == null) {
                LoadSaveData();
            }

            // Set the player's progress if this is not the main menu scene.
            // Scene 2 and 3 are menus
            Scene activeScene = SceneManager.GetActiveScene();
            if (activeScene.buildIndex > 3) {
                SaveData.lastLevel = activeScene.path.Replace("Assets/", "").Replace(".unity","");
            }

            WriteSaveData();
        }

        public SaveData SaveData { get; private set; }

        // Use to prevent reloading the data when a new scene loads
        bool isDataLoaded = false;
        public int currentlyLoadedProfileNumber { get; private set; }
        public const int MAX_NUMBER_OF_PROFILES = 3;

        //Loads the save data for a specific profile number
        public void LoadSaveData (int profileNumber = 0) {

            if (isDataLoaded && profileNumber == currentlyLoadedProfileNumber) { return; }

            // Load the first available profile automatically
            if (profileNumber <= 0) {
                
                for (int i = 1; i <= MAX_NUMBER_OF_PROFILES; i++)
                {
                    if (File.Exists(GetSaveDataFilePath(i))) {
                        SaveData = SaveData.ReadFromFile(GetSaveDataFilePath(i));

                        currentlyLoadedProfileNumber = i;
                        break;
                    }
                }
            } else {

                if (File.Exists(GetSaveDataFilePath(profileNumber))) {
                    SaveData = SaveData.ReadFromFile(GetSaveDataFilePath(profileNumber));
                } else {
                    SaveData = new SaveData();
                }

                currentlyLoadedProfileNumber = profileNumber;
            }
        }

        private const string SAVE_DATA_FILE_NAME_BASE = "savedata";
        private const string SAVE_DATA_FILE_EXTENSION = ".txt";
        // This is done through a getter because we're calling to a non-constant member (Application.dataPath) to construct this.
        private string SAVE_DATA_DIRECTORY { get { return Application.dataPath + "/saves/";} }

        // Get the full path and file name for our SaveData file.
        // ex: 'c:\projectdirectory\assets\saves\savedata1.txt'
        public string GetSaveDataFilePath(int profileNumber) {

            if(profileNumber < 1) {
                throw new System.ArgumentException("profileNumber must be greater than 1. Was: " + profileNumber);
            }

            if (!Directory.Exists(SAVE_DATA_DIRECTORY)) {
                Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
            }

            return SAVE_DATA_DIRECTORY + SAVE_DATA_FILE_NAME_BASE + profileNumber.ToString() + SAVE_DATA_FILE_EXTENSION;
        }

        public void WriteSaveData() {

            if (currentlyLoadedProfileNumber <= 0) {
                for (int i = 1; i <= MAX_NUMBER_OF_PROFILES; i++) {
                    if(!File.Exists(GetSaveDataFilePath(i))) {
                        currentlyLoadedProfileNumber = i;
                        break;
                    }
                }
            }

            if (currentlyLoadedProfileNumber <= 0) {
                throw new System.Exception("Cannot WriteSaveData. No available profiles and currentlyLoadedProfile = 0");
            } else {

                if (SaveData == null) {
                    SaveData = new SaveData();
                }

                SaveData.WriteToFile(GetSaveDataFilePath(currentlyLoadedProfileNumber));
            }
        }

    }
}
