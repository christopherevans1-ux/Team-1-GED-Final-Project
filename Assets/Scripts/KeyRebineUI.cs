using System;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;

public class KeyRebindUI : MonoBehaviour
{
    [SerializeField] private GameAction action;
    [SerializeField] private TMP_Text keyText;

    private static KeyRebindUI activeRebindUI;

    void Start()
    {
        UpdateKeyText();
    }

    public void StartRebinding()
    {
        //cancel another button's rebinding
        if(activeRebindUI != null)
        {
            activeRebindUI.CancelRebinding();
        }

        activeRebindUI = this;
        keyText.text = "Press a key";
    }

    void Update()
    {
        if (activeRebindUI != this)
            return;
        //press backspace to cancle
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            CancelRebinding();
            return;
        }

        foreach(KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            //only allow keyboard keys
            if (key == KeyCode.None || key >= KeyCode.Mouse0)
                continue;
            if(Input.GetKeyDown(key))
            {
                //check if another action took this key
                bool isUsed = false;
                foreach(GameAction otherAction in Enum.GetValues(typeof(GameAction)))
                {
                    if(otherAction != action && KeyBindings.GetKey(otherAction) == key)
                    {
                        isUsed = true;
                        break;
                    }
                }
                if(isUsed)
                {
                    keyText.text = "Key already used!";
                    return;
                }

                KeyBindings.SetKey(action, key);
                CancelRebinding();
                break;
            }
        }
    }

    void CancelRebinding()
    {
        if(activeRebindUI == this)
            activeRebindUI = null;
        UpdateKeyText();
    }
     void UpdateKeyText()
    {
        keyText.text = KeyBindings.GetKey(action).ToString();
    }
}
