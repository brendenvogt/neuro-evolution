import sys
import os
import signal 

RELOAD = 0
THROW = 1
DUCK = 2

INITIAL_DUCKS=5
MAX_BALLS=50
MAX_TURNS=50

A_EXEC=sys.argv[1]
B_EXEC=sys.argv[2]
GAME_COUNT=int(sys.argv[3])

A_WINS=0
B_WINS=0
DRAWS=0

GAME=0
def main():
	global GAME, A_WINS, B_WINS, DRAWS
	while GAME < GAME_COUNT:
		TURN=0
		A_BALLS=0
		B_BALLS=0
		A_DUCKS=INITIAL_DUCKS
		B_DUCKS=INITIAL_DUCKS
		WINNER="DRAW"
		while TURN != MAX_TURNS:
			print(f"Game {GAME}/{GAME_COUNT}, turn {TURN}/{MAX_TURNS}:")
			a_action = f"{A_EXEC} {TURN} {A_BALLS} {B_BALLS} {A_DUCKS} {B_DUCKS} {MAX_BALLS}"
			b_action = f"{B_EXEC} {TURN} {B_BALLS} {A_BALLS} {B_DUCKS} {A_DUCKS} {MAX_BALLS}"
			A_ACT = os.system(a_action)
			B_ACT = os.system(b_action)
			print(f"  A = Balls: {A_BALLS}, Ducks: {A_DUCKS}, Action: {A_ACT}")
			print(f"  B = Balls: {B_BALLS}, Ducks: {B_DUCKS}, Action: {B_ACT}")
			# reload, throw, duck
			if A_ACT == RELOAD and A_BALLS < MAX_BALLS : # a reloads
				if B_ACT == RELOAD and B_BALLS < MAX_BALLS: # b reloads
					A_BALLS += 1
					B_BALLS += 1
				elif B_ACT == THROW and B_BALLS > 0: # b throws
					WINNER="B"
					break
				elif B_ACT == DUCK and B_DUCKS > 0: # b ducks
					A_BALLS += 1
					B_DUCKS -= 1
				else:
					A_BALLS += 1
			elif A_ACT == THROW and A_BALLS > 0: # a throws
				if B_ACT == RELOAD and B_BALLS < MAX_BALLS: # b reloads
					WINNER="A"
					break
				elif B_ACT == THROW and B_BALLS > 0: # b throws
					A_BALLS -= 1
					B_BALLS -= 1
				elif B_ACT == DUCK and B_DUCKS > 0: # b ducks
					A_BALLS -= 1
					B_DUCKS -= 1
				else:
					WINNER="A"
					break
			elif A_ACT == DUCK and A_DUCKS > 0: # a ducks
				if B_ACT == RELOAD and B_BALLS < MAX_BALLS: # b reloads
					A_DUCKS -= 1
					B_BALLS += 1
				elif B_ACT == THROW and B_BALLS > 0: # b throws
					A_DUCKS -= 1
					B_BALLS -= 1
				elif B_ACT == DUCK and B_DUCKS > 0: # b ducks
					A_DUCKS -= 1
					B_DUCKS -= 1
				else:
					A_DUCKS -= 1
			else:
				if B_ACT == RELOAD and B_BALLS < MAX_BALLS: 
					B_BALLS += 1
				elif B_ACT == THROW and B_BALLS > 0:
					WINNER="B"
					break
				elif B_ACT == DUCK and B_DUCKS > 0:
					B_DUCKS -= 1
			TURN += 1

		if WINNER == "A":
			A_WINS += 1
		elif WINNER == "B":
			B_WINS += 1
		else:
			DRAWS += 1

		print(f"Result: {WINNER}")
		print()
		GAME += 1

	print()
	print(f"Summary: A: {A_WINS}  B: {B_WINS}  DRAW: {DRAWS}")

def signal_handler(signal, frame):
    print("\nprogram exiting gracefully")
    sys.exit(0)

if __name__ == "__main__":
	signal.signal(signal.SIGINT, signal_handler)
	main()