using Microsoft.AspNetCore.Components;
using TabloRazor.Components.Tables;

namespace TabloRazor.Components.Tables
{
    public class TableHeaderToolsBase<TableItem> : ComponentBase
    {
        [CascadingParameter(Name = "Table")] public ITable<TableItem> Table { get; set; }

    }
}