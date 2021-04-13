﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace SimpleMvcSitemap.Serialization
{
    interface IXmlNamespaceBuilder
    {
        XmlSerializerNamespaces Create(IEnumerable<string> namespaces);
    }
}