extends Control


func _ready():
	$liderboard.print_board()
	EventBus.connect("add_player_to_board", self, "_on_add_player")
	


func _on_add_player(text):
	$liderboard.updateBoard(text)

func _on_play_pressed():
	EventBus.emit_signal("load_game")
	visible = false


func _on_quit_pressed():
	get_tree().quit()
