using System;
using BluetoothLua;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(LuaCodeView), typeof(BluetoothLua.Droid.Renderer.LuaCodeViewRenderer))]
namespace BluetoothLua.Droid.Renderer
{
    public class LuaCodeViewRenderer : ViewRenderer<LuaCodeView, CodeView>
    {

        CodeView _codeView;
        Context _context;
        public LuaCodeViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<LuaCodeView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                _codeView = new CodeView(_context);
                _codeView.SetShowLineNumber(true);
                _codeView.SetLanguage(Language.Lua);
                SetNativeControl(_codeView);
            }
            if (e.OldElement != null)
            {
                _codeView.KeyPress -= CodeView_KeyPress;
            }
            if (e.NewElement != null)
            {
                _codeView.KeyPress += CodeView_KeyPress;
                _codeView.SetCode(this.Element.CodeText);
                _codeView.Apply();
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == LuaCodeView.CodeTextProperty.PropertyName)
            {
                if (!codeTextChanging)
                    _codeView.SetCode(this.Element.CodeText);
                codeTextChanging = false;
            }
        }

        bool codeTextChanging = false;
        void CodeView_KeyPress(object sender, KeyEventArgs e)
        {
            codeTextChanging = true;
            this.Element.CodeText = _codeView.Code;
        }

    }
}
