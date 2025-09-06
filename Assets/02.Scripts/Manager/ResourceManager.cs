using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GenericManagers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : SingleTon<ResourceManager>//ALL : (250707)�̱��� ����� �񵿱� �ε� ��ũ��Ʈ �����ϸ� ���� ĳ���صа� ����ϰ����� �˾Ƶθ� �۾��� ���� �� �� ���� �߰��մϴ�
{
    private Dictionary<string, object> preloaded;
    public Dictionary<string,object> GetPreLoad { get { return preloaded; } }
    //�޴��� �ν��Ͻ� ������ ����Ǵ� �Լ�
    protected override void Init()
    {
        base.Init();
        preloaded = new Dictionary<string, object>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">����Ÿ��</typeparam>
    /// <param name="key">Ÿ�� Ű��</param>
    /// <param name="callback">(obj)=>{targetInstance = obj}</param>
    public void LoadAsync<T>(string key, Action<T> callback, bool isCaching = false)
    {
        if (key.Contains(".sprite"))
        {
            key = $"{key}[{key.Replace(".sprite", "")}]";
        }
        AsyncOperationHandle<T> infoAsyncOP = Addressables.LoadAssetAsync<T>(key);
        infoAsyncOP.Completed += (op) =>
        {

            callback?.Invoke(infoAsyncOP.Result);
            if (isCaching) Addressables.Release(infoAsyncOP);
        };
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">����Ÿ��</typeparam>
    /// <param name="label">Ÿ�� Ű��</param>
    /// <param name="callback">(obj)=>{targetInstance = obj}</param>
    public void LoadAsyncAll<T>(string label, Action<(string, T)[]> callback, bool isCaching = true)
    {
        var labelKeys = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        //label�� TŸ���� ������Ʈ���� Ű���� �����´�
        labelKeys.WaitForCompletion();
        //resource�� ���� load�Ҷ����� ���

        Debug.Log(labelKeys.Result);
        if (labelKeys.Result.Count == 0) { Debug.LogError($"{label}���� ����ֽ��ϴ�."); callback.Invoke(null); }//�ش��ϴ� Ű�� ������� null�� ����

        int doneCount = 0;

        (string, T)[] tempT = new (string, T)[labelKeys.Result.Count];
        for (int i = 0; i < tempT.Length; i++)
        {
            int curIndex = i;
            string curKey = labelKeys.Result[i].PrimaryKey; //�ݹ��� ���ÿ� ����� Ŭ���� �̽��� ���� �� �ֱ⿡ �и�(�ٸ� ������ ���ø޸𸮸� �����ϴ� ����)
            LoadAsync<T>(labelKeys.Result[i].PrimaryKey, (result) =>
            {
                tempT[curIndex].Item1 = curKey;
                tempT[curIndex].Item2 = result;
                doneCount++;
                if (doneCount == labelKeys.Result.Count)
                {
                    callback?.Invoke(tempT);
                }
            }, isCaching);
        }
    }    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">����� Ÿ��</typeparam>
    /// <param name="label">��� �� �̸�</param>
    /// <param name="callback"> �ٽ�</param>
    public void PreLoadAsyncAll(string label, Action<int, int> callback)
    {
        var oper = Addressables.LoadResourceLocationsAsync(label, typeof(object));
        //label�� TŸ���� ������Ʈ���� Ű���� �����´�
        oper.WaitForCompletion();
        //resource�� ���� load�Ҷ����� ���
        if (oper.Result.Count == 0) { Debug.LogError($"{label}���� ����ֽ��ϴ�."); callback.Invoke(0,0); }//�ش��ϴ� Ű�� ������� null�� ����

        int curr = 1;
        for (int i = 0; i < oper.Result.Count; i++)
        {
            string key = oper.Result[i].PrimaryKey;//Ŭ�����̽� ����
            int max = oper.Result.Count;

            if (preloaded.ContainsKey(key))
            {
                callback?.Invoke(max, curr);
                curr++;
                continue;
            }

            LoadAsync<object>(key, (result) =>
            {
                callback?.Invoke(max, curr);
                curr++;
                if (result == null)
                {
                    Debug.Log("Ÿ���� �ùٸ��� ����");
                }
                else
                {
                    Debug.Log($"{key}프리로드 성공");

                    preloaded.Add(key, result);
                }
            },false);
        }
    }
    #region Json
    private string GetSavePath() => Application.persistentDataPath;

    public bool SaveData<T>(T data, string fileName, bool isOverride)
    {
        string path = Path.Combine(GetSavePath(), fileName);
        string jsonStr = JsonUtility.ToJson(data);
        Debug.Log(path);
        if (File.Exists(path))
        {
            if (isOverride == true)
            {
                File.WriteAllText(path, jsonStr);
                return true;
            }
        }
        else
        {
            if (!File.Exists(GetSavePath()))
            {
                Directory.CreateDirectory(GetSavePath());
            }
            StreamWriter file = File.CreateText(path);
            file.WriteLine(jsonStr);
            return true;
        }
        return false;
    }
    public T LoadData<T>(string fileName) where T : new()
    {
        string path = Path.Combine(GetSavePath(), fileName);

        if (File.Exists(path))
        {
            T result;
            try
            {
                result = JsonUtility.FromJson<T>(File.ReadAllText(path));
            }
            catch
            {
                //�Ľ� ���н�
                result = default(T);
            }
            return result;
        }
        else
        {
            Directory.CreateDirectory(GetSavePath());
            return default(T);
        }
    }
    #endregion
}
