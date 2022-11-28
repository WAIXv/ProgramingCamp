using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menuVol : MonoBehaviour
{
    private GameOption _gameOption;
    public Slider volControl;
    public Button Muteset;
    // Start is called before the first frame update
    private void Awake()
    {
        _gameOption = GameObject.Find("GameOption").GetComponent<GameOption>();
        volControl = GameObject.Find("Vol_change").GetComponent<Slider>();
        Muteset = GameObject.Find("MuteButton").GetComponent<Button>();
        volControl.value = _gameOption.Vol;
    }

    // Update is called once per frame
    public void changeVol()
    {
        _gameOption.Vol = volControl.value;
    }

    public void changeMute()
    {   
        _gameOption.ifMute =! _gameOption.ifMute;
        if(_gameOption.ifMute)
        {
            GameObject.Find("MuteButton").GetComponent<Image>().color=Color.red;
        }
        else
        {
            GameObject.Find("MuteButton").GetComponent<Image>().color=Color.green;
        }
    }
}
