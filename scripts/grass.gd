extends StaticBody2D
var timer := Timer.new()


func _ready():
	timer.one_shot = false
	add_child(timer)
	$sprite.frame = randi() % 6

func _process(delta):
	pass


func _on_visible_screen_entered():
	visible = true


func _on_visible_screen_exited():
	visible = false
