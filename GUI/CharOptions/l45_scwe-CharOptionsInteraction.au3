Local $sCharOptionsInteraction_OptionClick = Null

Func CharOptionsInteraction_BindBtns ()
  Local $iIndex = 0
  For $hCharOptionBtn In $g_ahCharOptionBtns
    GUICtrlSetOnEvent($hCharOptionBtn, "CharOptionsInteraction_BtnClick")
    $iIndex = $iIndex + 1
  Next
EndFunc

Func CharOptionsInteraction_OptionSelectEvent($sCallback)
  $sCharOptionsInteraction_OptionClick = $sCallback
EndFunc

Func CharOptionsInteraction_BtnClick ()
  If $sCharOptionsInteraction_OptionClick = Null Then
    Return
  EndIf
  
  Local $iIndex = CharOptions_BtnIndexFromId(@GUI_CtrlId)
  Local $iChar = $g_ahCharOptions[$iIndex]
  Call($sCharOptionsInteraction_OptionClick, $iChar)
EndFunc

Func CharOptionsInteraction_BtnOnEnter ()
  ControlClick($g_hGUI, $__g_sGUIName, $g_ahCharOptionBtns[$g_iCurrentlySelected])
EndFunc

;~ Func CharOptionsInteraction_BtnClick_0
;~   Call($sCharOptionsInteraction_OptionClick, $g_ahCharOptionBtns[0])
;~ EndFunc

;~ Func CharOptionsInteraction_BtnClick_1
;~   Call($sCharOptionsInteraction_OptionClick, $g_ahCharOptionBtns[1])
;~ EndFunc

;~ Func CharOptionsInteraction_BtnClick_2
;~   Call($sCharOptionsInteraction_OptionClick, $g_ahCharOptionBtns[2])
;~ EndFunc

;~ Func CharOptionsInteraction_BtnClick_3
;~   Call($sCharOptionsInteraction_OptionClick, $g_ahCharOptionBtns[3])
;~ EndFunc

;~ Func CharOptionsInteraction_BtnClick_4
;~   Call($sCharOptionsInteraction_OptionClick, $g_ahCharOptionBtns[4])
;~ EndFunc

;~ Func CharOptionsInteraction_BtnClick_5
;~   Call($sCharOptionsInteraction_OptionClick, $g_ahCharOptionBtns[5])
;~ EndFunc

;~ Func CharOptionsInteraction_BtnClick_6
;~   Call($sCharOptionsInteraction_OptionClick, $g_ahCharOptionBtns[6])
;~ EndFunc

;~ Func CharOptionsInteraction_BtnClick_7
;~   Call($sCharOptionsInteraction_OptionClick, $g_ahCharOptionBtns[7])
;~ EndFunc

;~ Func CharOptionsInteraction_BtnClick_8
;~   Call($sCharOptionsInteraction_OptionClick, $g_ahCharOptionBtns[8])
;~ EndFunc

;~ Func CharOptionsInteraction_BtnClick_9
;~   Call($sCharOptionsInteraction_OptionClick, $g_ahCharOptionBtns[9])
;~ EndFunc