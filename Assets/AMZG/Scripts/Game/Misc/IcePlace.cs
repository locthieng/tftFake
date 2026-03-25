using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IcePlace : MonoBehaviour
{
    [SerializeField] public List<GameObject> listIcePlace = new List<GameObject>();
    [SerializeField] private List<int> listNumberToShowIcelace;
    [SerializeField] private MainCharacterController character;
    private float speed = 15f;
    private float angle = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (character.CurrentState == CharacterState.Move)
        {
            //dung dua
            SwayIcePlace();
        }
        else
        {
            StopIcePlace();
        }
    }

    private void SwayIcePlace()
    {
        float rotY = Mathf.Sin(Time.time * speed) * angle;
        float rotZ = Mathf.Sin(Time.time * speed) * angle;
        transform.localRotation = Quaternion.Euler(0, rotY, rotZ);
    }  
    
    private void StopIcePlace()
    {
       transform.localRotation = Quaternion.identity;
    }    

    public void ShowIcePlace(int numberIce)
    {
        foreach (var ice in listIcePlace)
        {
            if (ice != null) ice.SetActive(false);
        }

        for (int i = 0; i < listNumberToShowIcelace.Count; i++)
        {
            if (numberIce > listNumberToShowIcelace[i])
            {
                if (listIcePlace[i] == null) return;
                listIcePlace[i].SetActive(true);
            }
        }

        if (numberIce > listNumberToShowIcelace[listNumberToShowIcelace.Count - 1])
        {
            return;
        }    
    }    

    public int CheckIcePlace(int numberIce)
    {
        if (numberIce > listNumberToShowIcelace[listNumberToShowIcelace.Count - 1])
        {
            return listNumberToShowIcelace.Count - 1;
        }

        for (int i = listNumberToShowIcelace.Count - 1; i >= 0; i--)
        {
            if (numberIce > listNumberToShowIcelace[i])
            {
                return i;
            }
        }

        return 0;
    }    
}
