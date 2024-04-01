extends CharacterBody2D
class_name Enemies
var speed
var hp = 1000000
var enemy_damage = 1
@onready var damage_label = load("res://scenes/damage_text.tscn")
var blood_orb_drop = load("res://scenes/blood_orb.tscn")
var modules_drop = load("res://scenes/modules_drop.tscn")

@onready var hp_bar = $hp_bar
@onready var collision = $collision
@onready var white_animation_bar = $middle_white_bar
@onready var status = $status1
var white_bar_timer := Timer.new()
var slowdown_timer := Timer.new()
var timer := Timer.new()
var fire_damage_timer := Timer.new()
var direction := Vector2.ZERO
var damage_label_instance = null
var damage_to_enemy = null

func _ready():
	speed = 60
	GlobalWorldInfo.add_enemy(self)
	damage_label_instance = damage_label.instantiate()
	add_child(damage_label_instance)
	add_child(white_bar_timer)
	add_child(slowdown_timer)
	add_child(timer)
	add_child(fire_damage_timer)
	
	white_bar_timer.one_shot = false
	slowdown_timer.one_shot = false
	timer.one_shot = false
	EventBus.connect("pulls_body", Callable(self, "_on_pulls_body"))
	EventBus.connect("damage_to_enemy", Callable(self, "_on_damage_to_enemy"))
	EventBus.connect("push_away_enemy", Callable(self, "_on_push_away_enemy"))


func set_direction(dir):
	direction = dir
	
func get_direction():
	return direction
	
func _input(event):
	if Input.is_action_just_pressed("E"):
		if Player.get_smite(self):
			queue_free()
func enemy_death():
	if self.hp <= 0:
		EventBus.emit_signal("enemy_killed")
		spawn_drop()
		queue_free()

func spawn_drop():
	var blood_orb = blood_orb_drop.instantiate()
	GlobalWorldInfo.get_world().add_child(blood_orb)
	blood_orb.global_position = self.global_position
	blood_orb.z_index = self.z_index


func update_hp():
	self.hp_bar.value = self.hp * 10
	while(white_animation_bar.value / 10 > self.hp && hp >= 0):
		white_animation_bar.value -= 1
		white_bar_timer.start(0.05)
		await white_bar_timer.timeout


func slowdown():
	speed = 10
	while speed < 60:
		speed += 1
		slowdown_timer.start(0.1)
		await slowdown_timer.timeout


func fire_damage(body):
	status.visible = true
	$period_dmg_particle.emitting = true
	for i in 3:
		fire_damage_timer.start(1)
		await fire_damage_timer.timeout
		if self.hp > 0:
			hp -= 1
		update_hp()
		enemy_death()
	$period_dmg_particle.emitting = false
	status.visible = false


func _on_pulls_body(body, position):
	pass


func _on_damage_to_enemy(body, damage, status):
	if self == body && body.hp >= 1:
		if status == "burn":
			self.fire_damage(body)
		EventBus.emit_signal("show_damage_value", damage_label_instance, damage)
		set_velocity(-direction.normalized() * speed)
		move_and_slide()
		body.hp -= damage
		slowdown()
		chasing_player()
		update_hp()
		self.enemy_death()


func chasing_player():
	pass


func _on_push_away_enemy(body, velocity):
	if self == body && body.hp >= 1:
		var distance = 7
		velocity = velocity.normalized() * distance
		for i in 20:
			velocity = velocity.normalized() * distance
			set_velocity(velocity)
			move_and_slide()
			await get_tree().create_timer(0.01).timeout
			distance += 7

func set_focused(state):
	$focus.visible = state
	
