using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ProfileIndex
{
    // Start is called before the first frame update

    public List<string> profileFileNames;

    public ProfileIndex()
    {
        profileFileNames = new List<string>();
    }
}
