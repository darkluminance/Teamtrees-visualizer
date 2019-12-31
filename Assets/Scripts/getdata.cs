using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using  System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

public class getdata : MonoBehaviour
{
    public Text treecount;
    private string text;
    Process p = new Process();
    private int initree, currenttree;
    public GameObject trees, newtree;
    // Start is called before the first frame update
    void Start()
    {
        p.StartInfo.UseShellExecute = true;
        p.StartInfo.FileName = "teamtree.exe";
        p.Start();
        
        text = System.IO.File.ReadAllText("value.txt");
        treecount.text = text;
        initree = int.Parse(text);
        for (int i = 1; i <= initree / 10000; i++)
        {
            maketree();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        text = System.IO.File.ReadAllText("value.txt");
        treecount.text = "$ " + text;
        currenttree = int.Parse(text);

        if ((currenttree / 10000) - (initree / 10000) > 0)
        {
            maketree();
            StartCoroutine("waitt");
        }

        initree = currenttree;
        //Debug.Log(int.Parse(text));

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Process pa in Process.GetProcessesByName("teamtree")) {
                pa.CloseMainWindow();
            }
        
            p.CloseMainWindow();
        
            p.Close();
            
            Application.Quit();
        }
    }

    private void OnApplicationQuit()
    {
        foreach (Process pa in Process.GetProcessesByName("teamtree")) {
            pa.CloseMainWindow();
        }
    }

    private void maketree()
    {
        Vector3 pos = Random.onUnitSphere * .63f;
        Instantiate(trees, pos, Quaternion.identity);
    }

    IEnumerator waitt()
    {
        
        newtree.SetActive(true);
        yield return new WaitForSeconds(3);
        newtree.SetActive(false);
    }
}
