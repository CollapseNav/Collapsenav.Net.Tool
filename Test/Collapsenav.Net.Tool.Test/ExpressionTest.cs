using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Collapsenav.Net.Tool.Test;
public class ExpressionTest
{
    [Fact]
    public void ExpressionAndTest()
    {
        int[] ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Expression<Func<int, bool>> exp = item => true;
        exp = exp.And(item => item > 3)
        .AndIf(true, item => item != 5)
        .AndIf(false, item => item != 8)
        .AndIf(string.Empty, item => item < 9)
        .AndIf("2333", item => item != 6)
        ;
        Assert.True(ints.AsQueryable().Where(exp).ToList().SequenceEqual(new[] { 4, 7, 8, 9, 10 }));
        int? i = null;
        exp = exp.AndIf(i, item => item != 10);
        Assert.True(ints.AsQueryable().Where(exp).ToList().SequenceEqual(new[] { 4, 7, 8, 9, 10 }));
        i = 1;
        exp = exp.AndIf(i, item => item != 10);
        Assert.True(ints.AsQueryable().Where(exp).ToList().SequenceEqual(new[] { 4, 7, 8, 9 }));

    }

    [Fact]
    public void ExpressionOrTest()
    {
        int[] ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Expression<Func<int, bool>> exp = item => false;
        exp = exp.Or(item => item == 3)
        .OrIf(true, item => item == 5)
        .OrIf(false, item => item == 8)
        .OrIf(string.Empty, item => item == 9)
        .OrIf("2333", item => item == 6)
        ;
        Assert.True(ints.AsQueryable().Where(exp).ToList().SequenceEqual(new[] { 3, 5, 6 }));
    }
    [Fact]
    public void ExpressionAndAlsoTest()
    {
        int[] ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Expression<Func<int, bool>> exp = item => true;
        exp = exp.AndAlso(item => item > 3)
        .AndAlsoIf(true, item => item != 5)
        .AndAlsoIf(false, item => item != 8)
        .AndAlsoIf(string.Empty, item => item < 9)
        .AndAlsoIf("2333", item => item != 6)
        ;
        Assert.True(ints.AsQueryable().Where(exp).ToList().SequenceEqual(new[] { 4, 7, 8, 9, 10 }));
    }
    [Fact]
    public void ExpressionOrElseTest()
    {
        int[] ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Expression<Func<int, bool>> exp = item => false;
        exp = exp.OrElse(item => item == 3)
        .OrElseIf(true, item => item == 5)
        .OrElseIf(false, item => item == 8)
        .OrElseIf(string.Empty, item => item == 9)
        .OrElseIf("2333", item => item == 6)
        ;
        Assert.True(ints.AsQueryable().Where(exp).ToList().SequenceEqual(new[] { 3, 5, 6 }));
    }
}
