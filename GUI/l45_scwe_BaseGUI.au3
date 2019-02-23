#include-once

; Metro Window add-on
#include "../addons/MetroUDF-v5.1/MetroGUI-UDF/MetroGUI_UDF.au3"

#include <GUIConstants.au3>

; Basic Requirements for GUI functionalities
#include <l45_scwe_BaseGUI_requirements.au3>

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
  $g_hGUI = _Metro_CreateGUI($__g_sGUIName, $g_iGUIWidth, $g_iGUIOuterHeight, -1, -1, False)
  _Metro_SetGUIOption($g_hGUI, False, True)

  _CreateHeader($bShowClose)

  GUISetOnEvent($GUI_EVENT_CLOSE, "_ExitGui")

  ;Show the GUI. We need this line, or our GUI will NOT be displayed!
  GUISetState (@SW_SHOW)
EndFunc

Func _CreateHeader ($bShowClose)
  ;Add/create control buttons to the GUI
  Local $ahControlButtons = _Metro_AddControlButtons($bShowClose, False, False, False, False) ;CloseBtn = True, MaximizeBtn = True, MinimizeBtn = True, FullscreenBtn = True, MenuBtn = False
  Local $hGUICloseButton = $ahControlButtons[0]
  GUICtrlSetOnEvent($hGUICloseButton, "_ExitGui")
EndFunc

Func _BaseGUI_WinResize ($iWidth, $iHeight)
  $g_iGUIWidth = $iWidth
  $g_iGUIOuterHeight = $iHeight
  $g_iGUIHeight = $g_iGUIOuterHeight - $g_iGUIHeaderHeight

  WinMove($g_hGUI, $__g_sGUIName, Default, Default, $iWidth, $g_iGUIOuterHeight)
EndFunc

Func _ExitGui ()
	_Metro_GUIDelete($g_hGUI) ;Delete GUI/release resources, make sure you use this when working with multiple GUIs!
	Exit ; Exit the program
EndFunc