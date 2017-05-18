/************************************************************************/
//18-May-2017
//
/* Removing duplicate Bookmarks from exported chrome bookmarks          */
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;

namespace bookmarksSorter
{
    class bookmarksSorter
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
               return;

            XDocument xmlDocument;
            var xmlStream = new System.IO.StreamReader(args[0]);

            // Remove useless elements and change:
            // "=" -> ";"
            // "&" -> ":"       
            // "HREF;" -> "HREF=" 
            // "ADD_DATE;" -> "ADD_DATE="

            try
            {
                xmlDocument = XDocument.Load(xmlStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            var links = xmlDocument.Descendants("A");
            // IEnumerable<XElement> links = xmlDocument.Descendants("href");
            Dictionary<string, XElement> uniqLinks = new Dictionary<string, XElement>();

            Debug.WriteLine(links.Count());

            foreach (var link in links)
            {
                var xAttribute = link.Attribute("HREF");
                if (xAttribute == null)
                    return;

                uniqLinks[xAttribute.Value] = link;
            }

            var list = uniqLinks.Keys.ToList();
            list.Sort();

            var newDoc = new XDocument();
            newDoc.Add(new XElement("root"));
            var xElement = newDoc.Element("root");
            if (xElement == null)
                return;
            
            foreach (var link in list)
            {
                Debug.Write("\r", link);
                
                xElement.Add(uniqLinks[link]);
            }

            newDoc.Save(args[1]);

            // Change back:
            // ";" -> "="
            // ":" -> "&"       

        }
    }
}
