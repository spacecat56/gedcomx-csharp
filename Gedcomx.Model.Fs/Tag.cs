// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;

namespace Gx.Fs {

  /// <remarks>
  ///  A tag in the FamilySearch system.
  /// </remarks>
  /// <summary>
  ///  A tag in the FamilySearch system.
  /// </summary>
  [System.SerializableAttribute()]
  [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://familysearch.org/v1/",TypeName="Tag")]
  [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://familysearch.org/v1/",TypeName="Tag")]
  [System.Xml.Serialization.XmlRootAttribute(Namespace="http://familysearch.org/v1/",ElementName="tag")]
  public partial class Tag {

    private string _resource;
    /// <summary>
    ///  A reference to the value of the tag.
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute(AttributeName="resource")]
    [System.Xml.Serialization.SoapAttributeAttribute(AttributeName="resource")]
    public string Resource {
      get {
        return this._resource;
      }
      set {
        this._resource = value;
      }
    }
  }
}  
