using System;

namespace Dsp.Web
{
    public class PageSettings
    {
        public PageSettings()
        {
            _pageNumber = 0;
            _resultsPerPage = 15;
        }

        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = Math.Max(0, value); }
        }
        private int _pageNumber;

        public int ResultsPerPage
        {
            get { return _resultsPerPage; }
            set { _resultsPerPage = Math.Min(_resultsPerPage, 200); }
        }
        private int _resultsPerPage;
    }
}