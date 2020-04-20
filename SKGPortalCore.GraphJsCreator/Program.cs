using GraphQL.Types;
using SKGPortalCore.Lib;
using SKGPortalCore.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SKGPortalCore.GraphJsCreator
{
    class Program
    {
        private const string OutputPath = @"./output";

        static void Main()
        {
            Type[] assembly = Assembly.Load("SKGPortalCore.Graph").GetTypes().Where(p => p.Namespace.CompareTo("SKGPortalCore.Graph") != 0).ToArray();
            if (Directory.Exists(OutputPath)) Directory.Delete(OutputPath, true);
            Fragment fragment = new Fragment(assembly);
            Mutation mutation = new Mutation(assembly);
            Query query = new Query(assembly);
            Client client = new Client(assembly);
            Process.Start("explorer.exe", @".\output");
        }

        class Fragment
        {
            public Type[] GraphTypes;
            readonly string Path = $@"{OutputPath}/GraphQL/fragments";
            public Fragment(Type[] assembly)
            {
                GraphTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQuerySetGraphType`1") == 0).ToArray();
                CreateFragment();
            }
            private void CreateFragment()
            {
                foreach (Type type in GraphTypes)
                {
                    string[] strs = type.Namespace.Split('.');
                    string subPath = string.Empty;
                    for (int i = 2; i < strs.Length; i++) { subPath = LibData.Merge(@"/", false, subPath, strs[i].ToCamelCase()); }
                    string fullPath = LibData.Merge(@"/", false, Path, subPath);
                    Directory.CreateDirectory(fullPath);
                    dynamic set = Activator.CreateInstance(type);
                    using StreamWriter file = new StreamWriter($@"{fullPath}/{LibData.ToCamelCase(set.Name)}.fragment.js", true);
                    file.Write(Template(set));
                }
            }
            private string Template(dynamic set)
            {
                string tableTemplate = string.Empty;
                foreach (var table in set.Fields)
                {
                    dynamic tableInst = Activator.CreateInstance(table.Type);
                    if (tableInst is ListGraphType) tableInst = Activator.CreateInstance(tableInst.Type);
                    else
                        tableTemplate += GetTableTemplate(tableInst, $"{tableInst.Name}List");
                    tableTemplate += GetTableTemplate(tableInst, tableInst.Name);
                }
                return $@"import {{ gql }} from ""apollo-boost"";

const {LibData.ToCamelCase(set.Name)}Fragment = {{
{tableTemplate}
}};

export default {LibData.ToCamelCase(set.Name)}Fragment;
";
            }
            private string GetTableTemplate(dynamic tableInst, string fragName)
            {
                string fields = GetFieldsTemplate(tableInst);
                return $@"  {LibData.ToCamelCase(fragName)}: gql`
    fragment {LibData.ToCamelCase(fragName)} on {tableInst.Name} {{
{fields}    }}`,
";
            }
            private string GetFieldsTemplate(dynamic tableInst)
            {
                StringBuilder str = new StringBuilder();
                foreach (var field in tableInst.Fields)
                    str.AppendLine($"       {LibData.ToCamelCase(field.Name)}");
                return str.ToString();
            }
        }
        class Mutation
        {
            public Type[] GraphTypes;
            readonly string Path = $@"{OutputPath}/GraphQL/mutations";
            public Mutation(Type[] assembly)
            {
                GraphTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseInputSetGraphType`1") == 0).ToArray();
                CreateMutation();
            }
            private void CreateMutation()
            {
                foreach (Type type in GraphTypes)
                {
                    string[] strs = type.Namespace.Split('.');
                    string subPath = string.Empty;
                    for (int i = 2; i < strs.Length; i++) { subPath = LibData.Merge(@"/", false, subPath, strs[i].ToCamelCase()); }
                    string fullPath = LibData.Merge(@"/", false, Path, subPath);
                    Directory.CreateDirectory(fullPath);
                    dynamic set = Activator.CreateInstance(type);
                    using StreamWriter file = new StreamWriter($@"{fullPath}/{LibData.ToCamelCase(set.Name)}.mutation.js", true);
                    file.Write(Template(subPath, set));
                }
            }
            private string Template(string subPath, dynamic set)
            {
                List<dynamic> tableInstList = new List<dynamic>();
                foreach (var table in set.Fields)
                {
                    dynamic tableInst = Activator.CreateInstance(table.Type);
                    if (tableInst is ListGraphType) tableInst = Activator.CreateInstance(tableInst.Type);
                    tableInstList.Add(tableInst);
                }

                string frontDir = string.Empty;
                int subLv = subPath.Split('/').Length;
                for (int i = 0; i < subLv; i++) frontDir = LibData.Merge("/", false, frontDir, "..");

                string fullPath = LibData.Merge("/", false, frontDir, "../fragments", subPath, $"{ LibData.ToCamelCase(set.Name)}.fragment");

                return $@"import {{ gql }} from ""apollo-boost"";
import {LibData.ToCamelCase(set.Name)}Fragment from ""{fullPath}""; 

{GetReturnedFields(set.Name, tableInstList)}
{GetCreate(set.Name, tableInstList.Count)}
{GetUpdate(set.Name, tableInstList.Count)}
{GetDelete(set.Name, tableInstList.Count)}
{GetApprove(set.Name, tableInstList.Count)}
{GetInvalid(set.Name, tableInstList.Count)}
{GetEndcase(set.Name, tableInstList.Count)}
";
            }
            private string GetReturnedFields(string setName, List<dynamic> tableInstList)
            {
                return $@"const getReturnedFields ({GetSearch(tableInstList.Count)})
  const fields = `
{GetDetailSearchs(tableInstList)}
    `;
  const fragments = `
{GetConditions(setName, tableInstList)}
`;
  return {{ fields,fragments }};
}};
";
            }
            private string GetCreate(string setName, int count)
            {
                return $@"export const CREATE_{setName.ToUpper()} = ({GetSearch(count)}) => {{
  const {{ fields, fragments }} = getReturnedFields({GetSearch(count)});
  return gql`
        mutation {nameof(BasicRepository<object>.Create)}(${SystemCP.Set}: {setName}InputType!, ${SystemCP.JWT}: String!) {{
            {nameof(BasicRepository<object>.Create)}({SystemCP.Set}: ${SystemCP.Set}, {SystemCP.JWT}: ${SystemCP.JWT}){{
               ${{fields}}
            }}
        }}
       ${{fragments}}
    `;
}};
";
            }
            private string GetUpdate(string setName, int count)
            {
                return $@"export const UPDATE_{setName.ToUpper()} = ({GetSearch(count)}) => {{
  const {{ fields, fragments }} = getReturnedFields({GetSearch(count)});
  return gql`
        mutation {nameof(BasicRepository<object>.Update)}(${SystemCP.KeyVal}: [ID], ${SystemCP.Set}: {setName}InputType!, ${SystemCP.JWT}: String!) {{
            {nameof(BasicRepository<object>.Update)}({SystemCP.KeyVal}:${SystemCP.KeyVal}, {SystemCP.Set}: ${SystemCP.Set}, {SystemCP.JWT}: ${SystemCP.JWT}){{
               ${{fields}}
            }}
        }}
       ${{fragments}}
    `;
}};
";
            }
            private string GetDelete(string setName, int count)
            {
                return $@"export const DELETE_{setName.ToUpper()} = () => {{
  return gql`
    mutation {nameof(BasicRepository<object>.Delete)}(${SystemCP.KeyVal}: [ID], ${SystemCP.JWT}: String!) {{
      {nameof(BasicRepository<object>.Delete)}({SystemCP.KeyVal}: ${SystemCP.KeyVal}, {SystemCP.JWT}: ${SystemCP.JWT})
    }}
  `;
}};
";
            }
            private string GetApprove(string setName, int count)
            {
                return $@"export const APPROVE_{setName.ToUpper()} = ({GetSearch(count)}) => {{
  const {{ fields, fragments }} = getReturnedFields({GetSearch(count)});
  return gql`
        mutation {nameof(BasicRepository<object>.Approve)}(${SystemCP.KeyVal}: [ID],${SystemCP.Status}: Boolean!, ${SystemCP.JWT}: String!) {{
            {nameof(BasicRepository<object>.Approve)}({SystemCP.KeyVal}: ${SystemCP.KeyVal},{SystemCP.Status}: ${SystemCP.Status}, {SystemCP.JWT}: ${SystemCP.JWT}){{
               ${{fields}}
            }}
        }}
       ${{fragments}}
    `;
}};
";
            }
            private string GetInvalid(string setName, int count)
            {
                return $@"export const INVALID_{setName.ToUpper()} = ({GetSearch(count)}) => {{
  const {{ fields, fragments }} = getReturnedFields({GetSearch(count)});
  return gql`
        mutation {nameof(BasicRepository<object>.Invalid)}(${SystemCP.KeyVal}: [ID],${SystemCP.Status}: Boolean!, ${SystemCP.JWT}: String!) {{
            {nameof(BasicRepository<object>.Invalid)}({SystemCP.KeyVal}: ${SystemCP.KeyVal},{SystemCP.Status}: ${SystemCP.Status}, {SystemCP.JWT}: ${SystemCP.JWT}){{
               ${{fields}}
            }}
        }}
       ${{fragments}}
    `;
}};
";
            }
            private string GetEndcase(string setName, int count)
            {
                return $@"export const ENDCASE_{setName.ToUpper()} = ({GetSearch(count)}) => {{
  const {{ fields, fragments }} = getReturnedFields({GetSearch(count)});
  return gql`
        mutation {nameof(BasicRepository<object>.EndCase)}(${SystemCP.KeyVal}: [ID],${SystemCP.Status}: Boolean!, ${SystemCP.JWT}: String!) {{
            {nameof(BasicRepository<object>.EndCase)}({SystemCP.KeyVal}: ${SystemCP.KeyVal},{SystemCP.Status}: ${SystemCP.Status}, {SystemCP.JWT}: ${SystemCP.JWT}){{
               ${{fields}}
            }}
        }}
       ${{fragments}}
    `;
}};
";
            }

            private string GetSearch(int count)
            {
                string result = string.Empty;
                for (int i = 1; i <= count; i++)
                    result = LibData.Merge(", ", false, result, $"search{i}");
                return result;
            }
            private string GetDetailSearchs(List<dynamic> tableInstList)
            {
                StringBuilder str = new StringBuilder();
                for (int i = 1; i <= tableInstList.Count; i++) str.AppendLine(GetDetailSearch(tableInstList[i - 1], i));
                return str.ToString();
            }

            private string GetDetailSearch(dynamic tableInst, int count)
            {
                return $@"                {LibData.ToCamelCase(tableInst.Name)} {{
                    ${{search{count} || ""...{LibData.ToCamelCase(tableInst.Name)}""}}
                }}";
            }
            private string GetConditions(string setName, List<dynamic> tableInstList)
            {
                StringBuilder str = new StringBuilder();
                for (int i = 1; i <= tableInstList.Count; i++) str.AppendLine("        " + GetCondition(setName, tableInstList[i - 1], i));
                return str.ToString();
            }
            private string GetCondition(string setName, dynamic tableInst, int count)
            {
                return $@"${{search{count} ? """" : {LibData.ToCamelCase(setName)}Fragment.{LibData.ToCamelCase(tableInst.Name)}}}";
            }
        }
        class Query
        {
            public Type[] GraphTypes;
            readonly string Path = $@"{OutputPath}/GraphQL/queries";
            public Query(Type[] assembly)
            {
                GraphTypes = assembly.Where(t => t.BaseType.Name.CompareTo("BaseQuerySetGraphType`1") == 0).ToArray();
                CreateQuery();
            }
            private void CreateQuery()
            {
                foreach (Type type in GraphTypes)
                {
                    string[] strs = type.Namespace.Split('.');
                    string subPath = string.Empty;
                    for (int i = 2; i < strs.Length; i++) { subPath = LibData.Merge(@"/", false, subPath, strs[i].ToCamelCase()); }
                    string fullPath = LibData.Merge(@"/", false, Path, subPath);
                    Directory.CreateDirectory(fullPath);
                    dynamic set = Activator.CreateInstance(type);
                    using StreamWriter file = new StreamWriter($@"{fullPath}/{LibData.ToCamelCase(set.Name)}.query.js", true);
                    file.Write(Template(subPath, set));
                }
            }
            private string Template(string subPath, dynamic set)
            {
                string frontDir = string.Empty;
                int subLv = subPath.Split('/').Length;
                for (int i = 0; i < subLv; i++) frontDir = LibData.Merge("/", false, frontDir, "..");

                string fullPath = LibData.Merge("/", false, frontDir, "../fragments", subPath, $"{ LibData.ToCamelCase(set.Name)}.fragment");
                return $@"import {{ gql }} from ""apollo-boost"";
import {LibData.ToCamelCase(set.Name)}Fragment from ""{fullPath}"";
{GetDataList(set)}
{GetDataInfo(set)}
";
            }
            private string GetDataList(dynamic set)
            {
                dynamic tableInst = null;
                foreach (var table in set.Fields)
                {
                    tableInst = Activator.CreateInstance(table.Type);
                    if (tableInst is ListGraphType) continue;
                    else break;
                }
                return $@"export const GET_{tableInst.Name.ToUpper()}_List = searchFields => {{
    return gql`

        query {nameof(BasicRepository<object>.QueryList)}(${SystemCP.Condition}: String!, ${SystemCP.JWT}: String!){{
            {nameof(BasicRepository<object>.QueryList)}({SystemCP.Condition}: ${SystemCP.Condition}, {SystemCP.JWT}: ${SystemCP.JWT}){{
                ${{searchFields || ""...{LibData.ToCamelCase(tableInst.Name)}List""}}
            }}
        }}
        ${{searchFields ? """" : {LibData.ToCamelCase(set.Name)}Fragment.{LibData.ToCamelCase(tableInst.Name)}List}}
    `;
}};
";
            }
            private string GetDataInfo(dynamic set)
            {
                List<dynamic> tableInstList = new List<dynamic>();
                foreach (var table in set.Fields)
                {
                    dynamic tableInst = Activator.CreateInstance(table.Type);
                    if (tableInst is ListGraphType) tableInst = Activator.CreateInstance(tableInst.Type);
                    tableInstList.Add(tableInst);
                }

                return $@"export const GET_{tableInstList[0].Name.ToUpper()}_INFO = ({GetSearch(tableInstList.Count)}) => {{
    return gql`
        query {nameof(BasicRepository<object>.QueryData)}(${SystemCP.KeyVal}: [ID], ${SystemCP.JWT}: String!){{
            {nameof(BasicRepository<object>.QueryData)}({SystemCP.KeyVal}: ${SystemCP.KeyVal}, {SystemCP.JWT}: ${SystemCP.JWT}){{
{GetDetailSearchs(tableInstList)}            }}
        }}
{GetConditions(set.Name, tableInstList)}    `;
}};";
            }
            private string GetSearch(int count)
            {
                string result = string.Empty;
                for (int i = 1; i <= count; i++)
                    result = LibData.Merge(", ", false, result, $"search{i}");
                return result;
            }
            private string GetDetailSearchs(List<dynamic> tableInstList)
            {
                StringBuilder str = new StringBuilder();
                for (int i = 1; i <= tableInstList.Count; i++) str.AppendLine(GetDetailSearch(tableInstList[i - 1], i));
                return str.ToString();
            }
            private string GetDetailSearch(dynamic tableInst, int count)
            {
                return $@"                {LibData.ToCamelCase(tableInst.Name)} {{
                    ${{search{count} || ""...{LibData.ToCamelCase(tableInst.Name)}""}}
                }}";
            }
            private string GetConditions(string setName, List<dynamic> tableInstList)
            {
                StringBuilder str = new StringBuilder();
                for (int i = 1; i <= tableInstList.Count; i++) str.AppendLine("        " + GetCondition(setName, tableInstList[i - 1], i));
                return str.ToString();
            }
            private string GetCondition(string setName, dynamic tableInst, int count)
            {
                return $@"${{search{count} ? """" : {LibData.ToCamelCase(setName)}Fragment.{LibData.ToCamelCase(tableInst.Name)}}}";
            }
        }
        class Client
        {
            public Type[] GraphTypes;
            public Client(Type[] assembly)
            {
                GraphTypes = assembly.Where(t => t.BaseType.Name.Contains("BaseSchema")).OrderBy(p => p.Namespace).ToArray();
                CreateClient();
            }
            private void CreateClient()
            {
                using StreamWriter file = new StreamWriter($@"{OutputPath}/client.js", true);
                file.Write(Template());
            }
            private string Template()
            {
                return $@"const ApolloClient = require('apollo-client');
const {{ createUploadLink }} = require('apollo-upload-client');
const {{ InMemoryCache }} = require('apollo-cache-inmemory');
var url=""https://localhost:5001/"";

export const client = new ApolloClient({{
  uri: url
}});

{GetApolloClients()}
";
            }
            private string GetApolloClients()
            {
                StringBuilder str = new StringBuilder();
                foreach (Type type in GraphTypes)
                    str.AppendLine(GetApolloClient(type.Name.Replace("Schema", "")));
                return str.ToString();
            }
            private string GetApolloClient(string schemaName)
            {
                return $@"export const {schemaName.ToCamelCase()}Client = new ApolloClient({{
  link: createUploadLink(),
  cache: new InMemoryCache(),
  uri: url+""{schemaName}""
}});";
            }
        }
    }
}
