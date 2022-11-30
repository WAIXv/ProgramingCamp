using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameVol : MonoBehaviour
{
    private GameOption _gameOption;
    public Slider volControl;
    public Button muteSet;
    // Start is called before the first frame update
    void Awake()
    {
        
        _gameOption = GameObject.Find("GameOption").GetComponent<GameOption>();
        muteSet  = GameObject.Find("MuteButton").GetComponent<Button>();
        volControl = GameObject.Find("Vol_change").GetComponent<Slider>();
        Debug.Log(_gameOption.Vol);
        volControl.value = _gameOption.Vol;
    }

    // Update is called once per frame
    public void volChange()
    {
        _gameOption.Vol = volControl.value;
    }

    public void muteChange()
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
