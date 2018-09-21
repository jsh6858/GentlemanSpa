using System;
using System.IO;
using UnityEngine;

//자잘하게 필요하지만 추가하기 애매한 데이터들을 위해 만듬
//사용시 번호와 사용 용도를 적어서 해석이 편하도록 작업 예정 (enum matching)

public class SaveData
{
    public static SaveData _Instance;
    public static SaveData Instance
    {
        get
        {
            if(null == _Instance)
                _Instance = new SaveData();

            return _Instance;
        }
    }

    BoxSaveData _boxSaveData;
    BoxSaveData boxSaveData
    {
        get
        {
            if(null == _boxSaveData)
            {
                _boxSaveData = new BoxSaveData();

                _boxSaveData.InitBoxSaveData();

                MemoryStream ms = new MemoryStream();
                BinaryReader br = new BinaryReader(ms);

                
            }
            return _boxSaveData;
        }
    }

    public void Save_Data()
    {
        const string fileName = "Assets/Resources/Data/SaveData.dat";

        BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.Create));

        bw.Write(10f);

        boxSaveData.DataWrite(bw);

        bw.Close();
    }

    public void Read_Data()
    {
        const string fileName = "Assets/Resources/Data/SaveData.dat";

        BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));

        boxSaveData.DataRead(br);

        br.Close();
    }
}

public class BoxSaveData
{
    public int[] intValues;
    // BOX_SAVE_DATA_INT_SORT
    public float[] floatValues;
    // BOX_SAVE_DATA_FLOAT_SORT
    public bool[] boolValues;
    // BOX_SAVE_DATA_BOOL_SORT

    int intValueCount = 10;
    int floatValueCount = 10;
    int boolValueCount = 10;

    public int ByteSizeInfo
    {
        get
        {
            return (sizeof(int) * intValueCount) + (sizeof(float) * floatValueCount) + (sizeof(bool) * boolValueCount);
        }
    }

    public BoxSaveData()
    {
        InitBoxSaveData();
    }

    public void InitBoxSaveData()
    {
        intValues = new int[intValueCount];

        for (int i = 0; i < intValueCount; i++)
        {
            intValues[i] = 0;
        }

        floatValues = new float[floatValueCount];

        for (int i = 0; i < floatValueCount; i++)
        {
            floatValues[i] = 0f;
        }

        boolValues = new bool[boolValueCount];

        for (int i = 0; i < boolValueCount; i++)
        {
            boolValues[i] = false;
        }
    }

    public void DataWrite(BinaryWriter bw)
    {
        for (int i = 0; i < intValueCount; i++)
        {
            bw.Write(intValues[i]);
        }

        for (int i = 0; i < floatValueCount; i++)
        {
            bw.Write(floatValues[i]);
        }

        for (int i = 0; i < boolValueCount; i++)
        {
            bw.Write(boolValues[i]);
        }
    }

    public void DataRead(BinaryReader br)
    {
        try
        {
            for (int i = 0; i < intValueCount; i++)
            {
                intValues[i] = br.ReadInt32();
            }

            for (int i = 0; i < floatValueCount; i++)
            {
                floatValues[i] = br.ReadInt64();
            }

            for (int i = 0; i < boolValueCount; i++)
            {
                boolValues[i] = br.ReadBoolean();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError(e.StackTrace);
            
            return;
        }
    }
}
