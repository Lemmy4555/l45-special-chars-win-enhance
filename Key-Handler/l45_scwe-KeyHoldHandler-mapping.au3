Global $g_KeyHoldHandler_CapitalEKey_Code = "E"
Global $g_KeyHoldHandler_CapitalEKey_IntCode = "45"
Global $g_KeyHoldHandler_CapitalEKey_Values[8] = [0x00C8, 0x00C9, 0x00CA, 0x00CB, 0x0112, 0x0114, 0x0116, 0x20AC] ; È, É, Ê, Ë, Ē, Ĕ, Ė, €

Global $g_KeyHoldHandler_SmallEKey_Code = "e"
Global $g_KeyHoldHandler_SmallEKey_IntCode = "45"
Global $g_KeyHoldHandler_SmallEKey_Values[8] = [0x00E8, 0x00E9, 0x00EA, 0x00EB, 0x0113, 0x0115, 0x0117, 0x20AC] ; è, é, ê, ê, ē, ĕ, ė, €

Global $g_KeyHoldHandler_DotKey_Code = "S"
Global $g_KeyHoldHandler_DotKey_IntCode = "53"
Global $g_KeyHoldHandler_DotKey_Values[6] = [0x007E, 0x2713, 0x2717, 0x2714, 0x2718, 0x25CF] ; ~, ✓, ✗, ✔, ✘, ●

Global $g_KeyHoldHandler_PlusKey_Code = "X"
Global $g_KeyHoldHandler_PlusKey_IntCode = "58"
Global $g_KeyHoldHandler_PlusKey_Values[1] = [0x00F7] ; ÷

Func _KeyHoldHandler_Mapping_Init ()
  HotKeySet($g_KeyHoldHandler_CapitalEKey_Code, "_KeyHoldHandler_Mapping_CapitalEKey")
  HotKeySet($g_KeyHoldHandler_SmallEKey_Code, "_KeyHoldHandler_Mapping_SmallEKey")
  HotKeySet($g_KeyHoldHandler_DotKey_Code, "_KeyHoldHandler_Mapping_DotKey")
  HotKeySet($g_KeyHoldHandler_PlusKey_Code, "_KeyHoldHandler_Mapping_PlusKey")
EndFunc

Func _KeyHoldHandler_Mapping_Reset ()
  HotKeySet($g_KeyHoldHandler_CapitalEKey_Code)
  HotKeySet($g_KeyHoldHandler_SmallEKey_Code)
  HotKeySet($g_KeyHoldHandler_DotKey_Code)
  HotKeySet($g_KeyHoldHandler_PlusKey_Code)
EndFunc

Func _KeyHoldHandler_Mapping_CapitalEKey ()
  KeyHoldHandler_HoldingKey($g_KeyHoldHandler_CapitalEKey_Code, $g_KeyHoldHandler_CapitalEKey_IntCode, $g_KeyHoldHandler_CapitalEKey_Values)
EndFunc

Func _KeyHoldHandler_Mapping_SmallEKey ()
  KeyHoldHandler_HoldingKey($g_KeyHoldHandler_SmallEKey_Code, $g_KeyHoldHandler_SmallEKey_IntCode, $g_KeyHoldHandler_SmallEKey_Values)
EndFunc

Func _KeyHoldHandler_Mapping_DotKey ()
  KeyHoldHandler_HoldingKey($g_KeyHoldHandler_DotKey_Code, $g_KeyHoldHandler_DotKey_IntCode, $g_KeyHoldHandler_DotKey_Values)
EndFunc

Func _KeyHoldHandler_Mapping_PlusKey ()
  KeyHoldHandler_HoldingKey($g_KeyHoldHandler_PlusKey_Code, $g_KeyHoldHandler_PlusKey_IntCode, $g_KeyHoldHandler_PlusKey_Values)
EndFunc