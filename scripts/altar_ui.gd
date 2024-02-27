extends Control
var room_sizes

func _ready():
	$orb_progress.value = 0
	
func _on_Button_pressed():
	if 4 > 0:
		$orb_progress.value += 10


func _on_start_pressed():
	pass

func _on_start_button_down():
	pass

func _on_start_toggled(button_pressed):
	if button_pressed:
		EventBus.emit_signal("spheres_donated", $orb_progress.value)


func _on_ColorRect_mouse_entered():
	EventBus.emit_signal("crosshair_switch", "ui")

func _on_ColorRect_mouse_exited():
	EventBus.emit_signal("crosshair_switch", "game")
