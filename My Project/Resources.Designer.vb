﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.3655
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("T4e_ECU_Editor.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        Friend ReadOnly Property binary_open_icon() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("binary-open-icon", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property cal_open_icon() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("cal-open-icon", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property calculator_icon() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("calculator_icon", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property chart() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("chart", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property colorgradient() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("colorgradient", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property light_bulb_off() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("light_bulb_off", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property light_bulb_on() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("light_bulb_on", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property plug_connect_icon() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("plug-connect-icon", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property plug_disconnect_icon() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("plug-disconnect-icon", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property reload_icon() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("reload_icon", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property seedstudio_can_shield() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("seedstudio_can_shield", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property sparkfun_can_shield() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("sparkfun_can_shield", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property t4e_ecu_editor_logo() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("t4e_ecu_editor_logo", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
    End Module
End Namespace