#include-once

#include <Misc.au3>

#Include <l45_scwe-KeyHoldHandler-mapping.au3>

Global $g_sUserInput
Global $g_bKeyHoldHandler_Paused = False

Local $iKeyHoldCounter = 0
Local $_sCallback
Local $bInitialized = False

Local Const $iCheckInterval = 10
Local Const $iHoldCounterMin = 40

Func KeyHoldHandler_KeyHoldEvent ($sCallback)
  $_sCallback = $sCallback

  If $bInitialized Then
    Return
  EndIf

  _KeyHoldHandler_Mapping_Init ()

  $bInitialized = True
EndFunc

Func KeyHoldHandler_Unbind ()
  $g_bKeyHoldHandler_Paused = True
  _KeyHoldHandler_Mapping_Reset()
EndFunc

Func KeyHoldHandler_Rebind ()
  _KeyHoldHandler_Mapping_Init()
  $g_bKeyHoldHandler_Paused = False
EndFunc

Func KeyHoldHandler_Pause ()
  $g_bKeyHoldHandler_Paused = True
EndFunc

Func KeyHoldHandler_Resume ()
  $g_bKeyHoldHandler_Paused = False
EndFunc

Func KeyHoldHandler_HoldingKey ($sKeyCode, $sKeyIntCode, $aiValues)
  If $iKeyHoldCounter > 0 OR $g_bKeyHoldHandler_Paused Then
    Return
  EndIf

  ConsoleWrite($sKeyCode & " Pressed" & @CRLF)

  Local $dll = DllOpen("user32.dll")

  $iKeyHoldCounter = 0

  While _IsPressed($sKeyIntCode, $dll)
    If $iKeyHoldCounter = $iHoldCounterMin Then
      Call($_sCallback, $sKeyCode, $sKeyIntCode, $aiValues)
    EndIf

    $iKeyHoldCounter = $iKeyHoldCounter + 1
    Sleep($iCheckInterval)
  WEnd
  
  ConsoleWrite($sKeyCode & " Released " & $iKeyHoldCounter & @CRLF)
  
  If $iKeyHoldCounter <= $iHoldCounterMin Then
    KeyHoldHandler_Unbind()
    Send($sKeyCode)
    KeyHoldHandler_Rebind()
  EndIf

  $iKeyHoldCounter = 0
  $g_sUserInput = $sKeyCode
    
	DllClose($dll)
EndFunc