using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.Page
{
    [ProtoContract]
    public class PageFilter
    {
        private int _pageIndex = 1;
        private int _pageSize = 10;
        private string _sortName;
        private string _sortOrder;

        private int _totalPageCount;

        [ProtoMember(1)]
        public int PageIndex
        {
            get { return _pageIndex; }
            set { if (value > 1) _pageIndex = value; }
        }

        [ProtoMember(2)]
        public int PageSize
        {
            get { return _pageSize; }
            set { if (value > 0 && value != _pageSize) _pageSize = value; }
        }

        [ProtoMember(3)]
        public int TotalPageCount
        {
            get { return _totalPageCount; }
            set { _totalPageCount = value; }
        }

        [ProtoMember(4)]
        public string SortName
        {
            get { return _sortName; }
            set { _sortName = value; }
        }

        [ProtoMember(5)]
        public string SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }
    }
}
