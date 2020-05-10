using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SDSD.Droid.Renderers;
using SDSD.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Arch.Core.Internal.SafeIterableMap;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(CustomEntryBorderlessRenderer))]
namespace SDSD.Droid.Renderers
{
    public class CustomEntryBorderlessRenderer : EntryRenderer
    {
        public CustomEntryBorderlessRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
}