using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MIKEMap : MonoBehaviour
{
    public static MIKEMap Main { get; private set; }

    private char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

    [SerializeField] private Transform mapStart, mapEnd, ignore;
    [Space]
    [SerializeField] private LayerMask mapLayer;

    void Awake()
    {
        //Debug.Log(GetPositionFromCode("V7"));
        if (Main == null)
            Main = this;
        else
            Destroy(this);
    }

    public Vector3 GetPositionFromCode(string code)
    {
        if (code.Length == 2)
        {

            char letter = code[0];
            int numberIndex = int.Parse(code[1].ToString());

            int letterIndex = 0;

            for (int i = 0; i < alphabet.Length; i++)
            {
                if (alphabet[i].ToString().Equals(letter.ToString().ToLower()))
                {
                    letterIndex = i;
                }
            }

            float x = Mathf.Lerp(mapStart.localPosition.x, mapEnd.localPosition.x, (float)numberIndex / 27f);
            float z = Mathf.Lerp(mapStart.localPosition.z, mapEnd.localPosition.z, (float)letterIndex / 25f);

            ignore.transform.localPosition = new Vector3(x, mapStart.position.y, z);
            return ignore.transform.position;

        }
        else
        {
            Debug.LogWarning("Code length must be size 2, of the format <Letter><Number>");
        }

        return Vector3.zero;
    }

    public Vector2 NormalizePosition(Vector3 localPosition)
    {
        float x = Mathf.InverseLerp(mapStart.localPosition.x, mapEnd.localPosition.x, localPosition.x);
        float z = Mathf.InverseLerp(mapStart.localPosition.z, mapEnd.localPosition.z, localPosition.z);

        return new Vector2(x, z);
    }

    public Vector3 GetPositionFromNormalized(Vector2 normalizedPosition)
    {
        float x = Mathf.Lerp(mapStart.localPosition.x, mapEnd.localPosition.x, normalizedPosition.x);
        float y = mapStart.position.y;
        float z = Mathf.Lerp(mapStart.localPosition.z, mapEnd.localPosition.z, normalizedPosition.y);

        if (Physics.Raycast(new Vector3(x, mapStart.position.y + 10, z), Vector3.down, out RaycastHit hit, 100, mapLayer))
        {
            y = hit.point.y;
        }

        ignore.transform.localPosition = new Vector3(x, y, z);
        return ignore.transform.position;
    }

}
