extends Goblins
class_name Goblin_melee
@onready var attack_area = $body
@onready var sprite = $body/sprite
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
	NONE
}



func _ready():
	super._ready()
	hp = 20
	speed = 60
	enemy_damage = 10
	self.hp_bar.max_value = hp * 10
	self.hp_bar.value = hp * 10
	self.white_animation_bar.max_value = hp * 10
	self.white_animation_bar.value = hp * 10
	spawn_goblin()
	attack_area.connect("body_entered", Callable(self, "_on_melee_goblin_attack_area_entered"))


func spawn_goblin():
	
	sprite.play("spawn")
	timer.start(1.4)
	await timer.timeout
	sprite.play("idle")
	$middle_white_bar.visible = true
	$hp_bar.visible = true
	$collision.set_deferred("disabled", false)
	timer.start(0.5)
	await timer.timeout
	current_state = States.NONE
	


func _process(delta):
	cd -= delta
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
			velocity = velocity.lerp(direction, t)
			
			set_velocity(velocity)
			move_and_slide()
			velocity = velocity
			if global_position.distance_to(Player.get_position()) < 100:
				current_state = States.MOVE
				return
			await get_tree().create_timer(delta).timeout
		if current_state == States.MOVE:
			return
		await idle(States.IDLE)


func randomize_direction():
	return Vector2(randi() % 3 - 1, randi() % 3 - 1)


func move(dir):
	direction = dir
	sprite.play("move")
	if hp > 0 && Player.get_hp() > 0 :
		current_state = States.MOVE
		set_velocity((direction).normalized() * speed)
		move_and_slide()
		if (direction.length() >= 1000):
			current_state = States.IDLE


func attract(delta):
	velocity = velocity.lerp((pull_source - global_position).normalized() * 50, delta / 0.1)
	set_velocity(velocity)
	move_and_slide()
	if (pull_source - global_position).length() <= 3:
		slowdown()
		current_state = States.MOVE



func idle(State):
	sprite.play("idle")
	timer.start(randf())
	await timer.timeout
	current_state = State



func attack():
	if direction.length() < 30 and current_state != States.ATTACK and attack_cooldown <= 0:
		
		attack_cooldown = 4
		current_state = States.ATTACK
		sprite.play("attack")
		timer.start(0.8)
		await timer.timeout
		direction = Player.get_position() - global_position
		self.attack_area.monitoring = true
		for i in 4:
			velocity = velocity.lerp(direction.normalized() * 320, 0.016 * acceleration)
			set_velocity(velocity)
			move_and_slide()
			timer.start(0.02)
			await timer.timeout
		timer.start(0.386)
		await timer.timeout
		idle(States.MOVE)
		self.attack_area.monitoring = false


func _on_melee_goblin_attack_area_entered(body):
	if body == Player.get_body() and current_state != States.DEALS_DAMAGE:
		current_state = States.DEALS_DAMAGE
		var player_offcet_dir = (-(self.global_position - Player.get_position()).normalized())
		EventBus.emit_signal("player_take_damage", player_offcet_dir, 10)


func _on_pulls_body(body, pos):
	if body == self:
		pull_source = pos
		current_state = States.PULLS
	
func _on_damage_to_enemy(body, damage, status):
	super._on_damage_to_enemy(body, damage, status)
	if self == body:
		current_state = States.NONE
		#$body/end_partcl.emitting = true
		sprite.play("take_damage")
		$anim_player.play("take_damage")
		await sprite.animation_finished
		current_state = States.IDLE
		
