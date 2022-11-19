namespace Shared.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
public sealed class IgnoreCoverage : Attribute
{
    
}