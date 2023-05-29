using Microsoft.AspNetCore.Components;

namespace TabloRazor.Components
{
    public interface ITab
    {
        RenderFragment ChildContent { get; }
    }
}
