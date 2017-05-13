using DataService = EX1.DataService;
// Change the namespace to access the different implementations
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSliderHandler : MonoBehaviour {

    // Use this for initialization
    void Start () {
       Slider slider = GetComponent<Slider>();
       slider.value = DataService.Instance.prefs.GetVolume();
       slider.onValueChanged.AddListener(
           (float value) => {
               DataService.Instance.prefs.SetVolume(value);
           }
       ); 
    }
}
