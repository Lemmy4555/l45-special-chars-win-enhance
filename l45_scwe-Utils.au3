Func Utils_ShowGUIOnActiveWindow()
	; Retrieve the position as well as height and width of the active window.
	Local $aPos = WinGetPos("[ACTIVE]")
	_BaseGUI_ShowGUIOnWindow($aPos)
EndFunc

Func Utils_IsActiveWindowNotMySelf()
	; Retrieve the position as well as height and width of the active window.
	Local $sTitle = WinGetTitle("[ACTIVE]")
	return $sTitle <> $__g_sGUIName
EndFunc

Func Utils_HideGUI()
	KeyHoldHandler_Resume()
	BaseGUI_HideGUI()
EndFunc

Func Utils_SendUserInputAndHideGUI ()
  KeyHoldHandler_Pause()

  BaseGUI_HideGUI()

	If $g_sUserInput <> Null Then
		ConsoleWrite("Write " & $g_sUserInput & @CRLF)
		Send($g_sUserInput)
		$g_sUserInput = Null
	EndIf

  KeyHoldHandler_Resume()
EndFunc