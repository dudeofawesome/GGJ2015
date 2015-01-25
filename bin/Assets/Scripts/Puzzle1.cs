public class numberGuessPuzzle {

	int[][] answer = {1,3,4,5}; // 1-6, 4 long
	int lives = 10;

	public int[] checkAnswer ( int[] playerSubmit ) {
		int[] results = {0,0,0}; // First one is right number, right position, 2nd is right number wrong position, 3rd is neitehr
		
		// God help me and this shitty hack
		bool[] tempHold = new bool[4];

		// Checking for right number and right position
		for ( int i = 0; i < answer.length; i++ ) {
			if ( playerSubmit[i] == answer[i] ) {
				results[0]++;
				tempHold[i] = true;
			}
		}

		for ( int i = 0; i < answer.length; i++ ) {
			for ( int j = 0; j < answer.length; j++ ) {
				if ( i != j && !tempHold[j] && playerSubmit[i] == answer[j] ) {
					results[1]++;
					tempHold[j] = true;
				}
			}
		}

		results[2] = 4 - results[0] - results[1];

		return results;
	}
}