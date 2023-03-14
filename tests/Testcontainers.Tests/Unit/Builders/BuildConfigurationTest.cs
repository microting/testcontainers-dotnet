﻿namespace DotNet.Testcontainers.Tests.Unit
{
  using System.Collections.Generic;
  using System.Linq;
  using DotNet.Testcontainers.Builders;
  using Xunit;

  public sealed class BuildConfigurationTest
  {
    [Theory]
    [InlineData(null, null, null)]
    [InlineData(null, "B", "B")]
    [InlineData("A", null, "A")]
    [InlineData("A", "B", "B")]
    public void CombineReferenceTypes(string oldValue, string newValue, string expected)
    {
      var actual = BuildConfiguration.Combine(oldValue, newValue);
      Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(null, null, null)]
    [InlineData(null, new[] { "2" }, new[] { "2" })]
    [InlineData(new[] { "1" }, null, new[] { "1" })]
    [InlineData(new[] { "1" }, new[] { "2" }, new[] { "1", "2" })]
    [InlineData(new[] { "1", "2", "3" }, new[] { "2", "3", "4" }, new[] { "1", "2", "2", "3", "3", "4" })]
    public void CombineEnumerables(IEnumerable<string> oldValue, IEnumerable<string> newValue, IEnumerable<string> expected)
    {
      var actual = BuildConfiguration.Combine(oldValue, newValue);
      Assert.Equal(expected?.OrderBy(item => item), actual?.OrderBy(item => item));
    }

    [Theory]
    [InlineData(null, null, null)]
    [InlineData(null, new[] { "2" }, new[] { "2" })]
    [InlineData(new[] { "1" }, null, new[] { "1" })]
    [InlineData(new[] { "1" }, new[] { "2" }, new[] { "1", "2" })]
    [InlineData(new[] { "1", "2", "3" }, new[] { "2", "3", "4" }, new[] { "1", "2", "3", "2", "3", "4" })]
    public void CombineReadOnlyLists(IReadOnlyList<string> oldValue, IReadOnlyList<string> newValue, IReadOnlyList<string> expected)
    {
      var actual = BuildConfiguration.Combine(oldValue, newValue);
      Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(DictionaryCombinationTestData))]
    public void CombineReadOnlyDictionaries(IReadOnlyDictionary<string, string> oldValue, IReadOnlyDictionary<string, string> newValue, IReadOnlyDictionary<string, string> expected)
    {
      var actual = BuildConfiguration.Combine(oldValue, newValue);
      Assert.Equal(expected, actual);
    }

    private sealed class DictionaryCombinationTestData : List<object[]>
    {
      public DictionaryCombinationTestData()
      {
        this.Add(new object[] { null, null, null });
        this.Add(new object[] { new Dictionary<string, string> { { "A", "A" } }, null, new Dictionary<string, string> { { "A", "A" } } });
        this.Add(new object[] { null, new Dictionary<string, string> { { "B", "B" } }, new Dictionary<string, string> { { "B", "B" } } });
        this.Add(new object[] { new Dictionary<string, string> { ["A"] = "old", ["B"] = "B" }, new Dictionary<string, string> { ["A"] = "new" }, new Dictionary<string, string> { ["A"] = "new", ["B"] = "B" } });
      }
    }
  }
}
