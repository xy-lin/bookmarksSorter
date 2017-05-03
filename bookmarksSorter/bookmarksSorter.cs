
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

            try
            {
                xmlDocument = XDocument.Load(xmlStream);
            }
            catch (System.Exception ex)
            {
                return;
            }            

            IEnumerable<XAttribute> links = xmlDocument.Descendants("A").Attributes("HREF");
            HashSet<string> uniqLinks = new HashSet<string>();

            Debug.WriteLine(links.Count());
            foreach (XAttribute link in links)
            {
                uniqLinks.Add(link.Value);
            }

            XDocument newDoc = new XDocument();
            newDoc.Add(new XElement("root"));

            foreach (string link in uniqLinks)
            {
                Debug.Write("\r", link);
                newDoc.Element("root").Add(new XElement("href", link));
            }

            newDoc.Save(args[1]);

            // Change back:
            // ";" -> "="
            // ":" -> "&"       

        }
    }
}
