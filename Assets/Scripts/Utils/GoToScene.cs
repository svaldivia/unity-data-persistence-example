using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GoToScene : MonoBehaviour {

    public string sceneName;

    void Start () {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(loadScene);
    }
    
    void loadScene() {
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += (scene, mode) => {
            Debug.LogFormat("Scene {0} Loaded", scene.name);
        };
    }
}
