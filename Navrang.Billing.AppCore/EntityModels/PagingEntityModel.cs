using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Navrang.Billing.AppCore.EntityModels
{
    public class PagingEntityModel
    {
        public PagingEntityModel() { }

        public PagingEntityModel(int _pageIndex, int _pageSize, int _TotalRecords) 
        {
            PageIndex = _pageIndex;
            PageSize = _pageSize;
            TotalRecords = _TotalRecords;
        }

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; }

        public int TotalPagesCount
        {
            get
            {
                return (TotalRecords + PageSize - 1) / PageSize;
                //return (int)Math.Ceiling((double)(TotalRecords / PageSize));
            }
        }

        public int firstRecordIndex
        {
            get
            {
                return ((PageIndex - 1) * PageSize) + 1;
            }
        }

        public int lastRecordIndex
        {
            get
            {
                return ((PageIndex - 1) * PageSize) + PageSize;
            }
        }

        public string firstPageClass { get { return PageIndex == 1 ? "disabled" : ""; } }
        public string prevPageClass { get { return PageIndex == 1 ? "disabled" : ""; } }
        public string nextPageClass { get { return PageIndex == TotalPagesCount ? "disabled" : ""; } }
        public string lastPageClass { get { return PageIndex == TotalPagesCount ? "disabled" : ""; } }


        public string pageNumebersDD
        {
            get
            {
                return (pageNumebersList != null && pageNumebersList.Count > 0)
                        ? string.Join("", pageNumebersList)
                        : "";
            }
        }

        private List<string> _pageNumebersList;
        public List<string> pageNumebersList
        {
            get
            {
                return GetPageNumberList(TotalPagesCount, PageIndex); 
                //if (_pageNumebersList == null || _pageNumebersList.Count <= 0)
                //{
                //    _pageNumebersList = GetPageNumberList(TotalPagesCount, PageIndex);
                //}

                //return _pageNumebersList;
            }
        }

        private List<string> GetPageNumberList(int totalPagesCount, int selectedValue)
        {
            List<string> lstPagesList = new List<string>();
            for (int i = 1; i <= totalPagesCount; i++)
            {
                if (selectedValue <= 0) selectedValue = 1;

                string selected = (i == selectedValue ? "selected=\"selected\"" : "");
                lstPagesList.Add($"<option value=\"{i}\" {selected}>{i}</option>");
            }
            return lstPagesList;
        }

    }
}
