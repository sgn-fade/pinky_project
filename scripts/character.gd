
extends KinematicBody2D
var speed = 60
var acceleration = 20
var dash_cooldown = 0
var velocity = Vector2.ZERO
onready var _animated_sprite = get_node("/root/World/player/aSprite")
onready var dash = get_node("/root/World/Ui/game_ui/dash_indicator")
onready var ui = get_node("/root/World/Ui")
var Base_weapon = load("res://scripts/weapon.gd")
var weapon = Base_weapon.new()
var input = Vector2.ZERO

var timer := Timer.new()
var dash_butt_hit_timer = 0
var dash_speed_const = 1

var current_state = States.IDLE

enum States{
	IDLE,
	MOVE,
	DASH,
	BUTT_HIT_DASH,
	SPELL,
	DEAD,
}
func _process(delta):
	z_index = global_position.y/2
	dash_butt_hit_timer -= delta
	match current_state:
		States.IDLE:
			rotating()
			move()
			die()
		States.MOVE:
			rotating()
			move()
			dash(delta)
			die()
		States.DASH:
			rotating()
			g_dash(delta)
		States.BUTT_HIT_DASH:
			pass
		States.SPELL:
			pass
		States.DEAD:
			pass

func rotating():
	if get_local_mouse_position().x >= 0:
		scale.x = 1
	else:
		scale.x = -1


func _ready():
	EventBus.connect("player_take_damage", self, "_on_player_take_damage")
	EventBus.connect("player_teleport", self, "teleport")
	EventBus.connect("player_cast_spell", self, "set_cast_state")
	timer.one_shot = false
	add_child(timer)
	Input.set_mouse_mode(1)

func _input(event):
	weapon.input(event)


func move():
	input = Vector2.ZERO
	var animation = "move"
	current_state = States.MOVE
	if Input.is_action_pressed("ui_right"):
		input.x += 1
	if Input.is_action_pressed("ui_left"):
		input.x += -1
	if Input.is_action_pressed("ui_up"):
		input.y += -1
	if Input.is_action_pressed("ui_down"):
		input.y += 1
	if input.length() == 0:
		animation = "idle"
		current_state = States.IDLE
	input = input.normalized()
	_animated_sprite.play(animation)
	velocity = velocity.linear_interpolate(input * speed, acceleration * 0.016)
	move_and_slide(velocity)


func g_dash(delta):
	var t = 0.05
	velocity = velocity.linear_interpolate(input * 150, 0.016 / t)
	if velocity.length() >= 149:
		current_state = States.IDLE
	move_and_slide(velocity)
	dash_speed_const += 1
	

func dash(delta):
	if Input.is_action_just_pressed("ui_dash") && dash.ready && current_state != States.DASH:
		current_state = States.DASH
		$dash_particles1.emitting = true
		$dash_particles2.emitting = true
		disable_collision()
		EventBus.emit_signal("dash_cooldown")
		dash_butt_hit_timer = 0.7
		character_slowdown()
		


func die():
	
	if Player.get_hp() <= 0:
		current_state = States.DEAD
		EventBus.emit_signal("character_dead")
		ui.visible = false
		$dead_layer/dead.emitting = true
		$Light2D.color = "FFFFFF"
		$Light2D.texture_scale = 0.25
		$Light2D.scale.x = 1.5
		$Light2D.energy = 1
		_animated_sprite.animation = "death"
		_animated_sprite.stop()
		_animated_sprite.frame = 0
		timer.start(0.6)
		yield(timer, "timeout")
		_animated_sprite.play("death")
		timer.start(1.375)
		yield(timer, "timeout")
		_animated_sprite.stop()


func set_idle_state():
	visible = true
	current_state = States.IDLE


func character_slowdown():
	speed = 0
	while speed < 60:
		speed += 4
		timer.start(0.05)
		yield(timer, "timeout")


func disable_collision():
	set_collision_mask_bit(2, false)
	$player_area.set_collision_mask_bit(2, false)


func enable_collision():
	$player_area.set_collision_mask_bit(2, true)
	set_collision_mask_bit(2, true)


func _on_player_take_damage(player_offcet_dir, enemy_damage):
	print(2)
	Player.update_hp(-enemy_damage)
	var hp = Player.get_hp() 
	var max_hp = Player.get_max_hp() 
	EventBus.emit_signal("update_character_hp_bar_value", hp, max_hp)
	disable_collision()
	velocity = velocity.linear_interpolate(player_offcet_dir * 1000, 0.40)
	move_and_slide(velocity)
	if hp >= 5:
		for i in 3:
			modulate = "49ffffff"
			timer.start(0.07)
			yield(timer, "timeout")
			modulate = "ffffff"
			timer.start(0.07)
			yield(timer, "timeout")
	enable_collision()





func c_shotgun_recoil():
	var player_offcet_dir = (-(get_global_mouse_position() - global_position).normalized())
	for i in 8:
			velocity = velocity.linear_interpolate(player_offcet_dir * 20, 0.40)
			move_and_slide(velocity)
			timer.start(0.005)
			yield(timer, "timeout")


func set_cast_state(animation_time, animation_name):
	current_state = States.SPELL
	_animated_sprite.play(animation_name)
	timer.start(animation_time)
	yield(timer, "timeout")
	current_state = States.IDLE


func teleport(pos):
	current_state = States.SPELL
	_animated_sprite.play("teleport_start")
	timer.start(1.25)
	yield(timer, "timeout")
	global_position = pos
	_animated_sprite.play("teleport_end")
	timer.start(0.833)
	yield(timer, "timeout")
	current_state = States.IDLE
	
func set_inventory_state():
	_animated_sprite.play("idle")
	current_state = States.INVENTORY
