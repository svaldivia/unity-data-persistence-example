using UnityEngine;
using UnityEngine.SceneManagement;
using EX2;

public class GameTest : MonoBehaviour {

    private string saveData = "NOT LOADED";

    void Start() {
        Debug.Log("Start");
        if(DataService.Instance.SaveData != null) {
            saveData = DataService.Instance.SaveData.ToString();
        }

        Debug.LogFormat("SaveData: \n {0}", saveData);
    }

    void OnGUI() {

        if (DataService.Instance.SaveData == null) {
            GUI.Label(new Rect(10, 10, 200,30), "No profile loaded");
            return;
        }

        GUI.Label(new Rect(10, 10, 200,30), "Currently Loaded Profile: " + DataService.Instance.currentlyLoadedProfileNumber);
        GUI.Label(new Rect(10, 30, 100,30), "Health: " + DataService.Instance.SaveData.health);
        GUI.Label(new Rect(10, 50, 150,30), "Coins: " + DataService.Instance.SaveData.coins);
        GUI.Label(new Rect(10, 70, 150,30), "Lives: " + DataService.Instance.SaveData.lives);
        GUI.Label(new Rect(10, 90, 150,50), "PowerUps: " + DataService.Instance.SaveData.GetPowerUpsString());

        
        if(GUI.Button(new Rect(200,10,120,30), "Health Up")) {
            DataService.Instance.SaveData.health++;
        }
        if(GUI.Button(new Rect(200,50,120,30), "Health Down")) {
            DataService.Instance.SaveData.health--;
        }
        if(GUI.Button(new Rect(200,90,120,30), "Coins Up")) {
            DataService.Instance.SaveData.coins++;
        }
        if(GUI.Button(new Rect(200,130,120,30), "Coins Down")) {
            DataService.Instance.SaveData.coins--;
        }
        if(GUI.Button(new Rect(200,170,120,30), "Lives Up")) {
            DataService.Instance.SaveData.lives++;
        }
        if(GUI.Button(new Rect(200,210,120,30), "Lives Down")) {
            DataService.Instance.SaveData.lives--;
        }
        if(GUI.Button(new Rect(340,10,170,30), "Toggle Fireballs powerup")) {
            if (DataService.Instance.SaveData.powerUps.Contains(PowerUp.Fireballs)){
                DataService.Instance.SaveData.powerUps.Remove(PowerUp.Fireballs);
            } else {
                DataService.Instance.SaveData.powerUps.Add(PowerUp.Fireballs);
            }
        }
        if(GUI.Button(new Rect(340,50,170,30), "Toggle DoubleJump powerup")) {
            if (DataService.Instance.SaveData.powerUps.Contains(PowerUp.DoubleJump)){
                DataService.Instance.SaveData.powerUps.Remove(PowerUp.DoubleJump);
            } else {
                DataService.Instance.SaveData.powerUps.Add(PowerUp.DoubleJump);
            }
        }
        if(GUI.Button(new Rect(340,90,120,30), "Save")) {
            DataService.Instance.WriteSaveData();
        }
        if(GUI.Button(new Rect(340,130,120,30), "Load next Level")) {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        if(GUI.Button(new Rect(340,170,160,30), "Return to Main Menu")) {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
