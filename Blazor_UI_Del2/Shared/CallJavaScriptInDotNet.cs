using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor_UI_Del2.Shared
{
    public partial class CallJavaScriptInDotNet
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        private IJSObjectReference _jsModule;
        public async Task ShowAlertWindow()
        {
            await _jsModule.InvokeVoidAsync("showAlert", "JS function called from .NET");
        }
    }
}
