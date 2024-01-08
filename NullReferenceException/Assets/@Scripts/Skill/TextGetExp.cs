using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextGetExp : MonoBehaviour
{
    public TMP_Text lvTxt;

    public void GetExp()
    {
        Main.Object.Player.AddExp(10000);
        lvTxt.text = $"{Main.Object.Player.Data.Lv} ({Main.Object.Player.Exp/ Main.Object.Player.MaxExp})" ;
    }
}
