using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using EX2;

// -----------------------------------------------------------------------------------------------
// Handles the save/load of profiles, meant to be attached to a canvas with the respective buttons
// -----------------------------------------------------------------------------------------------

public class SaveSlotButtonHandler : MonoBehaviour {

    public Text[] buttonLabels;

    private const string EMPTY_SLOT = "New Game";
    private const string USED_SLOT = "Load Save";

    void Start () {
        SetButtonLabels();
    }

    // Sets labels on button to indicate if we are loading an empty slot or an actual profile
    void SetButtonLabels() {
         if ( buttonLabels.Length != DataService.MAX_NUMBER_OF_PROFILES) {
             Debug.LogError("Incorrect number of button labels. Must be exactly " + DataService.MAX_NUMBER_OF_PROFILES);
         } else {
             for (int i = 0; i < DataService.MAX_NUMBER_OF_PROFILES; i++) {
                
                if (File.Exists(DataService.Instance.GetSaveDataFilePath(i + 1))) {
                    buttonLabels[i].text = USED_SLOT + (i + 1).ToString();
                } else {
                    buttonLabels[i].text = EMPTY_SLOT;
                }
             }
         }
    }

    // Assigned to each button via the inspector
    public void LoadGame(int profileNumber) {
        DataService.Instance.LoadSaveData(profileNumber);
        SceneManager.LoadScene(DataService.Instance.SaveData.lastLevel);
    }
}
