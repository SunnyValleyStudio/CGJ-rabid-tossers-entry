using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVSInput
{
    public enum InputAxis
    {
        Horizontal,
        Vertical
    }
    public class InputManagerArrowsSVS : IInputManagerSVS
    {
        private InputAxis _inputAxisToCheck;

        public InputManagerArrowsSVS(InputAxis inputAxisToCheck)
        {
            _inputAxisToCheck = inputAxisToCheck;
        }
        public Vector3 GetMainMovementInput()
        {
            float inputValue = Input.GetAxisRaw(_inputAxisToCheck.ToString());
            if (_inputAxisToCheck == InputAxis.Horizontal)
            {
                return new Vector3(inputValue, 0, 0);
            }
            else
            {
                return new Vector3(0, inputValue, 0);
            }
            
        }
    }
}

