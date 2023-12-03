extends Control


func _ready():
	EventBus.connect("player_dead", self, "_on_player_dead")

func _on_player_dead():
	$score.text = "SCORE: " + String(Player.get_score())
