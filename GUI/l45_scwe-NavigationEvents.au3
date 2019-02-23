Local $sOnUp = ""
Local $sOnDown = ""
Local $sOnRight = ""
Local $sOnLeft = ""
Local $sOnEnter = ""

Func NavigationEvents_Bind()
  HotKeySet("{UP}", $sOnUp)
  HotKeySet("{DOWN}", $sOnDown)
  HotKeySet("{RIGHT}", $sOnRight)
  HotKeySet("{LEFT}", $sOnLeft)
  HotKeySet("{TAB}", $sOnRight)
  HotKeySet("+{TAB}", $sOnLeft)
  HotKeySet("{ENTER}", $sOnEnter)
EndFunc

Func NavigationEvents_Unbind()
  HotKeySet("{UP}")
  HotKeySet("{DOWN}")
  HotKeySet("{RIGHT}")
  HotKeySet("{LEFT}")
  HotKeySet("{TAB}")
  HotKeySet("+{TAB}")
  HotKeySet("{ENTER}")
EndFunc

Func NavigationEvents_Register($sOnUpCb = "", $sOnDownCb = "", $sOnRightCb = "", $sOnLeftCb = "", $sOnEnterCb = "")
  if $sOnUpCb = Default Then
    $sOnUpCb = ""
  EndIf

  if $sOnDownCb = Default Then
    $sOnDownCb = ""
  EndIf

  if $sOnRightCb = Default Then
    $sOnRightCb = ""
  EndIf

  if $sOnLeftCb = Default Then
    $sOnLeftCb = ""
  EndIf

  if $sOnEnterCb = Default Then
    $sOnLeftCb = ""
  EndIf

  $sOnUp = $sOnUpCb
  $sOnDown = $sOnDownCb
  $sOnRight = $sOnRightCb
  $sOnLeft = $sOnLeftCb
  $sOnEnter = $sOnEnterCb
EndFunc