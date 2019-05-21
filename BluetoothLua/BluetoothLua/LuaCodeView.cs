using System;
using Xamarin.Forms;

namespace BluetoothLua
{
    public class LuaCodeView : View
    {
        public static readonly BindableProperty CodeTextProperty = BindableProperty.Create(
            propertyName: "CodeText",
            returnType: typeof(string),
            declaringType: typeof(LuaCodeView));

        public string CodeText
        {
            get { return (string)GetValue(CodeTextProperty); }
            set { SetValue(CodeTextProperty, value); }
        }
    }
}
