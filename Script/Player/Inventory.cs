using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    List<string> InventoryInfo;
    public GameObject Inven;
    bool[] Slot = new bool[6];
    public Image[] SlotImgs;

    private void Awake()
    {
        ReadDate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Inven.activeSelf == true)
                Inven.SetActive(false);
            else
            {
                Inven.SetActive(true);
                DisplayInven();
            }
        }
    }

    void DisplayInven() 
    {
        for (int i = 0; i < Slot.Length; i++)
        {
            if (Slot[i] == true)
            {
                string name = GetNameBySlot(i);
                Item item = ItemData.Instance.GetItemInfoByName(name);
                if (item != null)
                {
                    SlotImgs[i].sprite = item.img;
                }
            }
        }
    }

    void ReadDate() 
    {
        InventoryInfo = File.ReadAllLines("Assets/Datas/Inventory.txt").ToList();
        for (int i = 0; i < InventoryInfo.Count; i++)
        {
            int num = int.Parse(InventoryInfo[i].Split(',')[2]);
            Slot[num] = true;
        }
    }

    public void AddItem(string name, int count) 
    {
        int num = FindByName(name);
        if (count == 0)
            return;
        //있다면 수량추가
        if (num >= 0)
        { 
            string[] info = InventoryInfo[num].Split(',');
            int newcount = int.Parse(info[1]) + count;
            if (newcount <= 0)
            { 
                RemoveLine(num);
                return;
            }
            string newinfo = info[0]+ "," + newcount + "," + info[2];
            SetInfo(num, newinfo);
        }
        //없다면 새로 추가
        else 
        {
            if (count < 0)
                return;
            if (GetEmptySlot() != -1)
                return;
            int slot = GetEmptySlot();
            Slot[slot] = true;
            string newinfo = name + "," + count + "," + slot;
            SetInfo(num, newinfo);
        }
    }

    public int GetItemCount(string name) 
    {
        int num = FindByName(name);
        if (num < 0)
            return 0;

        return int.Parse(InventoryInfo[num].Split(',')[1]);
    }

    public int GetEmptySlot()
    {
        for (int i = 0; i < Slot.Length; i++) 
        {
            if (Slot[i] == false)
                return i;
        }
        return -1;
    }

    int FindByName(string name) 
    {
        for (int i = 0; i < InventoryInfo.Count; i++)
        {
            string[] data = InventoryInfo[i].Split(',');
            if (data[0] == name)
                return i;
        }
        return -1;
    }

    string GetNameBySlot(int num)
    {
        for (int i = 0; i < InventoryInfo.Count; i++)
        {
            string[] data = InventoryInfo[i].Split(',');
            int slotnum = int.Parse(data[2]);
            if (slotnum == num)
                return data[0];
        }
        return null;
    }

    void SetInfo(int num, string info)
    {
        if (num < 0)
            InventoryInfo.Add(info);
        else
            InventoryInfo[num] = info;
        File.WriteAllLines("Assets/Datas/Inventory",InventoryInfo);
    }

    void RemoveLine(int num)
    {
        InventoryInfo.RemoveAt(num);
        File.WriteAllLines("Assets/Datas/Inventory", InventoryInfo);
    }

}
