﻿using Microsoft.AspNetCore.Components;
using TabloRazor;

namespace TabloRazor
{
    public partial class PageMainTitle : TablerBaseComponent
    {
        [Parameter] public string HtmlTag { get; set; } = "h1";

        protected override string ClassNames => ClassBuilder
            .Add("page-title")
            .Add(BackgroundColor.GetColorClass("bg"))
            .Add(TextColor.GetColorClass("text"))
            .ToString();
    }
}