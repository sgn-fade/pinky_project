extends Control


func _ready():
	EventBus.connect("player_dead", Callable(self, "_on_player_dead"))

func _on_player_dead():
	$score.text = "SCORE: " + String(Player.get_score())

func _on_name_line_text_entered(text):
	$enter_name.visible = false
	$name_line.visible = false
	$quit.visible = true
	$restart.visible = true
	EventBus.emit_signal("add_player_to_board", text)
	


func _on_restart_pressed():
	get_tree().reload_current_scene()
	Player.restart()
	


func _on_quit_pressed():
	get_tree().quit()
