using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicJournal.Web.ViewModels
{
    public class PaginationList<T>
    {
        private int _itemCount = 0;
        private int _pageCount = 0;
        private int _itemsPerPage = 0;

        public IEnumerable<T> Items { get; private set; }

        /// <summary>
        /// Constructor, takes in a list of items and the number of items that fit within a single page
        /// </summary>
        /// <param name="collection">A list of items</param>
        /// <param name="itemsPerPage">The number of items that fit within a single page</param>
        public PaginationList(IEnumerable<T> collection, int itemsPerPage)
        {
            if (null == collection || collection.Count() == 0 || itemsPerPage <= 0) { _itemCount = 0; _pageCount = 0; }
            else
            {
                _itemCount = collection.Count();
                Items = collection;
                int partialPage = 0;
                if ((_itemCount % itemsPerPage) != 0) { partialPage = 1; }

                _pageCount = _itemCount / itemsPerPage + partialPage;
            }

            _itemsPerPage = itemsPerPage;
        }

        public IEnumerable<T> GetCurrentPage<TKey>(int page, Func<T, TKey> selector, Func<T, bool> predicate=null)
        {
            CurrentPage = page;
            if(predicate != null)
            {
                return Items.Where(predicate).OrderBy(selector)
                .Skip((page - 1) * _itemsPerPage)
                .Take(_itemsPerPage).ToList();
            }
            
            return Items.OrderBy(selector)
                .Skip((page - 1) * _itemsPerPage)
                .Take(_itemsPerPage).ToList(); ;
        }
        /// <summary>
        /// The number of items within the collection
        /// </summary>
        public int ItemCount
        {
            get
            {
                return _itemCount;

            }
        }
        /// <summary>
        /// The current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The number of pages
        /// </summary>
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
        }

        /// <summary>
        /// Returns the number of items in the page at the given page index 
        /// </summary>
        /// <param name="pageIndex">The zero-based page index to get the number of items for</param>
        /// <returns>The number of items on the specified page or -1 for pageIndex values that are out of range</returns>
        public int PageItemCount(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= _pageCount) { return -1; }
            if (_itemCount == 0) { return 0; }
            if (pageIndex == (_pageCount - 1))
            {
                return _itemCount - (_itemsPerPage * (pageIndex));
            }
            return _itemsPerPage;
        }

        /// <summary>
        /// Returns the page index of the page containing the item at the given item index.
        /// </summary>
        /// <param name="itemIndex">The zero-based index of the item to get the pageIndex for</param>
        /// <returns>The zero-based page index of the page containing the item at the given item index or -1 if the item index is out of range</returns>
        public int PageIndex(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= _itemCount) { return -1; }
            if (itemIndex < _itemsPerPage) { return 0; }
            return itemIndex / _itemsPerPage;
        }
    }
}
