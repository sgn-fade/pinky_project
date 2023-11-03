extends KinematicBody2D
var speed = 8000/60
var velocity = Vector2()
var character_pos 
var current_pos


func _ready():
	$Area2D.connect("body_entered", self, "_on_body_entered")
	character_pos = Player.global_position 
	look_at(character_pos) 
	current_pos = global_position

func _process(delta):
	if (global_position - current_pos).length() < 100:
		velocity = (character_pos - current_pos).normalized() * speed 
		velocity = move_and_slide(velocity) 
	else:
		queue_free()
	
func _on_body_entered(body):
	var player_offcet_dir = (-(self.global_position - Player.global_position).normalized())
	if body.name == "main_character":
		Player.take_damage(player_offcet_dir)
	queue_free()
	
