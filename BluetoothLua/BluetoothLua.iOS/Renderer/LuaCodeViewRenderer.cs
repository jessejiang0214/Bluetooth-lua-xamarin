using System;
using BluetoothLua;
using CoreGraphics;
using CYRTextViewXamariniOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LuaCodeView), typeof(BluetoothLua.iOS.Renderer.LuaCodeViewRenderer))]
namespace BluetoothLua.iOS.Renderer
{
    public class LuaCodeViewRenderer : ViewRenderer<LuaCodeView, CYRTextView>
    {
        CYRTextView _cyrtTextView;
        UIFont _defaultFont;
        UIFont _boldFont;
        UIFont _italicFont;

        protected override void OnElementChanged(ElementChangedEventArgs<LuaCodeView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                _defaultFont = UIFont.SystemFontOfSize(14);
                _boldFont = UIFont.BoldSystemFontOfSize(14);
                _italicFont = UIFont.ItalicSystemFontOfSize(14);

                _cyrtTextView = new CYRTextView(new CGRect(0, 0, e.NewElement.Width, e.NewElement.Height));
                _cyrtTextView.Font = _defaultFont;
                _cyrtTextView.TextColor = UIColor.Black;
                _cyrtTextView.Tokens = SolverTokens();
                SetNativeControl(_cyrtTextView);
            }
            if (e.OldElement != null)
            {
                _cyrtTextView.Changed -= CyrtTextView_Changed;
            }
            if (e.NewElement != null)
            {
                _cyrtTextView.Text = this.Element.CodeText;
                _cyrtTextView.Changed += CyrtTextView_Changed;
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == LuaCodeView.CodeTextProperty.PropertyName)
            {
                if (!codeTextChanging && !string.IsNullOrEmpty(this.Element.CodeText))
                    _cyrtTextView.Text = this.Element.CodeText;
                codeTextChanging = false;
            }
        }

        bool codeTextChanging = false;
        void CyrtTextView_Changed(object sender, EventArgs e)
        {
            codeTextChanging = true;
            this.Element.CodeText = _cyrtTextView.Text;
        }

        CYRToken[] SolverTokens()
        {
            return new CYRToken[]{
                CYRToken.TokenWithName("special_numbers","[ʝ]",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(0,0,255))),
                CYRToken.TokenWithName("mod",@"\bmod\b",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(245, 0, 110))),
                CYRToken.TokenWithName("string",@"\"".*?(\""|$)",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(24, 110, 109))),
                CYRToken.TokenWithName("hex_1",@"\\$[\\d a-f]+",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(0, 0, 255))),
                CYRToken.TokenWithName("octal_1",@"&[0-7]+",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(0, 0, 255))),
                CYRToken.TokenWithName("binary_1",@"%[01]+",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(0, 0, 255))),
                CYRToken.TokenWithName("float",@"\\d+\\.?\\d+e[\\+\\-]?\\d+|\\d+\\.\\d+|∞",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(0, 0, 255))),
                CYRToken.TokenWithName("integer",@"\\d+",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(0, 0, 255))),
                CYRToken.TokenWithName("operator",@"[/\\*,\\;:=<>\\+\\-\\^!·≤≥]",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(245,0,110))),
                CYRToken.TokenWithName("round_brackets",@"[\\(\\)]",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(161,75,0))),
                CYRToken.TokenWithName("square_brackets",@"[\\[\\]]",new NSDictionary<NSString, NSObject> (new NSString[]{UIStringAttributeKey.ForegroundColor, UIStringAttributeKey.Font}, new NSObject[]{UIColor.FromRGB(105,0,0),_boldFont})),
                CYRToken.TokenWithName("absolute_brackets",@"[|]",new NSDictionary<NSString, NSObject> (UIStringAttributeKey.ForegroundColor,UIColor.FromRGB(104,0, 111))),
                CYRToken.TokenWithName("reserved_words",@"(and|break|do|else|elseif|end|false|for|function|if|in|local|nil|not|or|repeat|return|then|true|until|while)",new NSDictionary<NSString, NSObject> (new NSString[]{UIStringAttributeKey.ForegroundColor, UIStringAttributeKey.Font}, new NSObject[]{UIColor.FromRGB(104,0,111),_boldFont})),
                CYRToken.TokenWithName("commentbrackets",@"//.*",new NSDictionary<NSString, NSObject> (new NSString[]{UIStringAttributeKey.ForegroundColor, UIStringAttributeKey.Font}, new NSObject[]{UIColor.FromRGB(31,131,0),_italicFont})),
            };
        }

    }
}
