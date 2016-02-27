Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports GPRSComServer



'''<summary>
'''这是 _IItemTest 的测试类，旨在
'''包含所有 _IItemTest 单元测试
'''</summary>
<TestClass()> _
Public Class _IItemTest


    Private testContextInstance As TestContext

    '''<summary>
    '''获取或设置测试上下文，上下文提供
    '''有关当前测试运行及其功能的信息。
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "附加测试属性"
    '
    '编写测试时，还可使用以下属性:
    '
    '使用 ClassInitialize 在运行类中的第一个测试前先运行代码
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    '使用 ClassCleanup 在运行完类中的所有测试后再运行代码
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    '使用 TestInitialize 在运行每个测试前先运行代码
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    '使用 TestCleanup 在运行完每个测试后运行代码
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    Friend Overridable Function Create_IItem_Accessor() As _IItem_Accessor
        'TODO: 实例化相应的具体类。
        Dim target As _IItem_Accessor = Nothing
        Return target
    End Function

    '''<summary>
    '''ItemDataType 的测试
    '''</summary>
    <TestMethod(), _
     DeploymentItem("GPRSComServer.exe")> _
    Public Sub ItemDataTypeTest()
        Dim target As _IItem_Accessor = Create_IItem_Accessor ' TODO: 初始化为适当的值
        Dim expected As String = String.Empty ' TODO: 初始化为适当的值
        Dim actual As String
        target.ItemDataType = expected
        actual = target.ItemDataType
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("验证此测试方法的正确性。")
    End Sub
End Class
