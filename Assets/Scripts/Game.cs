//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _fieldQuestion;
    [SerializeField] private GameObject _fieldAnswerVar1;
    [SerializeField] private GameObject _fieldAnswerVar2;
    [SerializeField] private GameObject _fieldAnswerVar3;
    [SerializeField] private GameObject _fieldAnswerVar4;
    [SerializeField] private GameObject _fieldAnswerVar5;
    [SerializeField] private GameObject _fieldAnswerVar6;
    [SerializeField] private GameObject _fieldAnswerVar7;
    [SerializeField] private GameObject _fieldAnswerVar8;

    [SerializeField] private GameObject _autoChange;
    [SerializeField] private GameObject _fieldQuantity;

    [SerializeField] private TMP_Text _ID;
    [SerializeField] private TMP_Text _rightAnswers;

    private TMP_Text _textWord;
    private TMP_Text _textQuantity;
    private Button _buttonWord;

    public Toggle _toggle;
    public bool _isAutoMode;

    public event UnityAction EventDoResetColor;
    public event UnityAction EventDoPrint;

    private List<int> _poolIDs = new List<int>();
    private List<int> _poolIDsConst = new List<int>();//copy _poolIDs
    public List<int> _poolRightAnswers = new List<int>();
    private string[] _wordsEn = new string[] { "abide abode abided", "arise arose arisen", "awake awoke awoken", "be was, were been", "bear bore borne", "beat beat beaten", "become became become", "begin began begun", "behold beheld beheld", "bend bent bent", "bereave bereft bereft", "beseech besought besought", "beset beset beset", "bet bet bet", "bid bade bidden", "bind bound bound", "bite bit bitten", "bleed bled bled", "blow blew blown", "break broke broken", "breed bred bred", "bring brought brought", "broadcast broadcast broadcast", "build built built", "burn burnt burnt", "burst burst burst", "bust bust bust", "buy bought bought", "cast cast cast", "catch caught caught", "choose chose chosen", "cleave cleft cleft", "cling clung clung", "clothe clad clad", "come came come", "cost cost cost", "creep crept crept", "cut cut cut", "deal dealt dealt", "dig dug dug", "disprove disproved disproven", "dive dove dived", "do did done", "draw drew drawn", "dream dreamt dreamt", "drink drank drunk", "drive drove driven", "dwell dwelt dwelt", "eat ate eaten", "fall fell fallen", "feed fed fed", "feel felt felt", "fight fought fought", "find found found", "fit fit fit", "flee fled fled", "fling flung flung", "fly flew flown", "forbid forbade forbidden", "forgo forewent foregone", "forecast forecast forecast", "foresee foresaw foreseen", "foretell foretold foretold", "forget forgot forgotten", "forgive forgave forgiven", "forsake forsook forsaken", "freeze froze frozen", "get got got", "gild gilt gilt", "give gave given", "go went gone", "grind ground ground", "grow grew grown", "hang hung hung", "have had had", "hear heard heard", "heave heaved heaved", "hew hewed hewed", "hide hid hidden", "hit hit hit", "hold held held", "hurt hurt hurt", "inlay inlaid inlaid", "input input input", "interweave interwove interwoven", "keep kept kept", "kneel knelt knelt", "knit knit knit", "know knew known", "lay laid laid", "lead led led", "lean leant leant", "leap leapt leapt", "learn learnt learnt", "leave left left", "lend lent lent", "let let let", "lie lay lain", "lie (regular verb) lied lied", "light lit lit", "lose lost lost", "make made made", "mean meant meant", "meet met met", "mistake mistook mistaken", "mow mowed mown", "offset offset offset", "output output output", "overcome overcame overcome", "pay paid paid", "plead pled pled", "preset preset preset", "prove proved proved", "put put put", "quit quit quit", "read read read", "relay relaid relaid", "relay (regular verb) relayed relayed", "rend rent rent", "rid rid rid", "ride rode ridden", "ring rang rung", "rise rose risen", "run ran run", "saw sawed sawn", "say said said", "see saw seen", "seek sought sought", "sell sold sold", "send sent sent", "set set set", "sew sewed sewn", "shake shook shaken", "shave shaved shaved", "shear sheared sheared", "shed shed shed", "shine shone shone", "shit shat shat", "shoe shod shod", "shoot shot shot", "show showed shown", "shrink shrank shrunk", "shut shut shut", "sing sang sung", "sink sank sunk", "sit sat sat", "slay slew slain", "slay (regular verb) slayed slayed", "sleep slept slept", "slide slid slid", "sling slung slung", "slink slunk slunk", "slit slit slit", "smell smelt smelt", "smite smote smitten", "sow sowed sown", "speak spoke spoken", "speed sped sped", "spell spelt spelt", "spend spent spent", "spill spilt spilt", "spin spun spun", "spit spat spat", "split split split", "spoil spoilt spoilt", "spread spread spread", "spring sprang sprung", "stand stood stood", "steal stole stolen", "stick stuck stuck", "sting stung stung", "stink stank stunk", "strew strewed strewn", "stride strode stridden", "strike struck struck", "string strung strung", "strive strove striven", "swear swore sworn", "sweat sweat sweat", "sweep swept swept", "swell swelled swollen", "swim swam swum", "swing swung swung", "take took taken", "teach taught taught", "tear tore torn", "tell told told", "think thought thought", "throw threw thrown", "thrust thrust thrust", "tread trod trodden", "typeset typeset typeset", "undergo underwent undergone", "understand understood understood", "undertake undertook undertaken", "undo undid undone", "upset upset upset", "wake woke woken", "wear wore worn", "weave wove woven", "wed wed wed", "weep wept wept", "wet wet wet", "win won won", "wind wound wound", "withdraw withdrew withdrawn", "withhold withheld withheld", "withstand withstood withstood", "wring wrung wrung", "write wrote written" };
    private string[] _wordsRu = new string[] { "�������, ���������", "���������, ����������", "�����������", "����", "������, ��������", "����", "�����������", "��������(��)", "������, ��������", "�����(��)", "������", "�������, ����������", "��������", "������� ����", "������, �������", "���������", "������", "�����������", "����", "������", "��������� (��������)", "��������", "���������� � �����", "�������", "����, ������", "����������", "��������(��)", "������", "�������, ������", "������", "��������", "�������", "�������(��)", "�����(��)", "���������", "������", "�������", "������", "����� ����", "������", "�����������", "������, �����������", "������", "��������, ������", "������ ���, �������", "����", "������", "�������, �������������", "���� (����)", "������", "�������", "�����������", "��������", "��������", "���������, ��������� (�� ������)", "������, ��������� ��������", "�������", "������", "���������", "��������������, �������������� (�� ����-����)", "�������������, ��������������", "����������", "�������������", "��������", "�������", "��������", "���������", "��������, �����������, ����������", "��������", "������", "����, �����", "������, ������", "�����, ����������", "������, ������", "�����", "�������", "���������, �������� ", "������, ������", "�������(��)", "�������", "������� (����)", "������, �������", "����������, ��������������", "������� (������)", "��������", "������� (����)", "����������� �� ������", "������", "�����", "������", "�����", "���������", "�������", "�����(��)", "���������, �������", "������ ������", "���������", "������", "�����", "��������, ��������", "������", "������, ���������", "��������", "���������", "���������", "������ (�����)", "��������������", "��������", "������������", "�������", "��������", "�����������������, ���������", "����������", "������", "����������, �����������", "������", "������������� (������ � �.�.)", "���������� (������ � �.�.)", "���������, ���������", "���������", "������ ������", "�������", "�����������", "������", "������", "�������", "������", "������", "���������", "��������", "�������������", "����", "������", "�����(��)", "������", "��������� (����)", "�������, �����", "������", "�������, �����������", "��������", "����������", "���������", "���������", "����", "�����������, ������", "������", "�������", "���������, ��������", "�����", "���������", "�������, �����������", "��������", "��������� (��������), ����������", "�������, ������", "�������, ���������", "�����", "�������������", "�������", "����������� �� ������", "�������, ��������� (�����)", "���������", "������, �������", "�������", "����������(��)", "�������", "��������������(��)", "���������, �����������", "������", "������", "�������, �������", "������", "������", "��������", "������", "�������", "����������, ����������", "����������, ���������", "��������", "������", "�����", "���������, ����������", "�������", "�����������(��)", "�����", "�������", "�����", "��������, ������������", "������", "�������", "�������", "�������", "��������, ��������", "���������, ������������", "��������", "�������������", "���������� ���������", "��������", "������", "������", "�����", "�������� � ����", "�������", "������", "����������, ���������", "������(��)", "��������, ��������, ������������", "��������������", "�������������", "�����������", "������" };
    //private string[] _wordsEn = new string[] { "1e", "2e", "3e", "4e", "5e", "6e", "7e", "8e", "9e", "10e", "11e" };
    //private string[] _wordsRu = new string[] { "1r", "2r", "3r", "4r", "5r", "6r", "7r", "8r", "9r", "10r", "11r" };

    public int _wordID;
    public int _posWordInPoolIDs;
    public string _word;

    public int[] _answersID = new int[9];
    public string[] _answersWord = new string[9];

    private void Awake()
    {
        _textWord = _fieldQuestion.GetComponent<TMP_Text>();
        _buttonWord = _fieldQuestion.GetComponent<Button>();
        _textQuantity = _fieldQuantity.GetComponent<TMP_Text>();
        _toggle = _autoChange.GetComponentInChildren<Toggle>();
    }

    private void Start()
    {
        for (int i = 0; i < _wordsEn.Length; i++)
        {
            _poolIDs.Add(i);
            _poolIDsConst.Add(i);
            _poolRightAnswers.Add(2);//���������� ������ �������
        }

        FillingWord();
        FillingAnswers();
        RandomisationAnswers();
        Printing();
    }

    public void GameBody()
    {
        if (_poolRightAnswers[_wordID] <= 0)
            _poolIDs.RemoveAt(_posWordInPoolIDs);

        EventDoResetColor?.Invoke();
        FillingWord();
        FillingAnswers();
        RandomisationAnswers();
        Printing();
    }

    private void FillingWord()
    {
        _posWordInPoolIDs = Random.Range(0, _poolIDs.Count);
        _wordID = _poolIDs[_posWordInPoolIDs];
        _word = _wordsEn[_wordID];
    }

    private void FillingAnswers()
    {
        _answersID[0] = _wordID;
        _answersWord[0] = _wordsRu[_wordID];

        List<int> poolIDsAnswerCopy = new List<int>(_poolIDsConst);

        poolIDsAnswerCopy.RemoveAt(_wordID);//��� ������ ������� _actWordID==_posActWordInPoolIDs
        int posInArray = Random.Range(0, poolIDsAnswerCopy.Count);
        _answersID[1] = poolIDsAnswerCopy[posInArray];
        _answersWord[1] = _wordsRu[_answersID[1]];

        poolIDsAnswerCopy.RemoveAt(posInArray);
        posInArray = Random.Range(0, poolIDsAnswerCopy.Count);
        _answersID[2] = poolIDsAnswerCopy[posInArray];
        _answersWord[2] = _wordsRu[_answersID[2]];

        poolIDsAnswerCopy.RemoveAt(posInArray);
        posInArray = Random.Range(0, poolIDsAnswerCopy.Count);
        _answersID[3] = poolIDsAnswerCopy[posInArray];
        _answersWord[3] = _wordsRu[_answersID[3]];

        poolIDsAnswerCopy.RemoveAt(posInArray);
        posInArray = Random.Range(0, poolIDsAnswerCopy.Count);
        _answersID[4] = poolIDsAnswerCopy[posInArray];
        _answersWord[4] = _wordsRu[_answersID[4]];

        poolIDsAnswerCopy.RemoveAt(posInArray);
        posInArray = Random.Range(0, poolIDsAnswerCopy.Count);
        _answersID[5] = poolIDsAnswerCopy[posInArray];
        _answersWord[5] = _wordsRu[_answersID[5]];

        poolIDsAnswerCopy.RemoveAt(posInArray);
        posInArray = Random.Range(0, poolIDsAnswerCopy.Count);
        _answersID[6] = poolIDsAnswerCopy[posInArray];
        _answersWord[6] = _wordsRu[_answersID[6]];

        poolIDsAnswerCopy.RemoveAt(posInArray);
        posInArray = Random.Range(0, poolIDsAnswerCopy.Count);
        _answersID[7] = poolIDsAnswerCopy[posInArray];
        _answersWord[7] = _wordsRu[_answersID[7]];

        poolIDsAnswerCopy.RemoveAt(posInArray);
        posInArray = Random.Range(0, poolIDsAnswerCopy.Count);
        _answersID[8] = poolIDsAnswerCopy[posInArray];
        _answersWord[8] = _wordsRu[_answersID[8]];
    }

    private void RandomisationAnswers()
    {
        int pos = Random.Range(1, _answersID.Length);

        _answersID[pos] = _answersID[0];
        _answersWord[pos] = _answersWord[0];
    }

    public void Printing()
    {
        EventDoPrint?.Invoke();

        _textQuantity.text = _poolIDs.Count.ToString();

        //   _ID.text = _wordID.ToString();
        //   _rightAnswers.text = _poolRightAnswers[_wordID].ToString();
    }

    public void PrintingRightAnswers()
    {
        _ID.text = _word;
        _rightAnswers.text = _wordsRu[_wordID];
    }


    private void Update()
    {
        _isAutoMode = _toggle.isOn;
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnChangeMode);
    }

    private void OnChangeMode(bool isOn)
    {
        _isAutoMode = isOn;

        ColorBlock cb = _toggle.colors;

        if (isOn)
        {
            cb.normalColor = Color.green;
            cb.highlightedColor = Color.green;
        }
        else
        {
            cb.normalColor = Color.white;
            cb.highlightedColor = Color.white;
        }

        _toggle.colors = cb;
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnChangeMode);
    }

}
