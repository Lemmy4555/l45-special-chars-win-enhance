Local $sOnUp = ""
Local $sOnDown = ""
Local $sOnRight = ""
Local $sOnLeft = ""

Func NavigationEvents_Bind()
  HotKeySet("{UP}", $sOnUp)
  HotKeySet("{DOWN}", $sOnDown)
  HotKeySet("{RIGHT}", $sOnRight)
  HotKeySet("{LEFT}", $sOnLeft)
EndFunc

Func NavigationEvents_Unbind()
  HotKeySet("{UP}")
  HotKeySet("{DOWN}")
  HotKeySet("{RIGHT}")
  HotKeySet("{LEFT}")
EndFunc

Func NavigationEvents_Register($sOnUpCb = "", $sOnDownCb = "", $sOnRightCb = "", $sOnLeftCb = "")
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

  $sOnUp = $sOnUpCb
  $sOnDown = $sOnDownCb
  $sOnRight = $sOnRightCb
  $sOnLeft = $sOnLeftCb

  NavigationEvents_Bind()
EndFunc