extends Control


func _ready():
	$liderboard.createBoard()


func _on_option_pressed():
	GlobalWorldInfo.add_player_to_board("We")
	$liderboard.createBoard()
func _on_play_pressed():
	EventBus.emit_signal("load_game")
	visible = false


func _on_quit_pressed():
	get_tree().quit()
