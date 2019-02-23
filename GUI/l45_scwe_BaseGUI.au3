#include-once

; Metro Window add-on
#include "../addons/MetroUDF-v5.1/MetroGUI-UDF/MetroGUI_UDF.au3"

#include <GUIConstants.au3>

; Basic Requirements for GUI functionalities
#include <l45_scwe-BaseGUI-requirements.au3>
#include <l45_scwe-NavigationEvents.au3>

Global Const $__g_sGUIName = "L45-scwe"

Local Const $sTheme = "LightTeal"
Local Const $g_eGUIBGColor = "0xFFFFFF"
Local Const $iDefaultHeaderHeight = 32

Global $g_bShowClose = False
Global $g_iGUIHeaderHeight = 0
Global $g_iGUIHeight = 300
Global $g_iGUIOuterHeight = 300
Global $g_iGUIWidth = 500

Global $g_hGUI

Func InitL45GUI ($bShowClose = False)
  ;Set Theme
  _SetTheme($sTheme) ;See MetroThemes.au3 for selectable themes or to add more
  $GUIThemeColor = $g_eGUIBGColor
  
  ;Top padding due to menu
  Local $iGUITopPadding
  if $bShowClose = True Then
    $g_iGUIHeaderHeight = $iDefaultHeaderHeight
  EndIf

  $g_iGUIOuterHeight = $g_iGUIHeaderHeight + $g_iGUIHeight

  ;Required Includes.
  ;Declare any variables.
  ;Create the GUI
  $g_hGUI = _Metro_CreateGUI($__g_sGUIName, $g_iGUIWidth, $g_iGUIOuterHeight, -1, -1, False, "", $WS_EX_TOPMOST)
  _Metro_SetGUIOption($g_hGUI, False, True)

  _CreateHeader($bShowClose)

  GUISetOnEvent($GUI_EVENT_CLOSE, "_BaseGUI_ExitGui")

  ;Show the GUI. We need this line, or our close button will NOT be displayed!
  BaseGUI_HideGUI()
EndFunc

Func _CreateHeader ($bShowClose)
  ;Add/create control buttons to the GUI
  Local $ahControlButtons = _Metro_AddControlButtons($bShowClose, False, False, False, False) ;CloseBtn = True, MaximizeBtn = True, MinimizeBtn = True, FullscreenBtn = True, MenuBtn = False
  Local $hGUICloseButton = $ahControlButtons[0]
  GUICtrlSetOnEvent($hGUICloseButton, "_BaseGUI_ExitGui")
EndFunc

Func _BaseGUI_WinResize ($iWidth, $iHeight)
  $g_iGUIWidth = $iWidth
  $g_iGUIOuterHeight = $iHeight
  $g_iGUIHeight = $g_iGUIOuterHeight - $g_iGUIHeaderHeight

  WinMove($g_hGUI, $__g_sGUIName, Default, Default, $g_iGUIWidth, $g_iGUIOuterHeight)
EndFunc

Func _BaseGUI_ExitGui ()
	_Metro_GUIDelete($g_hGUI) ;Delete GUI/release resources, make sure you use this when working with multiple GUIs!
	Exit ; Exit the program
EndFunc

Func BaseGUI_HideGUI ()
  NavigationEvents_Unbind()
	GUISetState(@SW_HIDE, $g_hGUI)
EndFunc

Func BaseGUI_ShowGUI ()
	GUISetState(@SW_SHOW, $g_hGUI)
  NavigationEvents_Bind()
EndFunc

Func _BaseGUI_ShowGUIOnWindow ($aPos)
  Local $iX = $aPos[0] + ($aPos[2] / 2) - $g_iGUIWidth
  Local $iY = $aPos[1] + ($aPos[3] / 2) - $g_iGUIOuterHeight

  ; Display the array values returned by WinGetPos.
  WinMove($g_hGUI, $__g_sGUIName, $iX, $iY, $g_iGUIWidth, $g_iGUIOuterHeight)

  BaseGUI_ShowGUI()
EndFunc