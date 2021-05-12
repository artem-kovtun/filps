using System;

namespace Filps.Domain.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryParamIgnoreAttribute : Attribute
    {
    }
}