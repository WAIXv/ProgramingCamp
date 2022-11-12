using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
   
{
    
    public int i = 1;
    public Test Test1;
    public Test Test2;
    // Start is called before the first frame update
    void Start()
    {
        print(i);
        Text11(Test1,Test2,out i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Text11(Test Test1, Test Test2, out int i)
    {
        
        Test2 = Instantiate(Test1);
        i = 2;
        print(Test1.i);
        print(Test2.i);
    }
}
