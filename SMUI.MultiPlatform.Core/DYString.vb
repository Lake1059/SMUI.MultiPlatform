Public Class DYString
    Public Shared Property CanNotFoundFolderInMods = "Cannot find the folder that already exists in Mods: "
    Public Shared Property RequiredFolderNotExist As String = "The required folder does not exist: "
    Public Shared Property RequiredFileNotExist As String = "The required file does not exist: "
    Public Shared Property DefinedNoUninstall As String = "Defined can not uninstall: "
    Public Shared Property DefinedFolderExclude As String = "Defined folder exclude: "
    Public Shared Property SUBWithOutEndSub As String = "SUB without END SUB."

    Public Shared Sub TranslateAllStringToChinese()
        CanNotFoundFolderInMods = "�޷��ҵ��� Mods �е��ļ��У�"
        RequiredFolderNotExist = "����ı����ļ��в����ڣ�"
        RequiredFileNotExist = "����ı����ļ������ڣ�"
        DefinedNoUninstall = "�Ѷ���Ϊ����ж�أ�"
        DefinedFolderExclude = "��⵽������ų��ļ��д��ڣ�"
        SUBWithOutEndSub = "SUB ����û�� END SUB"
    End Sub

End Class
