using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_ParamSetter : MonoBehaviour
{
    public FMOD_Instantiator fmodEv;
    public FMOD_PARAM paramName;
    public float value = 0f;

    private void Start()
    {
        if (fmodEv == null) GetComponentInParent<FMOD_Instantiator>();
    }

    public void setParamAutoValue()
    {
        fmodEv.setParam(paramName.ToString(), value);
    }

    public void setParamWithValue(float _value)
    {
        fmodEv.setParam(paramName.ToString(), _value);
    }
}
