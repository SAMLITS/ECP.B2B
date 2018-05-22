using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using ECP.Util.Common;
using ProtoBuf;

namespace ECP.B2b.ComEntity.Page
{
    [ProtoContract]
    public class PageResult<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        [ProtoMember(1)]
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        [ProtoMember(2)]
        public int PageSize { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        [ProtoMember(3)]
        public int Total { get; set; }

        /// <summary>
        /// 分页后得到的数据
        /// </summary>
        [ProtoMember(4)]
        public List<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// 得到分页后的数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static PageResult<T> GetJsonConvetData(IList<T> _data, PageFilter _filter, int total)
        {
            return new PageResult<T>
            {
                PageIndex = _filter.PageIndex,
                PageSize = _filter.PageSize,
                Total = total,
                Data = _data.ToList()
            };
        }
         

        /// <summary>
        /// 得到分页后的数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static PageResult<T> GetJsonConvetData(IEnumerable<T> _data, PageFilter _filter, int total)
        {
            return new PageResult<T>
            {
                PageIndex = _filter.PageIndex,
                PageSize = _filter.PageSize,
                Total = total,
                Data = _data.ToList()
            };
        }
    }

    public static class PageResult
    {
        /// <summary>
        /// 得到分页后的数据
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static PageResult<T> GetJsonConvetPageData<T>(this IEnumerable<T> _data, PageFilter _pageFilter)
        {
            return new PageResult<T>
            {
                PageIndex = _pageFilter.PageIndex,
                PageSize = _pageFilter.PageSize,
                Total = _pageFilter.TotalPageCount,
                Data = _data.ToList()
            };
        }
    }
}
