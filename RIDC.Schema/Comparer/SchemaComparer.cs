using KellermanSoftware.CompareNetObjects;
using RIDC.Shared.Attributes;

namespace RIDC.Schema.Comparer;

public static class SchemaComparer
{
    private static readonly ICompareLogic s_comparer = new CompareLogic(new ComparisonConfig
    {
        IgnoreCollectionOrder = true,
        IgnoreObjectTypes = true,
        TreatStringEmptyAndNullTheSame = true,
        AttributesToIgnore = new List<Type> { typeof(IgnoreCompareAttribute) }
    });

    public static void Compare<T>(this ICollection<T> oldValue, ICollection<T> newValue, Func<T, string> getId,
        out ICollection<T> modified, out ICollection<T> deleted, out ICollection<T> added) where T : new()
    {
        modified = new List<T>();
        deleted = new List<T>();
        added = new List<T>();

        foreach (var o in oldValue)
        {
            // 对于每一个旧的值，在新的值中查找
            var n = newValue.FirstOrDefault(x => getId.Invoke(x) == getId.Invoke(o));

            // 如果新的值中不存在一个旧的值，代表该值被删除
            if (n is null)
            {
                deleted.Add(o);
                continue;
            }

            var r = s_comparer.Compare(o, n);

            // 若两者除了 ID 以外有不同，表示存在更新
            if (r.AreEqual is false)
            {
                modified.Add(n);
            }
        }

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var n in newValue)
        {
            // 对于每一个新的值，在旧的值中查找
            var o = oldValue.FirstOrDefault(x => getId.Invoke(x) == getId.Invoke(n));

            // 如果一个值在新的值中存在，旧的值中不存在，表示为新增
            if (o is null)
            {
                added.Add(n);
            }
        }
    }
}
