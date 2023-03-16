/// <summary>
/// Switches LEDs using their Emission Properties. 
/// The status of the LED is based on the Signal object from Game4Automation
/// </summary>
using UnityEngine;
using game4automation;

namespace ThaIntersect
{
    public class StatusLED : MonoBehaviour
    {
        [SerializeField] int matIndex;
        [SerializeField] Signal signal;
        [SerializeField] Renderer _renderer;

        // Start is called before the first frame update
        void Start()
        {
            signal.EventSignalChanged.AddListener(PowerLED);    
        }

        void PowerLED(Signal _signal){
            if((bool)_signal.GetValue()){
                _renderer.materials[matIndex].EnableKeyword("_EMISSION");
            }else{
                _renderer.materials[matIndex].DisableKeyword("_EMISSION");
            }
        }
    }
    
}

