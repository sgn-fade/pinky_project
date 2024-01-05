extends "res://scripts/bullet.gd"
var direction = Vector2.ZERO
@onready var area = $Area2D

var timer := Timer.new()

func _ready():
	
	area.connect("body_entered", Callable(self, "_on_body_entered"))
	speed = 50
	timer.one_shot = false
	add_child(timer)
	$Sprite2D.play("shoot")
	timer.start(0.1)
	await timer.timeout
	$particles.emitting = true
	mouse_pos = Player.get_position()
	character_pos = global_position

func _process(delta):
	velocity = (mouse_pos - character_pos).normalized() * speed 
	if (global_position - character_pos).length() < max_distance: 
		set_velocity(velocity)
		move_and_slide()
		velocity = velocity 
	else:
		delete()

func _on_body_entered(body):
	if body.name == "player":
		var player_offcet_dir = (-(self.global_position - Player.get_position()).normalized())
		EventBus.emit_signal("player_take_damage", player_offcet_dir, 3)
	if body.name != "enemy":
		delete()
		
