using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace TabloRazor.Services
{
    public class TablerService
    {
        private readonly IJSRuntime jsRuntime;

        public TablerService(IJSRuntime jSRuntime)
        {
            this.jsRuntime = jSRuntime;
        }

        public async Task SetTheme(bool darkTheme)
        {
            var theme = "";
            if (darkTheme)
            {
                theme = "dark";
            }

            await jsRuntime.InvokeVoidAsync("TabloRazor.setTheme", theme);
        }

        public async Task SaveAsBinary(string fileName, string contentType, byte[] content)
        {
            await jsRuntime.InvokeVoidAsync("TabloRazor.saveAsBinary", fileName, contentType, content);
        }

        public async Task SaveAsFile(string fileName, string href)
        {
            await jsRuntime.InvokeVoidAsync("TabloRazor.saveAsFile", fileName, href);
        }

        public async Task PreventDefaultKey(ElementReference element, string eventName, string[] keys)
        {
            await jsRuntime.InvokeVoidAsync("TabloRazor.preventDefaultKey", element, eventName, keys);
        }

        public async Task FocusFirstInTableRow(ElementReference tableRow)
        {
            await jsRuntime.InvokeVoidAsync("TabloRazor.focusFirstInTableRow", tableRow, "");
        }

        public async Task NavigateTable(ElementReference tableCell, string key)
        {
            await jsRuntime.InvokeVoidAsync("TabloRazor.navigateTable", tableCell, key);
        }

        public async Task ScrollToFragment(string fragmentId)
        {
            await jsRuntime.InvokeVoidAsync("TabloRazor.scrollToFragment", fragmentId);
        }

        public async Task ShowAlert(string message)
        {
            await jsRuntime.InvokeVoidAsync("TabloRazor.showAlert", message);
        }

        public async Task CopyToClipboard(string text)
        {
            await jsRuntime.InvokeVoidAsync("TabloRazor.copyToClipboard", text);
        }

        public async Task<string> ReadFromClipboard()
        {
            return await jsRuntime.InvokeAsync<string>("TabloRazor.readFromClipboard");
        }

        public async Task DisableDraggable(ElementReference container, ElementReference element)
        {
            await jsRuntime.InvokeVoidAsync("TabloRazor.disableDraggable", container, element);
        }

        public async Task SetElementProperty(ElementReference element, string property, object value)
        {
            await jsRuntime.InvokeVoidAsync("TabloRazor.setPropByElement", element, property, value);
        }

    }


}
