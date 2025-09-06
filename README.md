\# SnapGameSimulator Problem for NatWest



SnapGameSimulator is a .NET-based card game simulation for the classic "Snap" card game. The game is designed to simulate the gameplay, determine winners, and provide a fun and interactive experience for players.



---



\## Features



\- \*\*Card Pack Generation\*\*: Dynamically generates shuffled card packs with customizable suits and ranks.

\- \*\*Player Management\*\*: Supports multiple players with individual card queues.

\- \*\*Game Simulation\*\*: Simulates the "Snap" card game, including card distribution, pile management, and winner selection.

\- \*\*Winner Selection\*\*: Determines the winner based on the number of cards won or declares a draw.

\- \*\*Extensibility\*\*: Built with interfaces for easy customization and testing.



---



\## Prerequisites



\- \*\*.NET 9.0 SDK\*\*: Ensure you have the .NET 9.0 SDK installed on your system.

\- \*\*IDE\*\*: Visual Studio 2022 or any other IDE that supports .NET development.



---



\## Installation



1\. Clone the repository:

2\. Navigate to the project directory:

3\. Restore dependencies:

4\. Build the project:



---



\## Usage



1\. Run the application using Visual Studio or via the command line:

&nbsp;  ```

&nbsp;  dotnet run --project SnapGameSimulator

&nbsp;  ```

2\. Follow the on-screen instructions:

\- Enter the number of card packs.

\- The game will simulate the "Snap" card game and display the results.



---



\## Project Structure



\- \*\*Domain\*\*: Contains the core game logic and domain-specific classes.

\- `GameEngine`: The main engine for simulating the game.

\- `CardPackGenerator`: Generates shuffled card packs.

\- `Loader`: Handles player loading and game setup.

\- `WinnerSelector`: Determines the winner of the game.



\- \*\*Models\*\*: Contains data models such as `Card` and `PlayerDetail`.



\- \*\*Tests\*\*: Unit tests for the core components of the game.



---



\## Example Output



Welcome to snap game. Loading... 

Number of players: 2 Dealing cards from 2 pack(s)... 

2 exhaust limit Player 1 Turn. Count: 0 cards Card Ace of Hearts on the pile... 

Player 2 Turn. Count: 0 cards Card Two of Clubs on the pile... 

Snap! Player 1 wins the pile! Player 1 now has 2 cards.

The winner is Player 1



---



\## Testing



Run the unit tests to ensure the application works as expected



