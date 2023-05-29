using Microsoft.AspNetCore.Components;
using TabloRazor.Components.Tables;

namespace TabloRazor.Components
{
    public class AllColumnRowBase<TableItem> : ComponentBase
    {
        [CascadingParameter(Name = "Table")] public ITable<TableItem> Table { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string CssClass { get; set; }
    }
}