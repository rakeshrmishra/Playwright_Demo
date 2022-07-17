using System.Xml;
using System;
using System.Text;
namespace Utils{
public class Helper{
public static String GetID(String elem){
return GetXmlValue(elem,"ID");
}

public static String GetValue(String elem){
return GetXmlValue(elem,"Value");
}
public static String GetXmlValue(String elem,String nodeName){
XmlDocument xml = new XmlDocument();
xml.Load("out.xml");
String str = "//DocumentElement//Elements[Element='"+elem+"']";
XmlNodeList xnList = xml.SelectNodes(str);
String result=String.Empty;
foreach (XmlNode xn in xnList)
 {
  result = xn[nodeName].InnerText;
 }
 return result;
}

public static string GetRandomTelNo()
        {
            Random rand = new Random();
            StringBuilder telNo = new StringBuilder(12);
            int number;
           
            number = rand.Next(7, 10);
            telNo = telNo.Append(number);
            for (int i = 1; i < 10; i++)
            {
                number = rand.Next(0, 8); // digit between 0 (incl) and 8 (excl)
                telNo = telNo.Append(number.ToString());
            }
            return telNo.ToString();
        }

}
}
