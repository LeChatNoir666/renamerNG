using System;
using System.Collections;

namespace RenamerNG.Macros
{
  /// <summary>
  /// Container for macro information
  /// </summary>
  [Serializable()]
  public class MacroFile :IEnumerable
  {
    const int version = 2;
    int fileVersion = version;
    ArrayList macros = new ArrayList();

    public int FileVersion
    {
      get { return fileVersion; }
    }

    public int Version
    {
      get { return version; }
    }

    public void Add(Macro m)
    {
      macros.Add(m);
    }

    public Macro this[int index]
    {
      get { return (Macro)macros[index]; }
    }

    public int Count
    {
      get { return macros.Count; }
    }

    #region IEnumerable Members

    public IEnumerator GetEnumerator()
    {
      return macros.GetEnumerator();
    }

    #endregion
  }
}
