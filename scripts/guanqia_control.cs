using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class guanqia_control : MonoBehaviour
{
    private GComponent mainui;
    [Header("��Ҫ���õ�����")]
    public int num;//��Ҫ�Ĺؿ�����Ҳ������Ҫ���õ�������
    [Header("���ַ�������ÿһ������Ҫ���õ�����(�ո����")]
    public string[] _numbers;//���ַ�������ÿһ������Ҫ���õ�����
    [Header("���ַ�������ÿһ���������ֵ�λ��(x,y)���ӿո�(�ո������")]
    public string[] num_location;//���ַ�������ÿһ���������ֵ�λ��

    private int current_level;//��ǰ���еĹؿ�
    
    private int[,] _input_num;//�洢ÿ���ؿ���Ҫ���õ����֡�
    //public int[] _num;//ÿ�����д��ڵ�����
    private int[,] _location_num;//�洢ÿ���ؿ��������ֵ�λ��
    //public int[] _location;//�洢��Ҫ����ؿ���λ��
    private GButton[] _button_num=new GButton[81];//�ؿ���ÿһ������
    [Header("��ť")]
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
       
        Get_data();//��ÿ���ؿ������ݽ��д洢
        Get_Box();//��ȡÿһ�����ӣ����洢��һλ�����У�
        _init();
        Set_inputfield();//����ǰ�ؿ�Ԥ���������ֵĸ��ӵ������ı����������
        Set_Boxnum(); //��Ԥ���������ֵĸ��ӽ��и�ֵ
        //��Ӱ�ť����
        _refush = mainui.GetChild("n95").asButton;
        _menu = mainui.GetChild("n94").asButton;
        _Submit = mainui.GetChild("n107").asButton;
        
        _refush.onClick.Add(() => { Reset_Play(); });
        _Submit.onClick.Add(() => { Get_inputAnswer();bool _pass = manager_control.instance.ispass(_input_Answer);
           // print_num();
        if(_pass==true){
                Debug.Log("��ϲͨ��");
        }
        if(_pass==false){
                Debug.Log("�ٽ�����");
        }
        });
        //Get_inputAnswer();//��ȡ����Ĵ�
    }
    //��ȡ��������
    private void Get_data(){
    //��ȡÿһ���ؿ����õ������Լ�ÿ�����ֵ�λ��
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

    //��ȡÿһ������
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
    //��Ԥ�����ùؿ����ӵ������ı�������
    private void Set_inputfield()
    {
        for (int i = 0; i < 81; i++)
        {
            if (_location_num[current_level, i] == 0) return;
            _button_num[_location_num[current_level, i]].GetChildAt(2).visible = false;
        }
    }
    //��Ԥ���������ֵĸ��ӽ��и�ֵ
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
    //����
    private void Reset_Play(){
        _init();
        Set_inputfield();
        Set_Boxnum();
    }
    //��ʼ��
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
    //��ȡÿ�������е�����
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
            Debug.Log("��"+i+"��"+str);
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
