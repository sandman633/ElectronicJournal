﻿using System;

namespace ElectronicJournal.Web.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int NextPage => CurrentPage + 1;
        public int PreviousPage => CurrentPage - 1;
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
