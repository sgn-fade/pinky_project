extends KinematicBody2D
var speed
var hp = 0
var enemy_damage = 1
onready var damage_label = load("res://scenes/damage_text.tscn")
var blood_orb_drop = load("res://scenes/blood_orb.tscn")
var modules_drop = load("res://scenes/modules_drop.tscn")

onready var hp_bar = $hp_bar
onready var collision = $collision
onready var white_animation_bar = $middle_white_bar
var white_bar_timer := Timer.new()
var slowdown_timer := Timer.new()
var timer := Timer.new()
var fire_damage_timer := Timer.new()
var direction := Vector2.ZERO
var damage_label_instance = null


func _ready():
	speed = 60
	
	damage_label_instance = damage_label.instance()
	add_child(damage_label_instance)
	add_child(white_bar_timer)
	add_child(slowdown_timer)
	add_child(timer)
	add_child(fire_damage_timer)
	
	white_bar_timer.one_shot = false
	slowdown_timer.one_shot = false
	timer.one_shot = false
	EventBus.connect("pulls_body", self, "_on_pulls_body")
	EventBus.connect("player_body_entered", self, "_on_player_body_entered")
	EventBus.connect("damage_to_enemy", self, "_on_damage_to_enemy")
	EventBus.connect("push_away_enemy", self, "_on_push_away_enemy")

func move(direction):
	pass

func _process(delta):
	self.z_index = self.global_position.y / 2


func _on_player_body_entered(body):
	if self == body && body.hp > 0 && Player.get_hp() > 0:
		var player_offcet_dir = (-(self.global_position - Player.global_position).normalized())
		#player.take_damage(player_offcet_dir, enemy_damage)
		if Player.get_hp() <= 0:
			queue_free()

	
func enemy_death():
	if self.hp <= 0:
		spawn_drop()
		queue_free()

func spawn_drop():
	#var blood_orb = blood_orb_drop.instance()
	#GlobalWorld.add_child(blood_orb)
	#blood_orb.global_position = self.global_position
	var modules_drops = modules_drop.instance()
	GlobalWorld.add_child(modules_drops)
	modules_drops.global_position = self.global_position
	modules_drops.z_index = self.z_index



func update_hp():
	self.hp_bar.value = self.hp * 10
	while(white_animation_bar.value / 10 > self.hp && hp >= 0):
		white_animation_bar.value -= 1
		white_bar_timer.start(0.05)
		yield(white_bar_timer, "timeout")


func slowdown():
	speed = 10
	while speed < 60:
		speed += 1
		slowdown_timer.start(0.1)
		yield(slowdown_timer, "timeout")


func fire_damage(body):
	$status1.visible = true
	$period_dmg_particle.emitting = true
	for i in 3:
		fire_damage_timer.start(1)
		yield(fire_damage_timer, "timeout")
		if self.hp > 0:
			hp -= 1
		update_hp()
		enemy_death()
	$period_dmg_particle.emitting = false
	$status1.visible = false
	
	

func _on_pulls_body(body, position):
	pass


func _on_damage_to_enemy(body, damage, status):
	if self == body && body.hp >= 1:
		if status == "burn":
			self.fire_damage(body)
		EventBus.emit_signal("show_damage_value", damage_label_instance, damage)
		move_and_slide(-direction.normalized() * speed)
		body.hp -= damage
		slowdown()
		update_hp()
		self.enemy_death()
		
func _on_push_away_enemy(body, velocity):
	#var t = 0.05
	#var delta = 0.016
	print(velocity)
	if self == body && body.hp >= 1:
		var distance = 7
		velocity = velocity.normalized() * distance
		#velocity = velocity.linear_interpolate(velocity * 150, delta / t)
		for i in 20:
			velocity = velocity.normalized() * distance
			move_and_slide(velocity)
			yield(get_tree().create_timer(0.01), "timeout")
			distance += 7
