#cs ----------------------------------------------------------------------------

 AutoIt Version: 3.3.14.5
 Author:         myName

 Script Function:
	Template AutoIt script.

#ce ----------------------------------------------------------------------------

; Script Start - Add your code below here
#include-once
; Metro Window add-on
#include "addons/MetroUDF-v5.1/MetroGUI-UDF/MetroGUI_UDF.au3"

#include <GUIConstantsEx.au3>
#include <EditConstants.au3>
#include <AutoItConstants.au3>
#include <WindowsConstants.au3>

#include <GUI/l45_scwe-BaseGUI.au3>
#include <GUI/CharOptions/l45_scwe-CharOptions.au3>
#include <Key-Handler/l45_scwe-KeyHoldHandler.au3>

InitL45GUI(False)
KeyHoldHandler_KeyHoldEvent("_Main_OnKeyHold")
BaseGUI_HideGUIEvent("KeyHoldHandler_Resume")
BaseGUI_ShowGUIEvent("KeyHoldHandler_Pause")
NavigationEvents_Register(Default, Default, "CharOptionsNavigation_SelectRightOption", "CharOptionsNavigation_SelectLeftOption", "CharOptionsInteraction_BtnOnEnter")
CharOptionsInteraction_OptionSelectEvent("_Main_OnOptionClicked")

Func _Main_OnKeyHold ($sKeyCode, $sKeyIntCode, $aiValues)
	Local $bIsNotMySelfActiveWindow = BaseGUI_IsActiveWindowNotMySelf() 

	if $bIsNotMySelfActiveWindow Then
    CharOptions_CreateCharOptions($aiValues)
		CharOptionsNavigation_SelectOption(0)
		BaseGUI_ShowGUIOnActiveWindow()
	EndIf
EndFunc

Func _Main_OnOptionClicked($iChar)
  KeyHoldHandler_Pause()

  BaseGUI_HideGUI()

	;ConsoleWrite("Write " & $iChar & @CRLF)

	Local $sChar = ChrW($iChar)
	Send($sChar)

  KeyHoldHandler_Resume()
EndFunc

;Endless While loop to keep the GUI Open
While 1
	Sleep(200)
WEnd