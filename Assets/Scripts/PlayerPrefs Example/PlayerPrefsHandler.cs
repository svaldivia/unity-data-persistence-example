using UnityEngine;

// -----------------------------------------------------------------------------------------
// Handles the saving, recalling, and applying of all PlayerPrefs for the app.
// --
// It is important to keep all the prefs keys in a single script and make them constant
// -----------------------------------------------------------------------------------------

public class PlayerPrefsHandler {

    /*
    Make keys const and public so they can be accessed anywhere as: PlayerPrefsHandler.MUTE_INT.
    Naming: <name>_<type>
    */
	#region PlayerPrefs keys
    public const string MUTE_INT = "mute";
    public const string VOLUME_F = "volume";
    #endregion

    private const bool DEBUG_ON = true;

    public void RestorePreferences() {
        SetMuted(GetIsMuted());
        SetVolume(GetVolume());
    }

    public void SetMuted(bool muted) {

        PlayerPrefs.SetInt(MUTE_INT, muted ? 1 : 0);

        AudioListener.pause = muted;

        if(DEBUG_ON) {
            Debug.LogFormat("SetMuted({0})", muted);
        }

    }

    public bool GetIsMuted() {
        // 1 = muted, default is 0
        return PlayerPrefs.GetInt(MUTE_INT, 0) == 1;
    }

    // Volume can only be between 0 - 1
    public void SetVolume(float volume) {
        volume = Mathf.Clamp(volume, 0 , 1);

        PlayerPrefs.SetFloat(VOLUME_F, volume);
        AudioListener.volume = volume;;

        if(DEBUG_ON) {
            Debug.LogFormat("SetVolume({0})", volume);
        }
    }

    public float GetVolume() {
        return Mathf.Clamp(PlayerPrefs.GetFloat(VOLUME_F, 1), 0, 1);
    }
}
