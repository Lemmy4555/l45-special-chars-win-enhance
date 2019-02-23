#include-once

Local Const $g_eButtonBKColor = "0xD3D3D3"
Local Const $g_eButtonTextColor = "0x444444"
Local Const $g_eButtonBorderColor = "0x444444"

Local Const $iBtnMargin = 5
Local Const $iBtnSize = 30

Global $ahCharOptionBtns[0] = []

Func CharOptions_CreateCharOptions ($asChars)
  Local $iCharsArrayLength = UBound($asChars)
  Local $iIndex = 0

  Redim $ahCharOptionBtns[$iCharsArrayLength]

  _CharOptions_WinResize($iCharsArrayLength)
  
  For $sChar In $asChars
    Local $hCharOptionBtn = _CharOptions_CreateButton($sChar, $iIndex)
    $ahCharOptionBtns[$iIndex] = $hCharOptionBtn
    $iIndex = $iIndex + 1
  Next
EndFunc

Func _CharOptions_CreateButton ($sChar, $iIndex)
  Local $iLeft = ($iBtnMargin * ($iIndex + 1)) + ($iBtnSize * $iIndex)
  Local $iTop = $g_iGUIHeaderHeight + $iBtnMargin

  Local $hBtn = _Metro_CreateButton($sChar, $iLeft , $iTop, $iBtnSize, $iBtnSize, $g_eButtonBKColor, $g_eButtonTextColor, "Arial", 10, 1, $g_eButtonBorderColor)
  return $hBtn
EndFunc

Func _CharOptions_WinResize ($iNButton)
  Local $iWidth = ($iBtnMargin * ($iNButton + 1)) + ($iBtnSize * $iNButton)
  Local $iHeight = ($iBtnMargin * 2) + $iBtnSize + $g_iGUIHeaderHeight

  _BaseGUI_WinResize($iWidth, $iHeight)
EndFunc