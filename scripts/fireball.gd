extends CharacterBody2D

var speed = 200
@onready var particles = $particles
@onready var end_particles = $end_partcl
@onready var area = $Area2D

var timer := Timer.new()

func _ready():
	area.connect("body_entered", Callable(self, "_on_body_entered"))
	timer.one_shot = false
	add_child(timer)
	$Sprite2D.play("shoot")
	var end_position = get_global_mouse_position()
	self.global_position = Player.get_position()
	self.global_position.y -= 5
	velocity = (end_position - Player.get_position()).normalized() * speed
	look_at(end_position) 


func _process(delta):
	velocity = velocity.normalized() * speed
	set_velocity(velocity)
	move_and_slide()
	velocity = velocity 


func delete_fireball():
	set_process(false)
	end_particles.emitting = true
	$Sprite2D.visible = false
	particles.emitting = false
	for i in 15:
		$light.texture_scale += 0.02
		$light.energy -= 0.05
		$Area2D/CollisionShape2D.scale.x += 0.03
		$Area2D/CollisionShape2D.scale.y += 0.03
		timer.start(0.03)
		await timer.timeout	
	queue_free()


func _on_body_entered(body):
	delete_fireball()
	EventBus.emit_signal("damage_to_enemy", body, 20, null)
	

