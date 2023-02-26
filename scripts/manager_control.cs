using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public  class manager_control:MonoBehaviour
{
    public static manager_control instance;
    private void Awake()
    {
        if(instance==null){
            instance = this;
        }
    }
    public  bool ispass(int [] _answer){
        for (int i = 0; i < 81; i += 9)
        {
            string str;
            str = "";
            for (int j = i; j < i + 9; j++)
            {

                str += _answer[j];
            }
            Debug.Log("第" + i + "组" + str);
        }
        //判断每一行与列是否都是1-9且没有重复
        for (int i=0;i<81;i+=9){
            int cnt = 0; int[] x = new int[9];
            for (int j=i;j<i+9;j++){
                x[cnt] = _answer[j];
                ++cnt;
                if(Hang_pass(x)==false)
                {
                    return false;
                }
         }
    }
        Debug.Log("行通过");
    for(int i=0;i<9;i++){
            int cnt = 0; int[] x = new int[9];
            for (int j=i;j<=72+i;j+=9){
                x[cnt] = _answer[j];
                cnt++;
                if(Hang_pass(x)==false){
                    return false;
                }
        }
    }
        Debug.Log("列通过");
        for (int i=0;i<72;i+=27){
    for(int j=i;j<i+9;j+=3){
                int cnt = 0; int[] x = new int[9];
                for (int k=j;k<=j+18;k+=9){
                      for(int z=j;z<j+3;z++){
                        x[cnt] = _answer[z];
                        cnt++;
                      }
                }
                if(Hang_pass(x)==false){
                    return false;
                }
    }
    }
        Debug.Log("九宫格通过");
        return true;
    }
    public static bool Hang_pass(int [] H_answer){
        
       for(int i=0;i<H_answer.Length;i++){
            int min_num = H_answer[i];
            for (int j=i;j<H_answer.Length;j++){
       if(H_answer[j]>min_num){
                    min_num = H_answer[j];
       }
       }
            H_answer[i] = min_num;
       }
        for(int i=0;i<H_answer.Length;i++){
        if(H_answer[i]!=i+1){
                return false;
        }
        }
        return true;
    }
}
