using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class guanqia_control : MonoBehaviour
{
    private GComponent mainui;
    [Header("需要设置的组数")]
    public int num;//需要的关卡数，也就是需要设置的组数。
    [Header("按字符串输入每一关中需要设置的数字(空格隔开")]
    public string[] _numbers;//按字符串输入每一关中需要设置的数字
    [Header("按字符串输入每一关输入数字的位置(x,y)最后加空格(空格隔开）")]
    public string[] num_location;//按字符串输入每一关输入数字的位置

    private int current_level;//当前进行的关卡
    
    private int[,] _input_num;//存储每个关卡需要设置的数字。
    //public int[] _num;//每个组中存在的数字
    private int[,] _location_num;//存储每个关卡设置数字的位置
    //public int[] _location;//存储需要输入关卡的位置
    private GButton[] _button_num=new GButton[81];//关卡中每一个格子
    [Header("按钮")]
    private GButton _refush;
    private GButton _menu;
    private GButton _Submit;

    private int[] _input_Answer = new int[81];
    public int CurrentLevel{
    get{
            return current_level;
    }
    set{
            if (value < 0) return;
            if (value > num - 1) return;
            current_level = value;
    }
    }
    public int[] InputAnswer{
    get{
            return _input_Answer;
    }
    }

    private void Awake()
    {
        if (num < 0) return;
        _input_num = new int[num,81];
        _location_num = new int[num, 81];
        current_level = 0;
        
        
    }
    void Start()
    {
        UIPanel panel = this.GetComponent<UIPanel>();
        mainui = panel.ui;
       
        Get_data();//将每个关卡的数据进行存储
        Get_Box();//获取每一个格子，并存储到一位数组中；
        _init();
        Set_inputfield();//将当前关卡预先设置数字的格子的输入文本框进行隐藏
        Set_Boxnum(); //将预先设置数字的格子进行赋值
        //添加按钮功能
        _refush = mainui.GetChild("n95").asButton;
        _menu = mainui.GetChild("n94").asButton;
        _Submit = mainui.GetChild("n107").asButton;
        
        _refush.onClick.Add(() => { Reset_Play(); });
        _Submit.onClick.Add(() => { Get_inputAnswer();bool _pass = manager_control.instance.ispass(_input_Answer);
           // print_num();
        if(_pass==true){
                Debug.Log("恭喜通关");
        }
        if(_pass==false){
                Debug.Log("再接再厉");
        }
        });
        //Get_inputAnswer();//获取输入的答案
    }
    //获取输入数据
    private void Get_data(){
    //获取每一个关卡设置的数字以及每个数字的位置
    for(int i=0;i<num;i++){
            int x = 0;
            int cnt = 0;
            //Debug.Log(_numbers.Length);
    for(int j=0;j<_numbers[i].Length;j++){
    if(_numbers[i][j]>='0'&&_numbers[i][j]<='9'){
            _input_num[i,x] = (int)_numbers[i][j]-'0';
                    x++;
    }
    }

    for(int j=0;j<num_location[i].Length;j+=4){
                int y = (int)num_location[i][j]-'0';
                int k = (int)num_location[i][j + 2]-'0';
                _location_num[i, cnt] = 9 * y + k;
                cnt++;
    }
    }
    }

    //获取每一个格子
    private void Get_Box(){
        int count = 0;
        for (int i = 2; i <= 90; i += 10)
        {
            for (int j = i; j <= i + 8; j++)
            {

                _button_num[count] = mainui.GetChild("n" + j).asButton;
                count++;
            }
        }
    }
    //将预先设置关卡格子的输入文本框隐藏
    private void Set_inputfield()
    {
        for (int i = 0; i < 81; i++)
        {
            if (_location_num[current_level, i] == 0) return;
            _button_num[_location_num[current_level, i]].GetChildAt(2).visible = false;
        }
    }
    //将预先设置数字的格子进行赋值
    private void Set_Boxnum(){
     for(int i=0;i<81;i++){
            if (_location_num[current_level, i] == 0) return;
            int x = _location_num[current_level, i];
            int y = _input_num[current_level, i];
            //Debug.Log(x);
           GTextField t= _button_num[x].GetChildAt(1).asTextField;
            t.color = Color.black;
            t.text = "" + y;
     }
    }
    //重玩
    private void Reset_Play(){
        _init();
        Set_inputfield();
        Set_Boxnum();
    }
    //初始化
    private void _init()
    {
        for(int i=0;i<_button_num.Length;i++){
            GTextField t = _button_num[i].GetChildAt(1).asTextField;
            GTextInput t1 = _button_num[i].GetChildAt(2).asTextInput;
            t.text = "";
            t1.text = "";
            t.visible = true;
            t1.visible = true;
        }
    }
    //获取每个格子中的数字
    private void Get_inputAnswer(){
    for(int i=0;i<81;i++){
            GTextField t = _button_num[i].GetChildAt(1).asTextField;
            GTextInput t1 = _button_num[i].GetChildAt(2).asTextInput;
            if(t.text!=""||t1.text!=""){
            if(t.text!=""){
                    _input_Answer[i] = (int)t.text[0] - '0';
                    //Debug.Log(t.text[0]);
                    
            }
            if(t1.text!=""){
                    _input_Answer[i] = (int)t1.text[0] - '0';
                    //Debug.Log(t1.text[0]);
                }
               // Debug.Log(_input_Answer[i]);
            }
            if(t.text == "" && t1.text==""){
                _input_Answer[i] = 0;
               // Debug.Log(_input_Answer[i]);
            }
            
        }
        Debug.Log(_input_Answer[10]);
    }
    private void print_num(){
        
        for (int i=0;i<81;i+=9){
            string str;
            str = "";
            for (int j=i;j<i+9;j++){
                
                str +=_input_Answer[j];
    }
            Debug.Log("第"+i+"组"+str);
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
