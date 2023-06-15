Public Class DYString

    Public Shared Property CanNotFoundFolderInMods = "Cannot find the folder that already exists in Mods: "
    Public Shared Property RequiredFolderNotExist As String = "The required folder does not exist: "
    Public Shared Property RequiredFileNotExist As String = "The required file does not exist: "
    Public Shared Property DefinedNoUninstall As String = "Defined can not uninstall: "
    Public Shared Property DefinedFolderExclude As String = "Defined folder exclude: "
    Public Shared Property SUBWithOutEndSub As String = "SUB without END SUB."
    Public Shared Property ItemPathDoesNotExist As String = " Item path does not exist: "
    Public Shared Property ThisItemDonotHaveCodeFile As String = "This item do not have a Code file: "
    Public Shared Property ThisOperationNeedsToSetGamePath As String = "This operation needs to set the game path."
    Public Shared Property GamePathNotExist As String = "Game path not exist: "
    Public Shared Property SMUICOREERROR As String = "[SMUI CORE ERROR] "

    Public Shared Sub TranslateAllStringToChinese()
        CanNotFoundFolderInMods = "�޷��ҵ��� Mods �е��ļ��У�"
        RequiredFolderNotExist = "����ı����ļ��в����ڣ�"
        RequiredFileNotExist = "����ı����ļ������ڣ�"
        DefinedNoUninstall = "�Ѷ���Ϊ����ж�أ�"
        DefinedFolderExclude = "��⵽������ų��ļ��д��ڣ�"
        SUBWithOutEndSub = "SUB ����û�� END SUB"
        ItemPathDoesNotExist = "��·�������ڣ�"
        ThisItemDonotHaveCodeFile = "��δ���ã�"
        ThisOperationNeedsToSetGamePath = "���������Ҫ������Ϸ·����"
        GamePathNotExist = "��Ϸ·�������ڣ�"
        SMUICOREERROR = "��SMUI ���Ĵ���"
    End Sub



End Class

