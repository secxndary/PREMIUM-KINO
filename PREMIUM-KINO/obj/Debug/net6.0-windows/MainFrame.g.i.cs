﻿#pragma checksum "..\..\..\MainFrame.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E10CC5C0E61F3F6346821DE5F43C4056122A8D4A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Xpf.DXBinding;
using PREMIUM_KINO;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PREMIUM_KINO {
    
    
    /// <summary>
    /// MainFrameUser
    /// </summary>
    public partial class MainFrameUser : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 28 "..\..\..\MainFrame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid filterSearchPanel;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\MainFrame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock genText;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\MainFrame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox filterGenre;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\MainFrame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock searchText;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\MainFrame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchTextBox;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\MainFrame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView mainFilmsListView;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\MainFrame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button darkTheme;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\MainFrame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button usaButton;
        
        #line default
        #line hidden
        
        
        #line 167 "..\..\..\MainFrame.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button russainButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PREMIUM-KINO;component/mainframe.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainFrame.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.filterSearchPanel = ((System.Windows.Controls.Grid)(target));
            
            #line 29 "..\..\..\MainFrame.xaml"
            this.filterSearchPanel.KeyUp += new System.Windows.Input.KeyEventHandler(this.Grid_KeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.genText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.filterGenre = ((System.Windows.Controls.ComboBox)(target));
            
            #line 48 "..\..\..\MainFrame.xaml"
            this.filterGenre.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.filterGenre_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.searchText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.searchTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 74 "..\..\..\MainFrame.xaml"
            this.searchTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.searchTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.mainFilmsListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 8:
            this.darkTheme = ((System.Windows.Controls.Button)(target));
            
            #line 142 "..\..\..\MainFrame.xaml"
            this.darkTheme.Click += new System.Windows.RoutedEventHandler(this.darkTheme_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.usaButton = ((System.Windows.Controls.Button)(target));
            
            #line 161 "..\..\..\MainFrame.xaml"
            this.usaButton.Click += new System.Windows.RoutedEventHandler(this.buttonEng_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.russainButton = ((System.Windows.Controls.Button)(target));
            
            #line 178 "..\..\..\MainFrame.xaml"
            this.russainButton.Click += new System.Windows.RoutedEventHandler(this.buttonRu_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 7:
            
            #line 124 "..\..\..\MainFrame.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.buyTicket_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

