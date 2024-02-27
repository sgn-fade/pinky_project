extends Goblins
class_name Goblin_mage
var fireball = load("res://scenes/particles/enemy's_fireball.tscn")
var attack_cooldown = 2
var fire_elemental = load("res://scenes/enemies/elementals/fire_elemental.tscn")

var acceleration = 1
var elemental_cooldown = 10

func _ready():
	sprite = $sprite
	play_animation("idle")
	
	hp = 10
	self.hp_bar.max_value = hp * 10
	self.hp_bar.value = hp * 10
	self.white_animation_bar.max_value = hp * 10
	self.white_animation_bar.value = hp * 10
	speed = 40



func _process(delta):
	self.attack_cooldown -= delta
	match current_state:
		States.SEARCHING:
			swap_sprite_direction()
		States.IDLE:
			searching_player(delta)
		States.MOVE:
			swap_sprite_direction()
			move(global_position - Player.get_position())
			#attack()
			cast_fireball()
		States.PULLS:
			attract(delta)

func move(dir):
	if abs(dir.length()) > 140:
		dir = -dir
		play_animation("move")
	if abs(dir.length()) > 135 and abs(dir.length()) < 140:
		dir = Vector2.ZERO
		play_animation("idle")
	super.move(dir)

func cast_fireball():
	if attack_cooldown <= 0 && current_state != States.ATTACK:
		transform.x.x *= -1
		attack_cooldown = 2
		current_state = States.ATTACK
		await play_animation("attack_before")
		var fireball_particle = fireball.instantiate()
		fireball_particle.global_position = $fireball_pos.global_position
		GlobalWorldInfo.get_world().add_child(fireball_particle)
		$fireball_cast_particles.emitting = true
		await play_animation("attack_after")
		current_state = States.MOVE
		play_animation("move")

#
#func summon_elemental():
	#if elemental_cooldown <= 0 && current_state != States.SUMMONING:
		#current_state = States.SUMMONING
		#var summon = fire_elemental.instantiate()
		#GlobalWorldInfo.get_world().add_child(summon)
		#summon.global_position = self.global_position + Vector2(10, 0)
		#elemental_cooldown = randi()%30+10
		#idle()
