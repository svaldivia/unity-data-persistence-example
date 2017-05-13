using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Button))]
public class ReloadScene : MonoBehaviour {

    void Start () {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(Reload);
    }

    void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += (scene, mode) => {
            Debug.Log("Scene Reloaded");
        };
    }
}
