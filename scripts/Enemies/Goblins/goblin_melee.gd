extends Goblins

onready var throwing_stone = preload("res://scenes/throwing_stone.tscn")
onready var attack_area = $body
onready var sprite = $body/sprite
onready var shadow_under = $body/shadow_under
var velocity = Vector2.ZERO
var throw_cooldown = 0
var attack_cooldown = 0
var acceleration = 40
var pull_source = Vector2.ZERO
var cd = 0
var distance_to_player = 0
var current_state = null

enum States {
	IDLE,
	MOVE,
	PULLS,
	ATTACK,
	DEALS_DAMAGE,
	THROWING_STONE,
	SEARCHING,
}



func _ready():
	hp = 20
	self.hp_bar.max_value = hp * 10
	self.hp_bar.value = hp * 10
	self.white_animation_bar.max_value = hp * 10
	self.white_animation_bar.value = hp * 10
	speed = 60
	enemy_damage = 10
	spawn_goblin()
	attack_area.connect("body_entered", self, "_on_melee_goblin_attack_area_entered")


func spawn_goblin():
	
	sprite.play("spawn")
	timer.start(1.4)
	yield(timer, "timeout")
	sprite.play("idle")
	$middle_white_bar.visible = true
	$hp_bar.visible = true
	shadow_under.visible = true
	$Light2D.visible = true
	$collision.set_deferred("disabled", false)
	timer.start(0.5)
	yield(timer, "timeout")
	current_state = States.IDLE
	


func _process(delta):
	cd -= delta
	z_index = global_position.y / 2
	self.attack_cooldown -= delta
	self.throw_cooldown -= delta
	

	match current_state:
		States.SEARCHING:
			swap_sprite_direction()
		States.IDLE:
			searching_players(delta)
		States.MOVE:
			swap_sprite_direction()
			move(Player.get_position() - global_position)
			attack()
		States.ATTACK:
			pass
		States.PULLS:
			attract(delta)
		States.DEALS_DAMAGE:
			pass
		States.THROWING_STONE:
			pass


func swap_sprite_direction():
	if direction.x <= 0:
		$body.transform.x.x = -1

	elif direction.x > 0:
		$body.transform.x.x = 1

func chasing_player():
	current_state = States.MOVE
	
func searching_players(delta):
	
	if current_state != States.SEARCHING:
		sprite.play("move")
		current_state = States.SEARCHING
		direction = randomize_direction().normalized() * (randi() % 100)
		var t = 0
		while(t < 1):
			if current_state == States.MOVE:
				return
			t += delta
			velocity = velocity.linear_interpolate(direction, t)
			
			velocity = move_and_slide(velocity)
			if global_position.distance_to(Player.get_position()) < 100:
				current_state = States.MOVE
				return
			yield(get_tree().create_timer(delta), "timeout")
		if current_state == States.MOVE:
			return
		yield(idle(States.IDLE), "completed")


func randomize_direction():
	return Vector2(randi() % 3 - 1, randi() % 3 - 1)


func move(dir):
	direction = dir
	sprite.play("move")
	if hp > 0 && Player.get_hp() > 0 :
		current_state = States.MOVE
		move_and_slide((direction).normalized() * speed)
		if (direction.length() >= 1000):
			current_state = States.IDLE


func attract(delta):
	velocity = velocity.linear_interpolate((pull_source - global_position).normalized() * 50, delta / 0.1)
	move_and_slide(velocity)
	if (pull_source - global_position).length() <= 3:
		slowdown()
		current_state = States.MOVE



func idle(State):
	sprite.play("idle")
	timer.start(randf())
	yield(timer, "timeout")
	current_state = State


func throw_stone():
	if(
			#randi()%2 == 1 and 
			self.throw_cooldown <= 0 and
			self.current_state != States.THROWING_STONE and
			direction.length() > 50
	):
		self.throw_cooldown = 3
		self.current_state = States.THROWING_STONE
		var current_goblins_stone = throwing_stone.instance()
		current_goblins_stone.global_position = global_position
		GlobalWorldInfo.get_world().add_child(current_goblins_stone)
		timer.start(0.5)
		yield(timer, "timeout")
		self.current_state = States.IDLE


func attack():
	if direction.length() < 30 and current_state != States.ATTACK and attack_cooldown <= 0:
		
		attack_cooldown = 4
		current_state = States.ATTACK
		sprite.play("attack")
		timer.start(0.8)
		yield(timer, "timeout")
		direction = Player.get_position() - global_position
		self.attack_area.monitoring = true
		for i in 4:
			velocity = velocity.linear_interpolate(direction.normalized() * 320, 0.016 * acceleration)
			move_and_slide(velocity)
			timer.start(0.02)
			yield(timer, "timeout")
		timer.start(0.386)
		yield(timer, "timeout")
		idle(States.MOVE)
		self.attack_area.monitoring = false


func _on_melee_goblin_attack_area_entered(body):
	if body.name == "player" and current_state != States.DEALS_DAMAGE:
		current_state = States.DEALS_DAMAGE
		var player_offcet_dir = (-(self.global_position - Player.get_position()).normalized())
		EventBus.emit_signal("player_take_damage", player_offcet_dir, 10)


func _on_pulls_body(body, pos):
	if body == self:
		pull_source = pos
		current_state = States.PULLS
	
