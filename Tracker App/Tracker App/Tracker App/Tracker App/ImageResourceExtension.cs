using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tracker_App
{
    [ContentProperty (nameof(Source))]
    class ImageResourceExtension : IMarkupExtension
    {
           public string Source { get; set; }
    }

    internal interface IMarkupExtension
    {
    }
}
