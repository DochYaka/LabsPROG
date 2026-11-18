// IXmlSerializable.cs
using System.Xml.Linq;

namespace CourseWork.Interfaces
{
    public interface IXmlSerializable
    {
        XElement ToXml();
        void FromXml(XElement element);
    }
}