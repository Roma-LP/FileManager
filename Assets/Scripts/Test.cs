using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        TestMethod2();   
    }

    private void TestMethod()
    {
        string dirName = "C:\\";
        // если папка существует
        if (Directory.Exists(dirName))
        {
            Debug.LogError("Подкаталоги:");
            string[] dirs = Directory.GetDirectories(dirName);
            foreach (string s in dirs)
            {
                Debug.LogError(s);
            }
            Console.WriteLine();
            Debug.LogError("Файлы:");
            string[] files = Directory.GetFiles(dirName);
            foreach (string s in files)
            {
                Debug.LogError(s);
            }
        }
    }

    private void TestMethod2()
    {
        string paths = "/storage/emulated/0/";
        Debug.LogError(Application.dataPath);
    }
}
