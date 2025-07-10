using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class MongoCollectionNameAttribute : Attribute
{
    public string Name { get; }

    public MongoCollectionNameAttribute(string name)
    {
        Name = name;
    }
}