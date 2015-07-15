using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace QLIB.Web
{
    /// <summary>
    /// System.Windows.Forms.WebBrowser 的增强版本，可以拦截浏览前的事件
    /// </summary>
    public partial class WebBrowserEx : System.Windows.Forms.WebBrowser
    {

        private SHDocVw.IWebBrowser2 Iwb2;



        protected override void AttachInterfaces(object nativeActiveXObject)
        {

            Iwb2 = (SHDocVw.IWebBrowser2)nativeActiveXObject;

            Iwb2.Silent = true;

            base.AttachInterfaces(nativeActiveXObject);

        }



        protected override void DetachInterfaces()
        {

            Iwb2 = null;

            base.DetachInterfaces();

        }



        System.Windows.Forms.AxHost.ConnectionPointCookie cookie;

        WebBrowserExtendedEvents events;



        //This method will be called to give you a chance to create your own event sink

        protected override void CreateSink()
        {

            //MAKE SURE TO CALL THE BASE or the normal events won't fire

            base.CreateSink();

            events = new WebBrowserExtendedEvents(this);

            cookie = new System.Windows.Forms.AxHost.ConnectionPointCookie(this.ActiveXInstance, events, typeof(DWebBrowserEvents2));

        }



        protected override void DetachSink()
        {

            if (null != cookie)
            {

                cookie.Disconnect();

                cookie = null;

            }

            base.DetachSink();

        }



        //This new event will fire when the page is navigating

        public event EventHandler BeforeNavigate;

        /// <summary>

        /// 可用于替代原来的NewWindow事件，新增了事件的Url参数支持。

        /// </summary>

        [CategoryAttribute("操作"), DescriptionAttribute("经过扩展的NewWindow事件，使用继承后的WebBrowserExtendedNavigatingEventArgs类型参数实现Url参数支持")]

        public event EventHandler BeforeNewWindow;



        protected void OnBeforeNewWindow(string url, out bool cancel)
        {

            EventHandler h = BeforeNewWindow;

            WebBrowserExtendedNavigatingEventArgs args = new WebBrowserExtendedNavigatingEventArgs(url, null);

            if (null != h)
            {

                h(this, args);

            }

            cancel = args.Cancel;

        }

        protected void OnBeforeNavigate(string url, string frame, out bool cancel)
        {
            EventHandler h = BeforeNavigate;

            WebBrowserExtendedNavigatingEventArgs args = new WebBrowserExtendedNavigatingEventArgs(url, frame);

            if (null != h)
            {

                h(this, args);

            }

            //Pass the cancellation chosen back out to the events

            cancel = args.Cancel;

        }



        //This class will capture events from the WebBrowser

        class WebBrowserExtendedEvents : System.Runtime.InteropServices.StandardOleMarshalObject, DWebBrowserEvents2
        {

            WebBrowserEx _Browser;

            public WebBrowserExtendedEvents(WebBrowserEx browser) { _Browser = browser; }



            //Implement whichever events you wish

            public void BeforeNavigate2(object pDisp, ref object URL, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
            {

                _Browser.OnBeforeNavigate((string)URL, (string)targetFrameName, out cancel);

            }



            public void NewWindow3(object pDisp, ref bool cancel, ref object flags, ref object URLContext, ref object URL)
            {

                _Browser.OnBeforeNewWindow((string)URL, out cancel);

            }



        }



        [System.Runtime.InteropServices.ComImport(), System.Runtime.InteropServices.Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"),

        System.Runtime.InteropServices.InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch),

        System.Runtime.InteropServices.TypeLibType(System.Runtime.InteropServices.TypeLibTypeFlags.FHidden)]

        public interface DWebBrowserEvents2
        {



            [System.Runtime.InteropServices.DispId(250)]

            void BeforeNavigate2(

                [System.Runtime.InteropServices.In,

                System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pDisp,

                [System.Runtime.InteropServices.In] ref object URL,

                [System.Runtime.InteropServices.In] ref object flags,

                [System.Runtime.InteropServices.In] ref object targetFrameName, [System.Runtime.InteropServices.In] ref object postData,

                [System.Runtime.InteropServices.In] ref object headers,

                [System.Runtime.InteropServices.In,

                System.Runtime.InteropServices.Out] ref bool cancel);

            [System.Runtime.InteropServices.DispId(273)]

            void NewWindow3(

                [System.Runtime.InteropServices.In,

                System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pDisp,

                [System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out] ref bool cancel,

                [System.Runtime.InteropServices.In] ref object flags,

                [System.Runtime.InteropServices.In] ref object URLContext,

                [System.Runtime.InteropServices.In] ref object URL);

        }

    }



    public class WebBrowserExtendedNavigatingEventArgs : CancelEventArgs
    {

        private string _Url;

        public string Url
        {

            get { return _Url; }

        }



        private string _Frame;

        public string Frame
        {

            get { return _Frame; }

        }



        public WebBrowserExtendedNavigatingEventArgs(string url, string frame)

            : base()
        {

            _Url = url;

            _Frame = frame;

        }

    }

}