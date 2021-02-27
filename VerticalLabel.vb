Imports System.ComponentModel

<ToolboxBitmap(GetType(VerticalLabel), "Varticallabel.ico")> _
Public Class VerticalLabel

    'Since we are not using the additional resources/capabilities of 
    'UserControl we will inherit from Control instead to save overhead
    'Inherits System.Windows.Forms.UserControl
    Inherits System.Windows.Forms.Control

    Private labelText As String
#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer. 
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        '
        'VerticalLabel
        '
        Me.Size = New System.Drawing.Size(24, 100)
    End Sub
#End Region

    Protected Overrides Sub OnPaint(ByVal e As  _
              System.Windows.Forms.PaintEventArgs)
        Dim sngControlWidth As Single
        Dim sngControlHeight As Single
        Dim sngTransformX As Single
        Dim sngTransformY As Single
        Dim labelColor As Color
        Dim labelBorderPen As New Pen(labelColor, 0)
        Dim labelBackColorBrush As New SolidBrush(labelColor)
        Dim labelForeColorBrush As New SolidBrush(MyBase.ForeColor)
        MyBase.OnPaint(e)
        sngControlWidth = Me.Size.Width
        sngControlHeight = Me.Size.Height
        e.Graphics.DrawRectangle(labelBorderPen, 0, 0, _
                   sngControlWidth, sngControlHeight)
        e.Graphics.FillRectangle(labelBackColorBrush, 0, _
                   0, sngControlWidth, sngControlHeight)
        ' set the translation point for the 
        ' graphics object - the new (0,0) location
        sngTransformX = 0
        sngTransformY = sngControlHeight
        ' translate the origin used for rotation and drawing 
        e.Graphics.TranslateTransform(sngTransformX, _
                            sngTransformY) ' (0, textwidth)
        'set the rotation angle for vertical text
        e.Graphics.RotateTransform(270)
        ' draw the text on the control
        e.Graphics.DrawString(labelText, Font, _
                   labelForeColorBrush, 0, 0)
    End Sub

    Private Sub VTextBox_Resize(ByVal sender As Object, _
            ByVal e As System.EventArgs) Handles MyBase.Resize
        Invalidate()
    End Sub

    <Category("Verticallabel"), _
    Description("Text is displayed vertiaclly in container")> _
    Public Overrides Property Text() As String
        Get
            Return labelText
        End Get
        Set(ByVal Value As String)
            labelText = Value
            Invalidate()
        End Set
    End Property


End Class
