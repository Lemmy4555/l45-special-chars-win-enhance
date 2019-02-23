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

#include <GUI/l45_scwe_BaseGUI.au3>
#include <GUI/l45_scwe_CharOptions.au3>
#include <Key-Handler/l45_scwe-KeyHoldHandler.au3>
#include <l45_scwe-Utils.au3>

InitL45GUI(True)

Local $asCharOptions[5] = ["A", "B", "C", "D", "E"]

KeyHoldHandler_KeyHoldEvent("_Main_OnKeyHold")
NavigationEvents_Register(Default, Default, "CharOptions_SelectRightOption", "CharOptions_SelectLeftOption")

Func _Main_OnKeyHold ($sKeyCode, $sKeyIntCode, $aiValues)
	Local $bIsNotMySelfActiveWindow = Utils_IsActiveWindowNotMySelf() 

	if $bIsNotMySelfActiveWindow Then
    CharOptions_CreateCharOptions($aiValues)
		KeyHoldHandler_Pause()
		Utils_ShowGUIOnActiveWindow()
		CharOptions_SelectOption(0)

		HotKeySet("{ESC}", "BaseGUI_HideGUI")
	EndIf
EndFunc

;Endless While loop to keep the GUI Open
While 1
	Sleep(2000)
WEnd
