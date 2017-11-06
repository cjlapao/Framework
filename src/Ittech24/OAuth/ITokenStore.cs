using System;
using System.Collections.Generic;
using System.Text;

namespace Ittech24.OAuth
{
    interface ITokenStore
    {
        IEnumerable<IToken> Items { get; }
        IToken this[int index] { get; }
        void Add(IToken item);
        void Remove(IToken item);
        void RemoveAt(int index);
        void Clear();
        void Replace(IToken original, IToken item);
    }
}
