// <auto-generated>
// 
//
// Generated by <a href="http://enunciate.codehaus.org">Enunciate</a>.
// </auto-generated>
using System;

namespace Gx.Fs.Tree {

  /// <remarks>
  ///  Enumeration of the possible change object modifiers.
  /// </remarks>
  /// <summary>
  ///  Enumeration of the possible change object modifiers.
  /// </summary>
  public enum ChangeObjectModifier {

    /// <summary>
    ///  Unspecified enum value.
    /// </summary>
    [System.Xml.Serialization.XmlEnumAttribute(Name="__NULL__")]
    [System.Xml.Serialization.SoapEnumAttribute(Name="__NULL__")]
    NULL,

    /// <summary>
    ///   The person.
    /// </summary>
    Person,

    /// <summary>
    ///   The couple.
    /// </summary>
    Couple,

    /// <summary>
    ///   The child-and-parents relationship.
    /// </summary>
    ChildAndParentsRelationship
  }

  /// <remarks>
  /// Utility class for converting to/from the QNames associated with ChangeObjectModifier.
  /// </remarks>
  /// <summary>
  /// Utility class for converting to/from the QNames associated with ChangeObjectModifier.
  /// </summary>
  public static class ChangeObjectModifierQNameUtil {

    /// <summary>
    /// Get the known ChangeObjectModifier for a given QName. If the QName isn't a known QName, ChangeObjectModifier.NULL will be returned.
    /// </summary>
    public static ChangeObjectModifier ConvertFromKnownQName(string qname) {
      if (qname != null) {
        if ("http://gedcomx.org/Person".Equals(qname)) {
          return ChangeObjectModifier.Person;
        }
        if ("http://gedcomx.org/Couple".Equals(qname)) {
          return ChangeObjectModifier.Couple;
        }
        if ("http://familysearch.org/v1/ChildAndParentsRelationship".Equals(qname)) {
          return ChangeObjectModifier.ChildAndParentsRelationship;
        }
      }
      return ChangeObjectModifier.NULL;
    }

    /// <summary>
    /// Convert the known ChangeObjectModifier to a QName. If ChangeObjectModifier.NULL, an ArgumentException will be thrown.
    /// </summary>
    public static string ConvertToKnownQName(ChangeObjectModifier known) {
      switch (known) {
        case ChangeObjectModifier.Person:
          return "http://gedcomx.org/Person";
        case ChangeObjectModifier.Couple:
          return "http://gedcomx.org/Couple";
        case ChangeObjectModifier.ChildAndParentsRelationship:
          return "http://familysearch.org/v1/ChildAndParentsRelationship";
        default:
          throw new System.ArgumentException("No known QName for: " + known, "known");
      }
    }
  }
}
