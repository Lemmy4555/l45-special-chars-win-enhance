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

  _KeyHoldHandler_Init ()

  $bInitialized = True
EndFunc

Func _KeyHoldHandler_Init ()
  HotKeySet($g_KeyHoldHandler_CapitalEKey_Code, "_KeyHoldHandler_CapitalEKey")
EndFunc

Func KeyHoldHandler_Unbind ()
  $g_bKeyHoldHandler_Paused = True
  HotKeySet($g_KeyHoldHandler_CapitalEKey_Code)
EndFunc

Func KeyHoldHandler_Rebind ()
  _KeyHoldHandler_Init()
  $g_bKeyHoldHandler_Paused = False
EndFunc

Func KeyHoldHandler_Pause ()
  $g_bKeyHoldHandler_Paused = True
EndFunc

Func KeyHoldHandler_Resume ()
  $g_bKeyHoldHandler_Paused = False
EndFunc

Func _KeyHoldHandler_CapitalEKey ()
  _KeyHoldHandler_HoldingKey($g_KeyHoldHandler_CapitalEKey_Code, $g_KeyHoldHandler_CapitalEKey_IntCode, $g_KeyHoldHandler_asCapitalEKey_Values)
EndFunc

Func _KeyHoldHandler_HoldingKey ($sKeyCode, $sKeyIntCode, $aiValues)
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