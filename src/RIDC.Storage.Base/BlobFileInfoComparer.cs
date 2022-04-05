using KellermanSoftware.CompareNetObjects;
using RIDC.Shared.Attributes;

namespace RIDC.Storage.Base;

public static class BlobFileInfoComparer
{
    private static readonly ICompareLogic s_comparer = new CompareLogic(new ComparisonConfig
    {
        IgnoreCollectionOrder = true,
        IgnoreObjectTypes = true,
        TreatStringEmptyAndNullTheSame = true,
        AttributesToIgnore = new List<Type> { typeof(IgnoreCompareAttribute) }
    });

    public static void Compare(this ICollection<BlobFileInfo> remote, ICollection<BlobFileInfo> local,
        out ICollection<BlobFileInfo> deleted, out ICollection<BlobFileInfo> added)
    {
        deleted = new List<BlobFileInfo>();
        added = new List<BlobFileInfo>();

        // 对于每一个远程文件，与本地不一致的，认为远程文件需要被删除，新增本地文件
        foreach (var remoteFile in remote)
        {
            var localFile = local.FirstOrDefault(x => x.Key == remoteFile.Key);
            if (localFile == null)
            {
                deleted.Add(remoteFile);
            }
            else
            {
                if (s_comparer.Compare(localFile, remoteFile).AreEqual)
                {
                    continue;
                }

                deleted.Add(remoteFile);
                added.Add(localFile);
            }
        }

        // 对于每一本地文件，远程不存在的则认为需要添加
        foreach (var localFile in local)
        {
            var remoteFile = remote.FirstOrDefault(x => x.Key == localFile.Key);
            if (remoteFile == null)
            {
                added.Add(localFile);
            }
        }
    }
}
