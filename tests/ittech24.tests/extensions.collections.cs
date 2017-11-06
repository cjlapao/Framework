using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Ittech24.Extensions;

namespace ittech24.tests
{
    public class ExtensionsCollectionsTests
    {
		[Fact]
		public void AppendToDictionary()
        {
            var dic = new Dictionary<string, string>();

            dic.Add("test1", "value1");
            dic.Append("test2", "value2");
            dic.Append("test1", "value3");

            Assert.True(dic.Count == 2);
            Assert.Equal(dic["test1"], "value3");
            Assert.Equal(dic["test2"], "value2");
        }
    }
}
