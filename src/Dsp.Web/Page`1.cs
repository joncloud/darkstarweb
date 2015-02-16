using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Dsp.Web
{
    public class Page<T>
    {
        public Page(IList<T> results, int pageNumber, int resultsPerPage, long totalCount)
        {
            PageNumber = pageNumber;
            Results = new ReadOnlyCollection<T>(results);
            ResultsPerPage = resultsPerPage;
            TotalCount = totalCount;
        }

        public int PageNumber { get; private set; }
        public ReadOnlyCollection<T> Results { get; private set; }
        public int ResultsPerPage { get; private set; }
        public long TotalCount { get; private set; }

        public HttpResponseMessage ToMessage(Uri requestUri)
        {
            NameValueCollection queryArguments = HttpUtility.ParseQueryString(requestUri.Query);
            queryArguments.Remove("resultsPerPage");
            queryArguments.Add("resultsPerPage", ResultsPerPage.ToString());

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);

            string content = JsonConvert.SerializeObject(Results);

            message.Content = new StringContent(content);

            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            IEnumerable<string> linkValues = CreateLinkValues(requestUri, queryArguments, PageNumber, ResultsPerPage, TotalCount);
            foreach (string linkValue in linkValues)
            {
                message.Headers.Add("Link", linkValue);
            }

            return message;
        }

        private static string CreateLinkValue(Uri requestUri, NameValueCollection queryArguments, string relation, int pageNumber)
        { 
            queryArguments.Remove("pageNumber");
            queryArguments.Add("pageNumber", pageNumber.ToString());

            UriBuilder builder = new UriBuilder(requestUri);

            IEnumerable<string> pairs = queryArguments.AllKeys.SelectMany(key => queryArguments.GetValues(key)
                                                                                               .Select(val => string.Join("=", key, val)));

            builder.Query = string.Join("&", pairs);


            return string.Format(
                "<{0}> rel=\"{1}\"",
                builder,
                relation);
        }

        private static IEnumerable<string> CreateLinkValues(Uri requestUri, NameValueCollection queryArguments, int currentPageNumber, int resultsPerPage, long totalCount)
        {
            // Previous page
            if (currentPageNumber > 0)
            {
                yield return CreateLinkValue(requestUri, queryArguments, "Previous", currentPageNumber - 1);
            }

            // Next
            int nextPageNumber = currentPageNumber + 1;
            long nextPageIndex = (long)resultsPerPage * nextPageNumber;
            if (nextPageIndex < totalCount)
            {
                yield return CreateLinkValue(requestUri, queryArguments, "Next", nextPageNumber);
            }

            // Last
            int lastPageNumber = (int)(totalCount / resultsPerPage);
            if (nextPageNumber <= lastPageNumber)
            {
                yield return CreateLinkValue(requestUri, queryArguments, "Last", lastPageNumber);
            }
        }
    }
}