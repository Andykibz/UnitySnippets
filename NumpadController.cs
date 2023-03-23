/// <summary>
/// The NumpadController supposed to process inputs from a UI Numpad in Unity;
/// Composed of buttons and an Input FIeld both from the TMPro namespace
/// It is ideal for VR applications.
/// Supports:
///     1. Negation of Numbers
///     2. Setting of Limits
///     3. Backspacing
/// </summary>
using UnityEngine.Events;
using UnityEngine;
using TMPro;


namespace ThaIntersect
{
    public class NumpadController : MonoBehaviour
    {
        public UnityEvent<float> OnValueEnter = new UnityEvent<float>();

        #if UNITY_EDITOR 
        [NaughtyAttributes.ReadOnly] 
        #endif
        [SerializeField] float value;
        [SerializeField] TMP_InputField textMeshIn;
        [SerializeField] Vector2 limits;
        [SerializeField] uint decimalPoints=2;
        bool negated;



        void Start()
        {
    
        }

        private void OnDisable() {
            OnValueEnter.RemoveAllListeners();
        }

        
        void Update()
        {
            
        }

        private void LateUpdate() {
        }

        /// <summary>
        /// Called when a number key is pressed
        /// </summary>
        /// <param name="_value"></param>
        public void onKeyPress(float _value){
            var val = textMeshIn.text+_value.ToString();
            float.TryParse(val, out float _val);
            if(val[0] == '.') return;
            if (limits.x <= _val && _val <= limits.y){
                if(val.Contains(".") && val.Split(".")[1].Length> decimalPoints){
                    Debug.Log($"Limited to {decimalPoints} Decimal Points");
                }else{
                    textMeshIn.text = val;


                }
            }
        }

        /// <summary>
        /// Called when the enter key is pressed
        /// </summary>
        public void onValueEnter()
        {
            if (float.TryParse(textMeshIn.text, out float _val))
            {
                value = _val;
                OnValueEnter?.Invoke(value);
            }else{
                Debug.LogError($"{textMeshIn.text} is not a number");
            }
        }

        /// <summary>
        /// Called when the '.' key is pressed
        /// </summary>
        /// <param name="_value"></param>
        public void onPointPress(string _value)
        {
            if( _value != "." ) return;
            if( decimalPoints < 1 ) return;
            var txt = textMeshIn.text;
            if (txt.Length == 0 ) txt = "0";
            if(txt.Contains(".")) return;
            textMeshIn.text = txt+_value;            
        }

        /// <summary>
        /// Delete the last character of text in the Input Field
        /// </summary>
        /// <param name="_value"></param>
        public void onDeletePress()
        {
            var txt = textMeshIn.text;
            if (txt.Length < 1) return;
            textMeshIn.text = txt.Substring(0, txt.Length - 1);
        }

        public void onNegateValue(){
            if(limits.x > 0) return;
            negated = !negated;
            var txt = textMeshIn.text;
            var val = '-';
            if( negated ){
                if(txt.Contains("-")) return;
                textMeshIn.text = val+txt;
            }else{
                if (txt.Contains("-")) textMeshIn.text = txt.TrimStart('-');                
            }
        }


    }
    
}
