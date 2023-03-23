/// <summary>
/// Depends on Ricimi
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using Ricimi;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Shared;
using UnityEngine.Events;

namespace ThaIntersect
{
    [RequireComponent(typeof(Collider),typeof(Button))]
    public class VRUITouch : MonoBehaviour
    {
        public UnityEvent OnVRTouch = new UnityEvent();
        CleanButton _button;
        Image bg;
        Color bgColor;
        HapticData hapticsFB = new HapticData(.05f,.1f,.8f);

        
        void Start()
        {
            _button = GetComponent<CleanButton>();
            bg = GetComponent<Image>();
            bgColor = bg.color;
            
        }

        private void OnDisable() {
            OnVRTouch.RemoveAllListeners();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "LeftTouch") {
                _button.onClick.Invoke();
                bg.color = new Color( bgColor.r, bgColor.g, bgColor.g,bgColor.a * 1.2f );
                HVRInputManager.Instance.LeftController.Vibrate(hapticsFB);

                OnVRTouch?.Invoke();
            }
            if(other.tag == "RightTouch") {
                _button.onClick.Invoke();
                bg.color = new Color(bgColor.r, bgColor.g, bgColor.g, bgColor.a * 1.2f);
                HVRInputManager.Instance.RightController.Vibrate(hapticsFB);

                OnVRTouch?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "LeftTouch" || other.tag == "RightTouch"  )
                bg.color = bgColor;
        }
    }
    
}
