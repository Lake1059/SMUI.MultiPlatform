

Public Class TaskQueue

    Public ItemPath As String
    Public GamePath As String
    Public GameBackupPath As String

    Public Task_Code As Objects.CDTask()
    Public Task_Parameter1 As String()
    Public Task_Parameter2 As String()
    Public Code_LineMark As Integer()


    Public Sub Reset()
        Task_Code = Array.Empty(Of Objects.CDTask)()
        Task_Parameter1 = Array.Empty(Of String)()
        Task_Parameter2 = Array.Empty(Of String)()
        Code_LineMark = Array.Empty(Of Integer)()
    End Sub

    Public Function LoadCode() As String
        Try
            If FileIO.FileSystem.DirectoryExists(ItemPath) = False Then
                Return "[SMUI CORE ERROR] Item path does not exist." : Exit Function
            End If
            If FileIO.FileSystem.FileExists(FileIO.FileSystem.CombinePath(ItemPath, "Code")) = False Then
                Return "[SMUI CORE ERROR] This item do not have a Code file." : Exit Function
            End If
            Reset()
            Dim line As String() = FileIO.FileSystem.ReadAllText(FileIO.FileSystem.CombinePath(ItemPath, "Code")).Split(vbCrLf)

            For i = 0 To line.Length - 1
                If Replace(line(i), " ", "") Is Nothing Then Continue For
                Select Case Replace(line(i), " ", "").ToUpper
                    Case "CDCD", "CDCP"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.CDCD
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 1
                    Case "CDGCD"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.CDGCD
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = line(i + 2)
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 2
                    Case "CDMAD"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.CDMAD
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 1
                    Case "CDGRF"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.CDGRF
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = line(i + 2)
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 2
                    Case "CDGCF"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.CDGCF
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = line(i + 2)
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 2
                    Case "CDF"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.CDF
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = line(i + 2)
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 2
                    Case "CDVD", "CDCC"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.CDVD
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = ""
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                    Case "RQ-D"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.RQ_D
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 1
                    Case "RQ-D-IN"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.RQ_D_IN
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 1
                    Case "RQ-D-UN"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.RQ_D_UN
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 1
                    Case "RQ-F"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.RQ_F
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 1
                    Case "RQ-F-IN"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.RQ_F_IN
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 1
                    Case "RQ-F-UN"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.RQ_F_UN
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = line(i + 1)
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i += 1
                    Case "CR-UN-OFF"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.CR_UN_OFF
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = ""
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                    Case "CR-CG-DB"
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.CR_CG_DB
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Task_Parameter1(Task_Parameter1.Length - 1) = ""
                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                    Case "SUBD-EX-IN"   'SUB D-EX-IN
                        ReDim Preserve Task_Code(Task_Code.Length)
                        Task_Code(Task_Code.Length - 1) = Objects.CDTask.SUB_D_EX_IN
                        ReDim Preserve Task_Parameter1(Task_Parameter1.Length)
                        Dim x1 As Integer = i + 1
                        For x1 = i + 1 To line.Length - 1
                            If Replace(line(x1), " ", "") Is Nothing Then Continue For
                            If Replace(line(x1), " ", "").ToUpper = "ENDSUB" Then
                                i = x1
                                Exit For
                            Else
                                If Task_Parameter1(Task_Parameter1.Length - 1) = "" Then
                                    Task_Parameter1(Task_Parameter1.Length - 1) = line(x1)
                                Else
                                    Task_Parameter1(Task_Parameter1.Length - 1) = Task_Parameter1(Task_Parameter1.Length - 1) & "|" & line(x1)
                                End If
                            End If

                            If x1 = line.Length - 1 And Replace(line(x1), " ", "").ToUpper <> "ENDSUB" Then
                                ReDim Preserve Code_LineMark(Code_LineMark.Length)
                                Code_LineMark(Code_LineMark.Length - 1) = i + 1
                                Return DYString.SUBWithOutEndSub
                                Exit Function
                            End If
                        Next

                        ReDim Preserve Task_Parameter2(Task_Parameter2.Length)
                        Task_Parameter2(Task_Parameter2.Length - 1) = ""
                        ReDim Preserve Code_LineMark(Code_LineMark.Length)
                        Code_LineMark(Code_LineMark.Length - 1) = i + 1
                        i = x1
                End Select
            Next
            Return ""
        Catch ex As Exception
            Reset()
            Return ex.Message
        End Try
    End Function

    Private CRCGDB As Boolean = False

    Public Function PerformInstall(ByVal TaskIndex As Integer) As String
        Try
            Select Case Task_Code(TaskIndex)
                Case Objects.CDTask.CR_CG_DB
                    CRCGDB = True
                Case Objects.CDTask.CDCD
                    FileIO.FileSystem.CopyDirectory(FileIO.FileSystem.CombinePath(ItemPath, Task_Parameter1(TaskIndex)), FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GamePath, "Mods"), Task_Parameter1(TaskIndex)), True)
                    If CRCGDB = False Then
                        Dim a1 As String = FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(ItemPath, ".config"), Task_Parameter1(TaskIndex)), "config.json")
                        Dim a2 As String = FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GamePath, "Mods"), Task_Parameter1(TaskIndex)), "config.json")
                        If FileIO.FileSystem.FileExists(a1) = True Then
                            FileIO.FileSystem.CopyFile(a1, a2, True)
                        End If
                    End If

                Case Objects.CDTask.CDGCD
                    FileIO.FileSystem.CopyDirectory(FileIO.FileSystem.CombinePath(ItemPath, Task_Parameter1(TaskIndex)), FileIO.FileSystem.CombinePath(GamePath, Task_Parameter2(TaskIndex)), True)
                Case Objects.CDTask.CDMAD
                    If FileIO.FileSystem.DirectoryExists(FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GamePath, "Mods"), Task_Parameter1(TaskIndex))) = False Then
                        Return DYString.CanNotFoundFolderInMods & "\" & Task_Parameter1(TaskIndex)
                        Exit Function
                    Else
                        FileIO.FileSystem.CopyDirectory(FileIO.FileSystem.CombinePath(ItemPath, Task_Parameter1(TaskIndex)), FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GamePath, "Mods"), Task_Parameter1(TaskIndex)), True)
                    End If
                Case Objects.CDTask.CDGRF
                    FileIO.FileSystem.CopyFile(FileIO.FileSystem.CombinePath(ItemPath, Task_Parameter1(TaskIndex)), FileIO.FileSystem.CombinePath(GamePath, Task_Parameter2(TaskIndex)), True)
                Case Objects.CDTask.CDGCF
                    FileIO.FileSystem.CopyFile(FileIO.FileSystem.CombinePath(ItemPath, Task_Parameter1(TaskIndex)), FileIO.FileSystem.CombinePath(GamePath, Task_Parameter2(TaskIndex)), True)

                Case Objects.CDTask.CDF
                    FileIO.FileSystem.CopyFile(FileIO.FileSystem.CombinePath(ItemPath, Task_Parameter1(TaskIndex)), FileIO.FileSystem.CombinePath(GamePath, Task_Parameter2(TaskIndex)), True)

                Case Objects.CDTask.CDVD
                    FileIO.FileSystem.CopyDirectory(FileIO.FileSystem.CombinePath(ItemPath, "Content"), FileIO.FileSystem.CombinePath(GamePath, "Content"), True)

                Case Objects.CDTask.RQ_D, Objects.CDTask.RQ_D_IN
                    If FileIO.FileSystem.DirectoryExists(FileIO.FileSystem.CombinePath(GamePath, Task_Parameter1(TaskIndex))) = False Then
                        Return DYString.RequiredFolderNotExist & Task_Parameter1(TaskIndex)
                        Exit Function
                    End If

                Case Objects.CDTask.RQ_F, Objects.CDTask.RQ_F_IN
                    If FileIO.FileSystem.FileExists(FileIO.FileSystem.CombinePath(GamePath, Task_Parameter1(TaskIndex))) = False Then
                        Return DYString.RequiredFileNotExist & Task_Parameter1(TaskIndex)
                        Exit Function
                    End If

                Case Objects.CDTask.SUB_D_EX_IN
                    Dim x1 As String() = Task_Parameter1(TaskIndex).Split("|")
                    For y = 0 To x1.Length - 1
                        If FileIO.FileSystem.DirectoryExists(FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GamePath, "Mods"), x1(y))) = True Then
                            Return DYString.DefinedFolderExclude & x1(y)
                            Exit Function
                        End If
                    Next

            End Select

            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function


    Public Function PerformUnInstall(ByVal TaskIndex As Integer) As String
        Try
            Select Case Task_Code(TaskIndex)
                Case Objects.CDTask.CR_CG_DB
                    CRCGDB = True

                Case Objects.CDTask.CDCD
                    If CRCGDB = False Then
                        Dim a1 As String = FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(ItemPath, ".config"), Task_Parameter1(TaskIndex)), "config.json")
                        Dim a2 As String = FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GamePath, "Mods"), Task_Parameter1(TaskIndex)), "config.json")
                        If FileIO.FileSystem.FileExists(a2) = True Then
                            FileIO.FileSystem.CopyFile(a2, a1, True)
                        End If
                    End If
                    FileIO.FileSystem.DeleteDirectory(FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GamePath, "Mods"), Task_Parameter1(TaskIndex)), FileIO.DeleteDirectoryOption.DeleteAllContents)

                Case Objects.CDTask.CDGCD
                    FileIO.FileSystem.DeleteDirectory(FileIO.FileSystem.CombinePath(GamePath, Task_Parameter2(TaskIndex)), FileIO.DeleteDirectoryOption.DeleteAllContents)
                Case Objects.CDTask.CDMAD

                Case Objects.CDTask.CDGRF
                    If FileIO.FileSystem.FileExists(FileIO.FileSystem.CombinePath(GameBackupPath, Task_Parameter2(TaskIndex))) = True Then
                        FileIO.FileSystem.CopyFile(FileIO.FileSystem.CombinePath(GameBackupPath, Task_Parameter2(TaskIndex)), FileIO.FileSystem.CombinePath(GamePath, Task_Parameter2(TaskIndex)), True)
                    Else
                        FileIO.FileSystem.DeleteFile(FileIO.FileSystem.CombinePath(GamePath, Task_Parameter2(TaskIndex)))
                    End If
                Case Objects.CDTask.CDGCF
                    FileIO.FileSystem.DeleteFile(FileIO.FileSystem.CombinePath(GamePath, Task_Parameter2(TaskIndex)))
                Case Objects.CDTask.CDF
                    If FileIO.FileSystem.FileExists(FileIO.FileSystem.CombinePath(GameBackupPath, Task_Parameter2(TaskIndex))) = True Then
                        FileIO.FileSystem.CopyFile(FileIO.FileSystem.CombinePath(GameBackupPath, Task_Parameter2(TaskIndex)), FileIO.FileSystem.CombinePath(GamePath, Task_Parameter2(TaskIndex)), True)
                    Else
                        FileIO.FileSystem.DeleteFile(FileIO.FileSystem.CombinePath(GamePath, Task_Parameter2(TaskIndex)))
                    End If
                Case Objects.CDTask.CDVD
                    Dim a As String = IO.Path.GetFileName(ItemPath)
                    Dim b As String = IO.Path.GetFileName(IO.Path.GetDirectoryName(ItemPath))
                    UninstallCDVD(FileIO.FileSystem.CombinePath(ItemPath, "Content"), b, a)

                Case Objects.CDTask.RQ_D, Objects.CDTask.RQ_D_UN
                    If FileIO.FileSystem.DirectoryExists(FileIO.FileSystem.CombinePath(GamePath, Task_Parameter1(TaskIndex))) = False Then
                        Return DYString.RequiredFolderNotExist & Task_Parameter1(TaskIndex)
                        Exit Function
                    End If

                Case Objects.CDTask.RQ_F, Objects.CDTask.RQ_F_UN
                    If FileIO.FileSystem.FileExists(FileIO.FileSystem.CombinePath(GamePath, Task_Parameter1(TaskIndex))) = False Then
                        Return DYString.RequiredFileNotExist & Task_Parameter1(TaskIndex)
                        Exit Function
                    End If

                Case Objects.CDTask.CR_UN_OFF
                    Return DYString.DefinedNoUninstall & IO.Path.GetFileName(ItemPath)
                    Exit Function
            End Select

            Return ""
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function


    Private Sub UninstallCDVD(ByVal PathInLibrary As String, ByVal CategoryInLibrary As String, ByVal NameInLibrary As String)
        If FileIO.FileSystem.DirectoryExists(PathInLibrary) = True Then
            'Dim mFile As System.IO.FileInfo
            'Dim mSubDirectory As System.IO.DirectoryInfo
            Dim mDirectory As New System.IO.DirectoryInfo(PathInLibrary)

            For Each sFile In mDirectory.GetFiles("*.*")
                On Error GoTo jx1
                Dim a As String = Mid(Replace(PathInLibrary, ItemPath, ""), 2)
                If FileIO.FileSystem.FileExists(FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GameBackupPath, a), sFile.Name)) = True Then
                    FileIO.FileSystem.CopyFile(FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GameBackupPath, a), sFile.Name), FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GamePath, a), sFile.Name), True)
                Else
                    FileIO.FileSystem.DeleteFile(FileIO.FileSystem.CombinePath(FileIO.FileSystem.CombinePath(GamePath, a), sFile.Name))
                End If
jx1:
            Next
            For Each sSubDirectory In mDirectory.GetDirectories
                UninstallCDVD(sSubDirectory.FullName, CategoryInLibrary, NameInLibrary)
            Next
        End If
    End Sub




End Class
