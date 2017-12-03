using System;
using System.Collections.Generic;
using System.Text;

namespace Coding.Exercise
{
    // not using System.ValueTuple because the udemy compiler doesn't like it
    //using ClassProperty = ValueTuple<string, string>;

    public class CodeBuilder
    {
        private string className;

        internal class ClassProperty
        {
            public string propertyName, propertyType;
        }

        //private List<ClassProperty> properties = new List<ClassProperty>();

        private List<ClassProperty> properties = new List<ClassProperty>();

        private const int indentSize = 2;

        public CodeBuilder(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentNullException(paramName: nameof(className));
            }
            this.className = className;
        }

        public CodeBuilder AddField(string propertyName, string propertyType)
        {
            properties.Add(new ClassProperty{propertyName = propertyName, propertyType = propertyType});
            return this;
        }

        public string ToStringImpl(int indent)
        {
            var stringBuilder = new StringBuilder();
            var i = new string(' ', indent * indentSize);
            stringBuilder.AppendLine($"public class {className}");
            stringBuilder.AppendLine("{");
            foreach (var property in properties)
            {
                stringBuilder.Append(new string(' ', indentSize * (indent + 1)));
                stringBuilder.AppendLine($"public {property.propertyType} {property.propertyName};");
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }


        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new CodeBuilder("Person");
            builder.AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(builder.ToString());
        }
        
    }
}
