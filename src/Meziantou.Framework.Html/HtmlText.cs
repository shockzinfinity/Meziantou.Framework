﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Meziantou.Framework.Html
{
    [DebuggerDisplay("'{Value}'")]
    public class HtmlText : HtmlNode
    {
        private string _value;
        private bool _cData;

        protected internal HtmlText(HtmlDocument ownerDocument)
            : base(string.Empty, "#text", string.Empty, ownerDocument)
        {
        }

        [Browsable(false)]
        public override HtmlAttributeList Attributes
        {
            get
            {
                return base.Attributes;
            }
        }

        [Browsable(false)]
        public override HtmlNodeList ChildNodes
        {
            get
            {
                return base.ChildNodes;
            }
        }

        public override HtmlNodeType NodeType
        {
            get
            {
                return HtmlNodeType.Text;
            }
        }

        public virtual bool IsWhitespace
        {
            get
            {
                return string.IsNullOrWhiteSpace(Value);
            }
        }

        public virtual bool IsCData
        {
            get
            {
                return _cData;
            }
            set
            {
                if (value != _cData)
                {
                    _cData = value;
                    OnPropertyChanged();
                }
            }
        }

        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                // do nothing
            }
        }

        public override string InnerText
        {
            get
            {
                return Value;
            }
            set
            {
                if (value != Value)
                {
                    Value = value;
                    OnPropertyChanged();
                }
            }
        }

        public override string InnerHtml
        {
            get
            {
                return Value;
            }

            set
            {
                if (value != Value)
                {
                    Value = value;
                    OnPropertyChanged();
                }
            }
        }

        public override string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value != _value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        public override void WriteTo(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (IsCData)
            {
                writer.Write("<![CDATA[");
                writer.Write(Value);
                writer.Write("]]>");
            }
            else
            {
                writer.Write(Value);
            }
        }

        public override void WriteContentTo(TextWriter writer)
        {
        }

        public override void WriteTo(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (IsCData)
            {
                writer.WriteCData(Value);
            }
            else if (IsWhitespace)
            {
                writer.WriteWhitespace(Value);
            }
            else
            {
                writer.WriteString(Value);
            }
        }

        public override void WriteContentTo(XmlWriter writer)
        {
        }

        public override void CopyTo(HtmlNode target, HtmlCloneOptions options)
        {
            base.CopyTo(target, options);
            var text = (HtmlText)target;
            text._cData = _cData;
            text._value = _value;
        }
    }
}
