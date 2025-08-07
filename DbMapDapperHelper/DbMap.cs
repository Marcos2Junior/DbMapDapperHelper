using DbMapDapperHelper.Core;
using DbMapDapperHelper.Interfaces;

namespace DbMapDapperHelper
{
    public static class DbMap
    {
        public static IDbMap<T1> Map<T1>() where T1 : IDbMapModel, new()
        {
            return new DbMap<T1>(1);
        }

        public static (IDbMap<T1>, IDbMap<T2>) Map<T1, T2>() where T1 : IDbMapModel, new() where T2 : IDbMapModel, new()
        {
            return (new DbMap<T1>(1), new DbMap<T2>(2));
        }

        public static (IDbMap<T1>, IDbMap<T2>, IDbMap<T3>) Map<T1, T2, T3>() where T1 : IDbMapModel, new() where T2 : IDbMapModel, new() where T3 : IDbMapModel, new()
        {
            return (new DbMap<T1>(1), new DbMap<T2>(2), new DbMap<T3>(3));
        }

        public static (IDbMap<T1>, IDbMap<T2>, IDbMap<T3>, IDbMap<T4>) Map<T1, T2, T3, T4>() where T1 : IDbMapModel, new() where T2 : IDbMapModel, new() where T3 : IDbMapModel, new() where T4 : IDbMapModel, new()
        {
            return (new DbMap<T1>(1), new DbMap<T2>(2), new DbMap<T3>(3), new DbMap<T4>(4));
        }
    }
}
