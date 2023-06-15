Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Threading

''' <summary>
''' 这是全局模组安装检查器的集成封装，只需要调用方法然后分析对象的数据然后根据你自己的设计反应到自己的UI上即可实现
''' </summary>
Public Class ModsGlobalCheck

    Public Structure Data_NoContentPackType
        Public UniqueID As String
        Public TargetUniqueID As String
    End Structure
    Public Structure Data_NoDependencyType
        Public UniqueID As String
        Public TargetUniqueID As String
    End Structure
    Public Structure Data_DependencyVersionLowType
        Public UniqueID As String
        Public TargetUniqueID As String
        Public MinimumVersion As String
    End Structure
    Public Structure Data_NeedUpdateSmapiType
        Public Name As String
        Public UniqueID As String
        Public MinimumApiVersion As String
    End Structure
    Public Structure Data_MultiLevelFolderType
        Public Name As String
        Public UniqueID As String
        Public Path As String
    End Structure

    ''' <summary>
    ''' 扫描到的 Name 键值，表示模组名称。Data_Name、Data_UniqueID、Data_UniqueIDVerison、Data_UniqueIDMinimumApiVersion 这四个数组的索引是互相对应的
    ''' </summary>
    Public Data_Name As String() = Array.Empty(Of String)()
    ''' <summary>
    ''' 扫描到的 UniqueID 键值，表示模组的 UniqueID。Data_Name、Data_UniqueID、Data_UniqueIDVerison、Data_UniqueIDMinimumApiVersion 这四个数组的索引是互相对应的
    ''' </summary>
    Public Data_UniqueID As String() = Array.Empty(Of String)()
    ''' <summary>
    ''' 扫描到的 Verison 键值，表示模组的版本号。Data_Name、Data_UniqueID、Data_UniqueIDVerison、Data_UniqueIDMinimumApiVersion 这四个数组的索引是互相对应的
    ''' </summary>
    Public Data_UniqueIDVerison As String() = Array.Empty(Of String)()
    ''' <summary>
    ''' 扫描到的 MinimumApiVersion 键值，表示模组的最低 SMAPI 版本号。Data_Name、Data_UniqueID、Data_UniqueIDVerison、Data_UniqueIDMinimumApiVersion 这四个数组的索引是互相对应的
    ''' </summary>
    Public Data_UniqueIDMinimumApiVersion As String() = Array.Empty(Of String)()

    ''' <summary>
    ''' 没有安装内容包依赖的模组
    ''' </summary>
    Public Data_NoContentPackMods As Data_NoContentPackType() = Array.Empty(Of Data_NoContentPackType)()
    ''' <summary>
    ''' 未安装其他依赖项的模组
    ''' </summary>
    Public Data_NoDependencyMods As Data_NoDependencyType() = Array.Empty(Of Data_NoDependencyType)()
    ''' <summary>
    ''' 已安装其他依赖项，但是已安装依赖项的版本过低的模组
    ''' </summary>
    Public Data_DependencyVersionLowMods As Data_DependencyVersionLowType() = Array.Empty(Of Data_DependencyVersionLowType)()
    ''' <summary>
    ''' 需要更新 SMAPI 的模组
    ''' </summary>
    Public Data_NeedUpdateSmapiMods As Data_NeedUpdateSmapiType() = Array.Empty(Of Data_NeedUpdateSmapiType)()
    ''' <summary>
    ''' 套娃放置的模组
    ''' </summary>
    Public Data_MultiLevelFolderMods As Data_MultiLevelFolderType() = Array.Empty(Of Data_MultiLevelFolderType)()
    ''' <summary>
    ''' 没有 UniqueID 的模组
    ''' </summary>
    Public Data_NoUniqueIDMods As String() = Array.Empty(Of String)()
    ''' <summary>
    ''' 无意义的文件夹，只要是经过循环检测之后没有找到 manifest.json 文件的文件夹全部是无意义文件夹
    ''' </summary>
    Public Data_MeaninglessFolder As String() = Array.Empty(Of String)()
    ''' <summary>
    ''' 无意义的文件，Mods 这一级目录下的所有文件（当然不包括子目录）全部是无意义文件
    ''' </summary>
    Public Data_MeaninglessFile As String() = Array.Empty(Of String)()

    Public Enum RealTimeOutputType
        ''' <summary>
        ''' 多行文本显示
        ''' </summary>
        MultiLine = 0
        ''' <summary>
        ''' 单行显示
        ''' </summary>
        Line = 1
    End Enum

    ''' <summary>
    ''' 设置实时信息输出的类型
    ''' </summary>
    Public SetRealTimeOutputType As RealTimeOutputType = RealTimeOutputType.MultiLine

    ''' <summary>
    ''' 开始进行全局模组安装检查
    ''' </summary>
    ''' <param name="ModsFolder">设置游戏 Mods 文件夹的路径，注意要指定到 Mods 文件夹，而不是游戏文件夹</param>
    ''' <param name="RealTimeOutput">可选。提供一个文本对象，一般是文本框的 Text 属性，程序会去改变对象的值</param>
    ''' <param name="SmapiVerison">可选。设置 SMAPI 版本号</param>
    ''' <param name="SystemPathConnector">可选。警告：！不要真TM给我省了，这是设置当前系统使用的路径分隔符，Windows 是斜杠，Android 是除号，设置的不对会造成数据计算全面爆炸</param>
    ''' <returns>如果执行完成，返回空字符串，如果发生错误，返回错误的描述</returns>
    Public Function StartCheck(ModsFolder As String, Optional ByRef RealTimeOutput As String = "", Optional SmapiVerison As String = "", Optional SystemPathConnector As String = "\") As String
        Try
            RealTimeOutput = ""
            If SetRealTimeOutputType = RealTimeOutputType.MultiLine Then
                RealTimeOutput &= "Scanning manifest files. . ." & vbCrLf
            Else
                RealTimeOutput = "Scanning manifest files. . ."
            End If
            Thread.CurrentThread.Join()
            Dim a1 As New SearchFile
            a1.SearchManifests(ModsFolder, True)
            If a1.ErrorString <> "" Then
                RealTimeOutput = a1.ErrorString
                Return a1.ErrorString
                Exit Function
            End If

            Dim ManifestsList As String() = a1.FileCollection

            If SetRealTimeOutputType = RealTimeOutputType.MultiLine Then
                RealTimeOutput &= "Reading manifest data. . ." & vbCrLf
            Else
                RealTimeOutput = "Reading manifest data. . ."
            End If
            Thread.CurrentThread.Join()
            For i = 0 To ManifestsList.Length - 1
                Dim a As String = FileIO.FileSystem.ReadAllText(FileIO.FileSystem.CombinePath(ModsFolder, ManifestsList(i)))
                Dim JsonData As Object = CType(JsonConvert.DeserializeObject(a), JObject)
                If JsonData.item("Name") IsNot Nothing Then
                    ReDim Preserve Data_Name(Data_Name.Length)
                    Data_Name(Data_Name.Length - 1) = JsonData.item("Name").ToString
                Else
                    Continue For
                End If
                If JsonData.item("UniqueID") IsNot Nothing Then
                    ReDim Preserve Data_UniqueID(Data_UniqueID.Length)
                    Data_UniqueID(Data_UniqueID.Length - 1) = JsonData.item("UniqueID").ToString
                Else
                    ReDim Preserve Data_NoUniqueIDMods(Data_NoUniqueIDMods.Length)
                    Data_NoUniqueIDMods(Data_NoUniqueIDMods.Length - 1) = ManifestsList(i)
                End If
                If JsonData.item("Version") IsNot Nothing Then
                    ReDim Preserve Data_UniqueIDVerison(Data_UniqueIDVerison.Length)
                    Data_UniqueIDVerison(Data_UniqueIDVerison.Length - 1) = JsonData.item("Version").ToString
                Else
                    ReDim Preserve Data_UniqueIDVerison(Data_UniqueIDVerison.Length)
                    Data_UniqueIDVerison(Data_UniqueIDVerison.Length - 1) = ""
                End If
                If JsonData.item("MinimumApiVersion") IsNot Nothing Then
                    ReDim Preserve Data_UniqueIDMinimumApiVersion(Data_UniqueIDMinimumApiVersion.Length)
                    Data_UniqueIDMinimumApiVersion(Data_UniqueIDMinimumApiVersion.Length - 1) = JsonData.item("MinimumApiVersion").ToString
                Else
                    ReDim Preserve Data_UniqueIDMinimumApiVersion(Data_UniqueIDMinimumApiVersion.Length)
                    Data_UniqueIDMinimumApiVersion(Data_UniqueIDMinimumApiVersion.Length - 1) = ""
                End If
                If ManifestsList(i).Split(SystemPathConnector).Length > 3 Then
                    Dim x1 As New Data_MultiLevelFolderType With {
                        .Name = Data_Name(i),
                        .UniqueID = Data_UniqueID(i),
                        .Path = IO.Path.GetDirectoryName(ManifestsList(i))
                    }
                    ReDim Preserve Data_MultiLevelFolderMods(Data_MultiLevelFolderMods.Length)
                    Data_MultiLevelFolderMods(Data_MultiLevelFolderMods.Length - 1) = x1
                End If
            Next


            If SmapiVerison <> "" Then
                If SetRealTimeOutputType = RealTimeOutputType.MultiLine Then
                    RealTimeOutput &= "Comparing SMAPI versions. . ." & vbCrLf
                Else
                    RealTimeOutput = "Comparing SMAPI versions. . ."
                End If
                Thread.CurrentThread.Join()
                For i = 0 To Data_UniqueID.Length - 1
                    If Data_UniqueIDMinimumApiVersion(i) <> "" Then
                        If SharedFunction.CompareVersion(Data_UniqueIDMinimumApiVersion(i), SmapiVerison) > 0 Then
                            Dim x1 As New Data_NeedUpdateSmapiType With {
                                .Name = Data_Name(i),
                                .UniqueID = Data_UniqueID(i),
                                .MinimumApiVersion = Data_UniqueIDMinimumApiVersion(i)
                             }
                            ReDim Preserve Data_NeedUpdateSmapiMods(Data_NeedUpdateSmapiMods.Length)
                            Data_NeedUpdateSmapiMods(Data_NeedUpdateSmapiMods.Length - 1) = x1
                        End If
                    End If
                Next
            End If

            If SetRealTimeOutputType = RealTimeOutputType.MultiLine Then
                RealTimeOutput &= "Checking dependencies. . ." & vbCrLf
            Else
                RealTimeOutput = "Checking dependencies. . ."
            End If
            Thread.CurrentThread.Join()
            For i = 0 To ManifestsList.Length - 1
                Dim a As String = FileIO.FileSystem.ReadAllText(FileIO.FileSystem.CombinePath(ModsFolder, ManifestsList(i)))
                Dim JsonData As Object = CType(JsonConvert.DeserializeObject(a), JObject)
                If JsonData.item("Name") Is Nothing Then Continue For
                If JsonData.item("UniqueID") Is Nothing Then Continue For
                If JsonData.item("ContentPackFor") Is Nothing Then GoTo jx1
                If JsonData.item("ContentPackFor").item("UniqueID") Is Nothing Then GoTo jx1
                Dim ContentPackString As String = JsonData.item("ContentPackFor").item("UniqueID").ToString
                For i3 = 0 To Data_UniqueID.Length - 1
                    If Data_UniqueID(i3).ToLower = ContentPackString.ToLower Then GoTo jx1
                Next
                Dim x1 As New Data_NoContentPackType With {
                    .UniqueID = JsonData.item("UniqueID").ToString,
                    .TargetUniqueID = ContentPackString
                }
                ReDim Preserve Data_NoContentPackMods(Data_NoContentPackMods.Length)
                Data_NoContentPackMods(Data_NoContentPackMods.Length - 1) = x1
jx1:
                If JsonData.item("Dependencies") Is Nothing Then Continue For
                For i5 = 0 To JsonData.item("Dependencies").Length - 1
                    If JsonData.item("Dependencies").item(i5)("UniqueID") Is Nothing Then GoTo jx2
                    If JsonData.item("Dependencies").item(i5)("IsRequired") IsNot Nothing Then
                        Select Case Replace(JsonData.item("Dependencies").item(i5)("IsRequired").ToString.ToLower, " ", "")
                            Case "false"
                                Continue For
                        End Select
                    End If
                    Dim DependenciesString As String = JsonData.item("Dependencies").item(i5)("UniqueID").ToString
                    For i7 = 0 To Data_UniqueID.Length - 1
                        If Data_UniqueID(i7).ToLower = DependenciesString.ToLower Then
                            If JsonData.item("Dependencies").item(i5)("MinimumVersion") IsNot Nothing Then
                                Dim DependenciesMinimumVersionString As String = JsonData.item("Dependencies").item(i5)("MinimumVersion").ToString
                                If SharedFunction.CompareVersion(Data_UniqueIDVerison(i7), DependenciesMinimumVersionString) < 0 Then
                                    Dim v1 As New Data_DependencyVersionLowType With {
                                        .UniqueID = JsonData.item("Dependencies").item(i5)("UniqueID").ToString,
                                        .TargetUniqueID = DependenciesString,
                                        .MinimumVersion = DependenciesMinimumVersionString
                                    }
                                    ReDim Preserve Data_DependencyVersionLowMods(Data_DependencyVersionLowMods.Length)
                                    Data_DependencyVersionLowMods(Data_DependencyVersionLowMods.Length - 1) = v1
                                End If
                            Else
                                GoTo jx3
                            End If
                            GoTo jx3
                        End If
                    Next
                    Dim x2 As New Data_NoDependencyType With {
                        .UniqueID = JsonData.item("UniqueID").ToString,
                        .TargetUniqueID = DependenciesString
                    }
                    ReDim Preserve Data_NoDependencyMods(Data_NoDependencyMods.Length)
                    Data_NoDependencyMods(Data_NoDependencyMods.Length - 1) = x2
jx3:
                Next
jx2:
            Next

            If SetRealTimeOutputType = RealTimeOutputType.MultiLine Then
                RealTimeOutput &= "Scanning meaningless folders. . ." & vbCrLf
            Else
                RealTimeOutput = "Scanning meaningless folders. . ."
            End If
            Thread.CurrentThread.Join()
            Dim p1 As String() = SharedFunction.SearchFolderWithoutSub(ModsFolder)
            Dim p1s1 As Boolean()
            ReDim p1s1(p1.Length - 1)
            For i = 0 To ManifestsList.Length - 1
                For i3 = 0 To p1.Length - 1
                    If InStr(ManifestsList(i3), p1(i3)) = 1 And ManifestsList(i3) <> p1(i3) Then
                        p1s1(i3) = True
                        GoTo jx5
                    End If
                Next
jx5:
            Next
            For i = 0 To p1s1.Length - 1
                If p1s1(i) = False Then
                    ReDim Preserve Data_MeaninglessFolder(Data_MeaninglessFolder.Length)
                    Data_MeaninglessFolder(Data_MeaninglessFolder.Length - 1) = p1(i)
                End If
            Next

            If SetRealTimeOutputType = RealTimeOutputType.MultiLine Then
                RealTimeOutput &= "Scanning meaningless files. . ." & vbCrLf
            Else
                RealTimeOutput = "Scanning meaningless files. . ."
            End If
            Thread.CurrentThread.Join()
            Dim sf1 As New SearchFile
            sf1.SearchFiles(ModsFolder, False)
            If sf1.ErrorString <> "" Then
                If SetRealTimeOutputType = RealTimeOutputType.MultiLine Then
                    RealTimeOutput &= "An error occurred while scanning meaningless files: " & sf1.ErrorString & vbNewLine
                Else
                    RealTimeOutput = "An error occurred while scanning meaningless files: " & sf1.ErrorString
                End If
                Thread.CurrentThread.Join()
                GoTo ed1
            End If
            For i = 0 To sf1.FileCollection.Length - 1
                ReDim Preserve Data_MeaninglessFile(Data_MeaninglessFile.Length)
                Data_MeaninglessFile(Data_MeaninglessFile.Length - 1) = sf1.FileCollection(i)
            Next
ed1:
            If SetRealTimeOutputType = RealTimeOutputType.MultiLine Then
                RealTimeOutput &= "Done."
            Else
                RealTimeOutput = "Done."
            End If
            Thread.CurrentThread.Join()
            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function






End Class
