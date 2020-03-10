using GraphQL.Types;
using SKGPortalCore.Graph.BillData;
using SKGPortalCore.Lib;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SKGPortalCore.GraphJsCreator
{
    class Program
    {
        private static string OutputPath = @".\output";

        static void Main()
        {
            Console.WriteLine("Hello World!");
            Type[] assembly = Assembly.Load("SKGPortalCore.Graph").GetTypes().Where(p => p.Namespace.CompareTo("SKGPortalCore.Graph") != 0).ToArray();
            //Set
            Type[] setTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQuerySetGraphType`1") == 0).ToArray();
            foreach (Type t in setTypes)
            {
                dynamic set = Activator.CreateInstance(t);
                string v = Template(set);
                Directory.CreateDirectory(OutputPath);
                using StreamWriter file = new StreamWriter($@"{OutputPath}\{LibData.ToCamelCase(set.Name)}.fragment.js", true);
                file.Write(v);
            }
        }

        private static string Template(dynamic set)
        {
            string tableTemplate = string.Empty;
            foreach (var table in set.Fields)
            {
                dynamic g = Activator.CreateInstance(table.Type);
                if (g is ListGraphType) g = Activator.CreateInstance(g.Type);
                tableTemplate += GetTableTemplate(g);
            }
            return $@"import {{ gql }} from ""apollo-boost"";

const {set.Name}Fragment = {{
{tableTemplate}
}};

export default {set.Name}Fragment;
";
        }

        private static string GetTableTemplate(dynamic c)
        {
            string fields = GetFieldsTemplate(c);
            return $@"  {LibData.ToCamelCase(c.Name)}: gql`
    fragment {LibData.ToCamelCase(c.Name)} on {c.GetType().Name} {{
{fields}    }}`,
";
        }

        private static string GetFieldsTemplate(dynamic c)
        {
            StringBuilder str = new StringBuilder();
            foreach (var field in c.Fields)
                str.AppendLine($"       {LibData.ToCamelCase(field.Name)}");
            return str.ToString();
        }
    }
}
