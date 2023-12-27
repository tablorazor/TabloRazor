using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloRazor.Components.Tables;
using TabloRazor.Components.Tables.Components;
using TailoredApps.Shared.Querying;

namespace TabloRazor.Table.WebApiDataProvider
{
    public class ListDataProvider<TResults, TRequest, TResponse, TQuery> : IDataProvider<TResults>
        where TResponse : IPagedResult<TResults>
        where TRequest : IPagedAndSortedRequest<TResponse, TQuery, TResults>
        where TQuery : QueryBase
    {
        private readonly IHttpClient httpClient;
        private readonly TRequest request;
        private readonly string baseUri;
        public ListDataProvider(IHttpClient httpClient, TRequest request, string baseUri)
        {
            this.httpClient = httpClient;
            this.request = request;
            this.baseUri = baseUri;
        }
        public async Task<IEnumerable<TableResult<object, TResults>>> GetData(List<IColumn<TResults>> columns, ITableState<TResults> state, IEnumerable<TResults> items, bool resetPage = false, bool addSorting = true, TResults moveToItem = default)
        {
            request.Count = state.PageSize;
            if (resetPage)
            {
                request.Page = 1;
                state.PageNumber = request.Page.Value - 1;
            }
            else if (state.TotalCount == 0)
            {
                state.PageNumber = 0;
                request.Page ??= 1;
            }
            else if ((state.TotalCount - 1) < state.PageSize * state.PageNumber)
            {
                state.PageNumber = (int)Math.Floor((decimal)(state.TotalCount / state.PageSize));
            }
            else if (moveToItem != null)
            {
                var pos = columns.First().Table.Items.ToList().IndexOf(moveToItem);
                if (pos > 0)
                {
                    state.PageNumber = (int)Math.Floor((decimal)(pos / state.PageSize));
                }
            }
            if (request.Page >= state.PageNumber)
            {
                request.Page = state.PageNumber + 1;
            }

            var sortColumn = columns.SingleOrDefault(z => z.SortColumn);
            if(sortColumn != null)
            {
                request.SortField = sortColumn.PropertyName;
                request.SortDir = sortColumn.SortDescending ? SortDirection.Desc : SortDirection.Asc;
            }
            var queryString = await request.ToQueryString();
            var call = await httpClient.Get<TResponse>(baseUri + "?"+ queryString,CancellationToken.None);
            state.TotalCount = call?.Result?.Count ?? 0;
            var results = call?.Result?.Results?.ToList() ?? new List<TResults>();
            List<TableResult<object, TResults>> result = new()
            {
                new TableResult<object,TResults>(null,results)
            };
            return result;
        }
    }
}
