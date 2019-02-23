#include-once

Local Const $g_eButtonBKColor = "0xD3D3D3"
Local Const $g_eButtonTextColor = "0x444444"
Local Const $g_eButtonBorderColor = "0x444444"

Local Const $iBtnMargin = 5
Local Const $iBtnSize = 30

Global $g_ahCharOptionBtns[0] = []
Global $g_iCurrentlySelected = Null

$g_MetroGUI_UDF_Custom_HoverOnCb = "_CharOptions_OnHoverOn"
$g_MetroGUI_UDF_Custom_HoverOffCb = "_CharOptions_OnHoverOff"

Func CharOptions_CreateCharOptions ($aiChars)
  Local $iCharsArrayLength = UBound($aiChars)
  Local $iIndex = 0

  Redim $g_ahCharOptionBtns[$iCharsArrayLength]

  _CharOptions_WinResize($iCharsArrayLength)
  
  For $iChar In $aiChars
    Local $sChar = ChrW($iChar)
    Local $hCharOptionBtn = _CharOptions_CreateButton($sChar, $iIndex)
    $g_ahCharOptionBtns[$iIndex] = $hCharOptionBtn
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

Func _CharOptions_BtnIndexFromId($iBtnId)
  Local $iIndex = 0
  For $hCharOptionBtn In $g_ahCharOptionBtns
    if $iBtnId = $hCharOptionBtn Then
      Return $iIndex
    EndIf
    $iIndex = $iIndex + 1
  Next

  Return -1
EndFunc

Func _CharOptions_OnHoverOn ($iBtnId) 
  Local $iBtnIndex = _CharOptions_BtnIndexFromId($iBtnId)
  If $iBtnIndex = $g_iCurrentlySelected Or $iBtnIndex = -1 Then
    Return
  EndIf

  If $g_iCurrentlySelected <> Null Then
    _iHoverOff($g_ahCharOptionBtns[$g_iCurrentlySelected], $g_ahCharOptionBtns[$g_iCurrentlySelected] - 7)
  EndIf

  $g_iCurrentlySelected = $iBtnIndex
EndFunc

Func _CharOptions_OnHoverOff ($iBtnId)
  Local $iBtnIndex = _CharOptions_BtnIndexFromId($iBtnId)
  If $iBtnIndex = -1 Then
    Return
  EndIf

  If $iBtnIndex <> $g_iCurrentlySelected Then
    Return
  EndIf

  $g_iCurrentlySelected = Null
EndFunc

Func CharOptions_UnselectAllOptions ()
  For $hCharOptionBtn In $g_ahCharOptionBtns
    _iHoverOff($hCharOptionBtn, $hCharOptionBtn - 7)
  Next
EndFunc

Func CharOptions_SelectOption ($iBtnIndex)
  If $g_iCurrentlySelected <> Null Then
    _iHoverOff($g_ahCharOptionBtns[$g_iCurrentlySelected], $g_ahCharOptionBtns[$iBtnIndex] - 7)
  EndIf

	_iHoverOn($g_ahCharOptionBtns[$iBtnIndex], $g_ahCharOptionBtns[$iBtnIndex] - 7)
  $g_iCurrentlySelected = $iBtnIndex
EndFunc

Func CharOptions_SelectRightOption ()
  Local $iNOptions = CharOptions_GetNOptions()
  
  If $iNOptions = 0 Then
    Return
  EndIf
  
  If $g_iCurrentlySelected = Null Or $iNOptions - 1 = $g_iCurrentlySelected Then
    CharOptions_SelectOption(0)
  Else
    CharOptions_SelectOption($g_iCurrentlySelected + 1)
  EndIf
EndFunc

Func CharOptions_SelectLeftOption ()
  Local $iNOptions = CharOptions_GetNOptions()

  If $iNOptions = 0 Then
    Return
  EndIf

  If $g_iCurrentlySelected = Null Or $g_iCurrentlySelected = 0 Then
    CharOptions_SelectOption($iNOptions - 1)
  Else
    CharOptions_SelectOption($g_iCurrentlySelected - 1)
  EndIf
EndFunc

Func CharOptions_GetNOptions ()
  return UBound($g_ahCharOptionBtns)
EndFunc