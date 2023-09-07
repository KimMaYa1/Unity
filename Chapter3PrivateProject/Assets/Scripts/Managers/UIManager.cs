
using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;

    public GameObject[] _npcs;
    private GameObject _player;
    private GameObject nameInputCollection;
    private GameObject _select;
    private GameObject _nameErrorText;
    private GameObject _uI;
    private GameObject _taklUI;
    private GameObject _attendPepleWindow;
    private GameObject _taklBox;
    private Text _inputName;
    private Text _name;
    private Text _npcName;
    private Text _attendPepleName;
    private Text _time;
    private bool _onChat = true;

    public bool Play = false;

    void Start()
    {
        _npcs = GameObject.FindGameObjectsWithTag("Character");
        nameInputCollection = Canvas.transform.Find("NameInputCollection").gameObject;
        _inputName = nameInputCollection.transform.Find("NameInput").gameObject.transform.Find("inputNameText").gameObject.GetComponent<Text>();
        _nameErrorText = nameInputCollection.transform.Find("NameErrorText").gameObject;
        _select = nameInputCollection.transform.Find("Select").gameObject;
        _uI = Canvas.transform.Find("UI").gameObject;
        _taklUI = Canvas.transform.Find("TalkUI").gameObject;
        _taklBox = Canvas.transform.Find("TalkBox").gameObject;
        _attendPepleWindow = _uI.transform.Find("AttendPepleWindow").gameObject;
        _time = _uI.transform.Find("Time").gameObject.GetComponent<Text>();
        _attendPepleName = _attendPepleWindow.transform.Find("AttendPepleNames").gameObject.GetComponent<Text>();
        _npcName = _taklUI.transform.Find("NpcName").gameObject.GetComponent<Text>();
        _nameErrorText.SetActive(false);
        _attendPepleWindow.SetActive(false);
        _uI.SetActive(false);
        _taklUI.SetActive(false);
        _taklBox.SetActive(false);
    }

    void Update()
    {
        if (_player.active)
        {
            TimeView();
            NpcTextBox();
            AttendListAdd();
        }
    }

    private void NpcTextBox()
    {
        float[] dist = new float[_npcs.Length];
        int count = 0;
        int num = 0;
        foreach (GameObject gameObject in _npcs)
        {
            dist[count++] = Vector3.Distance(_player.transform.position, gameObject.transform.position);
        }
        float min = dist[0];
        for (int i = 0; i < dist.Length; i++)
        {
            if(min > dist[i])
            {
                min = dist[i];
                num = i;
            }
        }
        if(min <= 5 && _onChat)
        {
            _taklUI.SetActive(true);
            _npcName.text = _npcs[num].GetComponentInChildren<Text>().text;
        }
        else if (min > 5)
        {
            _onChat = true;
            _taklUI.SetActive(false);
            _taklBox.SetActive(false);
        }
    }

    private void TimeView()
    {
        DateTime dateTime = DateTime.Now;
        _time.text = dateTime.Hour + ":" + dateTime.Minute;
    }

    private void AttendListAdd()
    {
        _attendPepleName.text = "";
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject gameObject in gameObjects)
        {
            if (!_attendPepleName.text.Contains(gameObject.transform.Find("PlayerCanvas").gameObject.transform.Find("Name").GetComponent<Text>().text))
            {
                _attendPepleName.text += gameObject.transform.Find("PlayerCanvas").gameObject.transform.Find("Name").GetComponent<Text>().text + "\n";
            }
        }
    }

    public void CatchPlayer(GameObject player)
    {
        _player = player;
        _name = _player.transform.Find("PlayerCanvas").gameObject.transform.Find("Name").gameObject.GetComponent<Text>();
    }

    private void ErrorName(string str)
    {
        _nameErrorText.GetComponent<Text>().text = str;
        _nameErrorText.SetActive(true);
    }

    public void OnPlay()
    {
        string str = _inputName.text;
        if (str.Length >= 2 && str.Length <= 10)
        {
            _nameErrorText.SetActive(false);
            _name.text = _inputName.text;
            nameInputCollection.SetActive(false);
            _uI.SetActive(true);
            Play = true;
            _player.GetComponent<PlayerInput>().enabled = true;
        }
        else if (str.Length < 2)
        {
            ErrorName("너무 짧습니다.");
            Play = false;
        }
        else if (str.Length > 10)
        {
            ErrorName("너무 깁니다.");
            Play = false;
        }
    }

    public void CharacterChange()
    {
        nameInputCollection.SetActive(true);
        nameInputCollection.transform.Find("Join").gameObject.SetActive(false);
        nameInputCollection.transform.Find("NameInput").gameObject.SetActive(false);
        nameInputCollection.transform.Find("CharacterSelect").gameObject.SetActive(false);
        OnSelect();
    }

    public void NameChange()
    {
        nameInputCollection.SetActive(true);
        nameInputCollection.transform.Find("Join").gameObject.SetActive(true);
        nameInputCollection.transform.Find("NameInput").gameObject.SetActive(true);
        nameInputCollection.transform.Find("CharacterSelect").gameObject.SetActive(false);
        nameInputCollection.transform.Find("NameInput").gameObject.GetComponent<InputField>().text = "";
        _player.GetComponent<PlayerInput>().enabled = false;
    }

    public void OnSelect()
    {
        _select.SetActive(true);
    }

    public void OnCharacter1()
    {
        _select.SetActive(false);
    }
    public void OnCharacter2()
    {
        _select.SetActive(false);
    }

    public void OnAttend()
    {
        _attendPepleWindow.SetActive(!_attendPepleWindow.active);
    }

    public void OnTalk()
    {
        _onChat = false;
        _taklUI.SetActive(false);
        _taklBox.SetActive(true);
    }

    public void OffTalk()
    {
        _onChat = true;
        _taklBox.SetActive(false);
    }
}
