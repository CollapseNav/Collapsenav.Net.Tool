using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Collapsenav.Net.Tool.Data.Test;
[AttributeUsage(AttributeTargets.Method)]
public class OrderAttribute : Attribute
{
    public int Sort { get; set; }
    public OrderAttribute(int sort)
    {
        this.Sort = sort;
    }
}
public class TestOrders : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        string typeName = typeof(OrderAttribute).AssemblyQualifiedName; ;
        var result = testCases.ToList();
        result.Sort((x, y) =>
        {
            var xOrder = x.TestMethod.Method.GetCustomAttributes(typeName)?.FirstOrDefault();
            if (xOrder == null)
            {
                return 0;
            }
            var yOrder = y.TestMethod.Method.GetCustomAttributes(typeName)?.FirstOrDefault();
            if (yOrder == null)
            {
                return 0;
            }
            var sortX = xOrder.GetNamedArgument<int>("Sort");
            var sortY = yOrder.GetNamedArgument<int>("Sort");
            return sortX - sortY;
        });
        return result;
    }
}
