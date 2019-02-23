Func _CharOptionsNavigation_OnHoverOn ($iBtnId) 
  Local $iBtnIndex = CharOptions_BtnIndexFromId($iBtnId)
  If $iBtnIndex = $g_iCurrentlySelected Or $iBtnIndex = -1 Then
    Return
  EndIf

  If $g_iCurrentlySelected <> Null Then
    _iHoverOff($g_ahCharOptionBtns[$g_iCurrentlySelected], $g_ahCharOptionBtns[$g_iCurrentlySelected] - 7)
  EndIf

  $g_iCurrentlySelected = $iBtnIndex
EndFunc

Func _CharOptionsNavigation_OnHoverOff ($iBtnId)
  Local $iBtnIndex = CharOptions_BtnIndexFromId($iBtnId)
  If $iBtnIndex = -1 Then
    Return
  EndIf

  If $iBtnIndex <> $g_iCurrentlySelected Then
    Return
  EndIf

  $g_iCurrentlySelected = Null
EndFunc

Func CharOptionsNavigation_UnselectAllOptions ()
  For $hCharOptionBtn In $g_ahCharOptionBtns
    _iHoverOff($hCharOptionBtn, $hCharOptionBtn - 7)
  Next
EndFunc

Func CharOptionsNavigation_SelectOption ($iBtnIndex)
  ;ConsoleWrite("CharOptionsNavigation_SelectOption " & $g_iCurrentlySelected & " " & $iBtnIndex & " " & UBound($g_ahCharOptionBtns) & @CRLF)
  If $g_iCurrentlySelected <> Null Then
    _iHoverOff($g_ahCharOptionBtns[$g_iCurrentlySelected], $g_ahCharOptionBtns[$g_iCurrentlySelected] - 7)
  EndIf

	_iHoverOn($g_ahCharOptionBtns[$iBtnIndex], $g_ahCharOptionBtns[$iBtnIndex] - 7)
  $g_iCurrentlySelected = $iBtnIndex
EndFunc

Func CharOptionsNavigation_SelectRightOption ()
  Local $iNOptions = CharOptions_GetNOptions()
  
  If $iNOptions = 0 Then
    Return
  EndIf
  
  If $g_iCurrentlySelected = Null Or $iNOptions - 1 = $g_iCurrentlySelected Then
    CharOptionsNavigation_SelectOption(0)
  Else
    CharOptionsNavigation_SelectOption($g_iCurrentlySelected + 1)
  EndIf
EndFunc

Func CharOptionsNavigation_SelectLeftOption ()
  Local $iNOptions = CharOptions_GetNOptions()

  If $iNOptions = 0 Then
    Return
  EndIf

  If $g_iCurrentlySelected = Null Or $g_iCurrentlySelected = 0 Then
    CharOptionsNavigation_SelectOption($iNOptions - 1)
  Else
    CharOptionsNavigation_SelectOption($g_iCurrentlySelected - 1)
  EndIf
EndFunc