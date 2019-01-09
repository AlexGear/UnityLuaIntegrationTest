using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NLua;

public class LuaScript : MonoBehaviour
{
    [SerializeField, TextArea(20, 40)] string code = "";

    private string oldCode;
    public string error { get; private set; }

    private Lua lua;

    private void Awake() {
        oldCode = code;
        lua = new Lua();
        lua.LoadCLRPackage();
        lua["transform"] = transform;
        DoString();
    }

    private void DoString() {
        try {
            lua.DoString(code);
            error = null;
        }
        catch(NLua.Exceptions.LuaException e) {
            error = FormatException(e);
        }
    }

    void Update() {
        if(oldCode != code) {
            oldCode = code;
            DoString();
        }
        CallLuaUpdate();
    }

    private void CallLuaUpdate() {
        LuaFunction func = lua.GetFunction("Update");
        if(func == null)
            return;
        try {
            func.Call();
        }
        catch(NLua.Exceptions.LuaException e) {
            Debug.LogError(FormatException(e), gameObject);
            throw e;
        }
    }

    public static string FormatException(NLua.Exceptions.LuaException e) {
        string source = (string.IsNullOrEmpty(e.Source)) ? "<no source>" : e.Source.Substring(0, e.Source.Length - 2);
        return string.Format("{0}\nLua (at {2})", e.Message, string.Empty, source);
    }
}
