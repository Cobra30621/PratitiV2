using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Fungus;

public class SaveSystem : IGameSystem
{
    
    private string SavePath => $"{Application.persistentDataPath}/save.text";
    public SaveSystem(GameMediator mediator):base(mediator)
	{
		Initialize();
    }


    public override void Initialize()
    {
        Load(); // 讀檔
    }

    public void Save(){
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
    }

    public void Load(){
        var state = LoadFile();
        RestoreState(state);
    }

    private Dictionary<string, object> LoadFile(){
        if(!File.Exists(SavePath)){
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open)){
            stream.Position = 0;
            var formatter = new BinaryFormatter();
            Dictionary<string, object> state = (Dictionary<string, object>)formatter.Deserialize(stream);
            stream.Close();
            return state;
        }
    }

    private void SaveFile(object state){
        using (FileStream stream = File.Open(SavePath, FileMode.Create)){
            stream.Position = 0;
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
            stream.Close();
        }
    }


    private void CaptureState(Dictionary<string, object> state){
        foreach (var saveable in GameObject.FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.ID] = saveable.CaptureState();
        }
    }

    private void RestoreState(Dictionary<string, object> state){
        foreach (var saveable in GameObject.FindObjectsOfType<SaveableEntity>())
        {
            if(state.TryGetValue(saveable.ID, out object value)){
                saveable.RestoreState(value);
            }
        }
    }
    
}