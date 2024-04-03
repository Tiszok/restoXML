using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
	internal class Program
	{
		static XElement idxml;


		private static void remove(XElement xml, Func<XElement,object> groupby )
		{
            var duplicate = xml.Elements().GroupBy(groupby);
            var ttt = duplicate.Where(x => x.Count() > 1).ToList();
            ttt.ForEach(x =>
            {
                for (int i = 1; i < x.Count(); i++)
                {
                    x.ElementAt(i).Remove();

                }
            });
        }

        private static void remove(XElement xml)
        {
			remove(xml, (x) => new { Origin = x.Attribute("org").ToString(), Translation = x.Attribute("trans").ToString() });
        }

        static void Main(string[] args)
		{

			//string dopodmianyfile = "C:\\!MOJE PROGRAMY\\AlseccoComparer\\do testow\\Oknares.xml";
			//string dopodmiany2file = "C:\\!MOJE PROGRAMY\\AlseccoComparer\\do testow\\Bazyres.xml";
			//string idfile = "C:\\Users\\maciej.kedziora\\Desktop\\18.01.2024 ALSECCO\\025\\modyfikacje\\id.xml";

			XElement dopodmianyxml = XElement.Load(dopodmianyfile);
			XElement dopodmiany2xml = XElement.Load(dopodmiany2file);

			//idxml = XElement.Load(idfile);

			//var duplicate = dopodmiany2xml.Elements().GroupBy(x => new { Origin = x.Attribute("Origin").ToString(), Translation = x.Attribute("Translation").ToString() });

			//var ttt =  duplicate.Where(x => x.Count() > 1).ToList();
			//ttt.ForEach(x => 
			//{
			//	for (int i = 1; i < x.Count(); i++)
			//	{
			//		x.ElementAt(i).Remove();

			//             }
			//});

			

            var test = dopodmianyxml.Element("Element");
			XElement translatedxml = new XElement("dictionary");

            foreach (XElement set in dopodmianyxml.Elements().ToList())//.Elements())
			{
				var toAdd = new XElement("tr", new XAttribute("org", set.Attribute("Origin").Value), new XAttribute("trans", set.Attribute("Translation").Value));
				translatedxml.Add(toAdd);
				//set.Attribute("Description").Remove();
				//set.Attribute("ResourceId").Remove();
				//set.Attribute("ResourceType").Remove();
				//set.Attribute("ControlId").Remove();
				//set.Attribute("ControlClass").Remove();
				//set.Attribute("Flags").Remove();
				

				//foreach (XElement setdesc in set.Elements())
				//{
				//	if (setdesc.Name.LocalName == "SetDescription")
				//	{
				//		string fittingref = setdesc.Attribute("fittingRef").Value;

				//		string found = findFittingID(fittingref);
				//		if (!string.IsNullOrEmpty(found))
				//		{
				//			setdesc.SetAttributeValue("fittingId", found);	
				//		}
				//	}

				//}
			}


			foreach (XElement set in dopodmiany2xml.Elements().ToList())//.Elements())
			{
				var toAdd = new XElement("tr", new XAttribute("org", set.Attribute("Origin").Value), new XAttribute("trans", set.Attribute("Translation").Value));
				translatedxml.Add(toAdd);
				
			}
			remove(translatedxml);
            translatedxml.Save("C:\\!MOJE PROGRAMY\\AlseccoComparer\\do testow\\gotowe.xml");
        }


		public static string findFittingID(string fittingref)
		{
			string id = "";

			foreach (XElement fittings in idxml.Elements().ToList()[1].Elements())
			{
				if (fittings.Name.LocalName == "Fitting" && fittings.Attribute("ref").Value == fittingref)
				{
					return fittings.Attribute("id").Value;
				}
			}


			return id;
		}
	}
}
