using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.XML.Serialization;
using System.IO;

namespace BinaryTreeCreator
{
  class SaveFileManager
  {
    //Attributes
    string Path;

    //Constructor
    public SaveFileManager(string Path)
    {
      this.Path = Path;
    }
    public BinaryNode loadFileAtPath()
    {
      try
      {
        XMLSerializer serializer = new XMLSerializer(typeof(BinaryNode));
        FileStream fs = new FileStream(Path, FileMode.Open);
        BinaryNode returnValue = serializer.Deserialize(fs) as BinaryNode;
        fs.Close();
        return returnValue;
      }
      catch
      {
        return null;
      }
    }
    public bool SaveFileAtPath(BinaryNode saveValue)
    {
      try
      {
        XMLSerializer serializer = new XMLSerializer(typeof(BinaryNode));
        FileStream fs = new FileStream(Path, FileMode.Create);
        serializer.Serialize(fs,saveValue);
        fs.Close();
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
