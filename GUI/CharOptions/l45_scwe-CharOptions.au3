#include-once

#include <GUIConstantsEx.au3>

Local Const $g_eButtonBKColor = "0xD3D3D3"
Local Const $g_eButtonTextColor = "0x444444"
Local Const $g_eButtonBorderColor = "0x444444"

Local Const $iBtnMargin = 5
Local Const $iBtnSize = 50

Global $g_ahCharOptionBtns[0] = []
Global $g_ahCharOptions[0] = []
Global $g_iCurrentlySelected = Null

$g_MetroGUI_UDF_Custom_HoverOnCb = "_CharOptionsNavigation_OnHoverOn"
$g_MetroGUI_UDF_Custom_HoverOffCb = "_CharOptionsNavigation_OnHoverOff"

#include <l45_scwe-CharOptionsNavigation.au3>
#include <l45_scwe-CharOptionsInteraction.au3>

Func CharOptions_CreateCharOptions ($aiChars)
  Local $iCharsArrayLength = UBound($aiChars)

  $g_iCurrentlySelected = Null

  For $hCharOptionBtn In $g_ahCharOptionBtns
    GUICtrlDelete($hCharOptionBtn)
  Next

  Redim $g_ahCharOptionBtns[$iCharsArrayLength]
  Redim $g_ahCharOptions[$iCharsArrayLength]
  $g_ahCharOptions = $aiChars

  _CharOptions_WinResize($iCharsArrayLength)
  
  Local $iIndex = 0
  For $iChar In $aiChars
    Local $sChar = ChrW($iChar)
    Local $hCharOptionBtn = _CharOptions_CreateButton($sChar, $iIndex)
    $g_ahCharOptionBtns[$iIndex] = $hCharOptionBtn
    $iIndex = $iIndex + 1
  Next

  CharOptionsInteraction_BindBtns()
EndFunc

Func _CharOptions_CreateButton ($sChar, $iIndex)
  Local $iLeft = ($iBtnMargin * ($iIndex + 1)) + ($iBtnSize * $iIndex)
  Local $iTop = $g_iGUIHeaderHeight + $iBtnMargin

  Local $hBtn = _Metro_CreateButton($sChar, $iLeft , $iTop, $iBtnSize, $iBtnSize, $g_eButtonBKColor, $g_eButtonTextColor, "Arial", 16, 1, $g_eButtonBorderColor)
  return $hBtn
EndFunc

Func _CharOptions_WinResize ($iNButton)
  Local $iWidth = ($iBtnMargin * ($iNButton + 1)) + ($iBtnSize * $iNButton)
  Local $iHeight = ($iBtnMargin * 2) + $iBtnSize + $g_iGUIHeaderHeight

  _BaseGUI_WinResize($iWidth, $iHeight)
EndFunc

Func CharOptions_BtnIndexFromId($iBtnId)
  Local $iIndex = 0
  For $hCharOptionBtn In $g_ahCharOptionBtns
    if $iBtnId = $hCharOptionBtn Then
      Return $iIndex
    EndIf
    $iIndex = $iIndex + 1
  Next

  Return -1
EndFunc

Func CharOptions_GetNOptions ()
  return UBound($g_ahCharOptionBtns)
EndFunc