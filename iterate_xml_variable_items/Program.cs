using System;
using System.Linq;
using System.Xml.Linq;

namespace iterate_xml_variable_items
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = XElement.Parse(source);
            foreach (var item in root.Elements("ITEM"))
            {
                foreach (var entry in item.Elements())
                {
                    Console.WriteLine($"{entry.Name.LocalName} = {(string)entry}");
                }
                Console.WriteLine();
            }
            // Find something specific
            var matchItem =
                root.Descendants("ITEM")
                .First(match => match.IsPortNameMatch("UniquePortName"));
            Console.WriteLine($"FOUND:{Environment.NewLine}{matchItem.ToString()}");
        }

    const string source =
    @"<ITEMS ASOF_DATE=""6/2/2022"" RECORDS=""1"" CREATE_DATE=""6/3/2022"" >
        <ITEM>
            <port_name>95512M</port_name>
            <bench_name>LEHSECUR</bench_name>
            <SomeValue>-808</SomeValue>
        </ITEM>
        <ITEM>
            <port_name>95512M</port_name>
            <bench_name>LEHSECUR</bench_name>
            <SomeValue>-808</SomeValue>
            <SomeOtherValue>-808</SomeOtherValue>
        </ITEM> 
        <ITEM>
            <port_name>95512M</port_name>
            <bench_name>LEHSECUR</bench_name>
            <SomethingElse>234</SomethingElse>
        </ITEM>  
        <ITEM>
            <port_name>UniquePortName</port_name>
            <bench_name>LEHSECUR</bench_name>
            <SomethingElse>234</SomethingElse>
        </ITEM> 
    </ITEMS>";
    }

    static class Extensions
    {
        // Test to see if ITEM has port name
        public static bool HasPortName(this XElement xElement, string name)
        {
            if (!string.Equals(xElement.Name.LocalName, "ITEM")) return false;
            var portName = xElement.Element("port_name");
            if (portName == null) return false;
            return !string.IsNullOrEmpty((string)portName);
        }

        public static bool IsPortNameMatch(this XElement xElement, string name)
        {
            if (!string.Equals(xElement.Name.LocalName, "ITEM")) return false;
            var portName = xElement.Element("port_name");
            if (portName == null) return false;
            return string.Equals((string)portName, name);
        }
    }
}
